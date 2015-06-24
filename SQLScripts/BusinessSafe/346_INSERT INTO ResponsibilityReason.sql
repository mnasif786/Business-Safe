        
USE [BusinessSafe]

SET DATEFORMAT YMD

SET IDENTITY_INSERT [ResponsibilityReason] ON;

IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 1)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityReason] ([Id], [Reason],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (1, 'Compliance', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000')   
END
IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 2)
BEGIN           
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityReason] ([Id], [Reason],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (2, 'Statutory', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000')
END
IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 3)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityReason] ([Id], [Reason],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (3, 'Best Practice', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000')
END
IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 4)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityReason] ([Id], [Reason],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (4, 'Accident', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000')
END
IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 5)
BEGIN           
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityReason] ([Id], [Reason],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (5, 'Training Failure', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000')      
END
IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 6)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityReason] ([Id], [Reason],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (6, 'Procedure Failure', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000')     		   
END
IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 7)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityReason] ([Id], [Reason],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
			(7, 'Equipment Failure', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000')     		   
END


SET IDENTITY_INSERT [ResponsibilityReason] OFF;

--//@UNDO 
USE [BusinessSafe]
GO
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 1)
BEGIN
	DELETE FROM ResponsibilityCategory WHERE Id = 1
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 2)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 2
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 3)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 3
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 4)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 4
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 5)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 5
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 6)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 6
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 7)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 7
END
	
GO