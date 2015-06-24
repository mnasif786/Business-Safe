USE [BusinessSafe]
GO

/****** Object:  Table [dbo].[Permission]    Script Date: 07/24/2012 14:12:50 ******/
SET IDENTITY_INSERT [dbo].[PermissionGroup] ON
INSERT [dbo].[PermissionGroup] ([PermissionGroupId], [Name], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (1, N'Profile', CAST(0x0000A09F0118B5E6 AS DateTime), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL, 0)
INSERT [dbo].[PermissionGroup] ([PermissionGroupId], [Name], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (2, N'Employees', CAST(0x0000A09F0118B5E6 AS DateTime), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL, 0)
INSERT [dbo].[PermissionGroup] ([PermissionGroupId], [Name], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (3, N'General Risk Assessments', CAST(0x0000A09F0118B5E6 AS DateTime), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL, 0)
INSERT [dbo].[PermissionGroup] ([PermissionGroupId], [Name], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (4, N'Employee Records', CAST(0x0000A09F0118B5E6 AS DateTime), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL, 0)
INSERT [dbo].[PermissionGroup] ([PermissionGroupId], [Name], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (5, N'General Risk Assessments Records', CAST(0x0000A09F0118B5E6 AS DateTime), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL, 0)
INSERT [dbo].[PermissionGroup] ([PermissionGroupId], [Name], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (6, N'General Risk Assessment Actions [All]', CAST(0x0000A09F0118B5E6 AS DateTime), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL, 0)
INSERT [dbo].[PermissionGroup] ([PermissionGroupId], [Name], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (7, N'General Risk Assessment Actions [Own]', CAST(0x0000A09F0118B5E7 AS DateTime), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL, 0)
INSERT [dbo].[PermissionGroup] ([PermissionGroupId], [Name], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (8, N'User Roles', CAST(0x0000A09F0118B5E7 AS DateTime), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[PermissionGroup] OFF

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM [BusinessSafe].[dbo].[PermissionGroup]
WHERE [PermissionGroupId] >= 1 AND [PermissionGroupId] <=7
