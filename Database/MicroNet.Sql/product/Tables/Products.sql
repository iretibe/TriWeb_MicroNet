CREATE TABLE [product].[Products] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ProductCode] NVARCHAR (20)    NOT NULL,
    [ProductName] NVARCHAR (100)   NOT NULL,
    [Description] NVARCHAR (MAX)   NOT NULL,
    [Note]        NVARCHAR (MAX)   NULL,
    [CreatedBy]   NVARCHAR (MAX)   NULL,
    [CreatedAt]   DATETIME2 (7)    NULL,
    [UpdatedBy]   NVARCHAR (MAX)   NULL,
    [UpdatedAt]   DATETIME2 (7)    NULL,
    [DeletedBy]   NVARCHAR (MAX)   NULL,
    [DeletedAt]   DATETIME2 (7)    NULL,
    [Version]     INT              NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC)
);

