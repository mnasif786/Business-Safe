USE [BusinessSafe]
GO

/****** Object:  Table [dbo].[Permission]    Script Date: 07/24/2012 14:12:50 ******/
SET IDENTITY_INSERT [dbo].[Permission] ON
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (37, N'UploadDocuments', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (38, N'DeleteDocuments', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)
INSERT [dbo].[Permission] ([PermissionId], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (39, N'DownloadDocuments', 0, GetDate(), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL)


SET IDENTITY_INSERT [dbo].[Permission] OFF

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM [BusinessSafe].[dbo].[Permission]
WHERE [PermissionId] >= 37 AND [PermissionId] <= 39
