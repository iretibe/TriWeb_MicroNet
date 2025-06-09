CREATE TABLE [account].[Withdrawals] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [AccountNumber]        NVARCHAR (MAX)   NOT NULL,
    [Amount]               DECIMAL (18, 2)  NOT NULL,
    [PaymentMode]          NVARCHAR (MAX)   NOT NULL,
    [Reference]            NVARCHAR (MAX)   NOT NULL,
    [WalletNumber]         NVARCHAR (MAX)   NOT NULL,
    [Network]              NVARCHAR (MAX)   NOT NULL,
    [AuditInfo_CreatedBy]  NVARCHAR (MAX)   NOT NULL,
    [AuditInfo_CreatedAt]  DATETIME2 (7)    NOT NULL,
    [AuditInfo_UpdatedBy]  NVARCHAR (MAX)   NOT NULL,
    [AuditInfo_UpdatedAt]  DATETIME2 (7)    NOT NULL,
    [AuditInfo_DeletedBy]  NVARCHAR (MAX)   NOT NULL,
    [AuditInfo_DeletedAt]  DATETIME2 (7)    NOT NULL,
    [AuditInfo_ApprovedBy] NVARCHAR (MAX)   NULL,
    [AuditInfo_ApprovedAt] DATETIME2 (7)    NULL,
    [Version]              INT              NOT NULL,
    CONSTRAINT [PK_Withdrawals] PRIMARY KEY CLUSTERED ([Id] ASC)
);

