USE [BusinessSafe]
GO

INSERT INTO [dbo].[GeneralRiskAssessment]
SELECT [Id] FROM [dbo].[MultiHazardRiskAssessment]

--//@UNDO

DELETE FROM [dbo].[GeneralRiskAssessment]