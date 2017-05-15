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
        [Display(Name = "作成日時")]
        public DateTimeOffset? CreatedAt { get; set; }
        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "更新日時")]
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
        public string Subject { get; set; }
        // タスクの内容
        public string Description { get; set; }
        // 予定時間
        public double? PlanTime { get; set; }
        // 実績時間
        public double? DoneTime { get; set; }
        // プロジェクトID
        public string ProjectId { get; set; }
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
        public string TrackerId { get; set; }
        // ステータス
        public string StatusId { get; set; }
        // 優先度
        public string PriorityId { get; set; }
        // 担当者
        public string AssignedToId { get; set; }
        // 進捗率
        public int DoneRate { get; set; }
        // 所有者
        public string AuthorId { get; set; }
        // 開始日と期日
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? DueDate { get; set; }

    }
    /// <summary>
    /// チケット駆動用
    /// </summary>
    public class TicketView : EntityData
    {
        // タスク番号（自前で振るため）
        [Display(Name = "チケット番号")]
        public string TaskNo { get; set; }
        // タスクのタイトル
        [Display(Name = "題名")]
        public string Subject { get; set; }
        // タスクの内容
        [Display(Name = "説明")]
        public string Description { get; set; }
        // 予定時間
        [Display(Name = "予定時間")]
        public double? PlanTime { get; set; }
        // 実績時間
        [Display(Name = "実績時間")]
        public double? DoneTime { get; set; }
        // プロジェクトID
        public string ProjectId { get; set; }

        // チケットID
        public string Ticket_Id { get; set; }
        public byte[] Ticket_Version { get; set; }

        [Display(Name = "トラッカー")]
        public string Tracker_Id { get; set; }
        [Display(Name = "トラッカー")]
        public string Tracker_Name { get; set; }
        [Display(Name = "ステータス")]
        public string Status_Id { get; set; }
        [Display(Name = "ステータス")]
        public string Status_Name { get; set; }
        public bool? Status_IsClosed { get; set; }
        [Display(Name = "優先度")]
        public string Priority_Id { get; set; }
        [Display(Name = "優先度")]
        public string Priority_Name { get; set; }
        [Display(Name = "担当者")]
        public string AssignedTo_Id { get; set; }
        [Display(Name = "担当者")]
        public string AssignedTo_UserName { get; set; }
        [Display(Name = "作成者")]
        public string Author_Id { get; set; }
        [Display(Name = "作成者")]
        public string Author_UserName { get; set; }
        public string Project_Name { get; set; }
        public string Project_ProjectNo { get; set; }

        // 開始日と期日
        [Display(Name = "開始日")]
        public DateTimeOffset? StartDate { get; set; }
        [Display(Name = "期日")]
        public DateTimeOffset? DueDate { get; set; }

        // トラッカー
        public Tracker Tracker { get; set; }
        // ステータス
        public Status Status { get; set; }
        // 優先度
        public Priority Priority { get; set; }
        // 担当者
        public User AssignedTo { get; set; }
        // 進捗率
        [Display(Name = "進捗率")]
        public int DoneRate { get; set; }
        // 所有者
        public User Author { get; set; }
        // プロジェクト
        public Project Project { get; set; }

        public TicketView() { }
        // コンバーター
        public TicketView( TaskItem task, TicketItem ticket )
        {
            this.Id = task.Id;
            this.Version = task.Version;
            this.CreatedAt = task.CreatedAt;
            this.UpdatedAt = task.UpdatedAt;
            this.Deleted = task.Deleted;

            this.TaskNo = task.TaskNo;
            this.Subject = task.Subject;
            this.Description = task.Description;
            this.PlanTime = task.PlanTime;
            this.DoneTime = task.DoneTime;
            this.ProjectId = task.ProjectId;

            this.Ticket_Id = ticket.Id;
            this.Ticket_Version = ticket.Version;
            this.DoneRate = ticket.DoneRate;
            this.StartDate = ticket.StartDate;
            this.DueDate = ticket.DueDate;
            this.Tracker = new Tracker() { Id = ticket.TrackerId };
            this.Status = new Status() { Id = ticket.StatusId};
            this.Priority = new Priority() { Id = ticket.PriorityId };
            this.AssignedTo = new User() { Id = ticket.AssignedToId };
            this.Author = new User() { Id = ticket.AuthorId };
            this.Project = new Project() { Id = task.ProjectId };
        }
        public static explicit operator TicketItem(TicketView src )
        {
            var dest = new TicketItem()
            {
                Id = src.Ticket_Id,
                Version = src.Ticket_Version,
                CreatedAt = src.CreatedAt,
                UpdatedAt = src.UpdatedAt,
                Deleted = src.Deleted,

                TaskId = src.Id,
                DoneRate = src.DoneRate,
                StartDate = src.StartDate,
                DueDate = src.DueDate,

                TrackerId = src.Tracker == null? src.Tracker_Id:  src.Tracker.Id,
                StatusId =  src.Status  == null? src.Status_Id: src.Status.Id,
                PriorityId = src.Priority == null? src.Priority_Id: src.Priority.Id,
                AssignedToId = src.AssignedTo == null? src.AssignedTo_Id: src.AssignedTo.Id,
                AuthorId = src.Author == null? src.Author_Id: src.Author.Id
            };
            return dest;
        }
        public static explicit operator TaskItem(TicketView src )
        {
            var dest = new TaskItem()
            {
                Id = src.Id,
                Version = src.Version,
                CreatedAt = src.CreatedAt,
                UpdatedAt = src.UpdatedAt,
                Deleted = src.Deleted,

                TaskNo = src.TaskNo,
                Subject = src.Subject,
                Description = src.Description,
                PlanTime = src.PlanTime,
                DoneTime = src.DoneTime,
                ProjectId = src.ProjectId,
            };
            return dest;
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
        public string Subject { get; set; }
        // タスクの内容
        public string Description { get; set; }
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
        public bool IsClosed { get; set; }
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
    /// ユーザー情報
    /// </summary>
    [Table("UserView")]
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public string Name {  get { return UserName; } }
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


    // プロジェクト
    [Table("Projects")]
    public class Project : EntityData
    {
        [Display(Name = "プロジェクト番号")]
        public string ProjectNo { get; set; }
        [Display(Name = "プロジェクト名")]
        public string Name { get; set; }
        [Display(Name = "説明")]
        public string Description { get; set; }
    }
}
