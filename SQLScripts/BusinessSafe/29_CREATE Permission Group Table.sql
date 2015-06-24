USE [BusinessSafe]
GO

print 'Create [PermissionGroup]'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'PermissionGroup' AND TYPE = 'U')
BEGIN
	CREATE TABLE [dbo].[PermissionGroup](
	[PermissionGroupId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[Deleted] [bit] NOT NULL,	
 CONSTRAINT [PK_PermissionGroup] PRIMARY KEY CLUSTERED 
(
	[PermissionGroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
	

END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [PermissionGroup] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'PermissionGroup' AND TYPE = 'U')
BEGIN
	DROP TABLE [PermissionGroup]
END
