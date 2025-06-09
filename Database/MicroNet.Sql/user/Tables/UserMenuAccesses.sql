CREATE TABLE [user].[UserMenuAccesses] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [UserId]    NVARCHAR (100)   NOT NULL,
    [MenuId]    UNIQUEIDENTIFIER NOT NULL,
    [IsChecked] BIT              NOT NULL,
    [CreatedBy] NVARCHAR (MAX)   NOT NULL,
    [CreatedAt] DATETIME2 (7)    NOT NULL,
    [UpdatedBy] NVARCHAR (MAX)   NULL,
    [UpdatedAt] DATETIME2 (7)    NULL,
    [DeletedBy] NVARCHAR (MAX)   NULL,
    [DeletedAt] DATETIME2 (7)    NULL,
    [Version]   INT              NOT NULL,
    CONSTRAINT [PK_UserMenuAccesses] PRIMARY KEY CLUSTERED ([Id] ASC)
);

