CREATE TABLE [loan].[LoanDocument] (
    [LoanRequestId] UNIQUEIDENTIFIER NOT NULL,
    [Id]            INT              IDENTITY (1, 1) NOT NULL,
    [FileName]      NVARCHAR (MAX)   NOT NULL,
    [FileUrl]       NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_LoanDocument] PRIMARY KEY CLUSTERED ([LoanRequestId] ASC, [Id] ASC),
    CONSTRAINT [FK_LoanDocument_LoanRequests_LoanRequestId] FOREIGN KEY ([LoanRequestId]) REFERENCES [loan].[LoanRequests] ([Id]) ON DELETE CASCADE
);

