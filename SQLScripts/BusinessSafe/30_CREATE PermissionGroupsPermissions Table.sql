USE [BusinessSafe]
GO

print 'Create [PermissionGroupsPermissions]'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'PermissionGroupsPermissions' AND TYPE = 'U')
BEGIN
	CREATE TABLE [dbo].[PermissionGroupsPermissions](
		[PermissionGroupId] [int] NOT NULL,
		[PermissionId] [int] NOT NULL,
	 CONSTRAINT [PK_PermissionGroupsPermissions] PRIMARY KEY CLUSTERED 
	(
		[PermissionGroupId] ASC,
		[PermissionId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	

END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [PermissionGroupsPermissions] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'PermissionGroupsPermissions' AND TYPE = 'U')
BEGIN
	DROP TABLE [PermissionGroupsPermissions]
END
