USE [BusinessSafe]
GO

UPDATE [Section] SET [ChecklistId] = 5 WHERE [Id] BETWEEN 35 AND 42

--//@UNDO 

UPDATE [Section] SET [ChecklistId] = 4 WHERE [Id] BETWEEN 35 AND 42
