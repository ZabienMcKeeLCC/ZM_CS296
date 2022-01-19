using CS295_TermProject.Controllers;
using CS295_TermProject.Interfaces;
using CS295_TermProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CS296_XUnit_Tests
{
    public class PostTests
    {
        [Fact]
        public void AddPostTest()
        {
            var fakeReplyRepo = new FakeReplyRepository();
            var fakePostRepo = new FakePostRepository();
            var controller = new ForumController(fakePostRepo, fakeReplyRepo);

            var post = new ForumPostModel();

            controller.WritePost(post);

            Assert.NotNull(fakePostRepo.Posts.ToList());
            Assert.Equal(fakePostRepo.Posts.Count()-1, fakePostRepo.Posts.ToList()[0].PostId);
        }
    }
}
