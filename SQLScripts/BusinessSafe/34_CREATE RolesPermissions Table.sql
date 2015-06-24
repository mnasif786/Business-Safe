USE [BusinessSafe]
GO

print 'Create [RolesPermissions]'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'RolesPermissions' AND TYPE = 'U')
BEGIN

	CREATE TABLE [dbo].[RolesPermissions](
		[RoleId] [uniqueidentifier] NOT NULL,
		[PermissionId] [int] NOT NULL,
	 CONSTRAINT [PK_RolesPermissions] PRIMARY KEY CLUSTERED 
	(
		[RoleId] ASC,
		[PermissionId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]


	

ALTER TABLE [dbo].[RolesPermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolesPermissions_Permission] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permission] ([PermissionId])


ALTER TABLE [dbo].[RolesPermissions] CHECK CONSTRAINT [FK_RolesPermissions_Permission]


ALTER TABLE [dbo].[RolesPermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolesPermissions_RolesPermissions] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])


ALTER TABLE [dbo].[RolesPermissions] CHECK CONSTRAINT [FK_RolesPermissions_RolesPermissions]




END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [RolesPermissions] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RolesPermissions' AND TYPE = 'U')
BEGIN
	DROP TABLE [RolesPermissions]
END
