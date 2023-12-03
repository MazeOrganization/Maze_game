CREATE TABLE [dbo].[Maze] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Size] INT NOT NULL,
    [BoardData]           NTEXT  NOT NULL,
    CONSTRAINT [PK_Maze] PRIMARY KEY CLUSTERED ([Id] ASC)
);

