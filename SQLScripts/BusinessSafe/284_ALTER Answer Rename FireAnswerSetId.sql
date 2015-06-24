USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Answer' AND COLUMN_NAME = 'FireAnswerSetId')
BEGIN
	EXEC sp_RENAME 'Answer.[FireAnswerSetId]' , 'FireRiskAssessmentChecklistId', 'COLUMN'
END
GO	

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Answer' AND COLUMN_NAME = 'FireAnswerSetId')
BEGIN
	EXEC sp_RENAME 'Answer.[FireRiskAssessmentChecklistId]' , 'FireAnswerSetId', 'COLUMN'
END
GO