USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentFurtherControlMeasureTask')
BEGIN
	EXEC sp_rename 'RiskAssessmentFurtherControlMeasureTask', 'FurtherControlMeasureTask'
END

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FurtherControlMeasureTask')
BEGIN
	EXEC sp_rename 'FurtherControlMeasureTask', 'RiskAssessmentFurtherControlMeasureTask'
END