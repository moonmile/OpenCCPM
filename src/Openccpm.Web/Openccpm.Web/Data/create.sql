EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'WbsView'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'WbsView'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'WbsView'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserView'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserView'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserRoleView'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserRoleView'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TicketView'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TicketView'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TicketView'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProjectUserView'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProjectUserView'

GO
ALTER TABLE [dbo].[WbsItems] DROP CONSTRAINT [FK_WbsItems_TaskItems]
GO
ALTER TABLE [dbo].[TicketItems] DROP CONSTRAINT [FK_Tickets_Trackers]
GO
ALTER TABLE [dbo].[TicketItems] DROP CONSTRAINT [FK_Tickets_TaskItems]
GO
ALTER TABLE [dbo].[TicketItems] DROP CONSTRAINT [FK_Tickets_Statuses]
GO
ALTER TABLE [dbo].[TicketItems] DROP CONSTRAINT [FK_Tickets_Priorities]
GO
ALTER TABLE [dbo].[TaskTrees] DROP CONSTRAINT [FK_TaskTrees_TaskItems1]
GO
ALTER TABLE [dbo].[TaskTrees] DROP CONSTRAINT [FK_TaskTrees_TaskItems]
GO
ALTER TABLE [dbo].[TaskPerts] DROP CONSTRAINT [FK_TaskPerts_TaskItems1]
GO
ALTER TABLE [dbo].[TaskPerts] DROP CONSTRAINT [FK_TaskPerts_TaskItems]
GO
ALTER TABLE [dbo].[StartEndTimes] DROP CONSTRAINT [FK_StartEndTimes_TaskItems1]
GO
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims] DROP CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[WbsItems] DROP CONSTRAINT [DF__WbsItems__Create__5BE2A6F2]
GO
ALTER TABLE [dbo].[WbsItems] DROP CONSTRAINT [DF__WbsItems__Id__5AEE82B9]
GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF__Users__CreatedAt__3E52440B]
GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF__Users__Id__3D5E1FD2]
GO
ALTER TABLE [dbo].[Trackers] DROP CONSTRAINT [DF__Trackers__Create__38996AB5]
GO
ALTER TABLE [dbo].[Trackers] DROP CONSTRAINT [DF__Trackers__Id__37A5467C]
GO
ALTER TABLE [dbo].[TicketItems] DROP CONSTRAINT [DF__Tickets__Created__440B1D61]
GO
ALTER TABLE [dbo].[TicketItems] DROP CONSTRAINT [DF__Tickets__Id__4316F928]
GO
ALTER TABLE [dbo].[TaskTrees] DROP CONSTRAINT [DF__TaskTrees__Creat__2E1BDC42]
GO
ALTER TABLE [dbo].[TaskTrees] DROP CONSTRAINT [DF__TaskTrees__Id__2D27B809]
GO
ALTER TABLE [dbo].[TaskPerts] DROP CONSTRAINT [DF__TaskPerts__Creat__286302EC]
GO
ALTER TABLE [dbo].[TaskPerts] DROP CONSTRAINT [DF__TaskPerts__Id__276EDEB3]
GO
ALTER TABLE [dbo].[TaskItems] DROP CONSTRAINT [DF__TaskItems__Creat__24927208]
GO
ALTER TABLE [dbo].[TaskItems] DROP CONSTRAINT [DF__TaskItems__Id__239E4DCF]
GO
ALTER TABLE [dbo].[Statuses] DROP CONSTRAINT [DF__Statuses__Create__412EB0B6]
GO
ALTER TABLE [dbo].[Statuses] DROP CONSTRAINT [DF__Statuses__Id__403A8C7D]
GO
ALTER TABLE [dbo].[StartEndTimes] DROP CONSTRAINT [DF__StartEndT__Creat__49C3F6B7]
GO
ALTER TABLE [dbo].[StartEndTimes] DROP CONSTRAINT [DF__StartEndTime__Id__48CFD27E]
GO
ALTER TABLE [dbo].[ProjectUsers] DROP CONSTRAINT [DF__ProjectUsers__Id__2DE6D218]
GO
ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [DF__Projects__Create__01142BA1]
GO
ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [DF__Projects__Id__00200768]
GO
ALTER TABLE [dbo].[Priorities] DROP CONSTRAINT [DF__Prioritie__Creat__3B75D760]
GO
ALTER TABLE [dbo].[Priorities] DROP CONSTRAINT [DF__Priorities__Id__3A81B327]
GO
/****** Object:  Table [dbo].[TaskTrees]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[TaskTrees]
GO
/****** Object:  Table [dbo].[TaskPerts]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[TaskPerts]
GO
/****** Object:  Table [dbo].[StartEndTimes]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[StartEndTimes]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[AspNetUserTokens]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[AspNetUserClaims]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[AspNetRoleClaims]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[__EFMigrationsHistory]
GO
/****** Object:  View [dbo].[TicketView]    Script Date: 2017/05/15 23:19:50 ******/
DROP VIEW [dbo].[TicketView]
GO
/****** Object:  Table [dbo].[TicketItems]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[TicketItems]
GO
/****** Object:  View [dbo].[WbsView]    Script Date: 2017/05/15 23:19:50 ******/
DROP VIEW [dbo].[WbsView]
GO
/****** Object:  Table [dbo].[WbsItems]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[WbsItems]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Priorities]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[Priorities]
GO
/****** Object:  Table [dbo].[Trackers]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[Trackers]
GO
/****** Object:  Table [dbo].[TaskItems]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[TaskItems]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[Statuses]
GO
/****** Object:  View [dbo].[ProjectUserView]    Script Date: 2017/05/15 23:19:50 ******/
DROP VIEW [dbo].[ProjectUserView]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[Projects]
GO
/****** Object:  Table [dbo].[ProjectUsers]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[ProjectUsers]
GO
/****** Object:  View [dbo].[UserRoleView]    Script Date: 2017/05/15 23:19:50 ******/
DROP VIEW [dbo].[UserRoleView]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[AspNetUserRoles]
GO
/****** Object:  View [dbo].[UserView]    Script Date: 2017/05/15 23:19:50 ******/
DROP VIEW [dbo].[UserView]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 2017/05/15 23:19:50 ******/
DROP TABLE [dbo].[AspNetUsers]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [dbo].[UserView]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UserView]
AS
SELECT                      Id, Email, UserName
FROM                         dbo.AspNetUsers

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [dbo].[UserRoleView]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UserRoleView]
AS
SELECT                      dbo.AspNetUsers.Id, dbo.AspNetUsers.Email, dbo.AspNetUsers.UserName, dbo.AspNetRoles.Id AS Role_Id, 
                                      dbo.AspNetRoles.Name AS Role_Name
