CREATE TABLE [revenue].[Transactions] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [AccountNumber]   NVARCHAR (MAX)   NOT NULL,
    [AccountName]     NVARCHAR (MAX)   NOT NULL,
    [Amount]          DECIMAL (18, 2)  NOT NULL,
    [Reference]       NVARCHAR (200)   NOT NULL,
    [IdType]          NVARCHAR (MAX)   NOT NULL,
    [IdNumber]        NVARCHAR (MAX)   NOT NULL,
    [DepositorName]   NVARCHAR (200)   NOT NULL,
    [DestinationType] NVARCHAR (200)   NOT NULL,
    [CreatedBy]       NVARCHAR (MAX)   NULL,
    [CreatedAt]       DATETIME2 (7)    NULL,
    [UpdatedBy]       NVARCHAR (MAX)   NULL,
    [UpdatedAt]       DATETIME2 (7)    NULL,
    [DeletedBy]       NVARCHAR (MAX)   NULL,
    [DeletedAt]       DATETIME2 (7)    NULL,
    [TransactionId]   UNIQUEIDENTIFIER NULL,
    [Version]         INT              NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Transactions_Transactions_TransactionId] FOREIGN KEY ([TransactionId]) REFERENCES [revenue].[Transactions] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Transactions_TransactionId]
    ON [revenue].[Transactions]([TransactionId] ASC);

