USE [BusinessSafe]
GO

/****** Object:  Table [dbo].[Permission]    Script Date: 07/24/2012 14:12:50 ******/
SET IDENTITY_INSERT [dbo].[DocumentType] ON
INSERT [dbo].[DocumentType] ([Id], [Name], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (13, N'PRAReview', CAST(0x0000A09F0118B5E6 AS DateTime), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL, 0)
INSERT [dbo].[DocumentType] ([Id], [Name], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (14, N'FRA Document', CAST(0x0000A09F0118B5E6 AS DateTime), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL, 0)
INSERT [dbo].[DocumentType] ([Id], [Name], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (15, N'FRAReview', CAST(0x0000A09F0118B5E6 AS DateTime), N'f9ad7115-be16-476e-8d58-fb6aeeebb51a', NULL, NULL, 0)

SET IDENTITY_INSERT [dbo].[DocumentType] OFF

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM [BusinessSafe].[dbo].[DocumentType]
WHERE [Id] >= 13 AND [Id] <=15
