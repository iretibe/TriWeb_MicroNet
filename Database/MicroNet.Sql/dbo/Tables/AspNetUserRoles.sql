CREATE TABLE [dbo].[AspNetUserRoles] (
    [Id]     UNIQUEIDENTIFIER CONSTRAINT [DF_AspNetUserRoles_Id] DEFAULT (newid()) NOT NULL,
    [UserId] NVARCHAR (450)   NOT NULL,
    [RoleId] NVARCHAR (450)   NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId]
    ON [dbo].[AspNetUserRoles]([RoleId] ASC);

