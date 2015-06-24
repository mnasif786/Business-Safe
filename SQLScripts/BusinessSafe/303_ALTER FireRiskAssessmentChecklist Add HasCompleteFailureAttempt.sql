USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentChecklist' AND COLUMN_NAME = 'HasCompleteFailureAttempt')
BEGIN
	ALTER TABLE [FireRiskAssessmentChecklist]
	ADD [HasCompleteFailureAttempt] [bit] NULL
END
GO


--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentChecklist' AND COLUMN_NAME = 'HasCompleteFailureAttempt')
BEGIN
	ALTER TABLE [FireRiskAssessmentChecklist]
	DROP COLUMN [HasCompleteFailureAttempt] 
END
GO