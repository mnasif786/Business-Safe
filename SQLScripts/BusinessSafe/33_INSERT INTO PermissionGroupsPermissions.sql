USE [BusinessSafe]
GO

/****** Object:  Table [dbo].[PermissionGroupsPermissions]    Script Date: 07/24/2012 14:12:50 ******/
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 1)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 2)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 3)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 4)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 5)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 6)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 7)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 8)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 9)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 10)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 11)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 28)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 29)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 30)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 31)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 32)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 33)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 34)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 35)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (1, 36)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (2, 12)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (2, 13)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (2, 14)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (2, 15)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 16)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 17)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 18)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 19)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 20)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 21)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 22)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 23)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 24)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 25)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 26)
INSERT [dbo].[PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 27)
/****** Object:  Table [dbo].[PermissionGroup]    Script Date: 07/24/2012 14:12:50 ******/

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM [BusinessSafe].[dbo].[PermissionGroupsPermissions]
WHERE ([PermissionGroupId] >= 1 AND [PermissionGroupId] <=7) 
AND   ([PermissionId] >= 2 AND [PermissionId] <= 28)
