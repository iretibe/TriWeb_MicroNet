CREATE TABLE [account].[AccountTransfers] (
    [Id]                          UNIQUEIDENTIFIER NOT NULL,
    [SourceAccount_AccountNumber] NVARCHAR (MAX)   NOT NULL,
    [SourceAccount_AccountName]   NVARCHAR (MAX)   NOT NULL,
    [SourceAccount_Balance]       DECIMAL (18, 2)  NOT NULL,
    [SourceAccount_BranchName]    NVARCHAR (MAX)   NOT NULL,
    [DestinationBranch]           NVARCHAR (MAX)   NOT NULL,
    [AuditInfo_CreatedBy]         NVARCHAR (MAX)   NOT NULL,
    [AuditInfo_CreatedAt]         DATETIME2 (7)    NOT NULL,
    [AuditInfo_UpdatedBy]         NVARCHAR (MAX)   NOT NULL,
    [AuditInfo_UpdatedAt]         DATETIME2 (7)    NOT NULL,
    [AuditInfo_DeletedBy]         NVARCHAR (MAX)   NOT NULL,
    [AuditInfo_DeletedAt]         DATETIME2 (7)    NOT NULL,
    [AuditInfo_ApprovedBy]        NVARCHAR (MAX)   NULL,
    [AuditInfo_ApprovedAt]        DATETIME2 (7)    NULL,
    [Version]                     INT              NOT NULL,
    CONSTRAINT [PK_AccountTransfers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

