--------------------------------------------------------------------------------------------------------------------------------------------------------
			/* Task Category required for user interface ddl */
----------------------------------------------------------------------------------------------------------------------------------------------------------
SET DATEFORMAT YMD

SET IDENTITY_INSERT [ResponsibilityTaskCategory] ON;


INSERT INTO [BusinessSafe].[dbo].[ResponsibilityTaskCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (1, 'My Tasks', 0, '2012-03-27 11:00:00.000', null, null, '2012-03-27 12:00:00.000')   
           
INSERT INTO [BusinessSafe].[dbo].[ResponsibilityTaskCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (2, 'Fire Risk Assessment', 0, '2012-03-27 11:00:00.000', null, null, '2012-03-27 12:00:00.000')

INSERT INTO [BusinessSafe].[dbo].[ResponsibilityTaskCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (3, 'General Risk Assessment', 0, '2012-03-27 11:00:00.000', null, null, '2012-03-27 12:00:00.000')
           
INSERT INTO [BusinessSafe].[dbo].[ResponsibilityTaskCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (4, 'Accident Report', 0, '2012-03-27 11:00:00.000', null, null, '2012-03-27 12:00:00.000')
           
INSERT INTO [BusinessSafe].[dbo].[ResponsibilityTaskCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (5, 'Personal', 0, '2012-03-27 11:00:00.000', null, null, '2012-03-27 12:00:00.000')                      

           

SET IDENTITY_INSERT [ResponsibilityTaskCategory] OFF;

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM ResponsibilityTaskCategory WHERE Id = 1
DELETE FROM ResponsibilityTaskCategory WHERE Id = 2
DELETE FROM ResponsibilityTaskCategory WHERE Id = 3
DELETE FROM ResponsibilityTaskCategory WHERE Id = 4
