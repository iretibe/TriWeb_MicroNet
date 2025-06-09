CREATE TABLE [device].[Devices] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Code]        NVARCHAR (MAX)   NOT NULL,
    [Name]        NVARCHAR (MAX)   NOT NULL,
    [Description] NVARCHAR (MAX)   NOT NULL,
    [Notes]       NVARCHAR (MAX)   NOT NULL,
    [CreatedBy]   NVARCHAR (MAX)   NOT NULL,
    [CreatedAt]   DATETIME2 (7)    NOT NULL,
    [UpdatedBy]   NVARCHAR (MAX)   NULL,
    [UpdatedAt]   DATETIME2 (7)    NULL,
    [IsDeleted]   BIT              NOT NULL,
    [Version]     INT              NOT NULL,
    [DeletedAt]   DATETIME2 (7)    NULL,
    [DeletedBy]   NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Devices] PRIMARY KEY CLUSTERED ([Id] ASC)
);

