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

            var item = new Ticket();
            item.TaskNo = "T001";
            item.Title = "最初のチケット";
            item.Desc = "内容...";
            item = await service.AddTicket(item);
            Assert.IsNotNull(item);
            var id = item.Id;
            Assert.AreEqual("T001", item.TaskNo);
            Assert.AreEqual("最初のチケット", item.Title);

            item = await service.GetTicket(id);
            Assert.AreEqual("T001", item.TaskNo);
            Assert.AreEqual("最初のチケット", item.Title);
        }
    }
}
