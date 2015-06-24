USE [BusinessSafe]
GO

UPDATE [BusinessSafe].[dbo].[Checklist] SET [Title] = 'Children and Young Persons' WHERE Id = 3

--//@UNDO 

UPDATE [BusinessSafe].[dbo].[Checklist] SET [Title] = 'Children and Young Persons Information Checklist' WHERE Id = 3
