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
/****** Object:  Table [dbo].[TaskTrees]    Script Date: 2017/05/16 11:10:23 ******/
DROP TABLE [dbo].[TaskTrees]
GO
/****** Object:  Table [dbo].[TaskPerts]    Script Date: 2017/05/16 11:10:23 ******/
DROP TABLE [dbo].[TaskPerts]
GO
/****** Object:  Table [dbo].[StartEndTimes]    Script Date: 2017/05/16 11:10:23 ******/
DROP TABLE [dbo].[StartEndTimes]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 2017/05/16 11:10:23 ******/
DROP TABLE [dbo].[AspNetUserTokens]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 2017/05/16 11:10:23 ******/
DROP TABLE [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 2017/05/16 11:10:23 ******/
DROP TABLE [dbo].[AspNetUserClaims]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 2017/05/16 11:10:23 ******/
DROP TABLE [dbo].[AspNetRoleClaims]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2017/05/16 11:10:23 ******/
DROP TABLE [dbo].[__EFMigrationsHistory]
GO
/****** Object:  View [dbo].[TicketView]    Script Date: 2017/05/16 11:10:23 ******/
DROP VIEW [dbo].[TicketView]
GO
/****** Object:  Table [dbo].[TicketItems]    Script Date: 2017/05/16 11:10:23 ******/
DROP TABLE [dbo].[TicketItems]
GO
/****** Object:  View [dbo].[WbsView]    Script Date: 2017/05/16 11:10:23 ******/
DROP VIEW [dbo].[WbsView]
GO
/****** Object:  Table [dbo].[WbsItems]    Script Date: 2017/05/16 11:10:23 ******/
DROP TABLE [dbo].[WbsItems]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2017/05/16 11:10:23 ******/
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Priorities]    Script Date: 2017/05/16 11:10:23 ******/
DROP TABLE [dbo].[Priorities]
GO
/****** Object:  Table [dbo].[Trackers]    Script Date: 2017/05/16 11:10:24 ******/
DROP TABLE [dbo].[Trackers]
GO
/****** Object:  Table [dbo].[TaskItems]    Script Date: 2017/05/16 11:10:24 ******/
DROP TABLE [dbo].[TaskItems]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 2017/05/16 11:10:24 ******/
DROP TABLE [dbo].[Statuses]
GO
/****** Object:  View [dbo].[ProjectUserView]    Script Date: 2017/05/16 11:10:24 ******/
DROP VIEW [dbo].[ProjectUserView]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 2017/05/16 11:10:24 ******/
DROP TABLE [dbo].[Projects]
GO
/****** Object:  Table [dbo].[ProjectUsers]    Script Date: 2017/05/16 11:10:24 ******/
DROP TABLE [dbo].[ProjectUsers]
GO
/****** Object:  View [dbo].[UserRoleView]    Script Date: 2017/05/16 11:10:24 ******/
DROP VIEW [dbo].[UserRoleView]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 2017/05/16 11:10:24 ******/
DROP TABLE [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 2017/05/16 11:10:24 ******/
DROP TABLE [dbo].[AspNetUserRoles]
GO
/****** Object:  View [dbo].[UserView]    Script Date: 2017/05/16 11:10:24 ******/
DROP VIEW [dbo].[UserView]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 2017/05/16 11:10:24 ******/
DROP TABLE [dbo].[AspNetUsers]
GO
