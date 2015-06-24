USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'PersonAppointed')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	ADD [PersonAppointed] [nvarchar](100)
END
GO

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'PersonAppointed')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [PersonAppointed]
END
GO