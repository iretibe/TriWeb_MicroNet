CREATE TABLE [loan].[DomainEventLogs] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [EventType]       NVARCHAR (MAX)   NOT NULL,
    [Payload]         NVARCHAR (MAX)   NOT NULL,
    [AggregateId]     UNIQUEIDENTIFIER NOT NULL,
    [AggregateType]   NVARCHAR (MAX)   NOT NULL,
    [OccurredAt]      DATETIME2 (7)    NOT NULL,
    [ErrorMessage]    NVARCHAR (MAX)   NULL,
    [Retries]         INT              NOT NULL,
    [LastAttemptedAt] DATETIME2 (7)    NOT NULL,
    [IsPublished]     BIT              NOT NULL,
    CONSTRAINT [PK_DomainEventLogs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

