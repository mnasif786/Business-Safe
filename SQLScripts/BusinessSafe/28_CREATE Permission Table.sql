USE [BusinessSafe]
GO

print 'Create [Permission]'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'Permission' AND TYPE = 'U')
BEGIN
	CREATE TABLE [dbo].[Permission](
		[PermissionId] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](100) NULL,
		[Deleted] [bit] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[LastModifiedOn] [datetime] NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
	 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
	(
		[PermissionId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [Permission] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Permission' AND TYPE = 'U')
BEGIN
	DROP TABLE [Permission]
END
