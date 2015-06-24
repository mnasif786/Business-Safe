--------------------------------------------------------------------------------------------------------------------------------------------------------
			/*  Hazards required for ddl */
----------------------------------------------------------------------------------------------------------------------------------------------------------

USE [BusinessSafe]
GO

print 'Insert data into [PeopleAtRisk]'
SET IDENTITY_INSERT [PeopleAtRisk] ON;
Go

	INSERT INTO [BusinessSafe].[dbo].[PeopleAtRisk]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (1
           ,'Employees'
           ,0           
           ,getdate()
           ,null)
           
	INSERT INTO [BusinessSafe].[dbo].[PeopleAtRisk]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (2
           ,'Contractors'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[PeopleAtRisk]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (3
           ,'Members of the Public'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[PeopleAtRisk]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (4
           ,'Patients'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[PeopleAtRisk]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (5
           ,'Residents'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[PeopleAtRisk]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (6
           ,'Volunteers'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[PeopleAtRisk]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (7
           ,'New and Expectant Mothers'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[PeopleAtRisk]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (8
           ,'Service Users'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[PeopleAtRisk]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (9
           ,'Children & Young Persons'
           ,0           
           ,getdate()
           ,null)
         
GO

SET IDENTITY_INSERT [PeopleAtRisk] OFF;

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM [PeopleAtRisk] WHERE Id = 1
DELETE FROM [PeopleAtRisk] WHERE Id = 2
DELETE FROM [PeopleAtRisk] WHERE Id = 3
DELETE FROM [PeopleAtRisk] WHERE Id = 4
DELETE FROM [PeopleAtRisk] WHERE Id = 5
DELETE FROM [PeopleAtRisk] WHERE Id = 6
DELETE FROM [PeopleAtRisk] WHERE Id = 7
DELETE FROM [PeopleAtRisk] WHERE Id = 8
DELETE FROM [PeopleAtRisk] WHERE Id = 9
Go