USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM [BusinessSafe].[dbo].[DocumentType] WHERE [Name] = 'Action')
BEGIN
	
	INSERT INTO [BusinessSafe].[dbo].[DocumentType]
           ([Name]
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy]
           ,[LastModifiedOn]
           ,[LastModifiedBy])
     VALUES
           ('Action', 0, getdate(), null, getdate(), null)
END
GO

--//@UNDO 
USE [BusinessSafe]
GO

IF EXISTS(SELECT * FROM [BusinessSafe].[dbo].[DocumentType] WHERE [Name] = 'Action')
BEGIN
	DELETE FROM [BusinessSafe].[dbo].[DocumentType] WHERE [Name] = 'Action'
END
GO