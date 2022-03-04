using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZM_CS296_Forum_Site.Models
{
    public class ReplyVM
    {
        public int PostId { get; set; }
        public AppUser Replier { get; set; }
        public string Message { get; set; }

    }
}
