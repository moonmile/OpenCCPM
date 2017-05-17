using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Openccpm.Web.Data;

namespace Openccpm.Web.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Openccpm.Web.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Openccpm.Web.Models.Priority", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("CreatedAt");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Name");

                    b.Property<int>("Position");

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Priorities");
                });

            modelBuilder.Entity("Openccpm.Web.Models.Project", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("CreatedAt");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("ProjectNo");

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Openccpm.Web.Models.ProjectUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProjectId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("ProjectUsers");
                });

            modelBuilder.Entity("Openccpm.Web.Models.ProjectUserView", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProjectId");

                    b.Property<string>("ProjectNo");

                    b.Property<string>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("ProjectUserView");
                });

            modelBuilder.Entity("Openccpm.Web.Models.StartEndTime", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("CreatedAt");

                    b.Property<bool>("Deleted");

                    b.Property<DateTimeOffset?>("EndAt");

                    b.Property<bool>("IsPlan");

                    b.Property<DateTimeOffset?>("StartAt");

                    b.Property<string>("TaskId");

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("StartEndTimes");
                });

            modelBuilder.Entity("Openccpm.Web.Models.Status", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("CreatedAt");

                    b.Property<bool>("Deleted");

                    b.Property<bool>("IsClosed");

                    b.Property<string>("Name");

                    b.Property<int>("Position");

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("Openccpm.Web.Models.TaskItem", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("CreatedAt");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<double?>("DoneTime");

                    b.Property<double?>("PlanTime");

                    b.Property<string>("ProjectId");

                    b.Property<string>("Subject");

                    b.Property<string>("TaskNo");

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("TaskItems");
                });

            modelBuilder.Entity("Openccpm.Web.Models.TaskPert", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("CreatedAt");

                    b.Property<bool>("Deleted");

                    b.Property<string>("PostTaskId");

                    b.Property<string>("PreTaskId");

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("TaskPerts");
                });

            modelBuilder.Entity("Openccpm.Web.Models.TaskTree", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ChildTaskId");

                    b.Property<DateTimeOffset?>("CreatedAt");

                    b.Property<bool>("Deleted");

                    b.Property<string>("ParentTaskId");

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("TaskTrees");
                });

            modelBuilder.Entity("Openccpm.Web.Models.TicketItem", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssignedToId");

                    b.Property<string>("AuthorId");

                    b.Property<DateTimeOffset?>("CreatedAt");

                    b.Property<bool>("Deleted");

                    b.Property<int>("DoneRate");

                    b.Property<DateTimeOffset?>("DueDate");

                    b.Property<string>("PriorityId");

                    b.Property<DateTimeOffset?>("StartDate");

                    b.Property<string>("StatusId");

                    b.Property<string>("TaskId");

                    b.Property<string>("TrackerId");

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("TicketItems");
                });

            modelBuilder.Entity("Openccpm.Web.Models.TicketView", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssignedToId");

                    b.Property<string>("AssignedTo_Id");

                    b.Property<string>("AssignedTo_UserName");

                    b.Property<string>("AuthorId");

                    b.Property<string>("Author_Id");

                    b.Property<string>("Author_UserName");

                    b.Property<DateTimeOffset?>("CreatedAt");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<int>("DoneRate");

                    b.Property<double?>("DoneTime");

                    b.Property<DateTimeOffset?>("DueDate");

                    b.Property<double?>("PlanTime");

                    b.Property<string>("PriorityId");

                    b.Property<string>("Priority_Id");

                    b.Property<string>("Priority_Name");

                    b.Property<string>("ProjectId");

                    b.Property<string>("Project_Name");

                    b.Property<string>("Project_ProjectNo");

                    b.Property<DateTimeOffset?>("StartDate");

                    b.Property<string>("StatusId");

                    b.Property<string>("Status_Id");

                    b.Property<bool?>("Status_IsClosed");

                    b.Property<string>("Status_Name");

                    b.Property<string>("Subject");

                    b.Property<string>("TaskNo");

                    b.Property<string>("Ticket_Id");

                    b.Property<byte[]>("Ticket_Version");

                    b.Property<string>("TrackerId");

                    b.Property<string>("Tracker_Id");

                    b.Property<string>("Tracker_Name");

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("AssignedToId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("StatusId");

                    b.HasIndex("TrackerId");

                    b.ToTable("TicketView");
                });

            modelBuilder.Entity("Openccpm.Web.Models.Tracker", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("CreatedAt");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Name");

                    b.Property<int>("Position");

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Trackers");
                });

            modelBuilder.Entity("Openccpm.Web.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("UserView");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Openccpm.Web.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Openccpm.Web.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Openccpm.Web.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Openccpm.Web.Models.TicketView", b =>
                {
                    b.HasOne("Openccpm.Web.Models.User", "AssignedTo")
                        .WithMany()
                        .HasForeignKey("AssignedToId");

                    b.HasOne("Openccpm.Web.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("Openccpm.Web.Models.Priority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId");

                    b.HasOne("Openccpm.Web.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.HasOne("Openccpm.Web.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.HasOne("Openccpm.Web.Models.Tracker", "Tracker")
                        .WithMany()
                        .HasForeignKey("TrackerId");
                });
        }
    }
}
