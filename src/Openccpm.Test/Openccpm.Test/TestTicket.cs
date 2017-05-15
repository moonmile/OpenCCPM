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
        new TicketDrivenService service;

        [TestInitialize]
        public override void SetUp()
        {
            context = new openccpm_dbEntities();
            // 初期データを投入
            this.CleanUp();
            Init();
            service = new TicketDrivenService("http://localhost:5000");

        }
        public void Init()
        {
            // 仮に初期化も入れる
            if (context.Trackers.Count() == 0)
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
                context.Users.Add(new Users() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Login = "管理者", LastName = "管理者", FirstName = "" });
                context.Users.Add(new Users() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Login = "ゲスト", LastName = "ゲスト", FirstName = "" });
                context.Users.Add(new Users() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Login = "masuda", LastName = "増田", FirstName = "智明" });
                context.Users.Add(new Users() { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.Now, Login = "yamada", LastName = "山田", FirstName = "太郎" });
                */
            }
            context.SaveChanges();
        }

        /// <summary>
        /// 簡単な初期化テスト
        /// </summary>
        [TestMethod]
        public async Task TestTicketInit()
        {
            // プロジェクトを作る
            var project = new Project() { ProjectNo = "P0100", Name = "最初のプロジェクト" };
            project = await service.Project.AddAsync(project);
            // トラッカー等を取得
            await service.Initalize(project.Id);

            var trId = service.ListTracker.SingleOrDefault(x => x.Name == "機能")?.Id;
            var stId = service.ListStatus.SingleOrDefault(x => x.Name == "新規")?.Id;
            var prId = service.ListPriority.SingleOrDefault(x => x.Name == "標準")?.Id;
            var userId = service.ListAssignTo.SingleOrDefault(x => x.UserName == "管理者")?.Id;

            // チケットを追加する
            var ticket = new TicketView()
            {
                ProjectId = project.Id,
                TaskNo = "TK001",
                Subject = "最初のチケット",
                Description = "内容",
                PlanTime = 1,
                DoneTime = 2,
                Tracker_Id = trId,
                Status_Id = stId,
                Priority_Id = prId,
                AssignedTo_Id = userId,
                DoneRate = 0,
                StartDate = DateTime.Parse("2017/05/11"),
                DueDate = DateTime.Parse("2017/05/21"),
            };
            await service.AddTicketAsync(ticket);

            var items = await service.GetTicketsAsync(project.Id);
            Assert.AreEqual(1, items.Count);

            var item = items[0];
            Assert.AreEqual(project.Id, item.ProjectId);
            Assert.AreEqual("P0100", item.Project_ProjectNo);
            Assert.AreEqual("最初のプロジェクト", item.Project_Name);
            // 一覧の場合はリレーション情報は取得しない

            // 個別に詳細情報を取得する
            ticket = await service.GetTicketAsync(item.Id);
            // プロジェクト情報
            Assert.AreEqual(project.Id, ticket.ProjectId);
            Assert.AreEqual("P0100", ticket.Project_ProjectNo);
            Assert.AreEqual("最初のプロジェクト", ticket.Project_Name);
            // チケット情報
            Assert.AreEqual("TK001", ticket.TaskNo);
            Assert.AreEqual("最初のチケット", ticket.Subject);
            Assert.AreEqual("内容", ticket.Description);
            Assert.AreEqual(1, ticket.PlanTime);
            Assert.AreEqual(2, ticket.DoneTime);
            Assert.AreEqual(0, ticket.DoneRate);
            Assert.AreEqual(DateTime.Parse("2017/05/11"), ticket.StartDate);
            Assert.AreEqual(DateTime.Parse("2017/05/21"), ticket.DueDate);
            // リレーションのチェック
            Assert.AreEqual("機能", ticket.Tracker.Name);
            Assert.AreEqual("新規", ticket.Status.Name);
            Assert.AreEqual("標準", ticket.Priority.Name);
            Assert.AreEqual("管理者", ticket.AssignedTo.UserName);
        }

        /// <summary>
        /// 簡単な初期化テスト
        /// 複数チケットの登録
        /// </summary>
        [TestMethod]
        public async Task TestTicketInit2()
        {
            // プロジェクトを作る
            var project = new Project() { ProjectNo = "P0100", Name = "最初のプロジェクト" };
            project = await service.Project.AddAsync(project);
            // トラッカー等を取得
            await service.Initalize(project.Id);

            var trId = service.ListTracker.SingleOrDefault(x => x.Name == "機能")?.Id;
            var stId = service.ListStatus.SingleOrDefault(x => x.Name == "新規")?.Id;
            var prId = service.ListPriority.SingleOrDefault(x => x.Name == "標準")?.Id;
            var userId = service.ListAssignTo.SingleOrDefault(x => x.UserName == "管理者")?.Id;

            // チケットを追加する
            var ticket1 = new TicketView()
            {
                ProjectId = project.Id,
                TaskNo = "TK001",
                Subject = "最初のチケット",
                Description = "内容",
                Tracker_Id = trId,
                Status_Id = stId,
                Priority_Id = prId,
                DoneRate = 0,
            };
            await service.AddTicketAsync(ticket1);
            var items = await service.GetTicketsAsync(project.Id);
            Assert.AreEqual(1, items.Count);

            // チケットを追加する
            var ticket2 = new TicketView()
            {
                ProjectId = project.Id,
                TaskNo = "TK002",
                Subject = "二番目のチケット",
                Description = "内容",
                Tracker_Id = trId,
                Status_Id = stId,
                Priority_Id = prId,
                DoneRate = 0,
            };
            // チケットを追加する
            var ticket3 = new TicketView()
            {
                ProjectId = project.Id,
                TaskNo = "TK003",
                Subject = "三番めのチケット",
                Description = "内容",
                Tracker_Id = trId,
                Status_Id = stId,
                Priority_Id = prId,
                DoneRate = 0,
            };
            await service.AddTicketAsync(ticket2);
            await service.AddTicketAsync(ticket3);
            items = await service.GetTicketsAsync(project.Id);
            Assert.AreEqual(3, items.Count);
            // デフォルトでは新しい順で取得できる
            Assert.AreEqual("TK003", items[0].TaskNo);
            Assert.AreEqual("TK002", items[1].TaskNo);
            Assert.AreEqual("TK001", items[2].TaskNo);
        }

        /// <summary>
        /// 簡単な初期化テスト
        /// プロジェクト内のチケットを返す
        /// </summary>
        [TestMethod]
        public async Task TestTicketInit3()
        {
            // プロジェクトを作る
            var project1 = new Project() { ProjectNo = "P0100", Name = "最初のプロジェクト" };
            var project2 = new Project() { ProjectNo = "P0101", Name = "次のプロジェクト" };
            project1 = await service.Project.AddAsync(project1);
            project2 = await service.Project.AddAsync(project2);
            // トラッカー等を取得
            await service.Initalize(project1.Id);

            var trId = service.ListTracker.SingleOrDefault(x => x.Name == "機能")?.Id;
            var stId = service.ListStatus.SingleOrDefault(x => x.Name == "新規")?.Id;
            var prId = service.ListPriority.SingleOrDefault(x => x.Name == "標準")?.Id;
            var userId = service.ListAssignTo.SingleOrDefault(x => x.UserName == "管理者")?.Id;

            // チケットを追加する
            var ticket1 = new TicketView()
            {
                ProjectId = project1.Id,
                TaskNo = "TK001",
                Subject = "最初のチケット",
                Description = "内容",
                Tracker_Id = trId,
                Status_Id = stId,
                Priority_Id = prId,
                DoneRate = 0,
            };
            await service.AddTicketAsync(ticket1);
            var items = await service.GetTicketsAsync(project1.Id);
            Assert.AreEqual(1, items.Count);

            // チケットを追加する
            var ticket2 = new TicketView()
            {
                ProjectId = project2.Id,
                TaskNo = "TK002",
                Subject = "二番目のチケット",
                Description = "内容",
                Tracker_Id = trId,
                Status_Id = stId,
                Priority_Id = prId,
                DoneRate = 0,
            };
            // チケットを追加する
            var ticket3 = new TicketView()
            {
                ProjectId = project1.Id,
                TaskNo = "TK003",
                Subject = "三番めのチケット",
                Description = "内容",
                Tracker_Id = trId,
                Status_Id = stId,
                Priority_Id = prId,
                DoneRate = 0,
            };
            await service.AddTicketAsync(ticket2);
            await service.AddTicketAsync(ticket3);
            // プロジェクト毎のチケットを取得
            items = await service.GetTicketsAsync(project1.Id);
            Assert.AreEqual(2, items.Count);
            items = await service.GetTicketsAsync(project2.Id);
            Assert.AreEqual(1, items.Count);
        }
        /// <summary>
        /// 簡単な初期化テスト
        /// チケットの更新
        /// </summary>
        [TestMethod]
        public async Task TestTicketUpdate()
        {
            // プロジェクトを作る
            var project = new Project() { ProjectNo = "P0100", Name = "最初のプロジェクト" };
            project = await service.Project.AddAsync(project);
            // トラッカー等を取得
            await service.Initalize(project.Id);

            var trId = service.ListTracker.SingleOrDefault(x => x.Name == "機能")?.Id;
            var stId = service.ListStatus.SingleOrDefault(x => x.Name == "新規")?.Id;
            var prId = service.ListPriority.SingleOrDefault(x => x.Name == "標準")?.Id;
            var userId = service.ListAssignTo.SingleOrDefault(x => x.UserName == "管理者")?.Id;

            // チケットを追加する
            var ticket = new TicketView()
            {
                ProjectId = project.Id,
                TaskNo = "TK001",
                Subject = "最初のチケット",
                Description = "内容",
                PlanTime = 1,
                DoneTime = 2,
                Tracker_Id = trId,
                Status_Id = stId,
                Priority_Id = prId,
                AssignedTo_Id = userId,
                DoneRate = 0,
                StartDate = DateTime.Parse("2017/05/11"),
                DueDate = DateTime.Parse("2017/05/21"),
            };
            ticket = await service.AddTicketAsync(ticket);

            var items = await service.GetTicketsAsync(project.Id);
            Assert.AreEqual(1, items.Count);
            // 個別に詳細情報を取得する
            ticket = await service.GetTicketAsync(ticket.Id);
            Assert.AreEqual("TK001", ticket.TaskNo);
            Assert.AreEqual("最初のチケット", ticket.Subject);
            Assert.AreEqual("内容", ticket.Description);
            Assert.IsNull(ticket.UpdatedAt);

            // タイトルと説明を更新する
            ticket.Subject = "１番目のチケット";
            ticket.Description = "タイトルの変更";
            await service.UpdateTicketAsync(ticket);

            ticket = await service.GetTicketAsync(ticket.Id);
            Assert.AreEqual("TK001", ticket.TaskNo);
            Assert.AreEqual("１番目のチケット", ticket.Subject);
            Assert.AreEqual("タイトルの変更", ticket.Description);
            // 更新日時が付けられる
            Assert.IsNotNull(ticket.UpdatedAt);
        }
        /// <summary>
        /// 担当者の切り替え
        /// </summary>
        [TestMethod]
        public async Task TestTicketUpdate2()
        {
            // プロジェクトを作る
            var project = new Project() { ProjectNo = "P0100", Name = "最初のプロジェクト" };
            project = await service.Project.AddAsync(project);
            // トラッカー等を取得
            await service.Initalize(project.Id);

            var trId = service.ListTracker.SingleOrDefault(x => x.Name == "機能")?.Id;
            var stId = service.ListStatus.SingleOrDefault(x => x.Name == "新規")?.Id;
            var prId = service.ListPriority.SingleOrDefault(x => x.Name == "標準")?.Id;
            var admin = service.ListAssignTo.SingleOrDefault(x => x.UserName == "管理者")?.Id;
            var guest = service.ListAssignTo.SingleOrDefault(x => x.UserName == "ゲスト")?.Id;

            // チケットを追加する
            var ticket = new TicketView()
            {
                ProjectId = project.Id,
                TaskNo = "TK001",
                Subject = "最初のチケット",
                Description = "内容",
                PlanTime = 1,
                DoneTime = 2,
                Tracker_Id = trId,
                Status_Id = stId,
                Priority_Id = prId,
                AssignedTo_Id = admin,      // 管理者
                DoneRate = 0,
                StartDate = DateTime.Parse("2017/05/11"),
                DueDate = DateTime.Parse("2017/05/21"),
            };
            ticket = await service.AddTicketAsync(ticket);

            var items = await service.GetTicketsAsync(project.Id);
            Assert.AreEqual(1, items.Count);
            // 個別に詳細情報を取得する
            ticket = await service.GetTicketAsync(ticket.Id);
            Assert.AreEqual("TK001", ticket.TaskNo);
            Assert.AreEqual(admin, ticket.AssignedTo_Id);
            Assert.AreEqual("管理者", ticket.AssignedTo_UserName);

            // 担当者を変える
            ticket.AssignedTo.Id = guest;
            await service.UpdateTicketAsync(ticket);

            ticket = await service.GetTicketAsync(ticket.Id);
            Assert.AreEqual("TK001", ticket.TaskNo);
            Assert.AreEqual(guest, ticket.AssignedTo_Id);
            Assert.AreEqual("ゲスト", ticket.AssignedTo_UserName);
        }
        /// <summary>
        /// ステータスの切り替え
        /// </summary>
        [TestMethod]
        public async Task TestTicketUpdate3()
        {
            // プロジェクトを作る
            var project = new Project() { ProjectNo = "P0100", Name = "最初のプロジェクト" };
            project = await service.Project.AddAsync(project);
            // トラッカー等を取得
            await service.Initalize(project.Id);

            var trId = service.ListTracker.SingleOrDefault(x => x.Name == "機能")?.Id;
            var stNew = service.ListStatus.SingleOrDefault(x => x.Name == "新規")?.Id;
            var stEnd = service.ListStatus.SingleOrDefault(x => x.Name == "終了")?.Id;
            var prId = service.ListPriority.SingleOrDefault(x => x.Name == "標準")?.Id;
            var userId = service.ListAssignTo.SingleOrDefault(x => x.UserName == "管理者")?.Id;

            // チケットを追加する
            var ticket = new TicketView()
            {
                ProjectId = project.Id,
                TaskNo = "TK001",
                Subject = "最初のチケット",
                Description = "内容",
                PlanTime = 1,
                DoneTime = 2,
                Tracker_Id = trId,
                Status_Id = stNew,          // 新規
                Priority_Id = prId,
                AssignedTo_Id = userId,
                DoneRate = 0,
                StartDate = DateTime.Parse("2017/05/11"),
                DueDate = DateTime.Parse("2017/05/21"),
            };
            ticket = await service.AddTicketAsync(ticket);

            var items = await service.GetTicketsAsync(project.Id);
            Assert.AreEqual(1, items.Count);
            // 個別に詳細情報を取得する
            ticket = await service.GetTicketAsync(ticket.Id);
            Assert.AreEqual("TK001", ticket.TaskNo);
            Assert.AreEqual(stNew, ticket.Status_Id);
            Assert.AreEqual("新規", ticket.Status_Name);

            // ステータスを変える
            ticket.Status.Id = stEnd;
            await service.UpdateTicketAsync(ticket);

            ticket = await service.GetTicketAsync(ticket.Id);
            Assert.AreEqual("TK001", ticket.TaskNo);
            Assert.AreEqual(stEnd, ticket.Status_Id);
            Assert.AreEqual("終了", ticket.Status_Name);
        }
        /// <summary>
        /// チケットの削除テスト
        /// </summary>
        [TestMethod]
        public async Task TestTicketDelete()
        {
            // プロジェクトを作る
            var project = new Project() { ProjectNo = "P0100", Name = "最初のプロジェクト" };
            project = await service.Project.AddAsync(project);
            // トラッカー等を取得
            await service.Initalize(project.Id);

            var trId = service.ListTracker.SingleOrDefault(x => x.Name == "機能")?.Id;
            var stId = service.ListStatus.SingleOrDefault(x => x.Name == "新規")?.Id;
            var prId = service.ListPriority.SingleOrDefault(x => x.Name == "標準")?.Id;
            var userId = service.ListAssignTo.SingleOrDefault(x => x.UserName == "管理者")?.Id;

            // チケットを追加する
            var ticket1 = new TicketView()
            {
                ProjectId = project.Id,
                TaskNo = "TK001",
                Subject = "最初のチケット",
                Description = "内容",
                Tracker_Id = trId,
                Status_Id = stId,
                Priority_Id = prId,
                DoneRate = 0,
            };
            var ticket2 = new TicketView()
            {
                ProjectId = project.Id,
                TaskNo = "TK002",
                Subject = "二番目のチケット",
                Description = "内容",
                Tracker_Id = trId,
                Status_Id = stId,
                Priority_Id = prId,
                DoneRate = 0,
            };
            var ticket3 = new TicketView()
            {
                ProjectId = project.Id,
                TaskNo = "TK003",
                Subject = "三番めのチケット",
                Description = "内容",
                Tracker_Id = trId,
                Status_Id = stId,
                Priority_Id = prId,
                DoneRate = 0,
            };
            ticket1 = await service.AddTicketAsync(ticket1);
            ticket2 = await service.AddTicketAsync(ticket2);
            ticket3 = await service.AddTicketAsync(ticket3);
            var items = await service.GetTicketsAsync(project.Id);
            Assert.AreEqual(3, items.Count);
            // デフォルトでは新しい順で取得できる
            Assert.AreEqual("TK003", items[0].TaskNo);
            Assert.AreEqual("TK002", items[1].TaskNo);
            Assert.AreEqual("TK001", items[2].TaskNo);

            // TK002 を削除する
            await service.DeleteTicketAsync(ticket2.Id);
            items = await service.GetTicketsAsync(project.Id);
            Assert.AreEqual(2, items.Count);
            Assert.AreEqual("TK003", items[0].TaskNo);
            Assert.AreEqual("TK001", items[1].TaskNo);
        }
    }
}
