USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM [dbo].[DocHandlerDocumentType] WHERE [Id] = 127 AND [DocumentGroupId] = 1)
BEGIN
	INSERT [dbo].[DocHandlerDocumentType] ([Id], [DocumentGroupId]) VALUES (127, 1)
END

IF NOT EXISTS(SELECT * FROM [dbo].[DocHandlerDocumentType] WHERE [Id] = 128 AND [DocumentGroupId] = 1)
BEGIN
	INSERT [dbo].[DocHandlerDocumentType] ([Id], [DocumentGroupId]) VALUES (128, 1)
END

--//@UNDO 
USE [BusinessSafe]
GO  

IF EXISTS(SELECT * FROM [dbo].[DocHandlerDocumentType] WHERE [Id] = 127 AND [DocumentGroupId] = 1)
BEGIN
	DELETE FROM [dbo].[DocHandlerDocumentType] WHERE [Id] = 127 AND [DocumentGroupId] = 1
END

IF EXISTS(SELECT * FROM [dbo].[DocHandlerDocumentType] WHERE [Id] = 128 AND [DocumentGroupId] = 1)
BEGIN
	DELETE FROM [dbo].[DocHandlerDocumentType] WHERE [Id] = 128 AND [DocumentGroupId] = 1
END
