USE [BusinessSafe]
GO

SET IDENTITY_INSERT [DocumentType] ON;
GO

IF NOT EXISTS (SELECT * FROM [BusinessSafe].[dbo].[DocumentType] WHERE ID = 3)
	BEGIN
		INSERT INTO [BusinessSafe].[dbo].[DocumentType]
			   ([ID]
			   ,[Name] 
			   ,[Deleted]
			   ,[CreatedOn]
			   ,[CreatedBy])
		 VALUES
			   (3
			   ,'Procedures'
			   ,0           
			   ,getdate()
			   ,null)
	END

IF NOT EXISTS (SELECT * FROM [BusinessSafe].[dbo].[DocumentType] WHERE ID = 4)
	BEGIN
		INSERT INTO [BusinessSafe].[dbo].[DocumentType]
			   ([ID]
			   ,[Name] 
			   ,[Deleted]
			   ,[CreatedOn]
			   ,[CreatedBy])
		 VALUES
			   (4
			   ,'Method Statements'
			   ,0           
			   ,getdate()
			   ,null)
    END

IF NOT EXISTS (SELECT * FROM [BusinessSafe].[dbo].[DocumentType] WHERE ID = 5)
	BEGIN
		INSERT INTO [BusinessSafe].[dbo].[DocumentType]
			   ([ID]
			   ,[Name] 
			   ,[Deleted]
			   ,[CreatedOn]
			   ,[CreatedBy])
		 VALUES
			   (5
			   ,'Photographs'
			   ,0           
			   ,getdate()
			   ,null)
	END

IF NOT EXISTS (SELECT * FROM [BusinessSafe].[dbo].[DocumentType] WHERE ID = 6)
	BEGIN
		INSERT INTO [BusinessSafe].[dbo].[DocumentType]
			   ([ID]
			   ,[Name] 
			   ,[Deleted]
			   ,[CreatedOn]
			   ,[CreatedBy])
		 VALUES
			   (6
			   ,'Safety Record Forms'
			   ,0           
			   ,getdate()
			   ,null)
	END

IF NOT EXISTS (SELECT * FROM [BusinessSafe].[dbo].[DocumentType] WHERE ID = 7)
	BEGIN
	INSERT INTO [BusinessSafe].[dbo].[DocumentType]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (7
           ,'Inspection reports'
           ,0           
           ,getdate()
           ,null)
	END           

IF NOT EXISTS (SELECT * FROM [BusinessSafe].[dbo].[DocumentType] WHERE ID = 8)
	BEGIN
	INSERT INTO [BusinessSafe].[dbo].[DocumentType]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (8
           ,'Misc'
           ,0           
           ,getdate()
           ,null)           
	END
GO

SET IDENTITY_INSERT [DocumentType] OFF;
GO

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM [DocumentType] WHERE Id in (3, 4, 5, 6, 7, 8)
Go