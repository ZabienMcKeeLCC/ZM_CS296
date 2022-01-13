using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS295_TermProject.Models
{
    public class ForumReplyModel
    {
        public ForumReplyModel() { }

        [Key]
        public int ReplyId { get; set; }
        public int PostId { get; set; }

        [StringLength(20, MinimumLength = 5, ErrorMessage = "Enter a Username between 5 and 20 characters")]
        [Required]
        public string Username { get; set; }

        [StringLength(2500,MinimumLength = 10, ErrorMessage = "Posts need to be between 10 and 2500 characters")]
        [Required]
        public string Message { get; set; }

        public string Date { get; set; }
    }
}
