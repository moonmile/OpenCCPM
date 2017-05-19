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

        public DbSet<Openccpm.Lib.Models.TaskItem> TaskItem { get; set; }
        public DbSet<Openccpm.Lib.Models.TicketItem> TicketItem { get; set; }
        public DbSet<Openccpm.Lib.Models.TicketView> TicketView { get; set; }


        public DbSet<Openccpm.Lib.Models.StartEndTime> StartEndTime { get; set; }
        public DbSet<Openccpm.Lib.Models.TaskTree> TaskTree { get; set; }
        public DbSet<Openccpm.Lib.Models.TaskPert> TaskPert { get; set; }

        public DbSet<Openccpm.Lib.Models.Tracker> Tracker { get; set; }
        public DbSet<Openccpm.Lib.Models.Status> Status { get; set; }
        public DbSet<Openccpm.Lib.Models.Priority> Priority { get; set; }

        public DbSet<Openccpm.Lib.Models.Project> Project { get; set; }
        public DbSet<Openccpm.Lib.Models.ProjectUser> ProjectUser { get; set; }


        // 利便性のためにビューを作る
        public IQueryable<Openccpm.Lib.Models.ProjectUserView> ProjectUserView { get; set; }
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
