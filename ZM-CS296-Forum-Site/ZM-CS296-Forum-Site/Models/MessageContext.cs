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
        public DbSet<Category> tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ForumPostModel>().HasData(new ForumPostModel { PostId = 1, Message = "Test", Title = "Test" });
        }



    }
}
