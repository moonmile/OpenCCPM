using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Openccpm.Test.Models;
using System.Linq;
using Openccpm.UWP.Controllers;
using System.Threading.Tasks;
using Openccpm.Web.Models;
using System.Data.Entity;

namespace Openccpm.Test
{

    /// <summary>
    /// WBS作成時のweb apiテスト
    /// </summary>
    [TestClass]
    public class TestWBS : TestBase
    {
        new WbsService service;

        [TestInitialize]
        public override void SetUp()
        {
            context = new openccpm_dbEntities();
            service = new WbsService("http://localhost:5000");
        }

        /*
        [TestMethod]
        public void TestMethod1()
        {
            context.TaskItems.Add(new Models.TaskItems() { Id = Guid.NewGuid().ToString(), TaskNo = "T001", Title = "最初のアイテム" });
            context.TaskItems.Add(new Models.TaskItems() { Id = Guid.NewGuid().ToString(), TaskNo = "T002", Title = "次のアイテム" });
            context.TaskItems.Add(new Models.TaskItems() { Id = Guid.NewGuid().ToString(), TaskNo = "T003", Title = "三番めのアイテム" });
            context.SaveChanges();

            var items = context.TaskItems.Select(x => x).ToList();
            Assert.AreEqual(3, items.Count());

            var id = items[0].Id;
            context.WbsItems.Add(new Models.WbsItems() { Id = Guid.NewGuid().ToString(), TaskId = items[0].Id, DoneRate = 100 });
            context.WbsItems.Add(new Models.WbsItems() { Id = Guid.NewGuid().ToString(), TaskId = items[1].Id, DoneRate = 50 });
            context.SaveChanges();

            var wbs = context.WbsView.Select(x => x).ToList();
            Assert.AreEqual(2, wbs.Count);
        }

        /// <summary>
        /// WBSアイテムのリストを取得
        /// </summary>
        [TestMethod]
        public async Task TestWbsGetItems()
        {
            context.TaskItems.Add(new Models.TaskItems() { Id = Guid.NewGuid().ToString(), TaskNo = "T001", Title = "最初のアイテム" });
            context.TaskItems.Add(new Models.TaskItems() { Id = Guid.NewGuid().ToString(), TaskNo = "T002", Title = "次のアイテム" });
            context.TaskItems.Add(new Models.TaskItems() { Id = Guid.NewGuid().ToString(), TaskNo = "T003", Title = "三番めのアイテム" });
            context.SaveChanges();
            var items = context.TaskItems.Select(x => x).ToList();
            context.WbsItems.Add(new Models.WbsItems() { Id = Guid.NewGuid().ToString(), TaskId = items[0].Id, DoneRate = 100 });
            context.WbsItems.Add(new Models.WbsItems() { Id = Guid.NewGuid().ToString(), TaskId = items[1].Id, DoneRate = 50 });
            context.SaveChanges();

            var lst = await service.GetWbses();
            Assert.AreEqual(2, lst.Count);
        }


        [TestMethod]
        public async Task TestGetProjectItem()
        {
            var serv = new ProjectService("http://localhost:5000");
            var items = await serv.GetProjectItem();
        }
        [TestMethod]
        public async Task TestGetProjectItemOne()
        {
            var serv = new ProjectService("http://localhost:5000");
            var items = await serv.GetProjectItem(1);
        }
        [TestMethod]
        public async Task TestGetProjectView()
        {
            var serv = new ProjectService("http://localhost:5000");
            var items = await serv.GetProjectView();
        }
        [TestMethod]
        public async Task TestGetProjectViewOne()
        {
            var serv = new ProjectService("http://localhost:5000");
            var items = await serv.GetProjectView(2);
        }
        */
    }
}
