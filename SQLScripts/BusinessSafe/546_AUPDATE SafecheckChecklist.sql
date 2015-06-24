USE [BusinessSafe]
GO

UPDATE [SafeCheckChecklist] SET [LastModifiedBy] = 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99' WHERE [LastModifiedBy] IS NULL
UPDATE [SafeCheckChecklist] SET [LastModifiedOn] = GETDATE() WHERE [LastModifiedOn] IS NULL