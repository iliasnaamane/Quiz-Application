
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/06/2017 00:57:30
-- Generated from EDMX file: C:\Users\Ilias\documents\visual studio 2017\Projects\quizapp\quizapp\Models\quizDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [quizDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_QuestionsQuiz]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionsSet] DROP CONSTRAINT [FK_QuestionsQuiz];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionsAnswers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnswersSet] DROP CONSTRAINT [FK_QuestionsAnswers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[QuizSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuizSet];
GO
IF OBJECT_ID(N'[dbo].[QuestionsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionsSet];
GO
IF OBJECT_ID(N'[dbo].[AnswersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnswersSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'QuizSet'
CREATE TABLE [dbo].[QuizSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [thematic] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'QuestionsSet'
CREATE TABLE [dbo].[QuestionsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [question] nvarchar(max)  NOT NULL,
    [Quiz_Id] int  NOT NULL
);
GO

-- Creating table 'AnswersSet'
CREATE TABLE [dbo].[AnswersSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [status] smallint  NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [Questions_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'QuizSet'
ALTER TABLE [dbo].[QuizSet]
ADD CONSTRAINT [PK_QuizSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionsSet'
ALTER TABLE [dbo].[QuestionsSet]
ADD CONSTRAINT [PK_QuestionsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AnswersSet'
ALTER TABLE [dbo].[AnswersSet]
ADD CONSTRAINT [PK_AnswersSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Quiz_Id] in table 'QuestionsSet'
ALTER TABLE [dbo].[QuestionsSet]
ADD CONSTRAINT [FK_QuestionsQuiz]
    FOREIGN KEY ([Quiz_Id])
    REFERENCES [dbo].[QuizSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionsQuiz'
CREATE INDEX [IX_FK_QuestionsQuiz]
ON [dbo].[QuestionsSet]
    ([Quiz_Id]);
GO

-- Creating foreign key on [Questions_Id] in table 'AnswersSet'
ALTER TABLE [dbo].[AnswersSet]
ADD CONSTRAINT [FK_QuestionsAnswers]
    FOREIGN KEY ([Questions_Id])
    REFERENCES [dbo].[QuestionsSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionsAnswers'
CREATE INDEX [IX_FK_QuestionsAnswers]
ON [dbo].[AnswersSet]
    ([Questions_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------