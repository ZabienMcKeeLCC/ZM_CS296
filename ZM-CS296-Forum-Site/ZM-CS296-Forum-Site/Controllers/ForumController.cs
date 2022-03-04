using ZM_CS296_Forum_Site.Interfaces;
using ZM_CS296_Forum_Site.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CS295_TermProject.Controllers
{

    public class ForumController : Controller
    {
        //private ReplyContext replyContext { get; set; }
        private IPostRepository postRepo;
        private IReplyRepository replyRepo;

        UserManager<AppUser> userManager;
        public ForumController(IPostRepository postCtx, IReplyRepository replyCtx, UserManager<AppUser> manager)
        {
            postRepo = postCtx;
            replyRepo = replyCtx;
            userManager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> BrowserAsync()
        {
            //List<ForumPostModel> comments = ForumDB.GetMessages();
            ViewBag.Search = "";
            ViewBag.comments = await postRepo.SelectAllAsync();
            return await Task.Run(() => View());
        }

        [HttpPost]
        public async Task<IActionResult> BrowserAsync(string search = "")
        {
            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.comments = postRepo.SelectWithFilterAsync(search);
                var returnList = ViewBag.comments;
                return View(returnList);
            }
            ViewBag.Search = search;
            ViewBag.replies = await replyRepo.SelectAllAsync();
            ViewBag.comments = await postRepo.SelectAllAsync();
            return View(search);
        }


        [HttpGet]
        public async Task<IActionResult> ForumPost(int postId)
        {

            ViewBag.Post = await postRepo.SelectByIdAsync(postId);

            return await Task.Run(() => View());
        }


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ WRITE POSTS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //-----Posts-----
        [HttpGet]
        public async Task<IActionResult> WritePost()
        {
            return await Task.Run(() => View());
        }

        [HttpPost]
        public async Task<IActionResult> WritePost(ForumPostModel postModel)
        {

            postModel.Poster = userManager.GetUserAsync(User).Result;
            DateTime clock = DateTime.Now;
            postModel.Date = clock.ToString();

            if (ModelState.IsValid)
            {
                postRepo.Insert(postModel);
            }

            return await Task.Run(() => RedirectToAction("Browser", postModel.PostId));
        }

        //-----Replies-----
        [HttpGet]
        public async Task<IActionResult> WriteReply(int postId)
        {
            var replyVM = new ReplyVM { PostId = postId };
            replyVM.Replier = userManager.GetUserAsync(User).Result;

            return await Task.Run(() => View(replyVM));
        }

        [HttpPost]
        public async Task<IActionResult> WriteReply(ReplyVM replyVM)
        {
            ForumReplyModel reply = new ForumReplyModel { PostId = replyVM.PostId };

            DateTime clock = DateTime.Now;
            reply.Date = clock.ToString();
            reply.PostId = replyVM.PostId;

            Console.WriteLine(replyVM);
            var post = (from r in postRepo.Posts.Include(r => r.Replies)
                        where r.PostId == replyVM.PostId
                        select r).First<ForumPostModel>();

            post.AddReply(reply);
            postRepo.UpdatePostAsync(post);

            return RedirectToAction("Browser");
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ DELETES ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [Authorize(Roles="Admin")]
        [HttpGet]
        public IActionResult DeletePost(int postId)
        {
            var post = postRepo.SelectByIdAsync(postId);
            return View(post);
        }

        [HttpPost]
        public IActionResult DeletePost(ForumPostModel post)
        {
            postRepo.DeleteAsync(post);
            DeleteReplyByPostId(post.PostId);

            postRepo.SaveAsync();
            return RedirectToAction("Browser");
        }
        [HttpGet]
        public IActionResult DeleteReply(int replyId)
        {
            var reply = replyRepo.SelectById(replyId);
            return View(reply);
        }

        [HttpPost]
        public IActionResult DeleteReply(ForumReplyModel reply)
        {
            //replyContext.replies.Remove(reply);
            //replyContext.SaveChanges();
            replyRepo.DeleteAsync(reply);
            replyRepo.SaveAsync();
            return RedirectToAction("Browser");
        }

        public async Task<IActionResult> DeleteReplyByPostId(int postId)
        {
            List<ForumReplyModel> replies = (List<ForumReplyModel>)await replyRepo.SelectAllAsync();
            foreach (ForumReplyModel reply in replies)
            {
                if (reply.PostId == postId)
                {
                    DeleteReply(reply);
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
