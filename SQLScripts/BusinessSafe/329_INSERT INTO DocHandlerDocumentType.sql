USE [BusinessSafe]
GO

INSERT [dbo].[DocHandlerDocumentType] ([Id], [DocumentGroupId]) VALUES (135, 1)


--//@UNDO 
USE [BusinessSafe]
GO  

DELETE FROM [dbo].[DocHandlerDocumentType] WHERE [Id] = 135 AND [DocumentGroupId] = 1
