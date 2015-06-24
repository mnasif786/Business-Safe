USE [BusinessSafe]
GO

UPDATE [dbo].[SafetyPhrase] 
SET [RequiresAdditionalInformation] = 1
WHERE [Id] IN (11, 12, 20, 28, 33, 42, 44, 45, 48, 49, 51, 66, 67, 69, 72, 76, 83)

--//@UNDO 
USE [BusinessSafe]
GO  


UPDATE [dbo].[SafetyPhrase] 
SET [RequiresAdditionalInformation] = 0
WHERE [Id] IN (11, 12, 20, 28, 33, 42, 44, 45, 48, 49, 51, 66, 67, 69, 72, 76, 83)
