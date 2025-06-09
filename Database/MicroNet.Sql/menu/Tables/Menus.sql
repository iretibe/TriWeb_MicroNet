CREATE TABLE [menu].[Menus] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (MAX)   NOT NULL,
    [Controller]  NVARCHAR (MAX)   NOT NULL,
    [Action]      NVARCHAR (MAX)   NOT NULL,
    [Icon]        NVARCHAR (MAX)   NOT NULL,
    [ParentId]    UNIQUEIDENTIFIER NULL,
    [MenuOrderId] INT              NOT NULL,
    [UId]         NVARCHAR (MAX)   NOT NULL,
    [MenuId]      UNIQUEIDENTIFIER NULL,
    [CreatedBy]   NVARCHAR (MAX)   NOT NULL,
    [CreatedOn]   DATETIME2 (7)    NOT NULL,
    [UpdatedBy]   NVARCHAR (MAX)   NOT NULL,
    [UpdatedOn]   DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Menus_Menus_MenuId] FOREIGN KEY ([MenuId]) REFERENCES [menu].[Menus] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Menus_MenuId]
    ON [menu].[Menus]([MenuId] ASC);

