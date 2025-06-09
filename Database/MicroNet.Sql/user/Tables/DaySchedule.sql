CREATE TABLE [user].[DaySchedule] (
    [UserGroupId] UNIQUEIDENTIFIER NOT NULL,
    [Id]          INT              IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_DaySchedule] PRIMARY KEY CLUSTERED ([UserGroupId] ASC, [Id] ASC),
    CONSTRAINT [FK_DaySchedule_UserGroups_UserGroupId] FOREIGN KEY ([UserGroupId]) REFERENCES [user].[UserGroups] ([Id]) ON DELETE CASCADE
);

