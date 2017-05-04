using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Openccpm.Test.Models;
using System.Linq;

namespace Openccpm.Test
{
    /// <summary>
    /// 試験用のデータベースをクリアする
    /// </summary>
    [TestClass]
    public class ClearDB
    {
        openccpm_dbEntities context;
        [TestInitialize]
        public void SetUp()
        {
            context = new openccpm_dbEntities();
        }
        [TestCleanup]
        public void CleanUp()
        {
            context.Database.ExecuteSqlCommand("delete from TaskTrees");
            context.Database.ExecuteSqlCommand("delete from TaskPerts");
            context.Database.ExecuteSqlCommand("delete from StartEndTimes");
            context.Database.ExecuteSqlCommand("delete from WbsItems");
            context.Database.ExecuteSqlCommand("delete from TaskItems");
        }

        [TestMethod]
        public void CheckTaskItems()
        {
            int cnt = context.TaskItems.Count();
            if (cnt > 0)
            {
                var items = context.TaskItems.Select(x => x);
                context.TaskItems.RemoveRange(items);
                context.SaveChanges();
            }
            cnt = context.TaskItems.Count();
            Assert.AreEqual(0, cnt);

        }
    }
}
