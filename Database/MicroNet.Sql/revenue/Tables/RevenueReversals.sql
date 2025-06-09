CREATE TABLE [revenue].[RevenueReversals] (
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [OriginalTransactionId] UNIQUEIDENTIFIER NOT NULL,
    [Amount]                DECIMAL (18, 2)  NOT NULL,
    [ReversedAt]            DATETIME2 (7)    NOT NULL,
    [Reason_Code]           NVARCHAR (20)    NOT NULL,
    [Reason_Description]    NVARCHAR (100)   NOT NULL,
    [ReversedBy]            NVARCHAR (100)   NOT NULL,
    [CreatedBy]             NVARCHAR (MAX)   NULL,
    [CreatedAt]             DATETIME2 (7)    NULL,
    [UpdatedBy]             NVARCHAR (MAX)   NULL,
    [UpdatedAt]             DATETIME2 (7)    NULL,
    [DeletedBy]             NVARCHAR (MAX)   NULL,
    [DeletedAt]             DATETIME2 (7)    NULL,
    [Version]               INT              NOT NULL,
    CONSTRAINT [PK_RevenueReversals] PRIMARY KEY CLUSTERED ([Id] ASC)
);

