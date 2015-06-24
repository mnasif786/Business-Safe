USE [BusinessSafe]
GO

SET IDENTITY_INSERT [BusinessSafe].[dbo].[DocumentType] ON

INSERT INTO [BusinessSafe].[dbo].[DocumentType] ([Id], [Name], [Deleted], [CreatedOn]) VALUES (10, 'GRA Review Document', 0 ,GETDATE())
INSERT INTO [BusinessSafe].[dbo].[DocumentType] ([Id], [Name], [Deleted], [CreatedOn]) VALUES (11, 'HSRA Review Document', 0 ,GETDATE())
GO

SET IDENTITY_INSERT [BusinessSafe].[dbo].[DocumentType] OFF
	

--//@UNDO
DELETE
FROM [BusinessSafe].[dbo].[DocumentType]
WHERE id IN (10, 11)



