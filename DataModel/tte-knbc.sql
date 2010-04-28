/****** Object:  ForeignKey [FK_Classe_Campus]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Classe_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Class]'))
ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK_Classe_Campus]
GO
/****** Object:  ForeignKey [FK_Classe_Periode]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Classe_Periode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Class]'))
ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK_Classe_Periode]
GO
/****** Object:  ForeignKey [FK_Evenement_Campus]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Evenement_Campus]
GO
/****** Object:  ForeignKey [FK_Evenement_Classe]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Classe]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Evenement_Classe]
GO
/****** Object:  ForeignKey [FK_Evenement_Intervenant]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Intervenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Evenement_Intervenant]
GO
/****** Object:  ForeignKey [FK_Evenement_Matiere]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Matiere]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Evenement_Matiere]
GO
/****** Object:  ForeignKey [FK_Evenement_Periode]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Periode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Evenement_Periode]
GO
/****** Object:  ForeignKey [FK_Evenement_Proprietaire]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Proprietaire]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Evenement_Proprietaire]
GO
/****** Object:  ForeignKey [FK_Periode_Promotion]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Periode_Promotion]') AND parent_object_id = OBJECT_ID(N'[dbo].[Period]'))
ALTER TABLE [dbo].[Period] DROP CONSTRAINT [FK_Periode_Promotion]
GO
/****** Object:  ForeignKey [FK_Droit_Campus]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Droit_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Right]'))
ALTER TABLE [dbo].[Right] DROP CONSTRAINT [FK_Droit_Campus]
GO
/****** Object:  ForeignKey [FK_Droit_Utilisateur]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Droit_Utilisateur]') AND parent_object_id = OBJECT_ID(N'[dbo].[Right]'))
ALTER TABLE [dbo].[Right] DROP CONSTRAINT [FK_Droit_Utilisateur]
GO
/****** Object:  ForeignKey [FK_Utilisateur_Classe]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Utilisateur_Classe]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_Utilisateur_Classe]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event]') AND type in (N'U'))
DROP TABLE [dbo].[Event]
GO
/****** Object:  Table [dbo].[Right]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Right]') AND type in (N'U'))
DROP TABLE [dbo].[Right]
GO
/****** Object:  Table [dbo].[User]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Class]') AND type in (N'U'))
DROP TABLE [dbo].[Class]
GO
/****** Object:  Table [dbo].[Period]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Period]') AND type in (N'U'))
DROP TABLE [dbo].[Period]
GO
/****** Object:  Table [dbo].[University]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[University]') AND type in (N'U'))
DROP TABLE [dbo].[University]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Subject]') AND type in (N'U'))
DROP TABLE [dbo].[Subject]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Promotion]') AND type in (N'U'))
DROP TABLE [dbo].[Promotion]
GO
/****** Object:  Table [dbo].[Campus]    Script Date: 04/28/2010 13:03:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Campus]') AND type in (N'U'))
DROP TABLE [dbo].[Campus]
GO
/****** Object:  Table [dbo].[Campus]    Script Date: 04/28/2010 13:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Campus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Campus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE French_CI_AS NOT NULL,
	[LastChange] [datetime] NOT NULL,
 CONSTRAINT [PK_Campus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Campus] ON
INSERT [dbo].[Campus] ([Id], [Name], [LastChange]) VALUES (2, N'Toulouse', CAST(0x00009D6600C978B1 AS DateTime))
INSERT [dbo].[Campus] ([Id], [Name], [LastChange]) VALUES (3, N'Bordeaux', CAST(0x00009D6600C99F34 AS DateTime))
SET IDENTITY_INSERT [dbo].[Campus] OFF
/****** Object:  Table [dbo].[Promotion]    Script Date: 04/28/2010 13:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Promotion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Promotion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE French_CI_AS NOT NULL,
	[LastChange] [datetime] NOT NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Promotion] ON
INSERT [dbo].[Promotion] ([Id], [Name], [LastChange]) VALUES (1, N'Master Info 2012', CAST(0x00009D6600C9BD4A AS DateTime))
INSERT [dbo].[Promotion] ([Id], [Name], [LastChange]) VALUES (3, N'Master Info 1013', CAST(0x00009D6600CCE520 AS DateTime))
SET IDENTITY_INSERT [dbo].[Promotion] OFF
/****** Object:  Table [dbo].[Subject]    Script Date: 04/28/2010 13:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Subject]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Subject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[Hours] [int] NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Subject] ON
INSERT [dbo].[Subject] ([Id], [Name], [Hours]) VALUES (1, N'C++', 36)
SET IDENTITY_INSERT [dbo].[Subject] OFF
/****** Object:  Table [dbo].[University]    Script Date: 04/28/2010 13:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[University]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[University](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastChange] [datetime] NOT NULL,
 CONSTRAINT [PK_University] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[University] ON
INSERT [dbo].[University] ([Id], [LastChange]) VALUES (1, CAST(0x00009D6600000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[University] OFF
/****** Object:  Table [dbo].[Period]    Script Date: 04/28/2010 13:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Period]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Period](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[Start] [datetime] NOT NULL,
	[End] [datetime] NOT NULL,
	[LastChange] [datetime] NOT NULL,
	[Promotion_Id] [int] NOT NULL,
 CONSTRAINT [PK_Period] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Period]') AND name = N'IX_FK_Periode_Promotion')
CREATE NONCLUSTERED INDEX [IX_FK_Periode_Promotion] ON [dbo].[Period] 
(
	[Promotion_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[Period] ON
INSERT [dbo].[Period] ([Id], [Name], [Start], [End], [LastChange], [Promotion_Id]) VALUES (2, N'MI2012-B3', CAST(0x00009CB400000000 AS DateTime), CAST(0x00009E1F00000000 AS DateTime), CAST(0x00009D6600000000 AS DateTime), 1)
INSERT [dbo].[Period] ([Id], [Name], [Start], [End], [LastChange], [Promotion_Id]) VALUES (4, N'MI2013-B2', CAST(0x00009CB400000000 AS DateTime), CAST(0x00009E1F00000000 AS DateTime), CAST(0x00009D6600000000 AS DateTime), 3)
SET IDENTITY_INSERT [dbo].[Period] OFF
/****** Object:  Table [dbo].[Class]    Script Date: 04/28/2010 13:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Class]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Class](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](10) COLLATE French_CI_AS NOT NULL,
	[LastChange] [datetime] NOT NULL,
	[Campus_Id] [int] NOT NULL,
	[Period_Id] [int] NOT NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Class]') AND name = N'IX_FK_Classe_Campus')
CREATE NONCLUSTERED INDEX [IX_FK_Classe_Campus] ON [dbo].[Class] 
(
	[Campus_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Class]') AND name = N'IX_FK_Classe_Periode')
CREATE NONCLUSTERED INDEX [IX_FK_Classe_Periode] ON [dbo].[Class] 
(
	[Period_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[Class] ON
INSERT [dbo].[Class] ([Id], [Name], [LastChange], [Campus_Id], [Period_Id]) VALUES (1, N'TLS-B3A', CAST(0x00009D6600000000 AS DateTime), 2, 2)
INSERT [dbo].[Class] ([Id], [Name], [LastChange], [Campus_Id], [Period_Id]) VALUES (2, N'BDX-B2A', CAST(0x00009D6600000000 AS DateTime), 3, 4)
SET IDENTITY_INSERT [dbo].[Class] OFF
/****** Object:  Table [dbo].[User]    Script Date: 04/28/2010 13:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE French_CI_AS NOT NULL,
	[Password] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[LastChange] [datetime] NOT NULL,
	[Class_Id] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND name = N'IX_FK_Utilisateur_Classe')
CREATE NONCLUSTERED INDEX [IX_FK_Utilisateur_Classe] ON [dbo].[User] 
(
	[Class_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([Id], [Name], [Password], [LastChange], [Class_Id]) VALUES (1, N'toto', N'', CAST(0x00009D6600000000 AS DateTime), 1)
INSERT [dbo].[User] ([Id], [Name], [Password], [LastChange], [Class_Id]) VALUES (2, N'admin', N'', CAST(0x00009D6600000000 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[Right]    Script Date: 04/28/2010 13:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Right]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Right](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[Campus_Id] [int] NULL,
	[User_Id] [int] NOT NULL,
 CONSTRAINT [PK_Right] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Right]') AND name = N'IX_FK_Droit_Campus')
CREATE NONCLUSTERED INDEX [IX_FK_Droit_Campus] ON [dbo].[Right] 
(
	[Campus_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Right]') AND name = N'IX_FK_Droit_Utilisateur')
CREATE NONCLUSTERED INDEX [IX_FK_Droit_Utilisateur] ON [dbo].[Right] 
(
	[User_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[Right] ON
INSERT [dbo].[Right] ([Id], [Type], [Campus_Id], [User_Id]) VALUES (1, 0, NULL, 2)
SET IDENTITY_INSERT [dbo].[Right] OFF
/****** Object:  Table [dbo].[Event]    Script Date: 04/28/2010 13:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) COLLATE French_CI_AS NULL,
	[Place] [nvarchar](50) COLLATE French_CI_AS NULL,
	[Name] [nvarchar](50) COLLATE French_CI_AS NOT NULL,
	[Start] [datetime] NOT NULL,
	[End] [datetime] NOT NULL,
	[Mandatory] [bit] NOT NULL,
	[Campus_Id] [int] NULL,
	[Class_Id] [int] NULL,
	[Speaker_Id] [int] NULL,
	[Subject_Id] [int] NULL,
	[Period_Id] [int] NULL,
	[Creator_Id] [int] NOT NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event]') AND name = N'IX_FK_Evenement_Campus')
CREATE NONCLUSTERED INDEX [IX_FK_Evenement_Campus] ON [dbo].[Event] 
(
	[Campus_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event]') AND name = N'IX_FK_Evenement_Classe')
CREATE NONCLUSTERED INDEX [IX_FK_Evenement_Classe] ON [dbo].[Event] 
(
	[Class_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event]') AND name = N'IX_FK_Evenement_Intervenant')
CREATE NONCLUSTERED INDEX [IX_FK_Evenement_Intervenant] ON [dbo].[Event] 
(
	[Speaker_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event]') AND name = N'IX_FK_Evenement_Matiere')
CREATE NONCLUSTERED INDEX [IX_FK_Evenement_Matiere] ON [dbo].[Event] 
(
	[Subject_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event]') AND name = N'IX_FK_Evenement_Periode')
CREATE NONCLUSTERED INDEX [IX_FK_Evenement_Periode] ON [dbo].[Event] 
(
	[Period_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event]') AND name = N'IX_FK_Evenement_Proprietaire')
CREATE NONCLUSTERED INDEX [IX_FK_Evenement_Proprietaire] ON [dbo].[Event] 
(
	[Creator_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[Event] ON
INSERT [dbo].[Event] ([Id], [Type], [Place], [Name], [Start], [End], [Mandatory], [Campus_Id], [Class_Id], [Speaker_Id], [Subject_Id], [Period_Id], [Creator_Id]) VALUES (4, N'Course', N'Salle1', N'C++', CAST(0x00009D6600E6B680 AS DateTime), CAST(0x00009D6600F73140 AS DateTime), 1, NULL, 2, 2, 1, NULL, 1)
INSERT [dbo].[Event] ([Id], [Type], [Place], [Name], [Start], [End], [Mandatory], [Campus_Id], [Class_Id], [Speaker_Id], [Subject_Id], [Period_Id], [Creator_Id]) VALUES (5, N'Course', N'Salle1', N'C++', CAST(0x00009D6600F73140 AS DateTime), CAST(0x00009D660107AC00 AS DateTime), 1, NULL, 2, 2, 1, NULL, 1)
SET IDENTITY_INSERT [dbo].[Event] OFF
/****** Object:  ForeignKey [FK_Classe_Campus]    Script Date: 04/28/2010 13:03:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Classe_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Class]'))
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Classe_Campus] FOREIGN KEY([Campus_Id])
REFERENCES [dbo].[Campus] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Classe_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Class]'))
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Classe_Campus]
GO
/****** Object:  ForeignKey [FK_Classe_Periode]    Script Date: 04/28/2010 13:03:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Classe_Periode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Class]'))
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Classe_Periode] FOREIGN KEY([Period_Id])
REFERENCES [dbo].[Period] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Classe_Periode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Class]'))
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Classe_Periode]
GO
/****** Object:  ForeignKey [FK_Evenement_Campus]    Script Date: 04/28/2010 13:03:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Campus] FOREIGN KEY([Campus_Id])
REFERENCES [dbo].[Campus] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Evenement_Campus]
GO
/****** Object:  ForeignKey [FK_Evenement_Classe]    Script Date: 04/28/2010 13:03:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Classe]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Classe] FOREIGN KEY([Class_Id])
REFERENCES [dbo].[Class] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Classe]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Evenement_Classe]
GO
/****** Object:  ForeignKey [FK_Evenement_Intervenant]    Script Date: 04/28/2010 13:03:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Intervenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Intervenant] FOREIGN KEY([Speaker_Id])
REFERENCES [dbo].[User] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Intervenant]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Evenement_Intervenant]
GO
/****** Object:  ForeignKey [FK_Evenement_Matiere]    Script Date: 04/28/2010 13:03:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Matiere]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Matiere] FOREIGN KEY([Subject_Id])
REFERENCES [dbo].[Subject] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Matiere]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Evenement_Matiere]
GO
/****** Object:  ForeignKey [FK_Evenement_Periode]    Script Date: 04/28/2010 13:03:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Periode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Periode] FOREIGN KEY([Period_Id])
REFERENCES [dbo].[Period] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Periode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Evenement_Periode]
GO
/****** Object:  ForeignKey [FK_Evenement_Proprietaire]    Script Date: 04/28/2010 13:03:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Proprietaire]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Proprietaire] FOREIGN KEY([Creator_Id])
REFERENCES [dbo].[User] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Proprietaire]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event]'))
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Evenement_Proprietaire]
GO
/****** Object:  ForeignKey [FK_Periode_Promotion]    Script Date: 04/28/2010 13:03:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Periode_Promotion]') AND parent_object_id = OBJECT_ID(N'[dbo].[Period]'))
ALTER TABLE [dbo].[Period]  WITH CHECK ADD  CONSTRAINT [FK_Periode_Promotion] FOREIGN KEY([Promotion_Id])
REFERENCES [dbo].[Promotion] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Periode_Promotion]') AND parent_object_id = OBJECT_ID(N'[dbo].[Period]'))
ALTER TABLE [dbo].[Period] CHECK CONSTRAINT [FK_Periode_Promotion]
GO
/****** Object:  ForeignKey [FK_Droit_Campus]    Script Date: 04/28/2010 13:03:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Droit_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Right]'))
ALTER TABLE [dbo].[Right]  WITH CHECK ADD  CONSTRAINT [FK_Droit_Campus] FOREIGN KEY([Campus_Id])
REFERENCES [dbo].[Campus] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Droit_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Right]'))
ALTER TABLE [dbo].[Right] CHECK CONSTRAINT [FK_Droit_Campus]
GO
/****** Object:  ForeignKey [FK_Droit_Utilisateur]    Script Date: 04/28/2010 13:03:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Droit_Utilisateur]') AND parent_object_id = OBJECT_ID(N'[dbo].[Right]'))
ALTER TABLE [dbo].[Right]  WITH CHECK ADD  CONSTRAINT [FK_Droit_Utilisateur] FOREIGN KEY([User_Id])
REFERENCES [dbo].[User] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Droit_Utilisateur]') AND parent_object_id = OBJECT_ID(N'[dbo].[Right]'))
ALTER TABLE [dbo].[Right] CHECK CONSTRAINT [FK_Droit_Utilisateur]
GO
/****** Object:  ForeignKey [FK_Utilisateur_Classe]    Script Date: 04/28/2010 13:03:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Utilisateur_Classe]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_Utilisateur_Classe] FOREIGN KEY([Class_Id])
REFERENCES [dbo].[Class] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Utilisateur_Classe]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_Utilisateur_Classe]
GO
