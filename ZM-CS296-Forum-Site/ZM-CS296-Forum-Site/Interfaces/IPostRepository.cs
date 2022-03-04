using ZM_CS296_Forum_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZM_CS296_Forum_Site.Interfaces
{
    public interface IPostRepository
    {


        public IQueryable<ForumPostModel> Posts { get; }
        void Insert(ForumPostModel obj);
        public Task<ForumPostModel> SelectByIdAsync(int id);
        public Task<IEnumerable<ForumPostModel>> SelectAllAsync();
        public Task UpdatePostAsync(ForumPostModel post);
        public Task<IEnumerable<ForumPostModel>> SelectWithFilterAsync(string filter);
        Task<int> DeleteAsync(ForumPostModel obj);
        void SaveAsync();

    }
}
