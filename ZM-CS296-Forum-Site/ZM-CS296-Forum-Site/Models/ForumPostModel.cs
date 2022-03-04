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
        
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Post Titles need to be between 10 and 100 characters")]
        public string Title { get; set; }
        
        [Required]
        [StringLength(2500, MinimumLength = 10, ErrorMessage = "Post Messages need to be between 10 and 2500 characters")]
        public string Message { get; set; }
        public string Date { get; set; }
        public AppUser Poster { get; set; }
        public ICollection<ForumReplyModel> Replies { get; set; }
        public void AddReply(ForumReplyModel reply)
        {
            Replies.Add(reply);
        }

        public void DeleteReply(ForumReplyModel reply)
        {
            Replies.Remove(reply);
        }

        public List<ForumReplyModel> GetReplies()
        {
            if(Replies == null)
            {
                return new List<ForumReplyModel>();
            }
            return Replies.ToList<ForumReplyModel>();
        }
        
    }
}
