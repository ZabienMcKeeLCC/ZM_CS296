using ZM_CS296_Forum_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZM_CS296_Forum_Site.Interfaces
{
    public interface IReplyRepository
    {
        public Task<IEnumerable<ForumReplyModel>> SelectAllAsync();
        void Insert(ForumReplyModel obj);
        Task<ForumReplyModel> SelectById(int id);
        void DeleteAsync(ForumReplyModel obj);
        void SaveAsync();
    }
}
