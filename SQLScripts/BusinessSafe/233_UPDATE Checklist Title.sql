USE [BusinessSafe]
GO

UPDATE [BusinessSafe].[dbo].[Checklist] SET [Title] = 'Display Screen Equipment' WHERE Id = 1
UPDATE [BusinessSafe].[dbo].[Checklist] SET [Title] = 'Manual Handling' WHERE Id = 2

--//@UNDO 

UPDATE [BusinessSafe].[dbo].[Checklist] SET [Title] = 'Display Screen Equipment Self Assessment Questionnaire' WHERE Id = 1
UPDATE [BusinessSafe].[dbo].[Checklist] SET [Title] = 'Manual Handling Self Assessment Questionnaire' WHERE Id = 2
