﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class openccpm_dbEntities : DbContext
    {
        public openccpm_dbEntities()
            : base("name=openccpm_dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<StartEndTimes> StartEndTimes { get; set; }
        public virtual DbSet<TaskPerts> TaskPerts { get; set; }
        public virtual DbSet<TaskTrees> TaskTrees { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Priorities> Priorities { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
        public virtual DbSet<Trackers> Trackers { get; set; }
        public virtual DbSet<WbsItems> WbsItems { get; set; }
        public virtual DbSet<TicketViews> TicketView { get; set; }
        public virtual DbSet<TaskItems> TaskItems { get; set; }
        public virtual DbSet<TicketItems> TicketItems { get; set; }
        public virtual DbSet<WbsViews> WbsView { get; set; }
    }
}
