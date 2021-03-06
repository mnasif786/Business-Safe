USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'TaskCompletionDueDate')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	ADD [TaskCompletionDueDate] [datetime] NULL	
END
GO	


--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'TaskCompletionDueDate')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	DROP COLUMN [TaskCompletionDueDate]
END
GO