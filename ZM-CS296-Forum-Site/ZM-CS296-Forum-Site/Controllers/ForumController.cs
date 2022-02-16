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
            ViewBag.replies = await replyRepo.SelectAllAsync();
            ViewBag.comments = await postRepo.SelectAllAsync();
            return View();
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
        public IActionResult WritePost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult WritePost(ForumPostModel postModel)
        {

            postModel.Username = userManager.GetUserAsync(User).Result.UserName;
            DateTime clock = DateTime.Now;
            postModel.Date = clock.ToString();

            if (ModelState.IsValid)
            {
                postRepo.Insert(postModel);
            }

            return RedirectToAction("Browser", postModel);
        }

        //Page that displays actual post
        [HttpGet]
        public async Task<IActionResult> ForumPost(int postId)
        {
            List<ForumReplyModel> allReplies = (List<ForumReplyModel>)await replyRepo.SelectAllAsync();
            List<ForumReplyModel> linkedReplies = new List<ForumReplyModel>();
            ViewBag.Id = postId;
            ViewBag.Post = await postRepo.SelectByIdAsync(postId);

            foreach (ForumReplyModel reply in allReplies)
            {
                if (reply.PostId == postId) { linkedReplies.Add(reply); }


            }

            ViewBag.replies = linkedReplies;
            return View();
        }
        [HttpGet]
        public IActionResult WriteReply(ForumPostModel postModel)
        {
            ViewBag.Post = postModel;
            return View();
        }

        [HttpPost]
        public IActionResult WriteReply(ForumReplyModel replyModel, int postId)
        {
            replyModel.Username = userManager.GetUserAsync(User).Result.UserName.ToString();
            //ViewBag.Post = replyModel;
            DateTime clock = DateTime.Now;
            replyModel.Date = clock.ToString();
            replyModel.PostId = postId;

            if (ModelState.IsValid)
            {

                //replyContext.replies.Add(postModel);
                //replyContext.SaveChanges();
                replyRepo.Insert(replyModel);
                replyRepo.Save();

            }

            return RedirectToAction("ForumPost", replyModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

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
            postRepo.Delete(post);
            DeleteReplyByPostId(post.PostId);

            postRepo.Save();
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
            replyRepo.Delete(reply);
            replyRepo.Save();
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
    }
}
