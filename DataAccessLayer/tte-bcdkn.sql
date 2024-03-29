USE [tte-bcdkn]
GO
/****** Object:  ForeignKey [FK_Class_PeriodPlanning]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Class_PeriodPlanning]') AND parent_object_id = OBJECT_ID(N'[dbo].[Class]'))
ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK_Class_PeriodPlanning]
GO
/****** Object:  ForeignKey [FK_Class_Planning]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Class_Planning]') AND parent_object_id = OBJECT_ID(N'[dbo].[Class]'))
ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK_Class_Planning]
GO
/****** Object:  ForeignKey [FK_Event_Owner]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Owner]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Owner]
GO
/****** Object:  ForeignKey [FK_Event_Planning]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Planning]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Planning]
GO
/****** Object:  ForeignKey [FK_Event_Speaker]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Speaker]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Speaker]
GO
/****** Object:  ForeignKey [FK_Event_Subject]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Subject]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Subject]
GO
/****** Object:  ForeignKey [FK_Modality_Subject]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Modality_Subject]') AND parent_object_id = OBJECT_ID(N'[dbo].[Modality]'))
ALTER TABLE [dbo].[Modality] DROP CONSTRAINT [FK_Modality_Subject]
GO
/****** Object:  ForeignKey [FK_Period_Planning]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Period_Planning]') AND parent_object_id = OBJECT_ID(N'[dbo].[Period]'))
ALTER TABLE [dbo].[Period] DROP CONSTRAINT [FK_Period_Planning]
GO
/****** Object:  ForeignKey [FK_Plannings_Parent]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Plannings_Parent]') AND parent_object_id = OBJECT_ID(N'[dbo].[Planning]'))
ALTER TABLE [dbo].[Planning] DROP CONSTRAINT [FK_Plannings_Parent]
GO
/****** Object:  ForeignKey [FK_Right_Campus]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Right_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [FK_Right_Campus]
GO
/****** Object:  ForeignKey [FK_Right_User]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Right_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [FK_Right_User]
GO
/****** Object:  ForeignKey [FK_User_Planing]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_Planing]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Planing]
GO
/****** Object:  Check [CK_PlanningsType]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_PlanningsType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Planning]'))
BEGIN
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_PlanningsType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Planning]'))
ALTER TABLE [dbo].[Planning] DROP CONSTRAINT [CK_PlanningsType]

END
GO
/****** Object:  Table [dbo].[Class]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Class_PeriodPlanning]') AND parent_object_id = OBJECT_ID(N'[dbo].[Class]'))
ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK_Class_PeriodPlanning]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Class_Planning]') AND parent_object_id = OBJECT_ID(N'[dbo].[Class]'))
ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK_Class_Planning]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Class]') AND type in (N'U'))
DROP TABLE [dbo].[Class]
GO
/****** Object:  Table [dbo].[Period]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Period_Planning]') AND parent_object_id = OBJECT_ID(N'[dbo].[Period]'))
ALTER TABLE [dbo].[Period] DROP CONSTRAINT [FK_Period_Planning]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Period]') AND type in (N'U'))
DROP TABLE [dbo].[Period]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Owner]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Owner]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Planning]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Planning]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Speaker]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Speaker]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Subject]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Subject]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event]') AND type in (N'U'))
DROP TABLE [dbo].[Event]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Right_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [FK_Right_Campus]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Right_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
ALTER TABLE [dbo].[Role] DROP CONSTRAINT [FK_Right_User]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
DROP TABLE [dbo].[Role]
GO
/****** Object:  Table [dbo].[User]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_Planing]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Planing]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Planning]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Plannings_Parent]') AND parent_object_id = OBJECT_ID(N'[dbo].[Planning]'))
ALTER TABLE [dbo].[Planning] DROP CONSTRAINT [FK_Plannings_Parent]
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_PlanningsType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Planning]'))
ALTER TABLE [dbo].[Planning] DROP CONSTRAINT [CK_PlanningsType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Planning]') AND type in (N'U'))
DROP TABLE [dbo].[Planning]
GO
/****** Object:  Table [dbo].[Modality]    Script Date: 06/13/2010 11:40:33 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Modality_Subject]') AND parent_object_id = OBJECT_ID(N'[dbo].[Modality]'))
ALTER TABLE [dbo].[Modality] DROP CONSTRAINT [FK_Modality_Subject]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Modality]') AND type in (N'U'))
DROP TABLE [dbo].[Modality]
GO
/****** Object:  Table [dbo].[Modality]    Script Date: 06/13/2010 11:40:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Modality]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Modality] ON
INSERT [dbo].[Modality] ([Id], [Name], [Subject], [Hours]) VALUES (3, N'Linux', NULL, NULL)
INSERT [dbo].[Modality] ([Id], [Name], [Subject], [Hours]) VALUES (4, N'présentiel', 3, 50)
INSERT [dbo].[Modality] ([Id], [Name], [Subject], [Hours]) VALUES (5, N'elearning', 3, 80)
INSERT [dbo].[Modality] ([Id], [Name], [Subject], [Hours]) VALUES (6, N'TP', 3, 120)
INSERT [dbo].[Modality] ([Id], [Name], [Subject], [Hours]) VALUES (7, N'Controle', 3, 3)
SET IDENTITY_INSERT [dbo].[Modality] OFF
/****** Object:  Table [dbo].[Planning]    Script Date: 06/13/2010 11:40:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Planning]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Planning] ON
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (1, N'SUPINFO', NULL, CAST(0x00009D9200E4FC98 AS DateTime), 0)
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (2, N'Toulouse', 1, CAST(0x00009D94007E1D0D AS DateTime), 1)
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (3, N'2012', 1, NULL, NULL)
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (4, N'Finnance', 3, CAST(0x00009D91012085DD AS DateTime), 2)
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (7, N'B3A', 2, CAST(0x00009D9200E07AA0 AS DateTime), 3)
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (8, N'Christophe', 7, CAST(0x00009D94009BE444 AS DateTime), 4)
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (9, N'Bordeau', 1, NULL, 1)
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (10, N'Paris', 1, NULL, 1)
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (12, N'2011', 1, NULL, NULL)
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (13, N'popi2', 7, CAST(0x00009D9300CE2F30 AS DateTime), 4)
INSERT [dbo].[Planning] ([Id], [Name], [Parent], [LastChange], [Type]) VALUES (14, N'Christophe2', NULL, CAST(0x00009D9300CE71A3 AS DateTime), 4)
SET IDENTITY_INSERT [dbo].[Planning] OFF
/****** Object:  Table [dbo].[User]    Script Date: 06/13/2010 11:40:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User](
	[Id] [int] NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[User] ([Id], [Login], [Password]) VALUES (8, N'popi', N'popi')
INSERT [dbo].[User] ([Id], [Login], [Password]) VALUES (13, N'popi4', N'36cbacd8bb0cab2e85f6b92a5637f76a')
INSERT [dbo].[User] ([Id], [Login], [Password]) VALUES (14, N'popi2', N'7d7a3c7b2a7e62fd6b6c955502c607a8')
/****** Object:  Table [dbo].[Role]    Script Date: 06/13/2010 11:40:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[User] [int] NOT NULL,
	[Target] [int] NULL,
 CONSTRAINT [PK_Right] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Role] ON
INSERT [dbo].[Role] ([Id], [User], [Target]) VALUES (1, 8, 1)
INSERT [dbo].[Role] ([Id], [User], [Target]) VALUES (2, 13, NULL)
SET IDENTITY_INSERT [dbo].[Role] OFF
/****** Object:  Table [dbo].[Event]    Script Date: 06/13/2010 11:40:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Event] ON
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (2, N'tondre la pelouse', 8, CAST(0x00009E0B00000000 AS DateTime), CAST(0x00009E0B00A4CB80 AS DateTime), 1, N'home', NULL, 8, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (3, N'fermer les volets', 1, CAST(0x00009E0B00A4CB80 AS DateTime), CAST(0x00009E0B01499700 AS DateTime), 1, N'préaut 2', NULL, 8, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (4, N'VEVENT', 2, CAST(0x00009E0B009450C0 AS DateTime), CAST(0x00009E0B00C5C100 AS DateTime), 1, N'de labas', NULL, 8, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (5, N'tondre la pelouse', 4, CAST(0x00009E0B00000000 AS DateTime), CAST(0x00009E0B00000000 AS DateTime), 1, N'home', NULL, 8, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (6, N'tondre la pelouse', 7, CAST(0x00009E0B00B54640 AS DateTime), CAST(0x00009E0B00000000 AS DateTime), 1, N'home', NULL, 8, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (7, N'peindre les volets', 7, CAST(0x00009E0B00000000 AS DateTime), CAST(0x00009E0B00000000 AS DateTime), 0, N'home', NULL, 8, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (8, N'couper du bois', 7, CAST(0x00009E0B00B54640 AS DateTime), CAST(0x00009E0B00000000 AS DateTime), 1, N'home', NULL, 8, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (9, N'couper du bois', 7, CAST(0x00009E0B00000000 AS DateTime), CAST(0x00009E0B00000000 AS DateTime), 0, N'home', NULL, 8, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (10, N'manau', 1, CAST(0x00009E0B00000000 AS DateTime), CAST(0x00009E0B00000000 AS DateTime), 1, N'd''ou', NULL, 8, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (11, N'manau', 1, CAST(0x00009E0B00000000 AS DateTime), CAST(0x00009E0B00000000 AS DateTime), 1, N'd''ou', NULL, 8, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (12, N'fermer', 2, CAST(0x00009E0B00000000 AS DateTime), CAST(0x00009E0B00000000 AS DateTime), 0, N'les volets', NULL, 8, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (13, N'fermer', 2, CAST(0x00009E0B00000000 AS DateTime), CAST(0x00009E0B00000000 AS DateTime), 0, N'les volets', NULL, 8, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Planning], [Start], [End], [Mandatory], [Place], [Speaker], [Owner], [Subject]) VALUES (14, N'', 8, CAST(0x00009D940083D600 AS DateTime), CAST(0x00009D9400000000 AS DateTime), 0, N'home', NULL, 8, NULL)
SET IDENTITY_INSERT [dbo].[Event] OFF
/****** Object:  Table [dbo].[Period]    Script Date: 06/13/2010 11:40:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Period]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Period](
	[Id] [int] NOT NULL,
	[Start] [datetime] NOT NULL,
	[End] [datetime] NOT NULL,
 CONSTRAINT [PK_Period] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[Period] ([Id], [Start], [End]) VALUES (4, CAST(0x00009C9E00000000 AS DateTime), CAST(0x00009E0B00000000 AS DateTime))
/****** Object:  Table [dbo].[Class]    Script Date: 06/13/2010 11:40:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Class]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Class](
	[Id] [int] NOT NULL,
	[Period] [int] NOT NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[Class] ([Id], [Period]) VALUES (7, 4)
/****** Object:  Check [CK_PlanningsType]    Script Date: 06/13/2010 11:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_PlanningsType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Planning]'))
ALTER TABLE [dbo].[Planning]  WITH CHECK ADD  CONSTRAINT [CK_PlanningsType] CHECK  (([Type]<(5) AND [Type]>(-1)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_PlanningsType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Planning]'))
ALTER TABLE [dbo].[Planning] CHECK CONSTRAINT [CK_PlanningsType]
GO
/****** Object:  ForeignKey [FK_Class_PeriodPlanning]    Script Date: 06/13/2010 11:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Class_PeriodPlanning]') AND parent_object_id = OBJECT_ID(N'[dbo].[Class]'))
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_PeriodPlanning] FOREIGN KEY([Period])
REFERENCES [dbo].[Planning] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Class_PeriodPlanning]') AND parent_object_id = OBJECT_ID(N'[dbo].[Class]'))
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_PeriodPlanning]
GO
/****** Object:  ForeignKey [FK_Class_Planning]    Script Date: 06/13/2010 11:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Class_Planning]') AND parent_object_id = OBJECT_ID(N'[dbo].[Class]'))
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Planning] FOREIGN KEY([Id])
REFERENCES [dbo].[Planning] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Class_Planning]') AND parent_object_id = OBJECT_ID(N'[dbo].[Class]'))
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Planning]
GO
/****** Object:  ForeignKey [FK_Event_Owner]    Script Date: 06/13/2010 11:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Owner]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Owner] FOREIGN KEY([Owner])
REFERENCES [dbo].[Planning] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Owner]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Owner]
GO
/****** Object:  ForeignKey [FK_Event_Planning]    Script Date: 06/13/2010 11:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Planning]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Planning] FOREIGN KEY([Planning])
REFERENCES [dbo].[Planning] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Planning]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Planning]
GO
/****** Object:  ForeignKey [FK_Event_Speaker]    Script Date: 06/13/2010 11:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Speaker]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Speaker] FOREIGN KEY([Speaker])
REFERENCES [dbo].[Planning] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Speaker]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Speaker]
GO
/****** Object:  ForeignKey [FK_Event_Subject]    Script Date: 06/13/2010 11:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Subject]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Subject] FOREIGN KEY([Subject])
REFERENCES [dbo].[Modality] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Subject]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Subject]
GO
/****** Object:  ForeignKey [FK_Modality_Subject]    Script Date: 06/13/2010 11:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Modality_Subject]') AND parent_object_id = OBJECT_ID(N'[dbo].[Modality]'))
ALTER TABLE [dbo].[Modality]  WITH CHECK ADD  CONSTRAINT [FK_Modality_Subject] FOREIGN KEY([Subject])
REFERENCES [dbo].[Modality] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Modality_Subject]') AND parent_object_id = OBJECT_ID(N'[dbo].[Modality]'))
ALTER TABLE [dbo].[Modality] CHECK CONSTRAINT [FK_Modality_Subject]
GO
/****** Object:  ForeignKey [FK_Period_Planning]    Script Date: 06/13/2010 11:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Period_Planning]') AND parent_object_id = OBJECT_ID(N'[dbo].[Period]'))
ALTER TABLE [dbo].[Period]  WITH CHECK ADD  CONSTRAINT [FK_Period_Planning] FOREIGN KEY([Id])
REFERENCES [dbo].[Planning] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Period_Planning]') AND parent_object_id = OBJECT_ID(N'[dbo].[Period]'))
ALTER TABLE [dbo].[Period] CHECK CONSTRAINT [FK_Period_Planning]
GO
/****** Object:  ForeignKey [FK_Plannings_Parent]    Script Date: 06/13/2010 11:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Plannings_Parent]') AND parent_object_id = OBJECT_ID(N'[dbo].[Planning]'))
ALTER TABLE [dbo].[Planning]  WITH CHECK ADD  CONSTRAINT [FK_Plannings_Parent] FOREIGN KEY([Parent])
REFERENCES [dbo].[Planning] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Plannings_Parent]') AND parent_object_id = OBJECT_ID(N'[dbo].[Planning]'))
ALTER TABLE [dbo].[Planning] CHECK CONSTRAINT [FK_Plannings_Parent]
GO
/****** Object:  ForeignKey [FK_Right_Campus]    Script Date: 06/13/2010 11:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Right_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Right_Campus] FOREIGN KEY([Target])
REFERENCES [dbo].[Planning] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Right_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Right_Campus]
GO
/****** Object:  ForeignKey [FK_Right_User]    Script Date: 06/13/2010 11:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Right_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Right_User] FOREIGN KEY([User])
REFERENCES [dbo].[User] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Right_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Role]'))
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Right_User]
GO
/****** Object:  ForeignKey [FK_User_Planing]    Script Date: 06/13/2010 11:40:33 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_Planing]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Planing] FOREIGN KEY([Id])
REFERENCES [dbo].[Planning] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_Planing]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Planing]
GO
