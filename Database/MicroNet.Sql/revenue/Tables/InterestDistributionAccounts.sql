CREATE TABLE [revenue].[InterestDistributionAccounts] (
    [InterestDistributionId] UNIQUEIDENTIFIER NOT NULL,
    [Id]                     INT              IDENTITY (1, 1) NOT NULL,
    [AccountNumber]          NVARCHAR (20)    NOT NULL,
    [ShareAmount]            DECIMAL (18, 2)  NOT NULL,
    CONSTRAINT [PK_InterestDistributionAccounts] PRIMARY KEY CLUSTERED ([InterestDistributionId] ASC, [Id] ASC),
    CONSTRAINT [FK_InterestDistributionAccounts_InterestDistributions_InterestDistributionId] FOREIGN KEY ([InterestDistributionId]) REFERENCES [revenue].[InterestDistributions] ([Id]) ON DELETE CASCADE
);

