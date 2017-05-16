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
        public DbSet<Openccpm.Lib.Models.User> User { get; set; }

        public DbSet<Openccpm.Lib.Models.Project> Project { get; set; }
        public DbSet<Openccpm.Lib.Models.ProjectUser> ProjectUser { get; set; }
        public DbSet<Openccpm.Lib.Models.ProjectUserView> ProjectUserView { get; set; }
    }
}
