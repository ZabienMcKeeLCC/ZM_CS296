using ZM_CS296_Forum_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZM_CS296_Forum_Site.Interfaces
{
    public class PostRepository : IPostRepository
    {

        private MessageContext ctx { get; set; }
        public PostRepository(MessageContext inputContext)
        {
            ctx = inputContext;
        }
        public void Delete(ForumPostModel obj)
        {
            ctx.posts.Remove(obj);
            ctx.SaveChanges();
        }

        public void Insert(ForumPostModel obj)
        {
            ctx.posts.Add(obj);
            ctx.SaveChanges();
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        public IEnumerable<ForumPostModel> SelectAll()
        {
            var list = ctx.posts.OrderByDescending(m => m.Date).ToList();
            if(list == null)
            {
                return new List<ForumPostModel>();
            }
            return list;
        }

        public ForumPostModel SelectById(int id)
        {
            return ctx.posts.Find(id);
        }

        public IEnumerable<ForumPostModel> SelectWithFilter(string filter)
        {
            IEnumerable<ForumPostModel> posts;
            if (!String.IsNullOrEmpty(filter))
            {
                 posts = ctx.posts.Where(s => s.Title.ToLower().Contains(filter.ToLower())).AsEnumerable();
            }
            else
            {
                posts = ctx.posts.ToList();
            }
            return posts;
        }
    }
}
