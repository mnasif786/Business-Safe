USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'FurtherControlMeasureTaskCategoryId')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	ADD FurtherControlMeasureTaskCategoryId BIGINT NOT NULL DEFAULT 3
END
GO	

--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'FurtherControlMeasureTaskCategoryId')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	DROP COLUMN [FurtherControlMeasureTaskCategoryId]
END
GO