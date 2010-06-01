
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 06/01/2010 14:47:04
-- Generated from EDMX file: C:\Users\Romaric\Documents\Visual Studio 2010\Projects\tte-knbc\DataAccessLayer\TteDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [tte-bcdkn];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Class_Period]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK_Class_Period];
GO
IF OBJECT_ID(N'[dbo].[FK_Class_PeriodPlanning]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK_Class_PeriodPlanning];
GO
IF OBJECT_ID(N'[dbo].[FK_Class_Planning]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK_Class_Planning];
GO
IF OBJECT_ID(N'[dbo].[FK_Event_Owner]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Owner];
GO
IF OBJECT_ID(N'[dbo].[FK_Event_OwnerPlanning]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_OwnerPlanning];
GO
IF OBJECT_ID(N'[dbo].[FK_Event_Planning]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Planning];
GO
IF OBJECT_ID(N'[dbo].[FK_Event_Speaker]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Speaker];
GO
IF OBJECT_ID(N'[dbo].[FK_Event_SpeakerPlaning]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_SpeakerPlaning];
GO
IF OBJECT_ID(N'[dbo].[FK_Event_Subject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Subject];
GO
IF OBJECT_ID(N'[dbo].[FK_Modality_Subject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Modality] DROP CONSTRAINT [FK_Modality_Subject];
GO
IF OBJECT_ID(N'[dbo].[FK_Period_Planning]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Period] DROP CONSTRAINT [FK_Period_Planning];
GO
IF OBJECT_ID(N'[dbo].[FK_Plannings_Parent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Planning] DROP CONSTRAINT [FK_Plannings_Parent];
GO
IF OBJECT_ID(N'[dbo].[FK_Right_Campus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Right] DROP CONSTRAINT [FK_Right_Campus];
GO
IF OBJECT_ID(N'[dbo].[FK_Right_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Right] DROP CONSTRAINT [FK_Right_User];
GO
IF OBJECT_ID(N'[dbo].[FK_User_Planing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Planing];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Class]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Class];
GO
IF OBJECT_ID(N'[dbo].[Event]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Event];
GO
IF OBJECT_ID(N'[dbo].[Modality]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Modality];
GO
IF OBJECT_ID(N'[dbo].[Period]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Period];
GO
IF OBJECT_ID(N'[dbo].[Planning]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Planning];
GO
IF OBJECT_ID(N'[dbo].[Right]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Right];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Class'
CREATE TABLE [dbo].[Class] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Period] int  NOT NULL
);
GO

-- Creating table 'Event'
CREATE TABLE [dbo].[Event] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Planning] int  NOT NULL,
    [Start] datetime  NOT NULL,
    [End] datetime  NOT NULL,
    [Mandatory] bit  NOT NULL,
    [Place] varchar(50)  NULL,
    [Speaker] int  NULL,
    [Owner] int  NOT NULL,
    [Subject] int  NULL
);
GO

-- Creating table 'Modality'
CREATE TABLE [dbo].[Modality] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varbinary(50)  NOT NULL,
    [Subject] int  NULL
);
GO

-- Creating table 'Period'
CREATE TABLE [dbo].[Period] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Start] datetime  NOT NULL,
    [End] datetime  NOT NULL
);
GO

-- Creating table 'Planning'
CREATE TABLE [dbo].[Planning] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Parent] int  NULL,
    [LastChange] datetime  NOT NULL,
    [Type] int  NOT NULL
);
GO

-- Creating table 'Right'
CREATE TABLE [dbo].[Right] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [User] int  NOT NULL,
    [Campus] int  NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Class'
ALTER TABLE [dbo].[Class]
ADD CONSTRAINT [PK_Class]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Event'
ALTER TABLE [dbo].[Event]
ADD CONSTRAINT [PK_Event]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Modality'
ALTER TABLE [dbo].[Modality]
ADD CONSTRAINT [PK_Modality]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Period'
ALTER TABLE [dbo].[Period]
ADD CONSTRAINT [PK_Period]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Planning'
ALTER TABLE [dbo].[Planning]
ADD CONSTRAINT [PK_Planning]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Right'
ALTER TABLE [dbo].[Right]
ADD CONSTRAINT [PK_Right]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Period] in table 'Class'
ALTER TABLE [dbo].[Class]
ADD CONSTRAINT [FK_Class_PeriodPlanning]
    FOREIGN KEY ([Period])
    REFERENCES [dbo].[Planning]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Class_PeriodPlanning'
CREATE INDEX [IX_FK_Class_PeriodPlanning]
ON [dbo].[Class]
    ([Period]);
GO

-- Creating foreign key on [Id] in table 'Class'
ALTER TABLE [dbo].[Class]
ADD CONSTRAINT [FK_Class_Planning]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Planning]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Owner] in table 'Event'
ALTER TABLE [dbo].[Event]
ADD CONSTRAINT [FK_Event_OwnerPlanning]
    FOREIGN KEY ([Owner])
    REFERENCES [dbo].[Planning]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Event_OwnerPlanning'
CREATE INDEX [IX_FK_Event_OwnerPlanning]
ON [dbo].[Event]
    ([Owner]);
GO

-- Creating foreign key on [Planning] in table 'Event'
ALTER TABLE [dbo].[Event]
ADD CONSTRAINT [FK_Event_Planning]
    FOREIGN KEY ([Planning])
    REFERENCES [dbo].[Planning]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Event_Planning'
CREATE INDEX [IX_FK_Event_Planning]
ON [dbo].[Event]
    ([Planning]);
GO

-- Creating foreign key on [Speaker] in table 'Event'
ALTER TABLE [dbo].[Event]
ADD CONSTRAINT [FK_Event_SpeakerPlaning]
    FOREIGN KEY ([Speaker])
    REFERENCES [dbo].[Planning]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Event_SpeakerPlaning'
CREATE INDEX [IX_FK_Event_SpeakerPlaning]
ON [dbo].[Event]
    ([Speaker]);
GO

-- Creating foreign key on [Subject] in table 'Event'
ALTER TABLE [dbo].[Event]
ADD CONSTRAINT [FK_Event_Subject]
    FOREIGN KEY ([Subject])
    REFERENCES [dbo].[Modality]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Event_Subject'
CREATE INDEX [IX_FK_Event_Subject]
ON [dbo].[Event]
    ([Subject]);
GO

-- Creating foreign key on [Subject] in table 'Modality'
ALTER TABLE [dbo].[Modality]
ADD CONSTRAINT [FK_Modality_Subject]
    FOREIGN KEY ([Subject])
    REFERENCES [dbo].[Modality]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Modality_Subject'
CREATE INDEX [IX_FK_Modality_Subject]
ON [dbo].[Modality]
    ([Subject]);
GO

-- Creating foreign key on [Id] in table 'Period'
ALTER TABLE [dbo].[Period]
ADD CONSTRAINT [FK_Period_Planning]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Planning]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Parent] in table 'Planning'
ALTER TABLE [dbo].[Planning]
ADD CONSTRAINT [FK_Plannings_Parent]
    FOREIGN KEY ([Parent])
    REFERENCES [dbo].[Planning]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Plannings_Parent'
CREATE INDEX [IX_FK_Plannings_Parent]
ON [dbo].[Planning]
    ([Parent]);
GO

-- Creating foreign key on [Campus] in table 'Right'
ALTER TABLE [dbo].[Right]
ADD CONSTRAINT [FK_Right_Campus]
    FOREIGN KEY ([Campus])
    REFERENCES [dbo].[Planning]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Right_Campus'
CREATE INDEX [IX_FK_Right_Campus]
ON [dbo].[Right]
    ([Campus]);
GO

-- Creating foreign key on [Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [FK_User_Planing]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Planning]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [User] in table 'Right'
ALTER TABLE [dbo].[Right]
ADD CONSTRAINT [FK_Right_User]
    FOREIGN KEY ([User])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Right_User'
CREATE INDEX [IX_FK_Right_User]
ON [dbo].[Right]
    ([User]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------