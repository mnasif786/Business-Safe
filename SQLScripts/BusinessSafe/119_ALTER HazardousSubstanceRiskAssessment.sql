USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'IsInhalationRouteOfEntry')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	ADD [IsInhalationRouteOfEntry] [bit] NULL		
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'IsIngestionRouteOfEntry')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	ADD [IsIngestionRouteOfEntry] [bit] NULL		
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'IsAbsorptionRouteOfEntry')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	ADD [IsAbsorptionRouteOfEntry] [bit] NULL	
END
GO	


--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'IsInhalationRouteOfEntry')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [IsInhalationRouteOfEntry]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'IsIngestionRouteOfEntry')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [IsIngestionRouteOfEntry]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'IsAbsorptionRouteOfEntry')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [IsAbsorptionRouteOfEntry]
END
GO