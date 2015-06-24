USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM [DocHandlerDocumentType] WHERE [Id] = 131 AND [DocumentGroupId] = 1)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType] ([Id], [DocumentGroupId]) VALUES (131, 1)   
END
GO

--//@UNDO 
USE [BusinessSafe]
GO

IF EXISTS(SELECT * FROM [DocHandlerDocumentType] WHERE [Id] = 131 AND [DocumentGroupId] = 1)
BEGIN
	DELETE FROM [BusinessSafe].[dbo].[DocHandlerDocumentType] WHERE [Id] = 131 AND [DocumentGroupId] = 1
END
GO