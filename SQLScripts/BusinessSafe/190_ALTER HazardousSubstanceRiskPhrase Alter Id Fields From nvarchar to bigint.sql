USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'HazardousSubstanceId')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	ALTER COLUMN [HazardousSubstanceId] bigint not null
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'RiskPhraseId')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	ALTER COLUMN [RiskPhraseId] bigint not null
END
GO

--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'HazardousSubstanceId')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	ALTER COLUMN [HazardousSubstanceId] nvarchar(200) not null
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'RiskPhraseId')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	ALTER COLUMN [RiskPhraseId] nvarchar(50) not null
END
GO