CREATE TABLE [revenue].[InterestDistributions] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [StartDate]     DATETIME2 (7)    NOT NULL,
    [EndDate]       DATETIME2 (7)    NOT NULL,
    [TotalInterest] DECIMAL (18, 2)  NOT NULL,
    [DistributedAt] DATETIME2 (7)    NOT NULL,
    [CreatedBy]     NVARCHAR (MAX)   NULL,
    [CreatedAt]     DATETIME2 (7)    NULL,
    [UpdatedBy]     NVARCHAR (MAX)   NULL,
    [UpdatedAt]     DATETIME2 (7)    NULL,
    [DeletedBy]     NVARCHAR (MAX)   NULL,
    [DeletedAt]     DATETIME2 (7)    NULL,
    [Version]       INT              NOT NULL,
    CONSTRAINT [PK_InterestDistributions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

