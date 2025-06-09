CREATE TABLE [user].[UserGroupMenus] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [UserGroupId]  UNIQUEIDENTIFIER NOT NULL,
    [MenuId]       UNIQUEIDENTIFIER NOT NULL,
    [IsChecked]    BIT              NOT NULL,
    [UserGroupId1] UNIQUEIDENTIFIER NULL,
    [Version]      INT              NOT NULL,
    CONSTRAINT [PK_UserGroupMenus] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserGroupMenus_UserGroups_UserGroupId1] FOREIGN KEY ([UserGroupId1]) REFERENCES [user].[UserGroups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_UserGroupMenus_UserGroupId1]
    ON [user].[UserGroupMenus]([UserGroupId1] ASC);

