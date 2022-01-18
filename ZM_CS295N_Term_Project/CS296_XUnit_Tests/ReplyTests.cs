using System;
using Xunit;
using System.Linq;
using CS295_TermProject.Interfaces;
using CS295_TermProject.Models;
using CS295_TermProject.Controllers;

namespace CS295_TermProject_Tests
{
    public class ReplyTests
    {

        [Fact]
        public void AddReplyTest()
        {
            var fakeReplyRepo = new FakeReplyRepository();
            var fakePostRepo = new FakePostRepository();
            var controller = new ForumController(fakePostRepo, fakeReplyRepo);

            var reply = new ForumReplyModel();

            controller.WriteReply(reply, 12);
            Console.WriteLine(fakeReplyRepo.printList());

            Assert.NotNull(fakeReplyRepo.Replies.ToList());
            Assert.Equal(12, fakeReplyRepo.Replies.ToList()[0].PostId);
        }

        [Fact]
        public void DeleteReplyTest()
        {
            var fakeReplyRepo = new FakeReplyRepository();
            var fakePostRepo = new FakePostRepository();
            var controller = new ForumController(fakePostRepo, fakeReplyRepo);

            var reply = new ForumReplyModel();

            controller.WriteReply(reply, 12);

            Assert.NotNull(fakeReplyRepo.Replies.ToList());
            Assert.Equal(12, fakeReplyRepo.Replies.ToList()[0].PostId);

            controller.DeleteReply(reply);

            Assert.Null(fakeReplyRepo.SelectById(12));
        }

    }
}