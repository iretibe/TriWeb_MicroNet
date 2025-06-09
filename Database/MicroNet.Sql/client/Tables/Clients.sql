CREATE TABLE [client].[Clients] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [FirstName]           NVARCHAR (MAX)   NOT NULL,
    [LastName]            NVARCHAR (MAX)   NOT NULL,
    [Email]               NVARCHAR (MAX)   NOT NULL,
    [PhoneNumber]         NVARCHAR (MAX)   NOT NULL,
    [DateOfBirth]         DATETIME2 (7)    NOT NULL,
    [Address_Street]      NVARCHAR (150)   NOT NULL,
    [Address_City]        NVARCHAR (100)   NOT NULL,
    [Address_State]       NVARCHAR (100)   NOT NULL,
    [Address_ZipCode]     NVARCHAR (20)    NOT NULL,
    [KYC_DocumentType]    NVARCHAR (MAX)   NOT NULL,
    [KYC_DocumentNumber]  NVARCHAR (MAX)   NOT NULL,
    [KYC_ExpiryDate]      DATETIME2 (7)    NOT NULL,
    [AuditInfo_CreatedBy] NVARCHAR (MAX)   NULL,
    [AuditInfo_CreatedAt] DATETIME2 (7)    NULL,
    [AuditInfo_UpdatedBy] NVARCHAR (MAX)   NULL,
    [AuditInfo_UpdatedAt] DATETIME2 (7)    NULL,
    [AuditInfo_DeletedBy] NVARCHAR (MAX)   NULL,
    [AuditInfo_DeletedAt] DATETIME2 (7)    NULL,
    [Version]             INT              NOT NULL,
    CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED ([Id] ASC)
);

