USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'TaskStatusId')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	ADD [TaskStatusId] [smallint] NULL
END
GO	


--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'TaskStatusId')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	DROP COLUMN [TaskStatusId]
END
GO