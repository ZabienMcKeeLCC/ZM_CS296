using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZM_CS296_Forum_Site.Models;

namespace ZM_CS296_Forum_Site.Interfaces
{
    public class CategoryRepository : ICategoryRepository
    {
        public MessageContext ctx { get; set; }
        public async void Insert(Category obj)
        {
            ctx.tags.AddAsync(obj);
            await ctx.SaveChangesAsync();
        }

        public async void SaveAsync()
        {
            await ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> SelectAllAsync()
        {
            List<Category> list = await ctx.tags.OrderByDescending(m => m.Name).ToListAsync<Category>();
            if (list == null)
            {
                return new List<Category>();
            }
            return list;
        }
    }
}
