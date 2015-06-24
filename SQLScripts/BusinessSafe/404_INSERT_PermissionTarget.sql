USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT ID FROM [dbo].[PermissionTarget] WHERE ID = 18)
BEGIN
	SET IDENTITY_INSERT [PermissionTarget] ON
	INSERT INTO [dbo].[PermissionTarget] ([Id], [Name], [PermissionGroupId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [Deleted])
	VALUES (18, 'Accident Records', 4, '16ac58fb-4ea4-4482-ac3d-000d607af67c', GETDATE(), NULL, NULL, 0)
	SET IDENTITY_INSERT [PermissionTarget] OFF
END
GO
--//@UNDO 

IF EXISTS (SELECT ID FROM [dbo].[PermissionTarget] WHERE ID = 18)
BEGIN
	DELETE FROM [PermissionTarget]
	WHERE ID = 18
END
