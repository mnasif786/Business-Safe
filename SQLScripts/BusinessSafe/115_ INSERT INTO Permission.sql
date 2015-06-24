

--UPDATE [Permission] SET [Name] = 'ViewGeneralRiskAssessmentsAll' WHERE [PermissionId] = 16 
--UPDATE [Permission] SET [Name] = 'AddGeneralRiskAssessmentsAll' WHERE [PermissionId] = 17
--UPDATE [Permission] SET [Name] = 'EditGeneralRiskAssessmentsAll' WHERE [PermissionId] = 18 
--UPDATE [Permission] SET [Name] = 'DeleteGeneralRiskAssessmentsAll' WHERE [PermissionId] = 19
--UPDATE [Permission] SET [Name] = 'ViewGRATasksAll' WHERE [PermissionId] = 20
--UPDATE [Permission] SET [Name] = 'AddAndAssignGRATasksAll' WHERE [PermissionId] = 21
--UPDATE [Permission] SET [Name] = 'EditAndReassignGRATasksAll' WHERE [PermissionId] = 22
--UPDATE [Permission] SET [Name] = 'DeleteGRATasksAll' WHERE [PermissionId] = 23
--UPDATE [Permission] SET [Name] = 'ViewOwnGRATasks' WHERE [PermissionId] = 24
--UPDATE [Permission] SET [Name] = 'AddAndAssignOwnGRATasks' WHERE [PermissionId] = 25
--UPDATE [Permission] SET [Name] = 'EditAndReassignOwnGRATasks' WHERE [PermissionId] = 26
--UPDATE [Permission] SET [Name] = 'DeleteOwnGRATasks' WHERE [PermissionId] = 27
--UPDATE [Permission] SET [Name] = 'AddAddedDocumentsAll' WHERE [PermissionId] = 37
--UPDATE [Permission] SET [Name] = 'DeleteAddedDocumentsAll' WHERE [PermissionId] = 38
--UPDATE [Permission] SET [Name] = 'ViewAddedDocumentsAll' WHERE [PermissionId] = 39

SET DATEFORMAT YMD

IF NOT EXISTS(SELECT * FROM [Permission] WHERE [PermissionId] > 39)
BEGIN
	SET IDENTITY_INSERT [Permission] ON
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (40, 'ViewGeneralRiskAssessmentsSiteGroup', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (41, 'AddGeneralRiskAssessmentsSiteGroup', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (42, 'EditGeneralRiskAssessmentsSiteGroup', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (43, 'DeleteGeneralRiskAssessmentsSiteGroup', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (44, 'ViewGeneralRiskAssessmentsOwnSite', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (45, 'AddGeneralRiskAssessmentsOwnSite', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (46, 'EditGeneralRiskAssessmentsOwnSite', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (47, 'DeleteGeneralRiskAssessmentsOwnSite', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (48, 'ViewGRATasksSiteGroup', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (49, 'AddAndAssignGRATasksSiteGroup', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (50, 'EditAndReassignGRATasksSiteGroup', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (51, 'DeleteGRATasksSiteGroup', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (52, 'ViewReferenceLibrary', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (53, 'ViewBusinessSafeSystem', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (54, 'EditAddedDocumentsAll', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (55, 'ViewAddedDocumentsSiteGroup', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (56, 'AddAddedDocumentsSiteGroup', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (57, 'EditAddedDocumentsSiteGroup', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (58, 'DeleteAddedDocumentsSiteGroup', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (59, 'ViewAddedDocumentsOwnSite', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	INSERT INTO [Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (60, 'ViewReferenceLibrary', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')	
	SET IDENTITY_INSERT [Permission] OFF
END

IF NOT EXISTS(SELECT * FROM [PermissionGroup] WHERE [PermissionGroupId] > 8)
BEGIN
	SET IDENTITY_INSERT [PermissionGroup] ON
	INSERT INTO [PermissionGroup] ([PermissionGroupId], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (9, 'My Documentation', 0, '2012-10-23 17:00:00', 'f9ad7115-be16-476e-8d58-fb6aeeebb51a')
	SET IDENTITY_INSERT [PermissionGroup] OFF
END

IF NOT EXISTS(SELECT * FROM [PermissionGroupsPermissions] WHERE [PermissionId] > 36)
BEGIN
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 40)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 41)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 42)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 43)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 44)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 45)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 46)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 47)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 48)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 49)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 50)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (3, 51)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (9, 37)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (9, 38)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (9, 39)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (9, 54)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (9, 55)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (9, 56)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (9, 57)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (9, 58)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (9, 59)
	INSERT INTO [PermissionGroupsPermissions] ([PermissionGroupId], [PermissionId]) VALUES (9, 60)
END

--//@UNDO 
USE [BusinessSafe]
GO

--UPDATE [Permission] SET [Name] = 'ViewGeneralRiskAssessments' WHERE [PermissionId] = 16 
--UPDATE [Permission] SET [Name] = 'AddGeneralRiskAssessments' WHERE [PermissionId] = 17
--UPDATE [Permission] SET [Name] = 'EditGeneralRiskAssessments' WHERE [PermissionId] = 18 
--UPDATE [Permission] SET [Name] = 'DeleteGeneralRiskAssessments' WHERE [PermissionId] = 19
--UPDATE [Permission] SET [Name] = 'ViewGRAActions' WHERE [PermissionId] = 20
--UPDATE [Permission] SET [Name] = 'AddGRAActions' WHERE [PermissionId] = 21
--UPDATE [Permission] SET [Name] = 'EditGRAActions' WHERE [PermissionId] = 22
--UPDATE [Permission] SET [Name] = 'DeleteGRAActions' WHERE [PermissionId] = 23
--UPDATE [Permission] SET [Name] = 'ViewOwnGRAActions' WHERE [PermissionId] = 24
--UPDATE [Permission] SET [Name] = 'AddOwnGRAActions' WHERE [PermissionId] = 25
--UPDATE [Permission] SET [Name] = 'EditOwnGRAActions' WHERE [PermissionId] = 26
--UPDATE [Permission] SET [Name] = 'DeleteOwnGRAActions' WHERE [PermissionId] = 27
--UPDATE [Permission] SET [Name] = 'UploadDocuments' WHERE [PermissionId] = 37
--UPDATE [Permission] SET [Name] = 'DeleteDocuments' WHERE [PermissionId] = 38
--UPDATE [Permission] SET [Name] = 'DownloadDocuments' WHERE [PermissionId] = 39

IF EXISTS(SELECT * FROM [Permission] WHERE [PermissionId] > 39)
BEGIN
	DELETE FROM [Permission] WHERE [PermissionId] > 39
END

IF EXISTS(SELECT * FROM [PermissionGroup] WHERE [PermissionGroupId] > 8)
BEGIN
	DELETE FROM [PermissionGroup] WHERE [PermissionGroupId] > 8
END

IF EXISTS(SELECT * FROM [PermissionGroupsPermissions] WHERE [PermissionId] > 36)
BEGIN
	DELETE FROM [PermissionGroupsPermissions] WHERE [PermissionId] > 36
END
