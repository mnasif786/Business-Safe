USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessment' AND COLUMN_NAME = 'RiskAssessorEmployeeId' AND DATA_TYPE = 'uniqueidentifier')
BEGIN
	ALTER TABLE RiskAssessment
	DROP COLUMN RiskAssessorEmployeeId

	ALTER TABLE RiskAssessment
	ADD RiskAssessorEmployeeId [uniqueidentifier] NULL
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessment' AND COLUMN_NAME = 'RiskAssessorEmployeeId' AND DATA_TYPE = 'uniqueidentifier')
BEGIN
	ALTER TABLE RiskAssessment
	DROP COLUMN RiskAssessorEmployeeId
	
	ALTER TABLE RiskAssessment
	ADD RiskAssessorEmployeeId [bigint] NULL
END
GO