        
USE [BusinessSafe]

SET DATEFORMAT YMD

SET IDENTITY_INSERT [ResponsibilityCategory] ON;

IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 1)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (1, 'Fire Safety', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000')   
END
IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 2)
BEGIN           
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (2, 'First-Aid', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000')
END
IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 3)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (3, 'Food Safety', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000')
END
IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 4)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (4, 'Management of Health and Safety', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000')
END
IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 5)
BEGIN           
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (5, 'Plant & Equipment', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000')      
END
IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 6)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (6, 'Premises', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000')     		   
END
IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 7)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
			(7, 'Training', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000')     		   
END
IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 8)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
			(8, 'Occupational Health', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000')     
END
IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 9)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
			(9, 'Hazardous & Dangerous Substances', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000') 
END
IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 10)
BEGIN
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
			(10, 'Health Care', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000') 
END
IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 11)
BEGIN			
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
			(11, 'Other', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000') 
END

SET IDENTITY_INSERT [ResponsibilityCategory] OFF;

--//@UNDO 
USE [BusinessSafe]
GO
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 1)
BEGIN
	DELETE FROM ResponsibilityCategory WHERE Id = 1
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 2)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 2
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 3)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 3
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 4)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 4
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 5)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 5
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 6)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 6
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 7)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 7
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 8)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 8
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 9)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 9
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 10)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 10
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 11)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 11
END
	
GO