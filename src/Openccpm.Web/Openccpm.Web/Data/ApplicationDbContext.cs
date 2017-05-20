using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Openccpm.Web.Models;

namespace Openccpm.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            initView();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Openccpm.Lib.Models.TicketItem> TicketItem { get; set; }
        public DbSet<Openccpm.Lib.Models.Tracker> Tracker { get; set; }
        public DbSet<Openccpm.Lib.Models.Status> Status { get; set; }
        public DbSet<Openccpm.Lib.Models.Priority> Priority { get; set; }
        public DbSet<Openccpm.Lib.Models.Project> Project { get; set; }
        public DbSet<Openccpm.Lib.Models.ProjectUser> ProjectUser { get; set; }


        // 利便性のためにビューを作る
        public IQueryable<Openccpm.Lib.Models.ProjectUserView> ProjectUserView { get; set; }
        public IQueryable<Openccpm.Lib.Models.TicketView> TicketView { get; set; }
        public IQueryable<Openccpm.Lib.Models.User> User { get; set; }

        private void initView()
        {
            ProjectUserView =
                from pu in ProjectUser
                join p in Project on pu.ProjectId equals p.Id
                join u in Users on pu.UserId equals u.Id
                select new Openccpm.Lib.Models.ProjectUserView()
                {
                    ProjectId = p.Id,
                    ProjectNo = p.ProjectNo,
                    ProjectName = p.Name,
                    UserId = u.Id,
                    UserName = u.UserName
                };

            TicketView =
                from tk in TicketItem
                join p in Project on tk.ProjectId equals p.Id
                join tr in Tracker on tk.TrackerId equals tr.Id
                join pr in Priority on tk.PriorityId equals pr.Id
                join st in Status on tk.StatusId equals st.Id

                join assign in Users on tk.AssignedToId equals assign.Id into sub1 from assign2 in sub1.DefaultIfEmpty()
                join author in Users on tk.AuthorId     equals author.Id into sub2 from author2 in sub2.DefaultIfEmpty()
                where tk.Deleted == false 
                select new Openccpm.Lib.Models.TicketView()
                {
                    Id = tk.Id,
                    CreatedAt = tk.CreatedAt,
                    UpdatedAt = tk.UpdatedAt,
                    Version = tk.Version,
                    TicketNo = tk.TicketNo,
                    Subject = tk.Subject,
                    Description = tk.Description,
                    PlanTime = tk.PlanTime,
                    DoneTime = tk.DoneTime,
                    DoneRate = tk.DoneRate,
                    StartDate = tk.StartDate,
                    DueDate = tk.DueDate,
                    ProjectId = p.Id,
                    TrackerId = tr.Id,
                    PriorityId = pr.Id,
                    StatusId = st.Id,
                    AssignedToId = assign2 == null? null: assign2.Id,
                    AuthorId = author2 == null? null: author2.Id,

                    Project = new Lib.Models.Project() { Id = p.Id, ProjectNo = p.ProjectNo, Name = p.Name, Description = p.Description },
                    Tracker = new Lib.Models.Tracker() {  Id = tr.Id, Name = tr.Name },
                    Priority = new Lib.Models.Priority() { Id = pr.Id, Name = pr.Name },
                    Status = new Lib.Models.Status() { Id = st.Id, Name = st.Name, IsClosed = st.IsClosed },
                    AssignedTo = assign2 == null? new Lib.Models.User(): new Lib.Models.User() { Id = assign2.Id, UserName = assign2.UserName, Email = assign2.Email },
                    Author     = author2 == null? new Lib.Models.User(): new Lib.Models.User() { Id = author2.Id, UserName = author2.UserName, Email = author2.Email }
                };

            // 直接 Users を扱うのは面倒なので User を再定義する
            User =
                from u in Users
                select new Openccpm.Lib.Models.User()
                {
                    Id = u.Id,
                    Email = u.Email,
                    UserName = u.UserName
                };
        }
    }
}
