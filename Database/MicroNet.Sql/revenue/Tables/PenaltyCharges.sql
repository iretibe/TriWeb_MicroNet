CREATE TABLE [revenue].[PenaltyCharges] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [AccountNumber]      NVARCHAR (20)    NOT NULL,
    [Amount]             DECIMAL (18, 2)  NOT NULL,
    [Reason_Code]        NVARCHAR (20)    NOT NULL,
    [Reason_Description] NVARCHAR (100)   NOT NULL,
    [ChargedAt]          DATETIME2 (7)    NOT NULL,
    [CreatedBy]          NVARCHAR (MAX)   NULL,
    [CreatedAt]          DATETIME2 (7)    NULL,
    [UpdatedBy]          NVARCHAR (MAX)   NULL,
    [UpdatedAt]          DATETIME2 (7)    NULL,
    [DeletedBy]          NVARCHAR (MAX)   NULL,
    [DeletedAt]          DATETIME2 (7)    NULL,
    [Version]            INT              NOT NULL,
    CONSTRAINT [PK_PenaltyCharges] PRIMARY KEY CLUSTERED ([Id] ASC)
);

