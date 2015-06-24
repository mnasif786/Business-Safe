USE [BusinessSafe]
GO

IF (OBJECT_ID('FK_RolesPermissions_Permission', 'F') IS NOT NULL)
BEGIN
  ALTER TABLE [dbo].[RolesPermissions] DROP CONSTRAINT [FK_RolesPermissions_Permission]
END
GO

--//@UNDO
USE [BusinessSafe]
GO

IF (OBJECT_ID('FK_RolesPermissions_Permission', 'F') IS NULL)
BEGIN
	ALTER TABLE [dbo].[RolesPermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolesPermissions_Permission] FOREIGN KEY([PermissionId])
	REFERENCES [dbo].[PREVIOUS_Permission] ([PermissionId])	
END
GO

