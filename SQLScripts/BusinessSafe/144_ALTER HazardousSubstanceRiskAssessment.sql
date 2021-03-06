USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'Quantity')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	ADD [Quantity] [int] NULL		
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'MatterState')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	ADD [MatterState] [int] NULL		
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'DustinessOrVolatility')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	ADD [DustinessOrVolatility] [int] NULL		
END
GO	

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'Quantity')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [Quantity]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'MatterState')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN  [MatterState]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'DustinessOrVolatility')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [DustinessOrVolatility]
END
GO

