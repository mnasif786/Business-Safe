USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Checklist' AND COLUMN_NAME = 'ChecklistRiskAssessmentType')
BEGIN
	ALTER TABLE [Checklist]
	ADD [ChecklistRiskAssessmentType] [int] NULL
END
GO	

IF EXISTS (SELECT * FROM [Checklist] WHERE [ChecklistRiskAssessmentType] IS NULL)
BEGIN
	UPDATE [Checklist] SET [ChecklistRiskAssessmentType] = 1 WHERE [ChecklistRiskAssessmentType] IS NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Checklist' AND COLUMN_NAME = 'ChecklistRiskAssessmentType' AND IS_NULLABLE = 'NO')
BEGIN
	ALTER TABLE [Checklist]
	ALTER COLUMN [ChecklistRiskAssessmentType] [int] NOT NULL
END
GO	

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Checklist' AND COLUMN_NAME = 'ChecklistRiskAssessmentType')
BEGIN
	ALTER TABLE [Answer]
	DROP COLUMN [ChecklistRiskAssessmentType]
END
GO