--------------------------------------------------------------------------------------------------------------------------------------------------------
			/* Add system users */
----------------------------------------------------------------------------------------------------------------------------------------------------------

USE [BusinessSafe]
GO

INSERT INTO [BusinessSafe].[dbo].[User]
           ([UserId]           
           ,[RoleId]           
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy]           
           ,[ClientId]
           ,[SiteId])
     VALUES
           ('B03C83EE-39F2-4F88-B4C4-7C276B1AAD99'
  
           ,'BACF7C01-D210-4DBC-942F-15D8456D3B92'
           ,0
           ,GETDATE()
           ,'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99' 
           ,0
           ,0)
GO



--//@UNDO 
USE [BusinessSafe]
GO  

DELETE FROM [User] WHERE [UserId] = N'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99'
