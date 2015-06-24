USE [BusinessSafe]
GO

SET IDENTITY_INSERT [DocumentType] ON;

INSERT INTO [BusinessSafe].[dbo].[DocumentType] ([Id], [Name],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (9, 'HSRA Document', 0, '2012-03-27 11:00:00.000', null, null, '2012-03-27 12:00:00.000')                      

SET IDENTITY_INSERT [DocumentType] OFF;

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM [DocumentType] WHERE Id = 9
