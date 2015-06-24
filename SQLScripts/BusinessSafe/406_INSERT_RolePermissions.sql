use businesssafe
go

--User Admin
IF NOT EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 59)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]([RoleId],[PermissionId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy],[Deleted])
	VALUES ('BACF7C01-D210-4DBC-942F-15D8456D3B92',59, getdate(),'BACF7C01-D210-4DBC-942F-15D8456D3B92',null, null, 0)
END

IF NOT EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 60)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]([RoleId],[PermissionId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy],[Deleted])
	VALUES ('BACF7C01-D210-4DBC-942F-15D8456D3B92',60, getdate(),'BACF7C01-D210-4DBC-942F-15D8456D3B92',null, null, 0)
END

IF NOT EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 61)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]([RoleId],[PermissionId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy],[Deleted])
	VALUES ('BACF7C01-D210-4DBC-942F-15D8456D3B92',61, getdate(),'BACF7C01-D210-4DBC-942F-15D8456D3B92',null, null, 0)
END

IF NOT EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 62)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]([RoleId],[PermissionId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy],[Deleted])
	VALUES ('BACF7C01-D210-4DBC-942F-15D8456D3B92',62, getdate(),'BACF7C01-D210-4DBC-942F-15D8456D3B92',null, null, 0)
END

--Health and Safety Manager
IF NOT EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 59)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]([RoleId],[PermissionId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy],[Deleted])
	VALUES ('1E382767-93DD-47E2-88F2-B3E7F7648642',59, getdate(),'BACF7C01-D210-4DBC-942F-15D8456D3B92',null, null, 0)
END

IF NOT EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 60)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]([RoleId],[PermissionId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy],[Deleted])
	VALUES ('1E382767-93DD-47E2-88F2-B3E7F7648642',60, getdate(),'BACF7C01-D210-4DBC-942F-15D8456D3B92',null, null, 0)
END

IF NOT EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 61)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]([RoleId],[PermissionId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy],[Deleted])
	VALUES ('1E382767-93DD-47E2-88F2-B3E7F7648642',61, getdate(),'BACF7C01-D210-4DBC-942F-15D8456D3B92',null, null, 0)
END

IF NOT EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 62)
BEGIN
INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]([RoleId],[PermissionId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy],[Deleted])
VALUES ('1E382767-93DD-47E2-88F2-B3E7F7648642',62, getdate(),'BACF7C01-D210-4DBC-942F-15D8456D3B92',null, null, 0)
END
GO

--//@UNDO 

--User Admin
IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 59)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 59 
END

IF  EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 60)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 60 
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 61)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 61
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 62)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 62
END

--Health and Safety Manager
IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 59)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 59
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 60)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 60
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 61)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 61
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 62)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 62
END
GO

