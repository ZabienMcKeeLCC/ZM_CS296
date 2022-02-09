using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZM_CS296_Forum_Site.Models
{
    public class MessageContext : IdentityDbContext<AppUser>
    {
        public MessageContext(DbContextOptions<MessageContext> options) : base(options) { }

        public DbSet<ForumReplyModel> replies { get; set; }
        public DbSet<ForumPostModel> posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ForumReplyModel>().HasData(
                new ForumReplyModel { ReplyId = 1, PostId = 1, Username = "Joseph Smith", Message = "THis is a test", Date = "1/2/2022"},
                new ForumReplyModel { ReplyId = 2, PostId = 1, Username = "Zachary Johnson", Message = "THis is a test", Date = "1/2/2022" }
                );
        }



    }
}
