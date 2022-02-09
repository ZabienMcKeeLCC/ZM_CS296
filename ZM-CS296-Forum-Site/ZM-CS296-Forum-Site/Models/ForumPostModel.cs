using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ZM_CS296_Forum_Site.Models
{

    public class ForumPostModel
    {
        public ForumPostModel() { }

        [Key]
        public int PostId { get; set; }
        
        public string Username { get; set; }

        [StringLength(100, MinimumLength = 10, ErrorMessage = "Post Titles need to be between 10 and 100 characters")]
        [Required]
        public string Title { get; set; }

        [StringLength(2500, MinimumLength = 10, ErrorMessage = "Post Messages need to be between 10 and 2500 characters")]
        [Required]
        public string Message { get; set; }

        public string Date { get; set; }
    }
}
