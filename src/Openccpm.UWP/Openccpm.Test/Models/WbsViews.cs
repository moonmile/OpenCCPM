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
    
    public partial class WbsViews
    {
        public string Id { get; set; }
        public System.DateTimeOffset CreatedAt { get; set; }
        public bool Deleted { get; set; }
        public string Desc { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTimeOffset> UpdatedAt { get; set; }
        public byte[] Version { get; set; }
        public string TaskNo { get; set; }
        public Nullable<double> PlanTime { get; set; }
        public Nullable<double> DoneTime { get; set; }
        public string Wbs_Id { get; set; }
        public string Tracker_Name { get; set; }
        public string Tracker_Id { get; set; }
        public string Status_Name { get; set; }
        public string Status_Id { get; set; }
        public string Priority_Name { get; set; }
        public string Priority_Id { get; set; }
        public string AssignedTo_FirstName { get; set; }
        public string AssignedTo_LastName { get; set; }
        public int DoneRate { get; set; }
        public string Author_FirstName { get; set; }
        public string AssignedTo_Id { get; set; }
        public string Author_Id { get; set; }
        public string Author_LastName { get; set; }
    }
}
