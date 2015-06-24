use businesssafe
go

DECLARE @UserAdminRole AS UNIQUEIDENTIFIER
DECLARE @GeneralUserRoleRole AS UNIQUEIDENTIFIER

SELECT @GeneralUserRoleRole = CAST('952EECB7-2B96-4399-82AE-7E2341D25E51' AS uniqueidentifier)
SELECT @UserAdminRole = CAST('BACF7C01-D210-4DBC-942F-15D8456D3B92' AS uniqueidentifier)

--User Admin
IF NOT EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 63)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]([RoleId],[PermissionId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy],[Deleted])
	VALUES (@UserAdminRole,63, getdate(),@UserAdminRole,null, null, 0)
END

IF NOT EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 64)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]([RoleId],[PermissionId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy],[Deleted])
	VALUES (@UserAdminRole,64, getdate(),@UserAdminRole,null, null, 0)
END

IF NOT EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 65)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]([RoleId],[PermissionId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy],[Deleted])
	VALUES (@UserAdminRole,65, getdate(),@UserAdminRole,null, null, 0)
END

IF NOT EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 66)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]([RoleId],[PermissionId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy],[Deleted])
	VALUES (@UserAdminRole,66, getdate(),@UserAdminRole,null, null, 0)
END

--General User
IF NOT EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 63)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]([RoleId],[PermissionId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy],[Deleted])
	VALUES (@GeneralUserRoleRole,63, getdate(),@GeneralUserRoleRole,null, null, 0)
END

IF NOT EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 64)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]([RoleId],[PermissionId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy],[Deleted])
	VALUES (@GeneralUserRoleRole,64, getdate(),@GeneralUserRoleRole,null, null, 0)
END

IF NOT EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 65)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]([RoleId],[PermissionId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy],[Deleted])
	VALUES (@GeneralUserRoleRole,65, getdate(),@GeneralUserRoleRole,null, null, 0)
END

IF NOT EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 66)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]([RoleId],[PermissionId],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy],[Deleted])
	VALUES (@GeneralUserRoleRole,66, getdate(),@GeneralUserRoleRole,null, null, 0)
END

--//@UNDO 

--User Admin
IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 63)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 63 
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 64)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 64 
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 65)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 65 
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 66)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 66 
END

--General User
IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 63)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 63 
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 64)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 64 
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 65)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 65 
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 66)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 66 
END