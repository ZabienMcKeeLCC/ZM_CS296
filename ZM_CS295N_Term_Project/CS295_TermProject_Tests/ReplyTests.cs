using xUnit.Framework;
using CS295_TermProject.Models;
using CS295_TermProject.Interfaces;
using System.Linq;

namespace CS295_TermProject_Tests
{
    [TestFixture]
    public class ReplyTests
    {
        public IReplyRepository ctx { get; set; }

        public ReplyTests(IReplyRepository injectedRepo) 
        {
            this.ctx = injectedRepo;
        }
        public ReplyTests()
        {
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetById()
        {
            
            ForumReplyModel model1 = new ForumReplyModel();
            model1.ReplyId = 5;
            model1.Username = "Mr. Testerson";
            model1.Message = "This is a Test Message";

            ctx.Insert(model1);

            ForumReplyModel model2 = ctx.SelectById(model1.ReplyId);
            Assert.AreEqual("This is a Test Message", model2.Message);
        }

        [Test]
        public void DeleteById()
        {
            
            ForumReplyModel model = new ForumReplyModel();
            model.Username = "Mr. Testerson";
            model.Message = "This is a Test Message";

            ctx.Insert(model);

            model = ctx.SelectById(model.ReplyId);
            Assert.AreEqual("This is a Test Message", model.Message);

            ctx.Delete(model);

            Assert.IsNull(ctx.SelectById(model.ReplyId));
        }
    }
}