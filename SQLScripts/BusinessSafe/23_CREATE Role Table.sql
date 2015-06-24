USE [BusinessSafe]
GO

print 'Create [Role]'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'Role' AND TYPE = 'U')
BEGIN
	CREATE TABLE [Role](
		[RoleId] [uniqueidentifier] NOT NULL,
		[Description] [nvarchar](256) NULL,
		[Name] [nvarchar](64) NULL,
		[Deleted] [bit] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[LastModifiedOn] [datetime] NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[ClientId] [bigint] NULL
	 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
	(
		[RoleId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [Role] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Role' AND TYPE = 'U')
BEGIN
	DROP TABLE [Role]
END
