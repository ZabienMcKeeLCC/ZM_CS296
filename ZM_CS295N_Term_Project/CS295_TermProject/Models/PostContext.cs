using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS295_TermProject.Models
{
    public class PostContext : IdentityDbContext
    {
        public PostContext(DbContextOptions<PostContext> options) : base(options) { }

        public DbSet<ForumReplyModel> replies { get; set; }
        public DbSet<ForumPostModel> posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ForumReplyModel>().HasData(
                new ForumReplyModel { ReplyId = 1, PostId = 1, Username = "Joseph SMith", Message = "THis is a test", Date = "1/2/2022"},
                new ForumReplyModel { ReplyId = 2, PostId = 1, Username = "Zachary Johnson", Message = "THis is a test", Date = "1/2/2022" }
                );
        }



    }
}
