using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Openccpm.Test.Models;
using System.Linq;
using Openccpm.UWP.Controllers;
using System.Threading.Tasks;
using Openccpm.Web.Models;

namespace Openccpm.Test
{

    /// <summary>
    /// チケット作成時のweb apiテスト
    /// </summary>
    [TestClass]
    public class TestTicket : TestBase
    {
        new TicketService service;

        [TestInitialize]
        public override void SetUp()
        {
            context = new openccpm_dbEntities();
            service = new TicketService("http://localhost:5000");
        }

        /// <summary>
        /// 簡単な初期化テスト
        /// </summary>
        [TestMethod]
        public async Task TestTicketInit()
        {
            var items = await service.GetTickets();
            Assert.AreEqual(0, items.Count);

            var item = new TicketView();
            item.TaskNo = "T001";
            item.Subject = "最初のチケット";
            item.Description = "内容...";
            item = await service.AddTicket(item);
            Assert.IsNotNull(item);
            var id = item.Id;
            Assert.AreEqual("T001", item.TaskNo);
            Assert.AreEqual("最初のチケット", item.Subject);

            item = await service.GetTicket(id);
            Assert.AreEqual("T001", item.TaskNo);
            Assert.AreEqual("最初のチケット", item.Subject);
        }

        /// <summary>
        /// 簡単な初期化テスト2
        /// </summary>
        [TestMethod]
        public async Task TestTicketInit2()
        {
            var items = await service.GetTickets();
            Assert.AreEqual(0, items.Count);

            var item = new TicketView();
            item.TaskNo = "T001";
            item.Subject = "最初のチケット";
            item.Description = "内容...";
            item.PlanTime = 10;
            item.DoneTime = 20;
            item.DoneRate = 50;
            item = await service.AddTicket(item);
            Assert.IsNotNull(item);
            var id = item.Id;
            Assert.AreEqual("T001", item.TaskNo);
            Assert.AreEqual("最初のチケット", item.Subject);

            item = await service.GetTicket(id);
            Assert.AreEqual("T001", item.TaskNo);
            Assert.AreEqual("最初のチケット", item.Subject);
            Assert.AreEqual(10, item.PlanTime);
            Assert.AreEqual(20, item.DoneTime);
            Assert.AreEqual(50, item.DoneRate);
        }
        /// <summary>
        /// 担当者の設定
        /// </summary>
        [TestMethod]
        public async Task TestTicketAuthor()
        {
            var items = await service.GetTickets();
            Assert.AreEqual(0, items.Count);

            var context = new openccpm_dbEntities();
            var user = context.Users.Add(new Openccpm.Test.Models.Users() {
                Id = Guid.NewGuid().ToString(),
                Login ="masuda", FirstName="tomoaki", LastName="masuda" });
            context.SaveChanges();

            var item = new TicketView();
            item.TaskNo = "T001";
            item.Subject = "最初のチケット";
            item.Description = "内容...";
            item.PlanTime = 10;
            item.DoneTime = 20;
            item.DoneRate = 50;
            item.AssignedTo = new User() { Id = user.Id };
            item = await service.AddTicket(item);
            Assert.IsNotNull(item);
            var id = item.Id;
            Assert.AreEqual("T001", item.TaskNo);
            Assert.AreEqual("最初のチケット", item.Subject);

            item = await service.GetTicket(id);
            Assert.AreEqual("T001", item.TaskNo);
            Assert.AreEqual("最初のチケット", item.Subject);
            Assert.AreEqual(10, item.PlanTime);
            Assert.AreEqual(20, item.DoneTime);
            Assert.AreEqual(50, item.DoneRate);
            Assert.IsNotNull(item.AssignedTo);
            Assert.AreEqual("masuda", item.AssignedTo.LastName);
            Assert.AreEqual("tomoaki", item.AssignedTo.FirstName);

            Assert.IsNull(item.Author);
        }
    }
}
