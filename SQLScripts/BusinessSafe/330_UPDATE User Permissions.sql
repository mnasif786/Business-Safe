ALTER TABLE [dbo].[PermissionTarget] ALTER COLUMN [Name] NVARCHAR(56)

UPDATE [dbo].[PermissionTarget] 
SET [Name] = 'Personal Risk Assessments',
	PermissionGroupId = 3
WHERE id = 15

UPDATE [dbo].[PermissionTarget] 
SET [Name] = 'General and Hazardous Substances Risk Assessments'
WHERE id = 5

UPDATE [dbo].[PermissionTarget] 
SET PermissionGroupId = 5
WHERE id in (7, 8, 9)

UPDATE [dbo].[PermissionTarget] 
SET PermissionGroupId = 4
WHERE id = 14

SET IDENTITY_INSERT [PermissionTarget] ON

IF NOT EXISTS (SELECT ID FROM [dbo].[PermissionTarget] WHERE ID = 16)
BEGIN
	INSERT INTO [dbo].[PermissionTarget] ([Id], [Name], [DisplayOrder], [PermissionGroupId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [Deleted])
								  VALUES (16, 'Fire Risk Assessments', null, 3, 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', getdate(), null, null, 0)
	INSERT INTO [dbo].[PermissionTarget] ([Id], [Name], [DisplayOrder], [PermissionGroupId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [Deleted])
								  VALUES (17, 'Responsibilities', null, 4, 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', getdate(), null, null, 0)
END

SET IDENTITY_INSERT [PermissionTarget] OFF

DELETE FROM [dbo].[Permission]
WHERE PermissionTargetId = 1 -- company details
AND (PermissionActivityId = 2 -- add
	OR PermissionActivityId = 4) -- delete

DELETE FROM [dbo].[Permission]
WHERE PermissionTargetId = 10 -- user roles

DELETE FROM [dbo].[PermissionTarget]
WHERE Id = 10 -- user roles
	
DELETE
FROM [dbo].[RolesPermissions]
Where PermissionId not in (
	select id
	from permission
)

-- Fire Risk Assessments
INSERT INTO [dbo].[Permission] ([PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
     VALUES (16, 1, 0, getdate(), 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', null, null)
INSERT INTO [dbo].[Permission] ([PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
     VALUES (16, 2, 0, getdate(), 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', null, null)
INSERT INTO [dbo].[Permission] ([PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
     VALUES (16, 3, 0, getdate(), 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', null, null)
INSERT INTO [dbo].[Permission] ([PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
     VALUES (16, 4, 0, getdate(), 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', null, null)

-- Responsibilities
INSERT INTO [dbo].[Permission] ([PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
     VALUES (17, 1, 0, getdate(), 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', null, null)
INSERT INTO [dbo].[Permission] ([PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
     VALUES (17, 2, 0, getdate(), 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', null, null)
INSERT INTO [dbo].[Permission] ([PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
     VALUES (17, 3, 0, getdate(), 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', null, null)
INSERT INTO [dbo].[Permission] ([PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
     VALUES (17, 4, 0, getdate(), 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', null, null)

-- delete redundant 'user permissions'
DELETE [dbo].[RolesPermissions]
WHERE PermissionId in (
	select id 
	from [dbo].[Permission]
	WHERE PermissionTargetId = 11 
)	

DELETE [dbo].[Permission]
WHERE PermissionTargetId = 11 

DELETE [dbo].[PermissionTarget]
WHERE Id = 11

-- user admin new permissions
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('BACF7C01-D210-4DBC-942F-15D8456D3B92', 51)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('BACF7C01-D210-4DBC-942F-15D8456D3B92', 52)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('BACF7C01-D210-4DBC-942F-15D8456D3B92', 53)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('BACF7C01-D210-4DBC-942F-15D8456D3B92', 54)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('BACF7C01-D210-4DBC-942F-15D8456D3B92', 55)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('BACF7C01-D210-4DBC-942F-15D8456D3B92', 56)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('BACF7C01-D210-4DBC-942F-15D8456D3B92', 57)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('BACF7C01-D210-4DBC-942F-15D8456D3B92', 58)

-- h&s manager permissions
DELETE FROM [dbo].[RolesPermissions] WHERE roleid = '1E382767-93DD-47E2-88F2-B3E7F7648642' and permissionid = 3
DELETE FROM [dbo].[RolesPermissions] WHERE roleid = '1E382767-93DD-47E2-88F2-B3E7F7648642' and permissionid = 40
DELETE FROM [dbo].[RolesPermissions] WHERE roleid = '1E382767-93DD-47E2-88F2-B3E7F7648642' and permissionid = 41
DELETE FROM [dbo].[RolesPermissions] WHERE roleid = '1E382767-93DD-47E2-88F2-B3E7F7648642' and permissionid = 42
DELETE FROM [dbo].[RolesPermissions] WHERE roleid = '1E382767-93DD-47E2-88F2-B3E7F7648642' and permissionid = 10
DELETE FROM [dbo].[RolesPermissions] WHERE roleid = '1E382767-93DD-47E2-88F2-B3E7F7648642' and permissionid = 11
DELETE FROM [dbo].[RolesPermissions] WHERE roleid = '1E382767-93DD-47E2-88F2-B3E7F7648642' and permissionid = 12
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('1E382767-93DD-47E2-88F2-B3E7F7648642', 47)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('1E382767-93DD-47E2-88F2-B3E7F7648642', 48)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('1E382767-93DD-47E2-88F2-B3E7F7648642', 49)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('1E382767-93DD-47E2-88F2-B3E7F7648642', 50)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('1E382767-93DD-47E2-88F2-B3E7F7648642', 51)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('1E382767-93DD-47E2-88F2-B3E7F7648642', 52)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('1E382767-93DD-47E2-88F2-B3E7F7648642', 53)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('1E382767-93DD-47E2-88F2-B3E7F7648642', 54)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('1E382767-93DD-47E2-88F2-B3E7F7648642', 55)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('1E382767-93DD-47E2-88F2-B3E7F7648642', 56)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('1E382767-93DD-47E2-88F2-B3E7F7648642', 57)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('1E382767-93DD-47E2-88F2-B3E7F7648642', 58)

-- general user
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('952EECB7-2B96-4399-82AE-7E2341D25E51', 43)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('952EECB7-2B96-4399-82AE-7E2341D25E51', 51)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('952EECB7-2B96-4399-82AE-7E2341D25E51', 55)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('952EECB7-2B96-4399-82AE-7E2341D25E51', 24)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('952EECB7-2B96-4399-82AE-7E2341D25E51', 26)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('952EECB7-2B96-4399-82AE-7E2341D25E51', 27)
INSERT INTO [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES ('952EECB7-2B96-4399-82AE-7E2341D25E51', 28)
