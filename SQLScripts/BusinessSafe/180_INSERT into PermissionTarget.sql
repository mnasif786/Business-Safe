USE [BusinessSafe]
GO

SET IDENTITY_INSERT [PermissionTarget] ON
INSERT INTO [dbo].[PermissionTarget] ([Id], [Name], [PermissionGroupId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [Deleted])
VALUES (14, 'Hazardous Substance Inventory', 5, '16ac58fb-4ea4-4482-ac3d-000d607af67c', GETDATE(), NULL, NULL, 0)
SET IDENTITY_INSERT [PermissionTarget] OFF
GO

--//@UNDO 
DELETE FROM [PermissionTarget]
WHERE ID = 14