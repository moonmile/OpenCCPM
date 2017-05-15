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

        /// <summary>
        /// マスターデータの投入
        /// </summary>
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
            if (context.AspNetUsers.Count() == 0)
            {
                /*
                context.Users.Add(new AspNetUsers() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Login = "admin", LastName = "管理者", FirstName = "" });
                context.Users.Add(new Users() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Login = "guest", LastName = "ゲスト", FirstName = "" });
                context.Users.Add(new Users() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Login = "masuda", LastName = "増田", FirstName = "智明" });
                context.Users.Add(new Users() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Login = "yamada", LastName = "山田", FirstName = "太郎" });
                */
            }
            context.SaveChanges();
        }
        /// <summary>
        /// 仮プロジェクト、仮チケットの投入
        /// </summary>
        [TestMethod]
        public void InitProject()
        {
            if (context.Projects.Count() > 0)
                return;

            context.Projects.Add(new Projects() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, ProjectNo = "P0100", Name = "最初のプロジェクト", Description = "解説" });
            context.Projects.Add(new Projects() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, ProjectNo = "P0200", Name = "次のプロジェクト", Description = "解説" });
            context.Projects.Add(new Projects() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, ProjectNo = "P0300", Name = "三番めのプロジェクト", Description = "解説" });
            context.SaveChanges();
            var project = context.Projects.SingleOrDefault(x => x.ProjectNo == "P0100");

            var task1 = context.TaskItems.Add(new TaskItems() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, TaskNo = "TK001", Subject = "最初のチケット", Description = "内容", ProjectId = project.Id });
            var task2 = context.TaskItems.Add(new TaskItems() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, TaskNo = "TK002", Subject = "次のチケット", Description = "内容", ProjectId = project.Id });
            var task3 = context.TaskItems.Add(new TaskItems() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, TaskNo = "TK003", Subject = "三番めのチケット", Description = "内容", ProjectId = project.Id });
            context.SaveChanges();

            var tracker = context.Trackers.SingleOrDefault(x => x.Name == "機能");
            var status = context.Statuses.SingleOrDefault(x => x.Name == "新規");
            var pri = context.Priorities.SingleOrDefault(x => x.Name == "標準");
            var assign = context.AspNetUsers.SingleOrDefault(x => x.UserName == "管理者");


            context.TicketItems.Add(new TicketItems() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, TaskId = task1.Id, TrackerId = tracker.Id, StatusId = status.Id, PriorityId = pri.Id, AssignedToId = assign.Id, DoneRate = 0 });
            context.TicketItems.Add(new TicketItems() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, TaskId = task2.Id, TrackerId = tracker.Id, StatusId = status.Id, PriorityId = pri.Id, AssignedToId = assign.Id, DoneRate = 0 });
            context.TicketItems.Add(new TicketItems() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, TaskId = task3.Id, TrackerId = tracker.Id, StatusId = status.Id, PriorityId = pri.Id, AssignedToId = assign.Id, DoneRate = 0 });
            context.SaveChanges();
        }
    }
}
