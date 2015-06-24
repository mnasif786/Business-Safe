USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM [dbo].[RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' and [PermissionId] = 19)
BEGIN
	INSERT [RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'952eecb7-2b96-4399-82ae-7e2341d25e51', 19)
END

IF NOT EXISTS(SELECT * FROM [dbo].[RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' and [PermissionId] = 49)
BEGIN
	INSERT [RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'952eecb7-2b96-4399-82ae-7e2341d25e51', 49)
END

IF NOT EXISTS(SELECT * FROM [dbo].[RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' and [PermissionId] = 53)
BEGIN
	INSERT [RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'952eecb7-2b96-4399-82ae-7e2341d25e51', 53)
END
--//@UNDO 
USE [BusinessSafe]
GO

IF EXISTS(SELECT * FROM [dbo].[RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' and [PermissionId] = 19)
BEGIN
	DELETE FROM [RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' AND [PermissionId] = 19
END

IF EXISTS(SELECT * FROM [dbo].[RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' and [PermissionId] = 49)
BEGIN
	DELETE FROM [RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' AND [PermissionId] = 49
END

IF EXISTS(SELECT * FROM [dbo].[RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' and [PermissionId] = 53)
BEGIN
	DELETE FROM [RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' AND [PermissionId] = 53
END



