using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Openccpm.Web.Data.Migrations
{
    public partial class openccpm_start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskItem",
                table: "TaskItem");

            migrationBuilder.RenameTable(
                name: "TaskItem",
                newName: "TaskItems");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "TaskItems",
                newName: "TaskNo");

            migrationBuilder.RenameColumn(
                name: "Desc",
                table: "TaskItems",
                newName: "Subject");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TaskItems",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DoneTime",
                table: "TaskItems",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PlanTime",
                table: "TaskItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectId",
                table: "TaskItems",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskItems",
                table: "TaskItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProjectNo = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ProjectId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUserView",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ProjectId = table.Column<string>(nullable: true),
                    ProjectNo = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUserView", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StartEndTimes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    EndAt = table.Column<DateTimeOffset>(nullable: true),
                    IsPlan = table.Column<bool>(nullable: false),
                    StartAt = table.Column<DateTimeOffset>(nullable: true),
                    TaskId = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartEndTimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    IsClosed = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskPerts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    PostTaskId = table.Column<string>(nullable: true),
                    PreTaskId = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskPerts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskTrees",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ChildTaskId = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    ParentTaskId = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketItems",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AssignedToId = table.Column<string>(nullable: true),
                    AuthorId = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    DoneRate = table.Column<int>(nullable: false),
                    DueDate = table.Column<DateTimeOffset>(nullable: true),
                    PriorityId = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTimeOffset>(nullable: true),
                    StatusId = table.Column<string>(nullable: true),
                    TaskId = table.Column<string>(nullable: true),
                    TrackerId = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trackers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trackers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserView",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserView", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketView",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AssignedToId = table.Column<string>(nullable: true),
                    AssignedTo_Id = table.Column<string>(nullable: true),
                    AssignedTo_UserName = table.Column<string>(nullable: true),
                    AuthorId = table.Column<string>(nullable: true),
                    Author_Id = table.Column<string>(nullable: true),
                    Author_UserName = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DoneRate = table.Column<int>(nullable: false),
                    DoneTime = table.Column<double>(nullable: true),
                    DueDate = table.Column<DateTimeOffset>(nullable: true),
                    PlanTime = table.Column<double>(nullable: true),
                    PriorityId = table.Column<string>(nullable: true),
                    Priority_Id = table.Column<string>(nullable: true),
                    Priority_Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<string>(nullable: true),
                    Project_Name = table.Column<string>(nullable: true),
                    Project_ProjectNo = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTimeOffset>(nullable: true),
                    StatusId = table.Column<string>(nullable: true),
                    Status_Id = table.Column<string>(nullable: true),
                    Status_IsClosed = table.Column<bool>(nullable: true),
                    Status_Name = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    TaskNo = table.Column<string>(nullable: true),
                    Ticket_Id = table.Column<string>(nullable: true),
                    Ticket_Version = table.Column<byte[]>(nullable: true),
                    TrackerId = table.Column<string>(nullable: true),
                    Tracker_Id = table.Column<string>(nullable: true),
                    Tracker_Name = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketView", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketView_UserView_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "UserView",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketView_UserView_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "UserView",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketView_Priorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketView_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketView_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketView_Trackers_TrackerId",
                        column: x => x.TrackerId,
                        principalTable: "Trackers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketView_AssignedToId",
                table: "TicketView",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketView_AuthorId",
                table: "TicketView",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketView_PriorityId",
                table: "TicketView",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketView_ProjectId",
                table: "TicketView",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketView_StatusId",
                table: "TicketView",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketView_TrackerId",
                table: "TicketView",
                column: "TrackerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectUsers");

            migrationBuilder.DropTable(
                name: "ProjectUserView");

            migrationBuilder.DropTable(
                name: "StartEndTimes");

            migrationBuilder.DropTable(
                name: "TaskPerts");

            migrationBuilder.DropTable(
                name: "TaskTrees");

            migrationBuilder.DropTable(
                name: "TicketItems");

            migrationBuilder.DropTable(
                name: "TicketView");

            migrationBuilder.DropTable(
                name: "UserView");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Trackers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskItems",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "DoneTime",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "PlanTime",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "TaskItems");

            migrationBuilder.RenameTable(
                name: "TaskItems",
                newName: "TaskItem");

            migrationBuilder.RenameColumn(
                name: "TaskNo",
                table: "TaskItem",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "TaskItem",
                newName: "Desc");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskItem",
                table: "TaskItem",
                column: "Id");
        }
    }
}
