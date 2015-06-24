USE [BusinessSafe]
GO

SET IDENTITY_INSERT [PermissionTarget] ON
INSERT INTO [dbo].[PermissionTarget] ([Id], [Name], [PermissionGroupId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [Deleted])
	  SELECT 1, 'Company Details', 1, NEWID(), GETDATE(), NULL, NULL, 0
UNION SELECT 2, 'Site Details', 1, NEWID(), GETDATE(), NULL, NULL, 0
UNION SELECT 3, 'Users', 1, NEWID(), GETDATE(), NULL, NULL, 0
UNION SELECT 4, 'Employee Records', 2, NEWID(), GETDATE(), NULL, NULL, 0
UNION SELECT 5, 'Risk Assessment Records', 3, NEWID(), GETDATE(), NULL, NULL, 0
UNION SELECT 6, 'Risk Assessment Tasks', 3, NEWID(), GETDATE(), NULL, NULL, 0
UNION SELECT 7, 'Added Documents', 4, NEWID(), GETDATE(), NULL, NULL, 0
UNION SELECT 8, 'BusinessSafe System', 4, NEWID(), GETDATE(), NULL, NULL, 0
UNION SELECT 9, 'Reference Library', 4, NEWID(), GETDATE(), NULL, NULL, 0
UNION SELECT 10, 'User Roles', 1, NEWID(), GETDATE(), NULL, NULL, 0
UNION SELECT 11, 'User Permissions', 1, NEWID(), GETDATE(), NULL, NULL, 0
UNION SELECT 12, 'Company Defaults', 1, NEWID(), GETDATE(), NULL, NULL, 0
SET IDENTITY_INSERT [PermissionTarget] OFF
GO

--//@UNDO 
DELETE FROM [PermissionTarget]
WHERE id BETWEEN 1 AND 12