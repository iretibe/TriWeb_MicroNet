CREATE TABLE [user].[UserSubGroupMenus] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [UserGroupId]  UNIQUEIDENTIFIER NOT NULL,
    [MenuId]       UNIQUEIDENTIFIER NOT NULL,
    [SubMenuId]    UNIQUEIDENTIFIER NOT NULL,
    [IsChecked]    BIT              NOT NULL,
    [UserGroupId1] UNIQUEIDENTIFIER NULL,
    [Version]      INT              NOT NULL,
    CONSTRAINT [PK_UserSubGroupMenus] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserSubGroupMenus_UserGroups_UserGroupId1] FOREIGN KEY ([UserGroupId1]) REFERENCES [user].[UserGroups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_UserSubGroupMenus_UserGroupId1]
    ON [user].[UserSubGroupMenus]([UserGroupId1] ASC);

