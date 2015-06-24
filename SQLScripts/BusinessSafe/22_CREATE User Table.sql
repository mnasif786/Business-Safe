USE [BusinessSafe]
GO

print 'Create [User]'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'User' AND TYPE = 'U')
BEGIN
	CREATE TABLE [User](
		[UserId] [uniqueidentifier] NOT NULL,
		[EmployeeId] [uniqueidentifier] NULL,
		[RoleId] [uniqueidentifier] NOT NULL,
		[RegistrationError] [bit] NULL,
		[RegistrationErrorMessage] [nvarchar](256) NULL,
		[Deleted] [bit] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[ClientId] [bigint] NOT NULL,
		[SiteId] [bigint] NULL,
	 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
	(
		[UserId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [User] TO [AllowAll]
GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'User' AND TYPE = 'U')
BEGIN
	DROP TABLE [User]
END
