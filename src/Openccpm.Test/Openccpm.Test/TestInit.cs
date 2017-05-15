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
            if (context.Trackers.Count() == 0)
            {
                context.Trackers.Add(new Trackers() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 1, Name = "バグ" });
                context.Trackers.Add(new Trackers() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 2, Name = "機能" });
                context.Trackers.Add(new Trackers() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 3, Name = "サポート" });
                context.Trackers.Add(new Trackers() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Position = 4, Name = "マイルストーン" });
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
            if (context.AspNetRoles.Count() == 0)
            {
                context.AspNetRoles.Add(new AspNetRoles() { Id = "1BD00BD1-C112-4DC7-82BE-0CDEAC1720A4", Name = "ProjectAdministrators", NormalizedName = "PROJECTADMINISTRATORS" });
                context.AspNetRoles.Add(new AspNetRoles() { Id = "2A226C6B-492C-41F5-B89D-3F7C926E26E9", Name = "ProjectMembers", NormalizedName = "PROJECTMEMBERS" });
                context.AspNetRoles.Add(new AspNetRoles() { Id = "3376DBBD-E2B2-4523-8F56-4AF17188421F", Name = "Administrators", NormalizedName = "ADMINISTRATORS" });
                context.AspNetRoles.Add(new AspNetRoles() { Id = "6591818A-8C8D-4888-8941-09A70A344EB7", Name = "Anonymous", NormalizedName = "ANONYMOUS" });
            }
            if (context.AspNetUsers.Count() == 0)
            {
                context.AspNetUsers.Add(new AspNetUsers() { Id = "b366afc2-bcac-43c0-bc9d-cd0c43c0f101", Email = "masuda@mail.com", UserName = "増田(管理)", NormalizedEmail = "MASUDA@MAIL.COM", NormalizedUserName = "MASUDA@MAIL.COM", PasswordHash = "AQAAAAEAACcQAAAAEHzCQ/3CpoxtZU6BpJBns2/QMaFNdZI2iA3+nZZK3Jl6tYFUD2hDfMTi8T+0aetp4g==", ConcurrencyStamp = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString() });
                context.AspNetUsers.Add(new AspNetUsers() { Id = "9e30fb21-2a41-4f60-8930-9cf205880c81", Email = "tomoaki@mail.com", UserName = "智明", NormalizedEmail = "TOMOAKI@MAIL.COM", NormalizedUserName = "TOMOAKI@MAIL.COM", PasswordHash = "AQAAAAEAACcQAAAAEJZvZpfC/1zm+Pqtt7W8ZNS30wPzhX4lvRaMeKnj84owmjGr9n1taZ77LLhN9stBeg==", ConcurrencyStamp = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString() });
                context.AspNetUsers.Add(new AspNetUsers() { Id = "fef8beea-44b9-4e25-9db0-8b782be7b731", Email = "admin@mail.com", UserName = "管理者", NormalizedEmail = "ADMIN@MAIL.COM", NormalizedUserName = "ADMIN@MAIL.COM", PasswordHash = "AQAAAAEAACcQAAAAEKStDa5Soa8QOrUi3OlCOso4SG6g4yGoXjILQx0bO2HmmKOcFpDLbwmhoa8cgG7UmA==", ConcurrencyStamp = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString() });
                context.AspNetUsers.Add(new AspNetUsers() { Id = "e0283b6a-d514-414e-8dce-2ae61a62dd33", Email = "guest@mail.com", UserName = "ゲスト", NormalizedEmail = "GUEST@MAIL.COM", NormalizedUserName = "GUEST@MAIL.COM", PasswordHash = "AQAAAAEAACcQAAAAEDYEyvmb8mTM4PqwRd7hA9E+hn+x5ryQcNBxHlx8rUBYVTHPH7OpPgVnA/53GyCRMA==", ConcurrencyStamp = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString() });
                context.Database.ExecuteSqlCommand("delete from AspNetUserRoles");
                context.Database.ExecuteSqlCommand("insert into AspNetUserRoles values ('b366afc2-bcac-43c0-bc9d-cd0c43c0f101', '1BD00BD1-C112-4DC7-82BE-0CDEAC1720A4')");
                context.Database.ExecuteSqlCommand("insert into AspNetUserRoles values ('b366afc2-bcac-43c0-bc9d-cd0c43c0f101', '2A226C6B-492C-41F5-B89D-3F7C926E26E9')");
                context.Database.ExecuteSqlCommand("insert into AspNetUserRoles values ('9e30fb21-2a41-4f60-8930-9cf205880c81', '2A226C6B-492C-41F5-B89D-3F7C926E26E9')");
                context.Database.ExecuteSqlCommand("insert into AspNetUserRoles values ('fef8beea-44b9-4e25-9db0-8b782be7b731', '3376DBBD-E2B2-4523-8F56-4AF17188421F')");
            }


            context.SaveChanges();
        }
        /// <summary>
        /// 仮プロジェクト、仮チケットの投入
        /// </summary>
        [TestMethod]
        public void InitProject()
        {
            context.Database.ExecuteSqlCommand("delete from ProjectUsers");
            context.Database.ExecuteSqlCommand("delete from Projects");
            context.Database.ExecuteSqlCommand("delete from TicketItems");
            context.Database.ExecuteSqlCommand("delete from TaskItems");

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

            // P0100 にメンバを追加する
            context.ProjectUsers.Add(new ProjectUsers() { Id = Guid.NewGuid().ToString(), ProjectId = project.Id, UserId = "b366afc2-bcac-43c0-bc9d-cd0c43c0f101" });
            context.ProjectUsers.Add(new ProjectUsers() { Id = Guid.NewGuid().ToString(), ProjectId = project.Id, UserId = "9e30fb21-2a41-4f60-8930-9cf205880c81" });
            context.SaveChanges();
        }
    }
}
