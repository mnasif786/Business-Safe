        
USE [BusinessSafe]

SET DATEFORMAT YMD

SET IDENTITY_INSERT TaskCategory ON;

IF NOT EXISTS(SELECT * FROM [dbo].TaskCategory WHERE [Id] = 7)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].TaskCategory ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (7, 'Responsibility', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000')   
END


SET IDENTITY_INSERT [TaskCategory] OFF;

--//@UNDO 
USE [BusinessSafe]
GO
IF EXISTS(SELECT * FROM [dbo].[TaskCategory] WHERE [Id] = 7)
BEGIN
	DELETE FROM TaskCategory WHERE Id = 7
END
	
GO