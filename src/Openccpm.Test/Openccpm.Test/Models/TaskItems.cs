//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはテンプレートから生成されました。
//
//     このファイルを手動で変更すると、アプリケーションで予期しない動作が発生する可能性があります。
//     このファイルに対する手動の変更は、コードが再生成されると上書きされます。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Openccpm.Test.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TaskItems
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaskItems()
        {
            this.StartEndTimes = new HashSet<StartEndTimes>();
            this.TaskPerts = new HashSet<TaskPerts>();
            this.TaskPerts1 = new HashSet<TaskPerts>();
            this.TaskTrees = new HashSet<TaskTrees>();
            this.TaskTrees1 = new HashSet<TaskTrees>();
            this.WbsItems = new HashSet<WbsItems>();
            this.TicketItems = new HashSet<TicketItems>();
        }
    
        public string Id { get; set; }
        public System.DateTimeOffset CreatedAt { get; set; }
        public bool Deleted { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public Nullable<System.DateTimeOffset> UpdatedAt { get; set; }
        public byte[] Version { get; set; }
        public string TaskNo { get; set; }
        public Nullable<double> PlanTime { get; set; }
        public Nullable<double> DoneTime { get; set; }
        public string ProjectId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StartEndTimes> StartEndTimes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskPerts> TaskPerts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskPerts> TaskPerts1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskTrees> TaskTrees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskTrees> TaskTrees1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WbsItems> WbsItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketItems> TicketItems { get; set; }
    }
}
