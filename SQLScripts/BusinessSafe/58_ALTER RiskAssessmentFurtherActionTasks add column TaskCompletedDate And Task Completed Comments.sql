USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'TaskCompletedDate')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	ADD [TaskCompletedDate] [datetime] NULL
	
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'TaskCompletedComments')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	ADD [TaskCompletedComments] [nvarchar] (250) NULL	
END
GO	


--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'TaskCompletedDate')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	DROP COLUMN [TaskCompletedDate]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'TaskCompletedComments')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	DROP COLUMN [TaskCompletedComments]
END
GO