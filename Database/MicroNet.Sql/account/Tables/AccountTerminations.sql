CREATE TABLE [account].[AccountTerminations] (
    [Id]                              UNIQUEIDENTIFIER NOT NULL,
    [TerminatedAccount_AccountNumber] NVARCHAR (MAX)   NOT NULL,
    [TerminatedAccount_AccountName]   NVARCHAR (MAX)   NOT NULL,
    [TerminatedAccount_Balance]       DECIMAL (18, 2)  NOT NULL,
    [TerminatedAccount_BranchName]    NVARCHAR (MAX)   NOT NULL,
    [Reason]                          NVARCHAR (MAX)   NOT NULL,
    [AuditInfo_CreatedBy]             NVARCHAR (MAX)   NOT NULL,
    [AuditInfo_CreatedAt]             DATETIME2 (7)    NOT NULL,
    [AuditInfo_UpdatedBy]             NVARCHAR (MAX)   NOT NULL,
    [AuditInfo_UpdatedAt]             DATETIME2 (7)    NOT NULL,
    [AuditInfo_DeletedBy]             NVARCHAR (MAX)   NOT NULL,
    [AuditInfo_DeletedAt]             DATETIME2 (7)    NOT NULL,
    [AuditInfo_ApprovedBy]            NVARCHAR (MAX)   NULL,
    [AuditInfo_ApprovedAt]            DATETIME2 (7)    NULL,
    [Version]                         INT              NOT NULL,
    CONSTRAINT [PK_AccountTerminations] PRIMARY KEY CLUSTERED ([Id] ASC)
);

