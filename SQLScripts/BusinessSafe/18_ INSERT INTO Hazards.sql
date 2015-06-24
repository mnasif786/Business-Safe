--------------------------------------------------------------------------------------------------------------------------------------------------------
			/*  Hazards required for ddl */
----------------------------------------------------------------------------------------------------------------------------------------------------------

USE [BusinessSafe]
GO

print 'Insert data into [Hazard]'
SET IDENTITY_INSERT [Hazard] ON;
Go

	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (1
           ,'Asbestos'
           ,0           
           ,getdate()
           ,null)
           
	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (2
           ,'Confined Spaces'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (3
           ,'Display Screens'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (4
           ,'Electrical'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (5
           ,'Fire'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (6
           ,'Gas Appliances and Equipment'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (7
           ,'Hazardous substances'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (8
           ,'Infection'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (9
           ,'Manual handling'
           ,0           
           ,getdate()
           ,null)
           	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (10
           ,'Mechanical'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (11
           ,'Noise'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (12
           ,'Repetitive Movements'
           ,0           
           ,getdate()
           ,null)
           
           	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (13
           ,'Public or Visitor Access'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (14
           ,'Road risk'
           ,0           
           ,getdate()
           ,null)

	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (15
           ,'Radiation'
           ,0           
           ,getdate()
           ,null)
           	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (16
           ,'Stress (work Related)'
           ,0           
           ,getdate()
           ,null)
           	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (17
           ,'Uneven, Wet or Slippery Floors'
           ,0           
           ,getdate()
           ,null)
           	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (18
           ,'Vibration'
           ,0           
           ,getdate()
           ,null)
           
	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (19
           ,'Violence, threatening behaviour'
           ,0           
           ,getdate()
           ,null)
           	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (20
           ,'Asbestos'
           ,0           
           ,getdate()
           ,null)
           
           	INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (21
           ,'Work at height'
           ,0           
           ,getdate()
           ,null)
           	
     INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (22
           ,'Work equipment'
           ,0           
           ,getdate()
           ,null)

     INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (23
           ,'Working Alone'
           ,0           
           ,getdate()
           ,null)

     INSERT INTO [BusinessSafe].[dbo].[Hazard]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (24
           ,'Workplace Transport'
           ,0           
           ,getdate()
           ,null)
           
GO

SET IDENTITY_INSERT [Hazard] OFF;
GO

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM [Hazard] WHERE Id = 1
DELETE FROM [Hazard] WHERE Id = 2
DELETE FROM [Hazard] WHERE Id = 3
DELETE FROM [Hazard] WHERE Id = 4
DELETE FROM [Hazard] WHERE Id = 5
DELETE FROM [Hazard] WHERE Id = 6
DELETE FROM [Hazard] WHERE Id = 7
DELETE FROM [Hazard] WHERE Id = 8
DELETE FROM [Hazard] WHERE Id = 9
DELETE FROM [Hazard] WHERE Id = 10
DELETE FROM [Hazard] WHERE Id = 11
DELETE FROM [Hazard] WHERE Id = 12
DELETE FROM [Hazard] WHERE Id = 13
DELETE FROM [Hazard] WHERE Id = 14
DELETE FROM [Hazard] WHERE Id = 15
DELETE FROM [Hazard] WHERE Id = 16
DELETE FROM [Hazard] WHERE Id = 17
DELETE FROM [Hazard] WHERE Id = 18
DELETE FROM [Hazard] WHERE Id = 19
DELETE FROM [Hazard] WHERE Id = 20
DELETE FROM [Hazard] WHERE Id = 21
DELETE FROM [Hazard] WHERE Id = 22
DELETE FROM [Hazard] WHERE Id = 23
DELETE FROM [Hazard] WHERE Id = 24

Go