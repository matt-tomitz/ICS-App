CREATE TABLE [dbo].[Contacts] (
    [AccountId]   INT           IDENTITY (1, 1) NOT NULL,
    [Id]          INT           NULL,
    [FirstName]   NVARCHAR (50) NULL,
    [LastName]    NVARCHAR (50) NULL,
    [Address]     NVARCHAR (50) NULL,
    [PhoneNumber] NVARCHAR (50) NULL,
    [DateCreated] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([AccountId] ASC)
);

