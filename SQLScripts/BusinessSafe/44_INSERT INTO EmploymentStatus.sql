
USE [BusinessSafe]
GO

INSERT [dbo].[EmploymentStatus] ([Id], [Name], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (1, N'Employed', '4CC5B9B8-E335-4E27-9AA3-C48B9AA6BC4D', getdate(), getdate(), '4CC5B9B8-E335-4E27-9AA3-C48B9AA6BC4D', 0)


--//@UNDO 
USE [BusinessSafe]
GO  

DELETE FROM [EmploymentStatus] 
