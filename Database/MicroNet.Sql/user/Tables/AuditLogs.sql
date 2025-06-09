CREATE TABLE [user].[AuditLogs] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [AuditDate]  DATETIME2 (7)    NOT NULL,
    [UserId]     NVARCHAR (100)   NOT NULL,
    [Data]       NVARCHAR (MAX)   NOT NULL,
    [Method]     NVARCHAR (10)    NOT NULL,
    [EntityType] NVARCHAR (100)   NOT NULL,
    [Version]    INT              NOT NULL,
    CONSTRAINT [PK_AuditLogs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

