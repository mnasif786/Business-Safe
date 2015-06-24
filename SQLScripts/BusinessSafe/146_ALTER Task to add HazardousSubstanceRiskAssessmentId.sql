USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'HazardousSubstanceRiskAssessmentId')
BEGIN
	ALTER TABLE [Task]
	ADD [HazardousSubstanceRiskAssessmentId] [bigint] NULL		
END
GO	

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'HazardousSubstanceRiskAssessmentId')
BEGIN
	ALTER TABLE [Task]
	DROP COLUMN [HazardousSubstanceRiskAssessmentId]
END
GO