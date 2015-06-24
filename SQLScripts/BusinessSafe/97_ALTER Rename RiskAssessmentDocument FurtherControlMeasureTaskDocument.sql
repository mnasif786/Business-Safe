USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentDocument')
BEGIN
	EXEC sp_rename 'RiskAssessmentDocument', 'PREVIOUS_RiskAssessmentDocument'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FurtherControlMeasureDocument')
BEGIN
	EXEC sp_rename 'FurtherControlMeasureDocument', 'PREVIOUS_FurtherControlMeasureDocument'
END

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_RiskAssessmentDocument')
BEGIN
	EXEC sp_rename 'PREVIOUS_RiskAssessmentDocument', 'RiskAssessmentDocument'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_FurtherControlMeasureDocument')
BEGIN
	EXEC sp_rename 'PREVIOUS_FurtherControlMeasureDocument', 'FurtherControlMeasureDocument'
END