USE [BusinessSafe]
GO

-- Admin
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 43)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 44)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 45)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 46)

-- H & S Manager
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1E382767-93DD-47E2-88F2-B3E7F7648642', 43)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1E382767-93DD-47E2-88F2-B3E7F7648642', 44)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1E382767-93DD-47E2-88F2-B3E7F7648642', 45)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1E382767-93DD-47E2-88F2-B3E7F7648642', 46)


--//@UNDO
USE [BusinessSafe]
GO

DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = N'bacf7c01-d210-4dbc-942f-15d8456d3b92' AND [PermissionId] IN (43,44,45,46)
DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = N'1E382767-93DD-47E2-88F2-B3E7F7648642' AND [PermissionId] IN (43,44,45,46)


