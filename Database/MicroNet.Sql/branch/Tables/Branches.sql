CREATE TABLE [branch].[Branches] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [BranchCode]          NVARCHAR (20)    NOT NULL,
    [BranchName]          NVARCHAR (100)   NOT NULL,
    [Region]              NVARCHAR (50)    NOT NULL,
    [SetupDate]           DATETIME2 (7)    NOT NULL,
    [Street]              NVARCHAR (100)   NOT NULL,
    [CreatedBy]           NVARCHAR (MAX)   NULL,
    [UpdatedBy]           NVARCHAR (MAX)   NULL,
    [AuditInfo_DeletedAt] DATETIME2 (7)    NULL,
    [AuditInfo_DeletedBy] NVARCHAR (MAX)   NULL,
    [CreatedAt]           DATETIME2 (7)    NULL,
    [ManagerName]         NVARCHAR (100)   DEFAULT (N'') NOT NULL,
    [PostalAddress]       NVARCHAR (50)    NULL,
    [UpdatedAt]           DATETIME2 (7)    NULL,
    [Version]             INT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Branches] PRIMARY KEY CLUSTERED ([Id] ASC)
);

