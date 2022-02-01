using ZM_CS296_Forum_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZM_CS296_Forum_Site.Interfaces
{
    public class ReplyRepository : IReplyRepository
    {

        private MessageContext ctx { get; set; }
        public ReplyRepository(MessageContext inputContext)
        {
            ctx = inputContext;
        }
        public void Delete(ForumReplyModel obj)
        {
            ctx.replies.Remove(obj);
        }

        public void Insert(ForumReplyModel obj)
        {
            ctx.replies.Add(obj);
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        public IEnumerable<ForumReplyModel> SelectAll()
        {
            
            return ctx.replies.ToList();
        }

        public ForumReplyModel SelectById(int id)
        {
            return ctx.replies.Find(id);
        }
    }
}
