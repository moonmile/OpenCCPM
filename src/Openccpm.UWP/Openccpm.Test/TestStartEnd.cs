using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Openccpm.Test.Models;
using System.Linq;
using Openccpm.UWP.Controllers;
using System.Threading.Tasks;
using Openccpm.Web.Models;

namespace Openccpm.Test
{
    [TestClass]
    public class TestStartEnd : TestBase
    {
        /// <summary>
        /// 簡単な開始/終了時間の設定
        /// </summary>
        [TestMethod]
        public async Task TestInit()
        {
            var item = new TaskItem();
            item.TaskNo = "T001";
            item.Subject = "最初のアイテム";
            item.Description = "内容...";
            item = await service.AddItem(item);
            Assert.IsNotNull(item);
            var id = item.Id;

            item = await service.GetItem(id);
            Assert.AreEqual("T001", item.TaskNo);
        }
    }
}
