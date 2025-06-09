﻿CREATE TABLE [dbo].[PushedAuthorizationRequests] (
    [Id]                 BIGINT         IDENTITY (1, 1) NOT NULL,
    [ReferenceValueHash] NVARCHAR (64)  NOT NULL,
    [ExpiresAtUtc]       DATETIME2 (7)  NOT NULL,
    [Parameters]         NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_PushedAuthorizationRequests] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_PushedAuthorizationRequests_ExpiresAtUtc]
    ON [dbo].[PushedAuthorizationRequests]([ExpiresAtUtc] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PushedAuthorizationRequests_ReferenceValueHash]
    ON [dbo].[PushedAuthorizationRequests]([ReferenceValueHash] ASC);

