USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' 
	AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
    DROP COLUMN [CreatedBy]
END
GO	

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' 
	AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
    DROP COLUMN [CreatedOn]
END
GO	

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' 
	AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
    DROP COLUMN [Deleted]
END
GO	

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' 
	AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
    DROP COLUMN [LastModifiedBy]
END
GO	

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' 
	AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
    DROP COLUMN [LastModifiedOn]
END
GO	

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' 
	AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
    DROP COLUMN [CreatedBy]
END
GO	

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' 
	AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
    DROP COLUMN [CreatedOn]
END
GO	

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' 
	AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
    DROP COLUMN [Deleted]
END
GO	

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' 
	AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
    DROP COLUMN [LastModifiedBy]
END
GO	

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' 
	AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
    DROP COLUMN [LastModifiedOn]
END
GO	

--//@UNDO 
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' 
	AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE HazardousSubstanceSafetyPhrase
	ADD [CreatedBy] [uniqueidentifier] NOT NULL		
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' 
	AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
    ADD [CreatedOn] [datetime] NOT NULL	
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' 
	AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
    ADD [Deleted] [bit] NOT NULL	
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' 
	AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE HazardousSubstanceSafetyPhrase
	ADD [LastModifiedBy] [uniqueidentifier] NULL		
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' 
	AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
    ADD [LastModifiedOn] [datetime] NULL	
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' 
	AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE HazardousSubstanceRiskPhrase
	ADD [CreatedBy] [uniqueidentifier] NOT NULL		
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' 
	AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
    ADD [CreatedOn] [datetime] NOT NULL	
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' 
	AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
    ADD [Deleted] [bit] NOT NULL	
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' 
	AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE HazardousSubstanceRiskPhrase
	ADD [LastModifiedBy] [uniqueidentifier] NULL		
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' 
	AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
    ADD [LastModifiedOn] [datetime] NULL	
END
GO	