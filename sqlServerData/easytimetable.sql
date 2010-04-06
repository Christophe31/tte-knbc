-- Romain cheynet de beauprès, Romaric, Structure de la DB SQLServeur 2008
/****** Object:  ForeignKey [FK_Droit_Campus]    Script Date: 04/06/2010 15:18:14 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Droit_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Droit]'))
ALTER TABLE [dbo].[Droit] DROP CONSTRAINT [FK_Droit_Campus]
GO
/****** Object:  ForeignKey [FK_Droit_Utilisateur]    Script Date: 04/06/2010 15:18:14 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Droit_Utilisateur]') AND parent_object_id = OBJECT_ID(N'[dbo].[Droit]'))
ALTER TABLE [dbo].[Droit] DROP CONSTRAINT [FK_Droit_Utilisateur]
GO
/****** Object:  ForeignKey [FK_Evenement_Campus]    Script Date: 04/06/2010 15:18:14 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evenement]'))
ALTER TABLE [dbo].[Evenement] DROP CONSTRAINT [FK_Evenement_Campus]
GO
/****** Object:  ForeignKey [FK_Evenement_Matiere]    Script Date: 04/06/2010 15:18:14 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Matiere]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evenement]'))
ALTER TABLE [dbo].[Evenement] DROP CONSTRAINT [FK_Evenement_Matiere]
GO
/****** Object:  ForeignKey [FK_Evenement_Periode]    Script Date: 04/06/2010 15:18:14 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Periode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evenement]'))
ALTER TABLE [dbo].[Evenement] DROP CONSTRAINT [FK_Evenement_Periode]
GO
/****** Object:  ForeignKey [FK_Evenement_Utilisateur]    Script Date: 04/06/2010 15:18:14 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Utilisateur]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evenement]'))
ALTER TABLE [dbo].[Evenement] DROP CONSTRAINT [FK_Evenement_Utilisateur]
GO
/****** Object:  ForeignKey [FK_Evenement_Utilisateur1]    Script Date: 04/06/2010 15:18:14 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Utilisateur1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evenement]'))
ALTER TABLE [dbo].[Evenement] DROP CONSTRAINT [FK_Evenement_Utilisateur1]
GO
/****** Object:  ForeignKey [FK_Periode_Promotion]    Script Date: 04/06/2010 15:18:14 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Periode_Promotion]') AND parent_object_id = OBJECT_ID(N'[dbo].[Periode]'))
ALTER TABLE [dbo].[Periode] DROP CONSTRAINT [FK_Periode_Promotion]
GO
/****** Object:  ForeignKey [FK_Utilisateur_Campus]    Script Date: 04/06/2010 15:18:14 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Utilisateur_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Utilisateur]'))
ALTER TABLE [dbo].[Utilisateur] DROP CONSTRAINT [FK_Utilisateur_Campus]
GO
/****** Object:  ForeignKey [FK_Utilisateur_Promotion]    Script Date: 04/06/2010 15:18:14 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Utilisateur_Promotion]') AND parent_object_id = OBJECT_ID(N'[dbo].[Utilisateur]'))
ALTER TABLE [dbo].[Utilisateur] DROP CONSTRAINT [FK_Utilisateur_Promotion]
GO
/****** Object:  Table [dbo].[Evenement]    Script Date: 04/06/2010 15:18:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Evenement]') AND type in (N'U'))
DROP TABLE [dbo].[Evenement]
GO
/****** Object:  Table [dbo].[Periode]    Script Date: 04/06/2010 15:18:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Periode]') AND type in (N'U'))
DROP TABLE [dbo].[Periode]
GO
/****** Object:  Table [dbo].[Droit]    Script Date: 04/06/2010 15:18:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Droit]') AND type in (N'U'))
DROP TABLE [dbo].[Droit]
GO
/****** Object:  Table [dbo].[Utilisateur]    Script Date: 04/06/2010 15:18:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Utilisateur]') AND type in (N'U'))
DROP TABLE [dbo].[Utilisateur]
GO
/****** Object:  Table [dbo].[Matiere]    Script Date: 04/06/2010 15:18:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Matiere]') AND type in (N'U'))
DROP TABLE [dbo].[Matiere]
GO
/****** Object:  Table [dbo].[Campus]    Script Date: 04/06/2010 15:18:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Campus]') AND type in (N'U'))
DROP TABLE [dbo].[Campus]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 04/06/2010 15:18:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Promotion]') AND type in (N'U'))
DROP TABLE [dbo].[Promotion]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 04/06/2010 15:18:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Promotion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Promotion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [nvarchar](50) COLLATE French_CI_AS NOT NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Campus]    Script Date: 04/06/2010 15:18:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Campus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Campus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [nvarchar](50) COLLATE French_CI_AS NOT NULL,
 CONSTRAINT [PK_Campus] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Matiere]    Script Date: 04/06/2010 15:18:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Matiere]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Matiere](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[Nbre_h] [int] NOT NULL,
 CONSTRAINT [PK_Matiere] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Utilisateur]    Script Date: 04/06/2010 15:18:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Utilisateur]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Utilisateur](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [nvarchar](50) COLLATE French_CI_AS NOT NULL,
	[Promotion] [int] NULL,
	[Campus] [int] NULL,
	[Password] [varchar](50) COLLATE French_CI_AS NOT NULL,
 CONSTRAINT [PK_Utilisateur] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Droit]    Script Date: 04/06/2010 15:18:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Droit]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Droit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[Utilisateur] [int] NOT NULL,
	[Campus] [int] NULL,
 CONSTRAINT [PK_Droit] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Periode]    Script Date: 04/06/2010 15:18:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Periode]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Periode](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) COLLATE French_CI_AS NOT NULL,
	[Promo] [int] NOT NULL,
	[Debut] [datetime] NOT NULL,
	[Fin] [datetime] NOT NULL,
 CONSTRAINT [PK_Periode] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Evenement]    Script Date: 04/06/2010 15:18:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Evenement]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Evenement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Createur] [int] NOT NULL,
	[Intervenant] [int] NULL,
	[Campus] [int] NULL,
	[Periode] [int] NULL,
	[Matiere] [int] NULL,
	[Type] [nvarchar](50) COLLATE French_CI_AS NULL,
	[Lieu] [nvarchar](50) COLLATE French_CI_AS NULL,
	[Nom] [nvarchar](50) COLLATE French_CI_AS NOT NULL,
	[Debut] [datetime] NOT NULL,
	[Fin] [datetime] NOT NULL,
	[Obligatoire] [bit] NOT NULL,
 CONSTRAINT [PK_Evenement] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  ForeignKey [FK_Droit_Campus]    Script Date: 04/06/2010 15:18:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Droit_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Droit]'))
ALTER TABLE [dbo].[Droit]  WITH CHECK ADD  CONSTRAINT [FK_Droit_Campus] FOREIGN KEY([Campus])
REFERENCES [dbo].[Campus] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Droit_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Droit]'))
ALTER TABLE [dbo].[Droit] CHECK CONSTRAINT [FK_Droit_Campus]
GO
/****** Object:  ForeignKey [FK_Droit_Utilisateur]    Script Date: 04/06/2010 15:18:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Droit_Utilisateur]') AND parent_object_id = OBJECT_ID(N'[dbo].[Droit]'))
ALTER TABLE [dbo].[Droit]  WITH CHECK ADD  CONSTRAINT [FK_Droit_Utilisateur] FOREIGN KEY([Utilisateur])
REFERENCES [dbo].[Utilisateur] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Droit_Utilisateur]') AND parent_object_id = OBJECT_ID(N'[dbo].[Droit]'))
ALTER TABLE [dbo].[Droit] CHECK CONSTRAINT [FK_Droit_Utilisateur]
GO
/****** Object:  ForeignKey [FK_Evenement_Campus]    Script Date: 04/06/2010 15:18:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evenement]'))
ALTER TABLE [dbo].[Evenement]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Campus] FOREIGN KEY([Campus])
REFERENCES [dbo].[Campus] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evenement]'))
ALTER TABLE [dbo].[Evenement] CHECK CONSTRAINT [FK_Evenement_Campus]
GO
/****** Object:  ForeignKey [FK_Evenement_Matiere]    Script Date: 04/06/2010 15:18:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Matiere]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evenement]'))
ALTER TABLE [dbo].[Evenement]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Matiere] FOREIGN KEY([Matiere])
REFERENCES [dbo].[Matiere] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Matiere]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evenement]'))
ALTER TABLE [dbo].[Evenement] CHECK CONSTRAINT [FK_Evenement_Matiere]
GO
/****** Object:  ForeignKey [FK_Evenement_Periode]    Script Date: 04/06/2010 15:18:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Periode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evenement]'))
ALTER TABLE [dbo].[Evenement]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Periode] FOREIGN KEY([Periode])
REFERENCES [dbo].[Periode] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Periode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evenement]'))
ALTER TABLE [dbo].[Evenement] CHECK CONSTRAINT [FK_Evenement_Periode]
GO
/****** Object:  ForeignKey [FK_Evenement_Utilisateur]    Script Date: 04/06/2010 15:18:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Utilisateur]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evenement]'))
ALTER TABLE [dbo].[Evenement]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Utilisateur] FOREIGN KEY([Createur])
REFERENCES [dbo].[Utilisateur] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Utilisateur]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evenement]'))
ALTER TABLE [dbo].[Evenement] CHECK CONSTRAINT [FK_Evenement_Utilisateur]
GO
/****** Object:  ForeignKey [FK_Evenement_Utilisateur1]    Script Date: 04/06/2010 15:18:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Utilisateur1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evenement]'))
ALTER TABLE [dbo].[Evenement]  WITH CHECK ADD  CONSTRAINT [FK_Evenement_Utilisateur1] FOREIGN KEY([Intervenant])
REFERENCES [dbo].[Utilisateur] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Evenement_Utilisateur1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evenement]'))
ALTER TABLE [dbo].[Evenement] CHECK CONSTRAINT [FK_Evenement_Utilisateur1]
GO
/****** Object:  ForeignKey [FK_Periode_Promotion]    Script Date: 04/06/2010 15:18:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Periode_Promotion]') AND parent_object_id = OBJECT_ID(N'[dbo].[Periode]'))
ALTER TABLE [dbo].[Periode]  WITH CHECK ADD  CONSTRAINT [FK_Periode_Promotion] FOREIGN KEY([Promo])
REFERENCES [dbo].[Promotion] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Periode_Promotion]') AND parent_object_id = OBJECT_ID(N'[dbo].[Periode]'))
ALTER TABLE [dbo].[Periode] CHECK CONSTRAINT [FK_Periode_Promotion]
GO
/****** Object:  ForeignKey [FK_Utilisateur_Campus]    Script Date: 04/06/2010 15:18:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Utilisateur_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Utilisateur]'))
ALTER TABLE [dbo].[Utilisateur]  WITH CHECK ADD  CONSTRAINT [FK_Utilisateur_Campus] FOREIGN KEY([Campus])
REFERENCES [dbo].[Campus] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Utilisateur_Campus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Utilisateur]'))
ALTER TABLE [dbo].[Utilisateur] CHECK CONSTRAINT [FK_Utilisateur_Campus]
GO
/****** Object:  ForeignKey [FK_Utilisateur_Promotion]    Script Date: 04/06/2010 15:18:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Utilisateur_Promotion]') AND parent_object_id = OBJECT_ID(N'[dbo].[Utilisateur]'))
ALTER TABLE [dbo].[Utilisateur]  WITH CHECK ADD  CONSTRAINT [FK_Utilisateur_Promotion] FOREIGN KEY([Promotion])
REFERENCES [dbo].[Promotion] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Utilisateur_Promotion]') AND parent_object_id = OBJECT_ID(N'[dbo].[Utilisateur]'))
ALTER TABLE [dbo].[Utilisateur] CHECK CONSTRAINT [FK_Utilisateur_Promotion]
GO
