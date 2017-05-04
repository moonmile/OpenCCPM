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
            item.Title = "最初のアイテム";
            item.Desc = "内容...";
            item = await service.AddItem(item);
            Assert.IsNotNull(item);
            var id = item.Id;

            // 設定なし
            Assert.IsNull(item.PlanStartEnds);
            Assert.IsNull(item.DoneStartEnds);

            // 予定を入力
            var plan = await service.AddStartEnd(id, DateTime.Parse("2017/05/02"), DateTime.Parse("2017/05/03"), true);
            Assert.IsNotNull(plan);
            // 実績を入力
            var done = await service.AddStartEnd(id, DateTime.Parse("2017/05/12"), DateTime.Parse("2017/05/13"), false);
            Assert.IsNotNull(done);

            item = await service.GetItem(id);
            Assert.AreEqual("T001", item.TaskNo);
            Assert.AreEqual(1, item.PlanStartEnds.Count);
            Assert.AreEqual(plan.StartAt, item.PlanStartEnds[0].StartAt);
            Assert.AreEqual(plan.EndAt, item.PlanStartEnds[0].EndAt);
            Assert.AreEqual(1, item.DoneStartEnds.Count);
            Assert.AreEqual(done.StartAt, item.DoneStartEnds[0].StartAt);
            Assert.AreEqual(done.EndAt, item.DoneStartEnds[0].EndAt);
        }


        /// <summary>
        /// 予定日時の更新
        /// </summary>
        [TestMethod]
        public async Task TestUpdatePlan()
        {
            var item = new TaskItem();
            item.TaskNo = "T001";
            item.Title = "最初のアイテム";
            item.Desc = "内容...";
            item = await service.AddItem(item);
            Assert.IsNotNull(item);
            var id = item.Id;

            // 設定なし
            Assert.IsNull(item.PlanStartEnds);
            Assert.IsNull(item.DoneStartEnds);

            // 予定を入力
            var plan = await service.AddStartEnd(id, DateTime.Parse("2017/05/02"), null, true);
            Assert.IsNotNull(plan);

            item = await service.GetItem(id);
            Assert.AreEqual("T001", item.TaskNo);
            Assert.AreEqual(1, item.PlanStartEnds.Count);
            Assert.AreEqual(plan.StartAt, item.PlanStartEnds[0].StartAt);
            Assert.AreEqual(null, item.PlanStartEnds[0].EndAt);

            // 予定を更新
            plan.EndAt = DateTime.Parse("2017/05/03");
            await service.UpdateStartEnd(plan);

            item = await service.GetItem(id);
            Assert.AreEqual("T001", item.TaskNo);
            Assert.AreEqual(1, item.PlanStartEnds.Count);
            Assert.AreEqual(plan.StartAt, item.PlanStartEnds[0].StartAt);
            Assert.AreEqual(plan.EndAt, item.PlanStartEnds[0].EndAt);
        }

        /// <summary>
        /// 予定日時の更新2
        /// </summary>
        [TestMethod]
        public async Task TestUpdatePlan2()
        {
            var item = new TaskItem();
            item.TaskNo = "T001";
            item.Title = "最初のアイテム";
            item.Desc = "内容...";
            item = await service.AddItem(item);
            Assert.IsNotNull(item);
            var id = item.Id;

            // 設定なし
            Assert.IsNull(item.PlanStartEnds);
            Assert.IsNull(item.DoneStartEnds);

            // 予定を入力
            var plan = await service.AddStartEnd(id, DateTime.Parse("2017/05/02"), null, true);
            Assert.IsNotNull(plan);

            item = await service.GetItem(id);
            Assert.AreEqual("T001", item.TaskNo);
            Assert.AreEqual(1, item.PlanStartEnds.Count);
            Assert.AreEqual(plan.StartAt, item.PlanStartEnds[0].StartAt);
            Assert.AreEqual(null, item.PlanStartEnds[0].EndAt);

            // 予定を更新（最初のアイテムを更新する）
            await service.UpdateStartEnd(id, DateTime.Parse("2017/05/02"), DateTime.Parse("2017/05/03"), true);

            item = await service.GetItem(id);
            Assert.AreEqual("T001", item.TaskNo);
            Assert.AreEqual(1, item.PlanStartEnds.Count);
            Assert.AreEqual(plan.StartAt, item.PlanStartEnds[0].StartAt);
            Assert.AreEqual(DateTime.Parse("2017/05/03"), item.PlanStartEnds[0].EndAt);
        }

        /// <summary>
        /// 予定日時の更新3
        /// UpdateStartEnd で自動追加される
        /// </summary>
        [TestMethod]
        public async Task TestUpdatePlan3()
        {
            var item = new TaskItem();
            item.TaskNo = "T001";
            item.Title = "最初のアイテム";
            item.Desc = "内容...";
            item = await service.AddItem(item);
            Assert.IsNotNull(item);
            var id = item.Id;

            // 設定なし
            Assert.IsNull(item.PlanStartEnds);
            Assert.IsNull(item.DoneStartEnds);

            // 予定を更新（自動で追加される）
            await service.UpdateStartEnd(id, DateTime.Parse("2017/05/02"), DateTime.Parse("2017/05/03"), true);

            item = await service.GetItem(id);
            Assert.AreEqual("T001", item.TaskNo);
            Assert.AreEqual(1, item.PlanStartEnds.Count);
            Assert.AreEqual(DateTime.Parse("2017/05/02"), item.PlanStartEnds[0].StartAt);
            Assert.AreEqual(DateTime.Parse("2017/05/03"), item.PlanStartEnds[0].EndAt);
        }

        /// <summary>
        /// 実績日時の更新
        /// UpdateStartEnd で自動追加される
        /// </summary>
        [TestMethod]
        public async Task TestUpdateDone()
        {
            var item = new TaskItem();
            item.TaskNo = "T001";
            item.Title = "最初のアイテム";
            item.Desc = "内容...";
            item = await service.AddItem(item);
            Assert.IsNotNull(item);
            var id = item.Id;

            // 設定なし
            Assert.IsNull(item.PlanStartEnds);
            Assert.IsNull(item.DoneStartEnds);

            // 予定を更新（自動で追加される）
            await service.UpdateStartEnd(id, DateTime.Parse("2017/05/12"), DateTime.Parse("2017/05/13"), false);

            item = await service.GetItem(id);
            Assert.AreEqual("T001", item.TaskNo);
            Assert.AreEqual(1, item.DoneStartEnds.Count);
            Assert.AreEqual(DateTime.Parse("2017/05/12"), item.DoneStartEnds[0].StartAt);
            Assert.AreEqual(DateTime.Parse("2017/05/13"), item.DoneStartEnds[0].EndAt);
        }

    }
}
