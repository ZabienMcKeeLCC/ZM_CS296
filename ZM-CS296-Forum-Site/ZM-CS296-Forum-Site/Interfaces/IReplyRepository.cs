using ZM_CS296_Forum_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZM_CS296_Forum_Site.Interfaces
{
    public interface IReplyRepository
    {
        IEnumerable<ForumReplyModel> SelectAll();
        void Insert(ForumReplyModel obj);
        ForumReplyModel SelectById(int id);
        void Delete(ForumReplyModel obj);
        void Save();
    }
}
