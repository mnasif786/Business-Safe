USE [BusinessSafe]
GO

SET IDENTITY_INSERT [PermissionTarget] ON
INSERT INTO [dbo].[PermissionTarget] ([Id], [Name], [PermissionGroupId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [Deleted])
VALUES (19, 'Action Plan', 4, '16AC58FB-4EA4-4482-AC3D-000D607AF67C', GETDATE(), NULL, NULL, 0)
SET IDENTITY_INSERT [PermissionTarget] OFF
GO

--//@UNDO 
DELETE FROM [PermissionTarget]
WHERE ID = 19