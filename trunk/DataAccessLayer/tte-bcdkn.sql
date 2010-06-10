USE [master]
GO
/****** Object:  Database [tte-bcdkn]    Script Date: 06/10/2010 19:25:11 ******/
CREATE DATABASE [tte-bcdkn] ON  PRIMARY 
( NAME = N'tte-bcdkn', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\tte-bcdkn.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'tte-bcdkn_log', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\tte-bcdkn_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'tte-bcdkn', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [tte-bcdkn].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [tte-bcdkn] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [tte-bcdkn] SET ANSI_NULLS OFF
GO
ALTER DATABASE [tte-bcdkn] SET ANSI_PADDING OFF
GO
ALTER DATABASE [tte-bcdkn] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [tte-bcdkn] SET ARITHABORT OFF
GO
ALTER DATABASE [tte-bcdkn] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [tte-bcdkn] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [tte-bcdkn] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [tte-bcdkn] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [tte-bcdkn] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [tte-bcdkn] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [tte-bcdkn] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [tte-bcdkn] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [tte-bcdkn] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [tte-bcdkn] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [tte-bcdkn] SET  ENABLE_BROKER
GO
ALTER DATABASE [tte-bcdkn] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [tte-bcdkn] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [tte-bcdkn] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [tte-bcdkn] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [tte-bcdkn] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [tte-bcdkn] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [tte-bcdkn] SET  READ_WRITE
GO
ALTER DATABASE [tte-bcdkn] SET RECOVERY SIMPLE
GO
ALTER DATABASE [tte-bcdkn] SET  MULTI_USER
GO
ALTER DATABASE [tte-bcdkn] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [tte-bcdkn] SET DB_CHAINING OFF
GO
USE [tte-bcdkn]
GO
/****** Object:  ForeignKey [FK_Modality_Subject]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Modality] DROP CONSTRAINT [FK_Modality_Subject]
GO
/****** Object:  ForeignKey [FK_Plannings_Parent]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Planning] DROP CONSTRAINT [FK_Plannings_Parent]
GO
/****** Object:  ForeignKey [FK_Right_Campus]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [FK_Right_Campus]
GO
/****** Object:  ForeignKey [FK_Right_User]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [FK_Right_User]
GO
/****** Object:  ForeignKey [FK_Event_Owner]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Owner]
GO
/****** Object:  ForeignKey [FK_Event_Planning]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Planning]
GO
/****** Object:  ForeignKey [FK_Event_Speaker]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Speaker]
GO
/****** Object:  ForeignKey [FK_Event_Subject]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Subject]
GO
/****** Object:  ForeignKey [FK_User_Planing]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Planing]
GO
/****** Object:  ForeignKey [FK_Period_Planning]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Period] DROP CONSTRAINT [FK_Period_Planning]
GO
/****** Object:  ForeignKey [FK_Class_PeriodPlanning]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK_Class_PeriodPlanning]
GO
/****** Object:  ForeignKey [FK_Class_Planning]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK_Class_Planning]
GO
/****** Object:  Check [CK_PlanningsType]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Planning] DROP CONSTRAINT [CK_PlanningsType]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK_Class_PeriodPlanning]
GO
ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK_Class_Planning]
GO
DROP TABLE [dbo].[Class]
GO
/****** Object:  Table [dbo].[Period]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Period] DROP CONSTRAINT [FK_Period_Planning]
GO
DROP TABLE [dbo].[Period]
GO
/****** Object:  Table [dbo].[User]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Planing]
GO
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Owner]
GO
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Planning]
GO
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Speaker]
GO
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Subject]
GO
DROP TABLE [dbo].[Event]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [FK_Right_Campus]
GO
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [FK_Right_User]
GO
DROP TABLE [dbo].[Role]
GO
/****** Object:  Table [dbo].[Planning]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Planning] DROP CONSTRAINT [FK_Plannings_Parent]
GO
ALTER TABLE [dbo].[Planning] DROP CONSTRAINT [CK_PlanningsType]
GO
DROP TABLE [dbo].[Planning]
GO
/****** Object:  Table [dbo].[Modality]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Modality] DROP CONSTRAINT [FK_Modality_Subject]
GO
DROP TABLE [dbo].[Modality]
GO
/****** Object:  Table [dbo].[Modality]    Script Date: 06/10/2010 19:25:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Modality](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Subject] [int] NULL,
	[Hours] [int] NULL,
 CONSTRAINT [PK_Modality] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Planning]    Script Date: 06/10/2010 19:25:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Planning](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Parent] [int] NULL,
	[LastChange] [datetime] NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_Planning] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Planning] ON
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (1, N'SUPINFO', NULL, CAST(0x00009E0B00000000 AS DateTime), 0)
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (2, N'Toulouse', 1, CAST(0x00009D9100DF8EA8 AS DateTime), 1)
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (3, N'2012', 1, NULL, NULL)
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (4, N'Finnance', 3, CAST(0x00009D91012085DD AS DateTime), 2)
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (7, N'B3A', 2, CAST(0x00009D9101214D41 AS DateTime), 3)
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (8, N'Christophe', 7, CAST(0x00009E0B00000000 AS DateTime), 4)
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (9, N'Bordeau', 1, NULL, 1)
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (10, N'Paris', 1, NULL, 1)
SET IDENTITY_INSERT [dbo].[Planning] OFF
/****** Object:  Table [dbo].[Role]    Script Date: 06/10/2010 19:25:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[User] [int] NOT NULL,
	[Target] [int] NULL,
 CONSTRAINT [PK_Right] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Role] ON
INSERT [dbo].[Role] ([Id], [User], [Target]) VALUES (1, 8, 1)
SET IDENTITY_INSERT [dbo].[Role] OFF
/****** Object:  Table [dbo].[Event]    Script Date: 06/10/2010 19:25:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Planning] [int] NOT NULL,
	[Start] [datetime] NOT NULL,
	[End] [datetime] NOT NULL,
	[Mandatory] [bit] NOT NULL,
	[Place] [varchar](50) NULL,
	[Speaker] [int] NULL,
	[Owner] [int] NOT NULL,
	[Subject] [int] NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Event] ON
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (2, N'tondre la pelouse', 8, CAST(0x00009E0B00000000 AS DateTime), CAST(0x00009E0B00A4CB80 AS DateTime), 1, N'home', NULL, 8, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (3, N'fermer les volets', 1, CAST(0x00009E0B00A4CB80 AS DateTime), CAST(0x00009E0B01499700 AS DateTime), 1, N'préaut 2', NULL, 8, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (4, N'VEVENT', 2, CAST(0x00009E0B00000000 AS DateTime), CAST(0x00009E0B00000000 AS DateTime), 1, N'home', NULL, 8, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (5, N'tondre la pelouse', 4, CAST(0x00009E0B00000000 AS DateTime), CAST(0x00009E0B00000000 AS DateTime), 1, N'home', NULL, 8, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (6, N'tondre la pelouse', 7, CAST(0x00009E0B00000000 AS DateTime), CAST(0x00009E0B00000000 AS DateTime), 1, N'home', NULL, 8, NULL)
SET IDENTITY_INSERT [dbo].[Event] OFF
/****** Object:  Table [dbo].[User]    Script Date: 06/10/2010 19:25:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[User] ([Id], [Login], [Password]) VALUES (8, N'popi', N'popi')
/****** Object:  Table [dbo].[Period]    Script Date: 06/10/2010 19:25:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Period](
	[Id] [int] NOT NULL,
	[Start] [datetime] NOT NULL,
	[End] [datetime] NOT NULL,
 CONSTRAINT [PK_Period] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Period] ([Id], [Start], [End]) VALUES (4, CAST(0x00009C9E00000000 AS DateTime), CAST(0x00009E0B00000000 AS DateTime))
/****** Object:  Table [dbo].[Class]    Script Date: 06/10/2010 19:25:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[Id] [int] NOT NULL,
	[Period] [int] NOT NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Class] ([Id], [Period]) VALUES (7, 4)
/****** Object:  Check [CK_PlanningsType]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Planning]  WITH CHECK ADD  CONSTRAINT [CK_PlanningsType] CHECK  (([Type]<(5) AND [Type]>(-1)))
GO
ALTER TABLE [dbo].[Planning] CHECK CONSTRAINT [CK_PlanningsType]
GO
/****** Object:  ForeignKey [FK_Modality_Subject]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Modality]  WITH CHECK ADD  CONSTRAINT [FK_Modality_Subject] FOREIGN KEY([Subject])
REFERENCES [dbo].[Modality] ([Id])
GO
ALTER TABLE [dbo].[Modality] CHECK CONSTRAINT [FK_Modality_Subject]
GO
/****** Object:  ForeignKey [FK_Plannings_Parent]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Planning]  WITH CHECK ADD  CONSTRAINT [FK_Plannings_Parent] FOREIGN KEY([Parent])
REFERENCES [dbo].[Planning] ([Id])
GO
ALTER TABLE [dbo].[Planning] CHECK CONSTRAINT [FK_Plannings_Parent]
GO
/****** Object:  ForeignKey [FK_Right_Campus]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Right_Campus] FOREIGN KEY([Target])
REFERENCES [dbo].[Planning] ([Id])
GO
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Right_Campus]
GO
/****** Object:  ForeignKey [FK_Right_User]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Right_User] FOREIGN KEY([User])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Right_User]
GO
/****** Object:  ForeignKey [FK_Event_Owner]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Owner] FOREIGN KEY([Owner])
REFERENCES [dbo].[Planning] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Owner]
GO
/****** Object:  ForeignKey [FK_Event_Planning]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Planning] FOREIGN KEY([Planning])
REFERENCES [dbo].[Planning] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Planning]
GO
/****** Object:  ForeignKey [FK_Event_Speaker]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Speaker] FOREIGN KEY([Speaker])
REFERENCES [dbo].[Planning] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Speaker]
GO
/****** Object:  ForeignKey [FK_Event_Subject]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Subject] FOREIGN KEY([Subject])
REFERENCES [dbo].[Modality] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Subject]
GO
/****** Object:  ForeignKey [FK_User_Planing]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Planing] FOREIGN KEY([Id])
REFERENCES [dbo].[Planning] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Planing]
GO
/****** Object:  ForeignKey [FK_Period_Planning]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Period]  WITH CHECK ADD  CONSTRAINT [FK_Period_Planning] FOREIGN KEY([Id])
REFERENCES [dbo].[Planning] ([Id])
GO
ALTER TABLE [dbo].[Period] CHECK CONSTRAINT [FK_Period_Planning]
GO
/****** Object:  ForeignKey [FK_Class_PeriodPlanning]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_PeriodPlanning] FOREIGN KEY([Period])
REFERENCES [dbo].[Planning] ([Id])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_PeriodPlanning]
GO
/****** Object:  ForeignKey [FK_Class_Planning]    Script Date: 06/10/2010 19:25:13 ******/
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Planning] FOREIGN KEY([Id])
REFERENCES [dbo].[Planning] ([Id])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Planning]
GO
