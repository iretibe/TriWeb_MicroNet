CREATE TABLE [branch].[BranchTerminationRules] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [Code]                NVARCHAR (MAX)   NOT NULL,
    [Name]                NVARCHAR (MAX)   NOT NULL,
    [Description]         NVARCHAR (MAX)   NOT NULL,
    [Notes]               NVARCHAR (MAX)   NULL,
    [AuditInfo_CreatedBy] NVARCHAR (MAX)   NULL,
    [AuditInfo_CreatedAt] DATETIME2 (7)    NULL,
    [AuditInfo_UpdatedBy] NVARCHAR (MAX)   NULL,
    [AuditInfo_UpdatedAt] DATETIME2 (7)    NULL,
    [AuditInfo_DeletedBy] NVARCHAR (MAX)   NULL,
    [AuditInfo_DeletedAt] DATETIME2 (7)    NULL,
    [Version]             INT              NOT NULL,
    CONSTRAINT [PK_BranchTerminationRules] PRIMARY KEY CLUSTERED ([Id] ASC)
);

