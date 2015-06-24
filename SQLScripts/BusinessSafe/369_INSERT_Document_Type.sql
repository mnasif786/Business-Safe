USE [BusinessSafe]
GO

SET IDENTITY_INSERT [BusinessSafe].[dbo].[DocumentType] ON
GO

INSERT INTO [BusinessSafe].[dbo].[DocumentType] ([Id], [Name], [Deleted], [CreatedOn]) VALUES (16, 'Responsibility', 0 ,GETDATE())
GO

SET IDENTITY_INSERT [BusinessSafe].[dbo].[DocumentType] OFF
GO	

--//@UNDO
DELETE
FROM [BusinessSafe].[dbo].[DocumentType]
WHERE id IN (16)
GO