FROM                         dbo.AspNetUsers INNER JOIN
                                      dbo.AspNetUserRoles ON dbo.AspNetUsers.Id = dbo.AspNetUserRoles.UserId INNER JOIN
                                      dbo.AspNetRoles ON dbo.AspNetUserRoles.RoleId = dbo.AspNetRoles.Id

GO
/****** Object:  Table [dbo].[ProjectUsers]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectUsers](
	[Id] [nvarchar](128) NOT NULL,
	[ProjectId] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_ProjectUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Projects]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [nvarchar](128) NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[Deleted] [bit] NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NULL,
	[Version] [timestamp] NOT NULL,
	[ProjectNo] [nvarchar](128) NULL,
	[Name] [nvarchar](256) NULL,
	[Description] [nvarchar](1024) NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[ProjectUserView]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ProjectUserView]
AS
SELECT                      dbo.ProjectUsers.ProjectId, dbo.ProjectUsers.UserId, dbo.AspNetUsers.UserName, dbo.ProjectUsers.Id, 
                                      dbo.Projects.ProjectNo
FROM                         dbo.AspNetUsers INNER JOIN
                                      dbo.ProjectUsers ON dbo.AspNetUsers.Id = dbo.ProjectUsers.UserId INNER JOIN
                                      dbo.Projects ON dbo.ProjectUsers.ProjectId = dbo.Projects.Id

GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[Id] [nvarchar](128) NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Position] [int] NOT NULL,
	[IsClosed] [bit] NOT NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaskItems]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskItems](
	[Id] [nvarchar](128) NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[Deleted] [bit] NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[Subject] [nvarchar](256) NULL,
	[UpdatedAt] [datetimeoffset](7) NULL,
	[Version] [timestamp] NOT NULL,
	[TaskNo] [nvarchar](128) NULL,
	[PlanTime] [float] NULL,
	[DoneTime] [float] NULL,
	[ProjectId] [nvarchar](128) NULL,
 CONSTRAINT [PK_TaskItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Trackers]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trackers](
	[Id] [nvarchar](128) NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Position] [int] NOT NULL,
 CONSTRAINT [PK_Trackers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Priorities]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Priorities](
	[Id] [nvarchar](128) NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Position] [int] NOT NULL,
 CONSTRAINT [PK_Priorities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](128) NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
	[Login] [nvarchar](128) NOT NULL,
	[FirstName] [nvarchar](128) NOT NULL,
	[LastName] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WbsItems]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WbsItems](
	[Id] [nvarchar](128) NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
	[TaskId] [nvarchar](128) NOT NULL,
	[TrackId] [nvarchar](128) NULL,
	[StatusId] [nvarchar](128) NULL,
	[PriorityId] [nvarchar](128) NULL,
	[AssignedId] [nvarchar](128) NULL,
	[DoneRate] [int] NOT NULL,
	[AuthorId] [nvarchar](128) NULL,
 CONSTRAINT [PK_WbsItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[WbsView]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[WbsView]
AS
SELECT                      dbo.TaskItems.Id, dbo.TaskItems.CreatedAt, dbo.TaskItems.Deleted, dbo.TaskItems.UpdatedAt, dbo.TaskItems.Version, 
                                      dbo.TaskItems.TaskNo, dbo.TaskItems.PlanTime, dbo.TaskItems.DoneTime, dbo.WbsItems.Id AS Wbs_Id, 
                                      dbo.Trackers.Id AS Tracker_Id, dbo.Trackers.Name AS Tracker_Name, dbo.Statuses.Id AS Status_Id, 
                                      dbo.Statuses.Name AS Status_Name, dbo.Priorities.Id AS Priority_Id, dbo.Priorities.Name AS Priority_Name, 
                                      Users_1.Id AS AssignedTo_Id, Users_1.FirstName AS AssignedTo_FirstName, 
                                      Users_1.LastName AS AssignedTo_LastName, dbo.Users.Id AS Author_Id, dbo.Users.FirstName AS Author_FirstName, 
                                      dbo.Users.LastName AS Author_LastName, dbo.TaskItems.Description, dbo.TaskItems.Subject
FROM                         dbo.TaskItems INNER JOIN
                                      dbo.WbsItems ON dbo.TaskItems.Id = dbo.WbsItems.TaskId INNER JOIN
                                      dbo.Users ON dbo.WbsItems.AuthorId = dbo.Users.Id LEFT OUTER JOIN
                                      dbo.Users AS Users_1 ON dbo.WbsItems.AssignedId = Users_1.Id LEFT OUTER JOIN
                                      dbo.Priorities ON dbo.WbsItems.PriorityId = dbo.Priorities.Id LEFT OUTER JOIN
                                      dbo.Statuses ON dbo.WbsItems.StatusId = dbo.Statuses.Id LEFT OUTER JOIN
                                      dbo.Trackers ON dbo.WbsItems.TaskId = dbo.Trackers.Id

GO
/****** Object:  Table [dbo].[TicketItems]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketItems](
	[Id] [nvarchar](128) NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
	[TaskId] [nvarchar](128) NOT NULL,
	[TrackerId] [nvarchar](128) NULL,
	[StatusId] [nvarchar](128) NULL,
	[PriorityId] [nvarchar](128) NULL,
	[AssignedToId] [nvarchar](128) NULL,
	[DoneRate] [int] NOT NULL,
	[AuthorId] [nvarchar](128) NULL,
	[DueDate] [datetimeoffset](7) NULL,
	[StartDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[TicketView]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TicketView]
AS
SELECT                      dbo.TaskItems.Id, dbo.TaskItems.CreatedAt, dbo.TaskItems.Deleted, dbo.TaskItems.Description, dbo.TaskItems.Subject, 
                                      dbo.TaskItems.UpdatedAt, dbo.TaskItems.Version, dbo.TaskItems.TaskNo, dbo.TaskItems.PlanTime, 
                                      dbo.TaskItems.DoneTime, dbo.TicketItems.Id AS Ticket_Id, dbo.Statuses.Id AS Status_Id, 
                                      dbo.Statuses.Name AS Status_Name, dbo.Trackers.Id AS Tracker_Id, dbo.Trackers.Name AS Tracker_Name, 
                                      dbo.Priorities.Id AS Priority_Id, dbo.Priorities.Name AS Priority_Name, dbo.TicketItems.AuthorId, 
                                      dbo.TicketItems.DoneRate, dbo.TicketItems.PriorityId, dbo.TicketItems.StatusId, dbo.TicketItems.AssignedToId, 
                                      dbo.TicketItems.TrackerId, dbo.TicketItems.Version AS Ticket_Version, dbo.TaskItems.ProjectId, 
                                      dbo.Projects.Id AS Project_Id, dbo.Projects.Name AS Project_Name, dbo.Projects.ProjectNo AS Project_ProjectNo, 
                                      dbo.Statuses.IsClosed AS Status_IsClosed, dbo.TicketItems.DueDate, dbo.TicketItems.StartDate, 
                                      dbo.AspNetUsers.Id AS AssignedTo_Id, dbo.AspNetUsers.UserName AS AssignedTo_UserName, 
                                      AspNetUsers_1.Id AS Author_Id, AspNetUsers_1.UserName AS Author_UserName
FROM                         dbo.TaskItems INNER JOIN
                                      dbo.TicketItems ON dbo.TaskItems.Id = dbo.TicketItems.TaskId LEFT OUTER JOIN
                                      dbo.AspNetUsers AS AspNetUsers_1 ON dbo.TicketItems.AuthorId = AspNetUsers_1.Id LEFT OUTER JOIN
                                      dbo.AspNetUsers ON dbo.TicketItems.AssignedToId = dbo.AspNetUsers.Id LEFT OUTER JOIN
                                      dbo.Projects ON dbo.TaskItems.ProjectId = dbo.Projects.Id LEFT OUTER JOIN
                                      dbo.Priorities ON dbo.TicketItems.PriorityId = dbo.Priorities.Id LEFT OUTER JOIN
                                      dbo.Trackers ON dbo.TicketItems.TrackerId = dbo.Trackers.Id LEFT OUTER JOIN
                                      dbo.Statuses ON dbo.TicketItems.StatusId = dbo.Statuses.Id
WHERE                       (dbo.TaskItems.Deleted = 0)

GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StartEndTimes]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StartEndTimes](
	[Id] [nvarchar](128) NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
	[TaskId] [nvarchar](128) NOT NULL,
	[StartAt] [datetimeoffset](7) NULL,
	[EndAt] [datetimeoffset](7) NULL,
	[IsPlan] [bit] NOT NULL,
 CONSTRAINT [PK_StartEndTimes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaskPerts]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskPerts](
	[Id] [nvarchar](128) NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
	[PreTaskId] [nvarchar](128) NOT NULL,
	[PostTaskId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_TaskPerts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaskTrees]    Script Date: 2017/05/15 23:19:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskTrees](
	[Id] [nvarchar](128) NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NULL,
	[Deleted] [bit] NOT NULL,
	[ParentTaskId] [nvarchar](128) NOT NULL,
	[ChildTaskId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_TaskTrees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Priorities] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Priorities] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[ProjectUsers] ADD  CONSTRAINT [DF__ProjectUsers__Id__2DE6D218]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[StartEndTimes] ADD  CONSTRAINT [DF__StartEndTime__Id__48CFD27E]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[StartEndTimes] ADD  CONSTRAINT [DF__StartEndT__Creat__49C3F6B7]  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Statuses] ADD  CONSTRAINT [DF__Statuses__Id__403A8C7D]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Statuses] ADD  CONSTRAINT [DF__Statuses__Create__412EB0B6]  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[TaskItems] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[TaskItems] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[TaskPerts] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[TaskPerts] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[TaskTrees] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[TaskTrees] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[TicketItems] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[TicketItems] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Trackers] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Trackers] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[WbsItems] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[WbsItems] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[StartEndTimes]  WITH CHECK ADD  CONSTRAINT [FK_StartEndTimes_TaskItems1] FOREIGN KEY([TaskId])
REFERENCES [dbo].[TaskItems] ([Id])
GO
ALTER TABLE [dbo].[StartEndTimes] CHECK CONSTRAINT [FK_StartEndTimes_TaskItems1]
GO
ALTER TABLE [dbo].[TaskPerts]  WITH CHECK ADD  CONSTRAINT [FK_TaskPerts_TaskItems] FOREIGN KEY([PreTaskId])
REFERENCES [dbo].[TaskItems] ([Id])
GO
ALTER TABLE [dbo].[TaskPerts] CHECK CONSTRAINT [FK_TaskPerts_TaskItems]
GO
ALTER TABLE [dbo].[TaskPerts]  WITH CHECK ADD  CONSTRAINT [FK_TaskPerts_TaskItems1] FOREIGN KEY([PostTaskId])
REFERENCES [dbo].[TaskItems] ([Id])
GO
ALTER TABLE [dbo].[TaskPerts] CHECK CONSTRAINT [FK_TaskPerts_TaskItems1]
GO
ALTER TABLE [dbo].[TaskTrees]  WITH CHECK ADD  CONSTRAINT [FK_TaskTrees_TaskItems] FOREIGN KEY([ParentTaskId])
REFERENCES [dbo].[TaskItems] ([Id])
GO
ALTER TABLE [dbo].[TaskTrees] CHECK CONSTRAINT [FK_TaskTrees_TaskItems]
GO
ALTER TABLE [dbo].[TaskTrees]  WITH CHECK ADD  CONSTRAINT [FK_TaskTrees_TaskItems1] FOREIGN KEY([ChildTaskId])
REFERENCES [dbo].[TaskItems] ([Id])
GO
ALTER TABLE [dbo].[TaskTrees] CHECK CONSTRAINT [FK_TaskTrees_TaskItems1]
GO
ALTER TABLE [dbo].[TicketItems]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Priorities] FOREIGN KEY([PriorityId])
REFERENCES [dbo].[Priorities] ([Id])
GO
ALTER TABLE [dbo].[TicketItems] CHECK CONSTRAINT [FK_Tickets_Priorities]
GO
ALTER TABLE [dbo].[TicketItems]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Statuses] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([Id])
GO
ALTER TABLE [dbo].[TicketItems] CHECK CONSTRAINT [FK_Tickets_Statuses]
GO
ALTER TABLE [dbo].[TicketItems]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_TaskItems] FOREIGN KEY([TaskId])
REFERENCES [dbo].[TaskItems] ([Id])
GO
ALTER TABLE [dbo].[TicketItems] CHECK CONSTRAINT [FK_Tickets_TaskItems]
GO
ALTER TABLE [dbo].[TicketItems]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Trackers] FOREIGN KEY([TrackerId])
REFERENCES [dbo].[Trackers] ([Id])
GO
ALTER TABLE [dbo].[TicketItems] CHECK CONSTRAINT [FK_Tickets_Trackers]
GO
ALTER TABLE [dbo].[WbsItems]  WITH CHECK ADD  CONSTRAINT [FK_WbsItems_TaskItems] FOREIGN KEY([TaskId])
REFERENCES [dbo].[TaskItems] ([Id])
GO
ALTER TABLE [dbo].[WbsItems] CHECK CONSTRAINT [FK_WbsItems_TaskItems]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[15] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AspNetUsers"
            Begin Extent = 
               Top = 85
               Left = 436
               Bottom = 215
               Right = 657
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProjectUsers"
            Begin Extent = 
               Top = 66
               Left = 90
               Bottom = 179
               Right = 245
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Projects"
            Begin Extent = 
               Top = 195
               Left = 257
               Bottom = 325
               Right = 412
            End
            DisplayFlags = 280
            TopColumn = 4
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProjectUserView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProjectUserView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[27] 4[32] 2[13] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[49] 4[26] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[20] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1[70] 4) )"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -82
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TaskItems"
            Begin Extent = 
               Top = 31
               Left = 113
               Bottom = 270
               Right = 268
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TicketItems"
            Begin Extent = 
               Top = 50
               Left = 320
               Bottom = 262
               Right = 480
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "Projects"
            Begin Extent = 
               Top = 370
               Left = 171
               Bottom = 500
               Right = 326
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "Priorities"
            Begin Extent = 
               Top = 364
               Left = 714
               Bottom = 494
               Right = 869
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Trackers"
            Begin Extent = 
               Top = 227
               Left = 706
               Bottom = 357
               Right = 861
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Statuses"
            Begin Extent = 
               Top = 78
               Left = 701
               Bottom = 208
               Right = 856
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "AspNetUsers"
            Begin Extent = 
               Top = 302
               Left = 561
               Bottom = 432
               Right = 782
            End
          ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TicketView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'  DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AspNetUsers_1"
            Begin Extent = 
               Top = 451
               Left = 470
               Bottom = 581
               Right = 691
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 41
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 2655
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1860
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TicketView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TicketView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AspNetUsers"
            Begin Extent = 
               Top = 49
               Left = 77
               Bottom = 179
               Right = 298
            End
            DisplayFlags = 280
            TopColumn = 11
         End
         Begin Table = "AspNetUserRoles"
            Begin Extent = 
               Top = 94
               Left = 379
               Bottom = 190
               Right = 534
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AspNetRoles"
            Begin Extent = 
               Top = 87
               Left = 645
               Bottom = 217
               Right = 834
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserRoleView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserRoleView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AspNetUsers"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 259
            End
            DisplayFlags = 280
            TopColumn = 11
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[25] 2[19] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "WbsItems"
            Begin Extent = 
               Top = 36
               Left = 323
               Bottom = 166
               Right = 478
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "Trackers"
            Begin Extent = 
               Top = 42
               Left = 635
               Bottom = 172
               Right = 790
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "Statuses"
            Begin Extent = 
               Top = 253
               Left = 503
               Bottom = 383
               Right = 658
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Priorities"
            Begin Extent = 
               Top = 184
               Left = 773
               Bottom = 314
               Right = 928
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "Users_1"
            Begin Extent = 
               Top = 288
               Left = 237
               Bottom = 418
               Right = 392
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "Users"
            Begin Extent = 
               Top = 331
               Left = 692
               Bottom = 461
               Right = 847
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "TaskItems"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 193
            End
            DisplayFlag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'WbsView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N's = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 24
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 2160
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'WbsView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'WbsView'
GO
ｆｓ