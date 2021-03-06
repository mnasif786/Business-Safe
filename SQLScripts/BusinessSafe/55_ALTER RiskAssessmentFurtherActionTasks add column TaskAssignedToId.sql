USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'TaskAssignedToId')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	ADD [TaskAssignedToId] [uniqueidentifier] NULL	
END
GO	


--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'TaskAssignedToId')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	DROP COLUMN [TaskAssignedToId]
END
GO