
using ZM_CS296_Forum_Site.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZM_CS296_Forum_Site.Interfaces
{
    public class PostRepository : IPostRepository
    {

        public MessageContext ctx { get; set; }
        public PostRepository(MessageContext inputContext)
        {
            ctx = inputContext;
        }

        public IQueryable<ForumPostModel> Posts
        {
            get
            {
                return ctx.posts.Include(r => r.Poster)
                                      .Include(r => r.Replies)
                                      .ThenInclude(r => r.Replier);
            }
        }

        public async Task<int> DeleteAsync(ForumPostModel obj)
        {
            ctx.posts.Remove(obj);
            return await ctx.SaveChangesAsync();
        }

        public void Insert(ForumPostModel obj)
        {
            ctx.posts.AddAsync(obj);
            ctx.SaveChanges();
        }

        public async void SaveAsync()
        {
            await ctx.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(ForumPostModel post)
        {
            ctx.posts.Update(post);
            await ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<ForumPostModel>> SelectAllAsync()
        {
            List<ForumPostModel> list = await ctx.posts.OrderByDescending(m => m.Date).ToListAsync<ForumPostModel>();
            if(list == null)
            {
                return new List<ForumPostModel>();
            }
            return list;
        }

        public async Task<ForumPostModel> SelectByIdAsync(int id)
        {
            return await ctx.posts.FindAsync(id);
        }

        public async Task<IEnumerable<ForumPostModel>> SelectWithFilterAsync(string filter)
        {
            IEnumerable<ForumPostModel> posts;
            if (!String.IsNullOrEmpty(filter))
            {
                 posts = await ctx.posts.Where(s => s.Title.ToLower().Contains(filter.ToLower())).ToListAsync<ForumPostModel>();
            }
            else
            {
                posts = await ctx.posts.ToListAsync<ForumPostModel>();
            }
            return posts;
        }
    }
}
