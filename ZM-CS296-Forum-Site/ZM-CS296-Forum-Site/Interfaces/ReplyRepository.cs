using ZM_CS296_Forum_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ZM_CS296_Forum_Site.Interfaces
{
    public class ReplyRepository : IReplyRepository
    {

        private MessageContext ctx { get; set; }
        public ReplyRepository(MessageContext inputContext)
        {
            ctx = inputContext;
        }
        public async void DeleteAsync(ForumReplyModel obj)
        {
            ctx.replies.Remove(obj);
            await ctx.SaveChangesAsync();
        }

        public void Insert(ForumReplyModel obj)
        {
            ctx.replies.Add(obj);
            ctx.SaveChanges();
        }

        public async void SaveAsync()
        {
            await ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<ForumReplyModel>> SelectAllAsync()
        {

            List<ForumReplyModel> list = await ctx.replies.OrderByDescending(m => m.Date).ToListAsync<ForumReplyModel>();
            if (list == null)
            {
                return new List<ForumReplyModel>();
            }
            return list;
        }

        public async Task<ForumReplyModel> SelectById(int id)
        {
            return await ctx.replies.FindAsync(id);
        }
    }
}
