USE [BusinessSafe]
GO

/****** Object:  Table [dbo].[Permission]    Script Date: 07/24/2012 14:12:50 ******/
SET IDENTITY_INSERT [dbo].[Permission] ON
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (1, N'ViewCompanyDetails', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (2, N'EditCompanyDetails', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (3, N'DeleteCompanyDetails', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (4, N'ViewSiteDetails', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (5, N'AddSiteDetails', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (6, N'EditSiteDetails', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (7, N'DeleteSiteDetails', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (8, N'ViewUsers', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (9, N'AddUsers', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (10, N'EditUsers', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (11, N'DeleteUsers', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (12, N'ViewEmployeeRecords', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (13, N'AddEmployeeRecords', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (14, N'EditEmployeeRecords', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (15, N'DeleteEmployeeRecords', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (16, N'ViewGeneralRiskAssessments', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (17, N'AddGeneralRiskAssessments', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (18, N'EditGeneralRiskAssessments', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (19, N'DeleteGeneralRiskAssessments', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (20, N'ViewGRAActions', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (21, N'AddGRAActions', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (22, N'EditGRAActions', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (23, N'DeleteGRAActions', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (24, N'ViewOwnGRAActions', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (25, N'AddOwnGRAActions', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (26, N'EditOwnGRAActions', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (27, N'DeleteOwnGRAActions', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (28, N'ViewUserRoles', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (29, N'AddUserRoles', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (30, N'EditUserRoles', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (31, N'DeleteUserRoles', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (32, N'ViewUserPermissions', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (33, N'AddCompanyDefaults', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (34, N'EditCompanyDefaults', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (35, N'DeleteCompanyDefaults', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (36, N'ViewCompanyDefaults', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)


SET IDENTITY_INSERT [dbo].[Permission] OFF

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM [BusinessSafe].[dbo].[Permission]
WHERE [PermissionId] >= 2 AND [PermissionId] <=32
