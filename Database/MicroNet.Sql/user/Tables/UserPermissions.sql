CREATE TABLE [user].[UserPermissions] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [UserId]      NVARCHAR (100)   NOT NULL,
    [UserGroupId] UNIQUEIDENTIFIER NOT NULL,
    [BranchId]    UNIQUEIDENTIFIER NOT NULL,
    [RoleName]    NVARCHAR (100)   NOT NULL,
    [CreatedBy]   NVARCHAR (100)   NOT NULL,
    [CreatedAt]   DATETIME2 (7)    NOT NULL,
    [UpdatedBy]   NVARCHAR (100)   NULL,
    [UpdatedAt]   DATETIME2 (7)    NULL,
    [DeletedBy]   NVARCHAR (100)   NULL,
    [DeletedAt]   DATETIME2 (7)    NULL,
    [IsDeleted]   BIT              DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Version]     INT              NOT NULL,
    CONSTRAINT [PK_UserPermissions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

