using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Openccpm.Test.Models;
using System.Linq;
using Openccpm.UWP.Controllers;
using System.Threading.Tasks;
using Openccpm.Web.Models;

namespace Openccpm.Test
{
    public class TestBase
    {
        protected openccpm_dbEntities context;
        protected TaskItemService service;

        [TestInitialize]
        public virtual void SetUp()
        {
            context = new openccpm_dbEntities();
            service = new TaskItemService("http://localhost:5000");
            // this.CleanUp();
        }
        [TestCleanup]
        public virtual void CleanUp()
        {

            context.Database.ExecuteSqlCommand("delete from TaskTrees");
            context.Database.ExecuteSqlCommand("delete from TaskPerts");
            context.Database.ExecuteSqlCommand("delete from StartEndTimes");
            context.Database.ExecuteSqlCommand("delete from WbsItems");
            context.Database.ExecuteSqlCommand("delete from TicketItems");
            context.Database.ExecuteSqlCommand("delete from TaskItems");
            context.Database.ExecuteSqlCommand("delete from Users");
            context.Database.ExecuteSqlCommand("delete from Projects");
        }
    }

    [TestClass]
    public class TestTaskItem : TestBase
    {
        /// <summary>
        /// 簡単な初期化テスト
        /// </summary>
        [TestMethod]
        public async Task TestInit()
        {
            var items = await service.GetItems();
            Assert.AreEqual(0, items.Count);

            var item = new TaskItem();
            item.TaskNo = "T001";
            item.Subject = "最初のアイテム";
            item.Description = "内容...";
            item = await service.AddItem(item);
            Assert.IsNotNull(item);
            var id = item.Id;
            Assert.AreEqual("T001", item.TaskNo);
            Assert.AreEqual("最初のアイテム", item.Subject);

            item = await service.GetItem(id);
            Assert.AreEqual("T001", item.TaskNo);
            Assert.AreEqual("最初のアイテム", item.Subject);

            await service.DeleteItem(id);
            items = await service.GetItems();
            Assert.AreEqual(0, items.Count);
        }

        /// <summary>
        /// タスクの更新テスト
        /// </summary>
        [TestMethod]
        public async Task TestUpdate()
        {
            var items = await service.GetItems();
            Assert.AreEqual(0, items.Count);

            var item = new TaskItem();
            item.TaskNo = "T001";
            item.Subject = "最初のアイテム";
            item.Description = "内容...";
            item = await service.AddItem(item);
            Assert.IsNotNull(item);
            var id = item.Id;
            // 作成日時
            Assert.IsNotNull(item.CreatedAt);
            var createAt = item.CreatedAt;
            // 更新日時
            Assert.IsNull(item.UpdatedAt);

            item.Description = "内容を更新する";
            await service.UpdateItem(item);
            // チェック
            item = await service.GetItem(id);
            Assert.AreEqual("内容を更新する", item.Description);
            // 作成日はそのまま
            Assert.AreEqual(createAt, item.CreatedAt);
            // 更新日が変わる
            Assert.IsNotNull(item.UpdatedAt);
        }
    }
}
