CREATE TABLE [branch].[ProductSummary] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [NumberOfLoans]    INT              NOT NULL,
    [TotalLoanAmount]  DECIMAL (18, 2)  NOT NULL,
    [TotalInterest]    DECIMAL (18, 2)  NOT NULL,
    [TotalRepayment]   DECIMAL (18, 2)  NOT NULL,
    [ProcessingFees]   DECIMAL (18, 2)  NOT NULL,
    [PenaltyCharges]   DECIMAL (18, 2)  NOT NULL,
    [TotalLoanBalance] DECIMAL (18, 2)  NOT NULL,
    [ProductAmount]    DECIMAL (18, 2)  NOT NULL,
    [Interest]         DECIMAL (18, 2)  NOT NULL,
    [Withdrawal]       DECIMAL (18, 2)  NOT NULL,
    [ManagementFees]   DECIMAL (18, 2)  NOT NULL,
    [Balance]          DECIMAL (18, 2)  NOT NULL,
    [BranchId]         UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ProductSummary] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductSummary_Branches_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [branch].[Branches] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ProductSummary_BranchId]
    ON [branch].[ProductSummary]([BranchId] ASC);

