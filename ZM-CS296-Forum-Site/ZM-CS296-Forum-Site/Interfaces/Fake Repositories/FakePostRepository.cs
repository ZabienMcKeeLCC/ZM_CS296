using ZM_CS296_Forum_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZM_CS296_Forum_Site.Interfaces;

namespace CS295_TermProject.Interfaces
{
    public class FakePostRepository : IPostRepository
    {
        private List<ForumPostModel> posts = new List<ForumPostModel>();
        public List<ForumPostModel> Posts { get { return posts; } }
        public FakePostRepository()
        {

        }

        public IEnumerable<ForumPostModel> SelectAll()
        {
            return posts;
        }

        public void Insert(ForumPostModel obj)
        {
            obj.PostId = posts.Count();
            posts.Add(obj);
        }

        public ForumPostModel SelectById(int id)
        {
            for(int i = 0; i < posts.Count(); i++)
            {
                if(posts[i].PostId == id)
                {
                    return posts[i];
                }
            }
            return null;
        }

        public void Delete(ForumPostModel obj)
        {
            for (int i = 0; i < posts.Count(); i++)
            {
                if (posts[i].PostId == obj.PostId)
                {
                    posts.Remove(posts[i]);
                }
            }
        }

        public IEnumerable<ForumPostModel> SelectWithFilter(string filter)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {

        }
    }
}
