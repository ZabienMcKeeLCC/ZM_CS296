using ZM_CS296_Forum_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZM_CS296_Forum_Site.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> SelectAllAsync();
        void Insert(Category obj);
        void SaveAsync();
    }
}
