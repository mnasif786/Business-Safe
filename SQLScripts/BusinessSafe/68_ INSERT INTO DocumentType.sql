--------------------------------------------------------------------------------------------------------------------------------------------------------
			/*  DocumentTypes required for ddl */
----------------------------------------------------------------------------------------------------------------------------------------------------------

USE [BusinessSafe]
GO

print 'Insert data into [DocumentType]'
SET IDENTITY_INSERT [DocumentType] ON;
Go

	INSERT INTO [BusinessSafe].[dbo].[DocumentType]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (1
           ,'GRA Document'
           ,0           
           ,getdate()
           ,null)
           
	INSERT INTO [BusinessSafe].[dbo].[DocumentType]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (2
           ,'Document Type 2'
           ,0           
           ,getdate()
           ,null)
           
	INSERT INTO [BusinessSafe].[dbo].[DocumentType]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (3
           ,'Document Type3'
           ,0           
           ,getdate()
           ,null)
GO

SET IDENTITY_INSERT [DocumentType] OFF;
GO

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM [Hazard] WHERE Id = 1
DELETE FROM [Hazard] WHERE Id = 2
DELETE FROM [Hazard] WHERE Id = 3

Go