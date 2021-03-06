USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherControlMeasureTask' AND COLUMN_NAME = 'OriginalFurtherControlMeasureTaskId')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherControlMeasureTask]
	ADD [OriginalFurtherControlMeasureTaskId] [bigint] NULL
	
END
GO	

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherControlMeasureTask' AND COLUMN_NAME = 'OriginalFurtherControlMeasureTaskId')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherControlMeasureTask]
	DROP COLUMN [OriginalFurtherControlMeasureTaskId]
END
GO