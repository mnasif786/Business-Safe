USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'HealthSurveillanceRequired')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	ADD [HealthSurveillanceRequired] [bit] NULL		
END
GO	

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'HealthSurveillanceRequired')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [HealthSurveillanceRequired] 
END
GO	

