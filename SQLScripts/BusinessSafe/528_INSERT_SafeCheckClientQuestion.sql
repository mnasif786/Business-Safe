USE [BusinessSafe]
GO

DELETE FROM [SafeCheckClientQuestion]
GO

INSERT INTO [SafeCheckClientQuestion]([Id], [ClientId], [ClientAccountNumber], [QuestionId])
SELECT NEWID(), 55881, 'DEN101', [Id] 
FROM [SafeCheckQuestion]




 