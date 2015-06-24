--------------------------------------------------------------------------------------------------------------------------------------------------------
			/*  Hazards required for ddl */
----------------------------------------------------------------------------------------------------------------------------------------------------------

USE [BusinessSafe]
GO

print 'Insert data into [CompanyVehicleType]'
--SET IDENTITY_INSERT [CompanyVehicleType] ON;
Go

	INSERT INTO [BusinessSafe].[dbo].[CompanyVehicleType]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy]
           ,[LastModifiedOn]
           ,[LastModifiedBy]
           )
     VALUES
           (1
           ,'Car'
           ,0           
           ,getdate()
           ,'b03c83ee-39f2-4f88-b4c4-7c276b1aad99'
           ,getdate()
           ,'b03c83ee-39f2-4f88-b4c4-7c276b1aad99')
           
        
GO

--SET IDENTITY_INSERT [CompanyVehicleType] OFF;

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM [CompanyVehicleType] WHERE Id = 1

Go