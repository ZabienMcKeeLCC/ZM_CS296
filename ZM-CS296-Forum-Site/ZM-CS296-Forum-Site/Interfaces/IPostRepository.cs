using ZM_CS296_Forum_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZM_CS296_Forum_Site.Interfaces
{
    public interface IPostRepository
    {

        IEnumerable<ForumPostModel> SelectAll();
        void Insert(ForumPostModel obj);
        ForumPostModel SelectById(int id);
        void Delete(ForumPostModel obj);
        public IEnumerable<ForumPostModel> SelectWithFilter(string filter);
        void Save();

    }
}
