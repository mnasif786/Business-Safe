USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessmentControlMeasure' AND COLUMN_NAME = 'ControlMeasure' AND CHARACTER_MAXIMUM_LENGTH = '150')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessmentControlMeasure]
	ALTER COLUMN [ControlMeasure] nvarchar(300)
END
GO

--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessmentControlMeasure' AND COLUMN_NAME = 'ControlMeasure' AND CHARACTER_MAXIMUM_LENGTH = '300')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessmentControlMeasure]
	ALTER COLUMN [ControlMeasure] nvarchar(150)
END
GO