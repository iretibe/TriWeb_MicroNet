CREATE TABLE [user].[UserGroups] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [UserGroupName]      NVARCHAR (100)   NOT NULL,
    [IsActive]           BIT              NOT NULL,
    [BranchId]           UNIQUEIDENTIFIER NOT NULL,
    [WorkingHours_Start] TIME (7)         NOT NULL,
    [WorkingHours_End]   TIME (7)         NOT NULL,
    [CreatedBy]          NVARCHAR (MAX)   NOT NULL,
    [CreatedAt]          DATETIME2 (7)    NOT NULL,
    [UpdatedBy]          NVARCHAR (MAX)   NULL,
    [UpdatedAt]          DATETIME2 (7)    NULL,
    [DeletedBy]          NVARCHAR (MAX)   NULL,
    [DeletedAt]          DATETIME2 (7)    NULL,
    [IsDeleted]          BIT              DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Version]            INT              NOT NULL,
    CONSTRAINT [PK_UserGroups] PRIMARY KEY CLUSTERED ([Id] ASC)
);

