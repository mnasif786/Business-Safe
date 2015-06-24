USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'Description' AND 'CHARACTER_MAXIMUM_LENGTH' = '50')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	ALTER COLUMN [Description] nvarchar(500)
END
GO	

--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'Description' AND 'CHARACTER_MAXIMUM_LENGTH' = '500')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	ALTER COLUMN [Description] nvarchar(50)
END
GO