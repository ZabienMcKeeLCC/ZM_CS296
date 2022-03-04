using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZM_CS296_Forum_Site.Models
{
    public class ForumReplyModel
    {
        public ForumReplyModel() { }

        [Key]
        public int ReplyId { get; set; }
        public int PostId { get; set; }
        
        [Required]
        [StringLength(2500,MinimumLength = 10, ErrorMessage = "Posts need to be between 10 and 2500 characters")]
        public string Message { get; set; }
        public string Date { get; set; }
        public AppUser Replier { get; set; }
    }
}
