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
    public class TestProject : TestBase
    {
        new TicketDrivenService service;

        [TestInitialize]
        public override void SetUp()
        {
            context = new openccpm_dbEntities();
            service = new TicketDrivenService("http://localhost:5000");
        }

        /// <summary>
        /// 簡単な初期化テスト
        /// プロジェクトをひとつだけ作る
        /// </summary>
        [TestMethod]
        public async Task TestProjectInit()
        {
            var user = await service.LogInAsync("masuda@mail.com", "masuda");
            var items = await service.Project.GetItemsAsync();
            Assert.AreEqual(0, items.Count);

            var item = new Project();
            item.ProjectNo = "P0100";
            item.Name = "最初のプロジェクト";
            item.Description = "内容";

            item = await service.Project.AddAsync(item);
            Assert.IsNotNull(item);
            var id = item.Id;
            Assert.AreEqual("P0100", item.ProjectNo);
            Assert.AreEqual("最初のプロジェクト", item.Name);
            Assert.AreEqual("内容", item.Description);

            item = await service.Project.GetAsync("P0100");
            Assert.AreEqual("P0100", item.ProjectNo);
            Assert.AreEqual("最初のプロジェクト", item.Name);
        }

        /// <summary>
        /// 簡単な初期化テスト2
        /// プロジェクトを複数作る
        /// </summary>
        [TestMethod]
        public async Task TestProjectInit2()
        {
            var user = await service.LogInAsync("masuda@mail.com", "masuda");
            var items = await service.Project.GetItemsAsync();
            Assert.AreEqual(0, items.Count);

            var item1 = new Project() { ProjectNo = "P0100", Name = "最初のプロジェクト" };
            var item2 = new Project() { ProjectNo = "P0101", Name = "次のプロジェクト" };
            var item3 = new Project() { ProjectNo = "P0102", Name = "三番めのプロジェクト" };

            await service.Project.AddAsync(item1);
            await service.Project.AddAsync(item2);
            await service.Project.AddAsync(item3);

            items = await service.Project.GetItemsAsync();
            Assert.AreEqual(3, items.Count);

            Assert.AreEqual("P0100", items[2].ProjectNo);
            Assert.AreEqual("P0101", items[1].ProjectNo);
            Assert.AreEqual("P0102", items[0].ProjectNo);
        }
        /// <summary>
        /// プロジェクトをひとつだけ削除したパターン
        /// </summary>
        [TestMethod]
        public async Task TestProjectDelete()
        {
            var user = await service.LogInAsync("masuda@mail.com", "masuda");
            var items = await service.Project.GetItemsAsync();
            Assert.AreEqual(0, items.Count);

            var item1 = new Project() { ProjectNo = "P0100", Name = "最初のプロジェクト" };
            var item2 = new Project() { ProjectNo = "P0101", Name = "次のプロジェクト" };
            var item3 = new Project() { ProjectNo = "P0102", Name = "三番めのプロジェクト" };

            await service.Project.AddAsync(item1);
            await service.Project.AddAsync(item2);
            await service.Project.AddAsync(item3);

            items = await service.Project.GetItemsAsync();
            Assert.AreEqual(3, items.Count);

            Assert.AreEqual("P0101", items[1].ProjectNo);
            var id = items[1].Id;

            await service.Project.DeleteAsync(id);

            items = await service.Project.GetItemsAsync();
            Assert.AreEqual(2, items.Count);

            Assert.AreEqual("P0100", items[1].ProjectNo);
            Assert.AreEqual("P0102", items[0].ProjectNo);

            // 実際は3レコード残っている
            Assert.AreEqual(3, context.Projects.Count());
        }
        /// <summary>
        /// プロジェクトの内容を更新
        /// </summary>
        [TestMethod]
        public async Task TestProjectUpdate()
        {
            var user = await service.LogInAsync("masuda@mail.com", "masuda");
            var items = await service.Project.GetItemsAsync();
            Assert.AreEqual(0, items.Count);

            var item1 = new Project() { ProjectNo = "P0100", Name = "最初のプロジェクト" };
            var item2 = new Project() { ProjectNo = "P0101", Name = "次のプロジェクト" };
            var item3 = new Project() { ProjectNo = "P0102", Name = "三番めのプロジェクト" };

            await service.Project.AddAsync(item1);
            await service.Project.AddAsync(item2);
            await service.Project.AddAsync(item3);

            items = await service.Project.GetItemsAsync();
            Assert.AreEqual(3, items.Count);

            Assert.AreEqual("P0101", items[1].ProjectNo);
            var item = items[1];
            item.Name = "二番目のプロジェクト";
            item.Description = "内容";

            await service.Project.UpdateAsync(item);

            item = await service.Project.GetAsync(item.Id);

            Assert.AreEqual("二番目のプロジェクト", item.Name);
            Assert.AreEqual("内容", item.Description);
        }
    }
}
