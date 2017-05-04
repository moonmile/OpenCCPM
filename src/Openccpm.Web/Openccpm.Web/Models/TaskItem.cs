using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Openccpm.Web.Models
{
    public abstract class EntityData 
    {
        [Key]
        public string Id { get; set; }
        [Timestamp]
        public byte[] Version { get; set; }
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset? CreatedAt { get; set; }
        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool Deleted { get; set; }
    }

    /// <summary>
    /// タスクのベースエンティティ
    /// </summary>
    [Table("TaskItems")]
    public class TaskItem : EntityData
    {
        // タスク番号（自前で振るため）
        public string TaskNo { get; set; }
        // タスクのタイトル
        public string Title { get; set; }
        // タスクの内容
        public string Desc { get; set; }
        // 予定時間
        public double? PlanTime { get; set; }
        // 実績時間
        public double? DoneTime { get; set; }

        // リレーション用
        // 予定開始/終了のリスト
        public List<StartEndTime> PlanStartEnds;
        // 実績開始/終了のリスト
        public List<StartEndTime> DoneStartEnds;
        // 親タスク for WBS
        public TaskItem ParentTask;
        // 子タスクのリスト for WBS
        public List<TaskItem> ChildTasks;
        // 前タスクのリスト for PERT
        public List<TaskItem> PreTasks;
        // 後タスクのリスト for PART
        public List<TaskItem> PostTasks;
    }


    /// <summary>
    /// チケット駆動用
    /// </summary>
    [Table("TicketItems")]
    public class TicketItem : EntityData
    {
        // タスクID
        public string TaskId { get; set; }
        // トラッカー
        public string TrackId { get; set; }
        // ステータス
        public string StatusId { get; set; }
        // 優先度
        public string PriorityId { get; set; }
        // 担当者
        public string AssignedId { get; set; }
        // 進捗率
        public int DoneRate { get; set; }
        // 所有者
        public string AuthorId { get; set; }

        public TicketItem() { }
        // コンバート用
        public TicketItem( Ticket t )
        {
            this.Id = t.TicketId;
            this.Version = t.Version;
            this.CreatedAt = t.CreatedAt;
            this.UpdatedAt = t.UpdatedAt;
            this.Deleted = t.Deleted;

            this.TaskId = t.Id;
            this.TrackId = t.Tracker?.Id;
            this.StatusId = t.Status?.Id;
            this.PriorityId = t.Priority?.Id;
            this.AssignedId = t.AssignedTo?.Id;
            this.DoneRate = t.DoneRate;
            this.AuthorId = t.Author?.Id;
        }
    }
    /// <summary>
    /// チケット駆動用
    /// </summary>
    public class Ticket : TaskItem
    {
        // チケットID
        public string TicketId { get; set; }
        // トラッカー
        public Tracker Tracker { get; set; }
        // ステータス
        public Status Status { get; set; }
        // 優先度
        public Priority Priority { get; set; }
        // 担当者
        public User AssignedTo { get; set; }
        // 進捗率
        public int DoneRate { get; set; }
        // 所有者
        public User Author { get; set; }

        public Ticket() { }
        // コンバート用
        public Ticket( TaskItem task, TicketItem ticket )
        {
            this.Id = task.Id;
            this.Version = task.Version;
            this.CreatedAt = task.CreatedAt;
            this.UpdatedAt = task.UpdatedAt;
            this.Deleted = task.Deleted;

            this.TaskNo = task.TaskNo;
            this.Title = task.Title;
            this.Desc = task.Desc;

            this.TicketId = ticket.Id;
            this.DoneRate = ticket.DoneRate;
            this.Tracker = new Tracker() { Id = ticket.TrackId };
            this.Status = new Status() { Id = ticket.StatusId};
            this.Priority = new Priority() { Id = ticket.PriorityId };
            this.AssignedTo = new User() { Id = ticket.AssignedId };
            this.Author = new User() { Id = ticket.AuthorId };
        }
    }

    /// <summary>
    /// WBS作成用
    /// </summary>
    [Table("WbsItems")]
    public class WbsItem : EntityData
    {
        // 連携タスクID
        public string TaskId { get; set; }
        // トラッカー
        public string TrackId { get; set; }
        // ステータス
        public string StatusId { get; set; }
        // 優先度
        public string PriorityId { get; set; }
        // 担当者
        public string AssignedId { get; set; }
        // 進捗率
        public int DoneRate { get; set; }
        // 所有者
        public string AuthorId { get; set; }
    }
    /// <summary>
    /// WBSビュー
    /// ※ TaskItem から継承するとエラーになるので EntityData から継承する
    /// </summary>
    [Table("WbsView")]
    public class WbsView : EntityData
    {
        // タスク番号（自前で振るため）
        public string TaskNo { get; set; }
        // タスクのタイトル
        public string Title { get; set; }
        // タスクの内容
        public string Desc { get; set; }
        // 予定時間
        public double? PlanTime { get; set; }
        // 実績時間
        public double? DoneTime { get; set; }

        public string Wbs_Id { get; set; }
        public string Tracker_Id { get; set; }
        public string Tracker_Name { get; set; }
        public string Status_Id { get; set; }
        public string Status_Name { get; set; }
        public string Priority_Id { get; set; }
        public string Priority_Name { get; set; }
        public string AssignedTo_Id { get; set; }
        public string AssignedTo_FirstName { get; set; }
        public string AssignedTo_LastName { get; set; }
        public string Author_Id { get; set; }
        public string Author_FirstName { get; set; }
        public string Author_LastName { get; set; }

        // トラッカー
        public Tracker Tracker { get; set; }
        // ステータス
        public Status Status { get; set; }
        // 優先度
        public Priority Priority { get; set; }
        // 担当者
        public User AssignedTo { get; set; }
        // 進捗率
        public int DoneRate { get; set; }
        // 所有者
        public User Author { get; set; }
    }



    /// <summary>
    /// トラッカー
    /// </summary>
    [Table("Trackers")]
    public class Tracker : EntityData
    {
        public string Name { get; set; }
        public int Position { get; set; }
    }
    /// <summary>
    /// ステータス
    /// </summary>
    [Table("Statuses")]
    public class Status : EntityData
    {
        public string Name { get; set; }
        public int Position { get; set; }
    }
    /// <summary>
    /// 優先度
    /// </summary>
    [Table("Priorities")]
    public class Priority : EntityData
    {
        public string Name { get; set; }
        public int Position { get; set; }
    }
    /// <summary>
    /// ユーザー
    /// </summary>
    [Table("Users")]
    public class User : EntityData
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set;  }
    }





    // 開始/終了日時のテーブル
    [Table("StartEndTimes")]
    public class StartEndTime : EntityData
    {
        // 対象のタスクID
        public string TaskId { get; set; }
        // 開始日時
        public DateTimeOffset? StartAt { get; set; }
        // 終了日時
        public DateTimeOffset? EndAt { get; set; }
        // 計画 or 実績
        public bool IsPlan { get; set; }
    }

    // タスクの親子関係
    [Table("TaskTrees")]
    public class TaskTree : EntityData
    {
        // 親タスクID
        public string ParentTaskId { get; set; }
        // 子タスクID
        public string ChildTaskId { get; set; }
    }

    // タスクの前後関係
    [Table("TaskPerts")]
    public class TaskPert : EntityData
    {
        // 前タスクID
        public string PreTaskId { get; set; }
        // 後タスクID
        public string PostTaskId { get; set; }
    }


    // ビューのテスト用
    [Table("ProjectItem")]
    public class ProjectItem
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public bool Completed { get; set; }
    }
    // ビューのテスト用
    [Table("ProjectView")]
    public class ProjectView
    {
        [Key]
        public int Id { get; set; }
        public string Project_Code { get; set; }
        public string Project_Name { get; set; }
    }


}
