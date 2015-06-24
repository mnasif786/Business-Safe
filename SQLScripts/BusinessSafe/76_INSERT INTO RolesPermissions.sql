USE [BusinessSafe]
GO

/****** Object:  Table [dbo].[PermissionGroupsPermissions]    Script Date: 07/24/2012 14:12:50 ******/
/****** Object:  Table [dbo].[RolesPermissions]    Script Date: 07/24/2012 14:54:23 ******/
INSERT [RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 37)
INSERT [RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'952eecb7-2b96-4399-82ae-7e2341d25e51', 37)
INSERT [RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 37)
INSERT [RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 38)
INSERT [RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'952eecb7-2b96-4399-82ae-7e2341d25e51', 38)
INSERT [RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 38)
INSERT [RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 39)
INSERT [RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'952eecb7-2b96-4399-82ae-7e2341d25e51', 39)
INSERT [RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 39)
/****** Object:  Table [dbo].[PermissionGroup]    Script Date: 07/24/2012 14:12:50 ******/

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM [RolesPermissions] WHERE [RoleId] = N'952eecb7-2b96-4399-82ae-7e2341d25e51' AND [PermissionId] = 37
DELETE FROM [RolesPermissions] WHERE [RoleId] = N'1e382767-93dd-47e2-88f2-b3e7f7648642' AND [PermissionId] = 37
DELETE FROM [RolesPermissions] WHERE [RoleId] = N'bacf7c01-d210-4dbc-942f-15d8456d3b92' AND [PermissionId] = 37
DELETE FROM [RolesPermissions] WHERE [RoleId] = N'952eecb7-2b96-4399-82ae-7e2341d25e51' AND [PermissionId] = 38
DELETE FROM [RolesPermissions] WHERE [RoleId] = N'1e382767-93dd-47e2-88f2-b3e7f7648642' AND [PermissionId] = 38
DELETE FROM [RolesPermissions] WHERE [RoleId] = N'bacf7c01-d210-4dbc-942f-15d8456d3b92' AND [PermissionId] = 38
DELETE FROM [RolesPermissions] WHERE [RoleId] = N'952eecb7-2b96-4399-82ae-7e2341d25e51' AND [PermissionId] = 39
DELETE FROM [RolesPermissions] WHERE [RoleId] = N'1e382767-93dd-47e2-88f2-b3e7f7648642' AND [PermissionId] = 39
DELETE FROM [RolesPermissions] WHERE [RoleId] = N'bacf7c01-d210-4dbc-942f-15d8456d3b92' AND [PermissionId] = 39