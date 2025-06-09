CREATE TABLE [revenue].[ManagementFees] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [AccountNumber]    NVARCHAR (20)    NOT NULL,
    [Fee_Type]         NVARCHAR (50)    NOT NULL,
    [Fee_RateOrAmount] DECIMAL (18, 2)  NOT NULL,
    [Fee_Frequency]    NVARCHAR (50)    NOT NULL,
    [CalculatedAmount] DECIMAL (18, 2)  NOT NULL,
    [ChargedAt]        DATETIME2 (7)    NOT NULL,
    [CreatedBy]        NVARCHAR (MAX)   NULL,
    [CreatedAt]        DATETIME2 (7)    NULL,
    [UpdatedBy]        NVARCHAR (MAX)   NULL,
    [UpdatedAt]        DATETIME2 (7)    NULL,
    [DeletedBy]        NVARCHAR (MAX)   NULL,
    [DeletedAt]        DATETIME2 (7)    NULL,
    [Version]          INT              NOT NULL,
    CONSTRAINT [PK_ManagementFees] PRIMARY KEY CLUSTERED ([Id] ASC)
);

