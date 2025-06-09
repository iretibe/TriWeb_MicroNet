CREATE TABLE [dbo].[ActiveSessions] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [UserId]           NVARCHAR (MAX)   NOT NULL,
    [SessionId]        NVARCHAR (MAX)   NOT NULL,
    [LoginTime]        DATETIME2 (7)    NOT NULL,
    [LastActivityTime] DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_ActiveSessions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

