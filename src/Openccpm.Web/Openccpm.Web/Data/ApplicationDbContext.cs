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

        public DbSet<Openccpm.Web.Models.TaskItem> TaskItem { get; set; }
        public DbSet<Openccpm.Web.Models.TicketItem> TicketItem { get; set; }
        public DbSet<Openccpm.Web.Models.TicketView> TicketView { get; set; }

        public DbSet<Openccpm.Web.Models.WbsItem> WbsItem { get; set; }
        public DbSet<Openccpm.Web.Models.WbsView> WbsView { get; set; }


        public DbSet<Openccpm.Web.Models.StartEndTime> StartEndTime { get; set; }
        public DbSet<Openccpm.Web.Models.TaskTree> TaskTree { get; set; }
        public DbSet<Openccpm.Web.Models.TaskPert> TaskPert { get; set; }

        public DbSet<Openccpm.Web.Models.Tracker> Tracker { get; set; }
        public DbSet<Openccpm.Web.Models.Status> Status { get; set; }
        public DbSet<Openccpm.Web.Models.Priority> Priority { get; set; }
        public DbSet<Openccpm.Web.Models.User> User { get; set; }

        public DbSet<Openccpm.Web.Models.ProjectItem> ProjectItem { get; set; }
        public DbSet<Openccpm.Web.Models.ProjectView> ProjectView { get; set; }

    }
}
