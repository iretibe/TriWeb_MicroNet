CREATE TABLE [user].[DomainEventLogs] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [EventType]       NVARCHAR (250)   NOT NULL,
    [Payload]         NVARCHAR (MAX)   NOT NULL,
    [AggregateId]     UNIQUEIDENTIFIER NOT NULL,
    [AggregateType]   NVARCHAR (100)   NOT NULL,
    [OccurredAt]      DATETIME2 (7)    NOT NULL,
    [ErrorMessage]    NVARCHAR (MAX)   NULL,
    [Retries]         INT              NOT NULL,
    [LastAttemptedAt] DATETIME2 (7)    NOT NULL,
    [IsPublished]     BIT              NOT NULL,
    CONSTRAINT [PK_DomainEventLogs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

