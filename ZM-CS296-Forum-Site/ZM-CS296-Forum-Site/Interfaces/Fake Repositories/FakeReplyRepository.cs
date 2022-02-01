using ZM_CS296_Forum_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZM_CS296_Forum_Site.Interfaces;

namespace CS295_TermProject.Interfaces
{
    public class FakeReplyRepository : IReplyRepository
    {
        private List<ForumReplyModel> replies = new List<ForumReplyModel>();
        public List<ForumReplyModel> Replies { get { return replies; } }
        public FakeReplyRepository()
        {

        }
        
        public void Delete(ForumReplyModel obj)
        {
            //ctx.replies.Remove(obj);
            int i = 0;
            while (i < Replies.Count())
            {
                if (replies[i].ReplyId == obj.ReplyId)
                {
                    replies.Remove(obj);
                    i++;
                }
            }
        }

        public void Insert(ForumReplyModel obj)
        {
            obj.ReplyId = replies.Count();
            replies.Add(obj);
        }

        public void Save()
        {

        }

        public IEnumerable<ForumReplyModel> SelectAll()
        {

            return Replies;
        }

        public ForumReplyModel SelectById(int id)
        {
            for (int i = 0; i < Replies.Count(); i++)
            {
                if (replies[i].ReplyId == id)
                {
                    return replies[i];
                }
            }
            return null;
        }

        public string printList()
        {
            int i = 0;
            string buffer = "";
            while (i < Replies.Count())
            {
                buffer += replies[i].PostId+", ";
                i++;
            }
            return buffer;
        }
    }
}
