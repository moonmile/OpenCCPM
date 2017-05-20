using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Openccpm.Lib.Models
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
    /// チケット駆動用
    /// </summary>
    [Table("TicketItems")]
    public class TicketItem : EntityData
    {
        // チケット番号（自前で振るため）
        [Display(Name = "チケット番号")]
        public string TicketNo { get; set; }
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
        // 進捗率
        public int DoneRate { get; set; }
        // 開始日と期日
        [Display(Name = "開始日")]
        public DateTimeOffset? StartDate { get; set; }
        [Display(Name = "期日")]
        public DateTimeOffset? DueDate { get; set; }

        // リレーション用
        // プロジェクトID
        [Display(Name = "プロジェクト")]
        public string ProjectId { get; set; }
        // トラッカー
        [Display(Name = "トラッカー")]
        public string TrackerId { get; set; }
        // トラッカー
        [Display(Name = "優先度")]
        public string PriorityId { get; set; }
        // ステータス
        [Display(Name = "状態")]
        public string StatusId { get; set; }
        // 担当者
        [Display(Name = "担当者")]
        public string AssignedToId { get; set; }
        // 所有者
        [Display(Name = "所有者")]
        public string AuthorId { get; set; }

    }

    [NotMapped]
    public class TicketView : TicketItem
    {
        [Display(Name = "プロジェクト番号")]
        public string Project_ProjectNo { get { return Project?.ProjectNo; } }
        [Display(Name = "プロジェクト名")]
        public string Project_Name { get { return Project?.Name; } }

        // トラッカー
        [Display(Name = "トラッカー")]
        public string Tracker_Name { get { return Tracker?.Name; } }
        // トラッカー
        [Display(Name = "優先度")]
        public string Priority_Name { get { return Priority?.Name; } }
        // ステータス
        [Display(Name = "状態")]
        public string Status_Name { get { return Status?.Name; } }
        [Display(Name = "状態")]
        public bool? Status_IsClosed { get { return Status?.IsClosed; } }
        // 担当者
        [Display(Name = "担当者")]
        public string AssignedTo_UserName { get { return AssignedTo?.UserName; } }
        // 所有者
        [Display(Name = "所有者")]
        public string Author_UserName { get { return Author?.UserName; } }

        // トラッカー
        public Tracker Tracker { get; set; }
        // ステータス
        public Status Status { get; set; }
        // 優先度
        public Priority Priority { get; set; }
        // 担当者
        public User AssignedTo { get; set; }
        // 所有者
        public User Author { get; set; }
        // プロジェクト
        public Project Project { get; set; }
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
    [NotMapped]
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public string Name {  get { return UserName; } }
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
    // プロジェクトに含まれるユーザー
    [Table("ProjectUsers")]
    public class ProjectUser
    {
        [Key]
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string UserId { get; set; }
    }

    [NotMapped]
    public class ProjectUserView
    {
        public string ProjectId { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }

    public class LoginParameter
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
    }
}
