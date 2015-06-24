USE [BusinessSafe]
GO

INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 47)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 48)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 49)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 50)

--//@UNDO
USE [BusinessSafe]
GO

DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = N'bacf7c01-d210-4dbc-942f-15d8456d3b92' AND [PermissionId] IN (47,48,49,50)

