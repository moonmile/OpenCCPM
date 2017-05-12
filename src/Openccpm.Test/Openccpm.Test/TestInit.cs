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
    /// 初期データ投入用
    /// </summary>
    [TestClass]
    public class TestInit : TestBase
    {
        [TestCleanup]
        public new void CleanUp()
        {
            // データを削除しない
        }

        [TestMethod]
        public void Init()
        {
            // 仮に初期化も入れる
            if ( context.Trackers.Count() == 0 )
            {
                context.Trackers.Add(new Trackers() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 1, Name = "バグ" });
                context.Trackers.Add(new Trackers() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 2, Name = "機能" });
                context.Trackers.Add(new Trackers() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 3, Name = "サポート" });
            }
            if (context.Statuses.Count() == 0)
            {
                context.Statuses.Add(new Statuses() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 1, Name = "新規" });
                context.Statuses.Add(new Statuses() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 2, Name = "進行中" });
                context.Statuses.Add(new Statuses() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 3, Name = "解決" });
                context.Statuses.Add(new Statuses() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 4, Name = "フィードバック" });
                context.Statuses.Add(new Statuses() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 5, Name = "終了" });
                context.Statuses.Add(new Statuses() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 6, Name = "却下" });
            }
            if (context.Priorities.Count() == 0)
            {
                context.Priorities.Add(new Priorities() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 1, Name = "低め" });
                context.Priorities.Add(new Priorities() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 2, Name = "標準" });
                context.Priorities.Add(new Priorities() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 3, Name = "高め" });
                context.Priorities.Add(new Priorities() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 4, Name = "急いで" });
                context.Priorities.Add(new Priorities() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 5, Name = "今すぐ" });
            }
            if (context.Users.Count() == 0)
            {
                context.Users.Add(new Users() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Login = "admin", LastName = "管理者", FirstName = "" });
                context.Users.Add(new Users() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Login = "guest", LastName = "ゲスト", FirstName = "" });
                context.Users.Add(new Users() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Login = "masuda", LastName = "増田", FirstName = "智明" });
                context.Users.Add(new Users() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Login = "yamada", LastName = "山田", FirstName = "太郎" });
            }
            context.SaveChanges();
        }
    }
}
