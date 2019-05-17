CREATE TABLE [dbo].[UserTable] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [UserName]     NVARCHAR (50) NULL,
    [PasswordHash] NVARCHAR (50) NULL,
    [DateCreated]  DATETIME      DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_UserTable] PRIMARY KEY CLUSTERED ([Id] ASC)
);

