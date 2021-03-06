USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherControlMeasureTask' AND COLUMN_NAME = 'FollowingFurtherControlMeasureTaskId')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherControlMeasureTask]
	ADD [FollowingFurtherControlMeasureTaskId] [bigint] NULL
	
END
GO	

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherControlMeasureTask' AND COLUMN_NAME = 'FollowingFurtherControlMeasureTaskId')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherControlMeasureTask]
	DROP COLUMN [FollowingFurtherControlMeasureTaskId]
END
GO