CREATE TABLE [product].[Loans] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [LoanCode]            NVARCHAR (20)    NOT NULL,
    [LoanName]            NVARCHAR (100)   NOT NULL,
    [MaximumAmount]       DECIMAL (18, 2)  NOT NULL,
    [PercentageOfSavings] DECIMAL (18, 2)  NOT NULL,
    [InterestRate]        DECIMAL (18, 2)  NOT NULL,
    [RepaymentPeriod]     INT              NOT NULL,
    [GracePeriod]         INT              NOT NULL,
    [CreatedBy]           NVARCHAR (MAX)   NULL,
    [CreatedAt]           DATETIME2 (7)    NULL,
    [UpdatedBy]           NVARCHAR (MAX)   NULL,
    [UpdatedAt]           DATETIME2 (7)    NULL,
    [DeletedBy]           NVARCHAR (MAX)   NULL,
    [DeletedAt]           DATETIME2 (7)    NULL,
    [Version]             INT              NOT NULL,
    CONSTRAINT [PK_Loans] PRIMARY KEY CLUSTERED ([Id] ASC)
);

