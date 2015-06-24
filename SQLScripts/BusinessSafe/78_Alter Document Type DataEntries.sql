----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the DocumentType table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

UPDATE [BusinessSafe].[dbo].[DocumentType] SET [Name] = 'Checklist' WHERE ID = 2
GO
--//@UNDO 

UPDATE [BusinessSafe].[dbo].[DocumentType] SET [Name] ='Document Type 2' WHERE ID = 2
GO
