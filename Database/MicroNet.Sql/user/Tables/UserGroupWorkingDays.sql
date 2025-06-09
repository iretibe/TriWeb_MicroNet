CREATE TABLE [user].[UserGroupWorkingDays] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [UserGroupId] UNIQUEIDENTIFIER NOT NULL,
    [DayOfWeek]   INT              NOT NULL,
    [CreatedBy]   NVARCHAR (MAX)   NOT NULL,
    [CreatedAt]   DATETIME2 (7)    NOT NULL,
    [Version]     INT              NOT NULL,
    CONSTRAINT [PK_UserGroupWorkingDays] PRIMARY KEY CLUSTERED ([Id] ASC)
);

