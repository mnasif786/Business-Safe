USE [BusinessSafe]
GO

SET IDENTITY_INSERT [ResponsibilityTaskCategory] ON;

INSERT INTO [BusinessSafe].[dbo].[ResponsibilityTaskCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
           (6, 'Hazardous Substance Risk Assessment', 0, '2012-03-27 11:00:00.000', null, null, '2012-03-27 12:00:00.000')                      

SET IDENTITY_INSERT [ResponsibilityTaskCategory] OFF;

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM ResponsibilityTaskCategory WHERE Id = 6
