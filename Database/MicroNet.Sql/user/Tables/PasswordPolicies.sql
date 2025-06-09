CREATE TABLE [user].[PasswordPolicies] (
    [Id]                                  UNIQUEIDENTIFIER NOT NULL,
    [PolicyName]                          NVARCHAR (100)   NOT NULL,
    [Requirements_RequiredLength]         INT              NOT NULL,
    [Requirements_RequireNonAlphanumeric] BIT              NOT NULL,
    [Requirements_RequireDigit]           BIT              NOT NULL,
    [Requirements_RequireLowercase]       BIT              NOT NULL,
    [Requirements_RequireUppercase]       BIT              NOT NULL,
    [Requirements_RequireUniqueChars]     BIT              NOT NULL,
    [CreatedBy]                           NVARCHAR (MAX)   NOT NULL,
    [CreatedAt]                           DATETIME2 (7)    NOT NULL,
    [UpdatedBy]                           NVARCHAR (MAX)   NULL,
    [UpdatedAt]                           DATETIME2 (7)    NULL,
    [DeletedBy]                           NVARCHAR (MAX)   NULL,
    [DeletedAt]                           DATETIME2 (7)    NULL,
    [UserGroupId]                         UNIQUEIDENTIFIER NOT NULL,
    [IsDeleted]                           BIT              DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Version]                             INT              NOT NULL,
    CONSTRAINT [PK_PasswordPolicies] PRIMARY KEY CLUSTERED ([Id] ASC)
);

