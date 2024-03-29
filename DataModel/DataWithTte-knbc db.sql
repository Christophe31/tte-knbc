USE [master]
GO
/****** Object:  Database [tte-knbc]    Script Date: 06/06/2010 03:45:52 ******/
CREATE DATABASE [tte-knbc] ON  PRIMARY 
( NAME = N'tte-knbc', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\tte-knbc.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'tte-knbc_log', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\tte-knbc_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 COLLATE French_CI_AS
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'tte-knbc', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [tte-knbc].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [tte-knbc] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [tte-knbc] SET ANSI_NULLS OFF
GO
ALTER DATABASE [tte-knbc] SET ANSI_PADDING OFF
GO
ALTER DATABASE [tte-knbc] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [tte-knbc] SET ARITHABORT OFF
GO
ALTER DATABASE [tte-knbc] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [tte-knbc] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [tte-knbc] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [tte-knbc] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [tte-knbc] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [tte-knbc] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [tte-knbc] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [tte-knbc] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [tte-knbc] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [tte-knbc] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [tte-knbc] SET  ENABLE_BROKER
GO
ALTER DATABASE [tte-knbc] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [tte-knbc] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [tte-knbc] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [tte-knbc] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [tte-knbc] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [tte-knbc] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [tte-knbc] SET  READ_WRITE
GO
ALTER DATABASE [tte-knbc] SET RECOVERY SIMPLE
GO
ALTER DATABASE [tte-knbc] SET  MULTI_USER
GO
ALTER DATABASE [tte-knbc] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [tte-knbc] SET DB_CHAINING OFF
GO
USE [tte-knbc]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 06/06/2010 03:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE French_CI_AS NOT NULL,
	[LastChange] [datetime] NOT NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should be unique.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promotion', @level2type=N'COLUMN',@level2name=N'Name'
GO
SET IDENTITY_INSERT [dbo].[Promotion] ON
INSERT [dbo].[Promotion] ([Id], [Name], [LastChange]) VALUES (5, N'2013', CAST(0x00009D6C00A53C7B AS DateTime))
SET IDENTITY_INSERT [dbo].[Promotion] OFF
/****** Object:  Table [dbo].[Campus]    Script Date: 06/06/2010 03:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Campus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE French_CI_AS NOT NULL,
	[LastChange] [datetime] NOT NULL,
 CONSTRAINT [PK_Campus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'should be unique.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Campus', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Campuses (ex:Toulouse)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Campus'
GO
SET IDENTITY_INSERT [dbo].[Campus] ON
INSERT [dbo].[Campus] ([Id], [Name], [LastChange]) VALUES (3, N'Toulouse', CAST(0x00009D6C00A584C0 AS DateTime))
INSERT [dbo].[Campus] ([Id], [Name], [LastChange]) VALUES (4, N'Bordeau', CAST(0x00009D7D010D4F5F AS DateTime))
INSERT [dbo].[Campus] ([Id], [Name], [LastChange]) VALUES (5, N'Paris', CAST(0x00009D7D010D5711 AS DateTime))
SET IDENTITY_INSERT [dbo].[Campus] OFF
/****** Object:  Table [dbo].[University]    Script Date: 06/06/2010 03:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[University](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastChange] [datetime] NOT NULL,
 CONSTRAINT [PK_University] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Last University Planing Change For Client Cache Management.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'University', @level2type=N'COLUMN',@level2name=N'LastChange'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'University entity.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'University'
GO
SET IDENTITY_INSERT [dbo].[University] ON
INSERT [dbo].[University] ([Id], [LastChange]) VALUES (1, CAST(0x00009CF100000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[University] OFF
/****** Object:  Table [dbo].[Subject]    Script Date: 06/06/2010 03:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Subject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[Hours] [int] NOT NULL,
	[Modality] [varchar](50) COLLATE French_CI_AS NOT NULL,
 CONSTRAINT [PK_Matiere] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Subject] ON
INSERT [dbo].[Subject] ([Id], [Name], [Hours], [Modality]) VALUES (1, N'Linux', 50, N'Presentiel')
INSERT [dbo].[Subject] ([Id], [Name], [Hours], [Modality]) VALUES (2, N'Linux', 10, N'elearning')
SET IDENTITY_INSERT [dbo].[Subject] OFF
/****** Object:  Table [dbo].[Event]    Script Date: 06/06/2010 03:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Creator] [int] NOT NULL,
	[Speaker] [int] NULL,
	[Campus] [int] NULL,
	[Period] [int] NULL,
	[Class] [int] NULL,
	[Subject] [int] NULL,
	[Place] [nvarchar](50) COLLATE French_CI_AS NULL,
	[Name] [nvarchar](50) COLLATE French_CI_AS NOT NULL,
	[Start] [datetime] NOT NULL,
	[End] [datetime] NOT NULL,
	[Mandatory] [bit] NOT NULL,
	[University] [bit] NOT NULL,
 CONSTRAINT [PK_Evenement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Event] ON
INSERT [dbo].[Event] ([Id], [Creator], [Speaker], [Campus], [Period], [Class], [Subject], [Place], [Name], [Start], [End], [Mandatory], [University]) VALUES (2, 11, 12, NULL, NULL, NULL, NULL, NULL, N'linux', CAST(0x00009E0B00000000 AS DateTime), CAST(0x00009E0B00D63BC0 AS DateTime), 0, 1)
INSERT [dbo].[Event] ([Id], [Creator], [Speaker], [Campus], [Period], [Class], [Subject], [Place], [Name], [Start], [End], [Mandatory], [University]) VALUES (9, 11, 11, NULL, NULL, NULL, NULL, NULL, N'Discour de bienvenue', CAST(0x00009E0B00D63BC0 AS DateTime), CAST(0x00009E0B00F73140 AS DateTime), 1, 1)
INSERT [dbo].[Event] ([Id], [Creator], [Speaker], [Campus], [Period], [Class], [Subject], [Place], [Name], [Start], [End], [Mandatory], [University]) VALUES (14, 13, 12, 3, NULL, NULL, 2, NULL, N'truc', CAST(0x00009E0B00C5C100 AS DateTime), CAST(0x00009E0B00D63BC0 AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[Event] OFF
/****** Object:  Table [dbo].[Right]    Script Date: 06/06/2010 03:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Right](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[User] [int] NOT NULL,
	[Campus] [int] NULL,
 CONSTRAINT [PK_Droit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Right] ON
INSERT [dbo].[Right] ([Id], [User], [Campus]) VALUES (1, 11, NULL)
INSERT [dbo].[Right] ([Id], [User], [Campus]) VALUES (2, 12, 3)
SET IDENTITY_INSERT [dbo].[Right] OFF
/****** Object:  Table [dbo].[Period]    Script Date: 06/06/2010 03:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Period](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[Promotion] [int] NOT NULL,
	[Start] [datetime] NOT NULL,
	[End] [datetime] NOT NULL,
	[LastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_Periode] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should be unique' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Period', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Represent time period in scolarity (Semester 1 2012)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Period'
GO
SET IDENTITY_INSERT [dbo].[Period] ON
INSERT [dbo].[Period] ([Id], [Name], [Promotion], [Start], [End], [LastUpdate]) VALUES (3, N'Semestre1', 5, CAST(0x000099C300000000 AS DateTime), CAST(0x00009B3100000000 AS DateTime), CAST(0x000099C300000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[Period] OFF
/****** Object:  Table [dbo].[Class]    Script Date: 06/06/2010 03:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Class](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[Campus] [int] NOT NULL,
	[Period] [int] NOT NULL,
	[LastChange] [datetime] NOT NULL,
 CONSTRAINT [PK_Classe] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Should be unique' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Class', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Classes (ex: B3A Toulouse)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Class'
GO
SET IDENTITY_INSERT [dbo].[Class] ON
INSERT [dbo].[Class] ([Id], [Name], [Campus], [Period], [LastChange]) VALUES (5, N'B1', 3, 3, CAST(0x000099C300000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[Class] OFF
/****** Object:  Table [dbo].[User]    Script Date: 06/06/2010 03:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE French_CI_AS NOT NULL,
	[Class] [int] NULL,
	[Password] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[LastChange] [datetime] NOT NULL,
 CONSTRAINT [PK_Utilisateur] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([Id], [Name], [Class], [Password], [LastChange]) VALUES (11, N'admin', 5, N'motdepasse', CAST(0x00009B3100000000 AS DateTime))
INSERT [dbo].[User] ([Id], [Name], [Class], [Password], [LastChange]) VALUES (12, N'bidulle', 5, N'pamplemousse', CAST(0x00009D760152EC1D AS DateTime))
INSERT [dbo].[User] ([Id], [Name], [Class], [Password], [LastChange]) VALUES (13, N'bidule', 5, N'cca10f7b4b65438913700acc9aeaeb43', CAST(0x00009D7D010D23E7 AS DateTime))
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  ForeignKey [FK_Evenement_Campus]    Script Date: 06/06/2010 03:45:54 ******/
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Campus] FOREIGN KEY([Campus])
REFERENCES [dbo].[Campus] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Evenement_Campus]
GO
/****** Object:  ForeignKey [FK_Evenement_Classe]    Script Date: 06/06/2010 03:45:54 ******/
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Classe] FOREIGN KEY([Class])
REFERENCES [dbo].[Class] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Evenement_Classe]
GO
/****** Object:  ForeignKey [FK_Evenement_Intervenant]    Script Date: 06/06/2010 03:45:54 ******/
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Intervenant] FOREIGN KEY([Speaker])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Evenement_Intervenant]
GO
/****** Object:  ForeignKey [FK_Evenement_Matiere]    Script Date: 06/06/2010 03:45:54 ******/
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Matiere] FOREIGN KEY([Subject])
REFERENCES [dbo].[Subject] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Evenement_Matiere]
GO
/****** Object:  ForeignKey [FK_Evenement_Periode]    Script Date: 06/06/2010 03:45:54 ******/
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Periode] FOREIGN KEY([Period])
REFERENCES [dbo].[Period] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Evenement_Periode]
GO
/****** Object:  ForeignKey [FK_Evenement_Proprietaire]    Script Date: 06/06/2010 03:45:54 ******/
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Proprietaire] FOREIGN KEY([Creator])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Evenement_Proprietaire]
GO
/****** Object:  ForeignKey [FK_Droit_Campus]    Script Date: 06/06/2010 03:45:54 ******/
ALTER TABLE [dbo].[Right]  WITH CHECK ADD  CONSTRAINT [FK_Droit_Campus] FOREIGN KEY([Campus])
REFERENCES [dbo].[Campus] ([Id])
GO
ALTER TABLE [dbo].[Right] CHECK CONSTRAINT [FK_Droit_Campus]
GO
/****** Object:  ForeignKey [FK_Droit_Utilisateur]    Script Date: 06/06/2010 03:45:54 ******/
ALTER TABLE [dbo].[Right]  WITH CHECK ADD  CONSTRAINT [FK_Droit_Utilisateur] FOREIGN KEY([User])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Right] CHECK CONSTRAINT [FK_Droit_Utilisateur]
GO
/****** Object:  ForeignKey [FK_Periode_Promotion]    Script Date: 06/06/2010 03:45:54 ******/
ALTER TABLE [dbo].[Period]  WITH CHECK ADD  CONSTRAINT [FK_Periode_Promotion] FOREIGN KEY([Promotion])
REFERENCES [dbo].[Promotion] ([Id])
GO
ALTER TABLE [dbo].[Period] CHECK CONSTRAINT [FK_Periode_Promotion]
GO
/****** Object:  ForeignKey [FK_Classe_Campus]    Script Date: 06/06/2010 03:45:54 ******/
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Classe_Campus] FOREIGN KEY([Campus])
REFERENCES [dbo].[Campus] ([Id])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Classe_Campus]
GO
/****** Object:  ForeignKey [FK_Classe_Periode]    Script Date: 06/06/2010 03:45:54 ******/
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Classe_Periode] FOREIGN KEY([Period])
REFERENCES [dbo].[Period] ([Id])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Classe_Periode]
GO
/****** Object:  ForeignKey [FK_Utilisateur_Classe]    Script Date: 06/06/2010 03:45:54 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_Utilisateur_Classe] FOREIGN KEY([Class])
REFERENCES [dbo].[Class] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_Utilisateur_Classe]
GO
