IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	ADD [Deleted] [bit] NULL

	ALTER TABLE [dbo].[HazardousSubstanceRiskPhrase] ADD  CONSTRAINT [DF_HazardousSubstanceRiskPhrase_Deleted]  DEFAULT ((0)) FOR [Deleted]
END
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	ADD [CreatedOn] DATETIME NULL
	
	ALTER TABLE [dbo].[HazardousSubstanceRiskPhrase] ADD  CONSTRAINT [DF_HazardousSubstanceRiskPhrase_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
END
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	ADD [CreatedBy] UNIQUEIDENTIFIER NULL
		
	ALTER TABLE [dbo].[HazardousSubstanceRiskPhrase] ADD  CONSTRAINT [DF_HazardousSubstanceRiskPhrase_CreatedBy]  DEFAULT ('B03C83EE-39F2-4F88-B4C4-7C276B1AAD99') FOR [CreatedBy]
END
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	ADD [LastModifiedOn] DATETIME NULL
END
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	ADD [LastModifiedBy] UNIQUEIDENTIFIER NULL
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstanceRiskPhrase] 
	DROP CONSTRAINT [DF_HazardousSubstanceRiskPhrase_Deleted]
	
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	DROP COLUMN [Deleted] 
	
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstanceRiskPhrase] 
	DROP CONSTRAINT [DF_HazardousSubstanceRiskPhrase_CreatedOn]
	
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	DROP COLUMN [CreatedOn] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstanceRiskPhrase] 
	DROP CONSTRAINT [DF_HazardousSubstanceRiskPhrase_CreatedBy]
	
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	DROP COLUMN [CreatedBy] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	DROP COLUMN [LastModifiedOn] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	DROP COLUMN [LastModifiedBy] 
END
	