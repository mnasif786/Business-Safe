IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	ADD [Deleted] [bit] NULL

	ALTER TABLE [dbo].[HazardousSubstanceSafetyPhrase] ADD  CONSTRAINT [DF_HazardousSubstanceSafetyPhrase_Deleted]  DEFAULT ((0)) FOR [Deleted]
END
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	ADD [CreatedOn] DATETIME NULL
	
	ALTER TABLE [dbo].[HazardousSubstanceSafetyPhrase] ADD  CONSTRAINT [DF_HazardousSubstanceSafetyPhrase_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
END
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	ADD [CreatedBy] UNIQUEIDENTIFIER NULL
		
	ALTER TABLE [dbo].[HazardousSubstanceSafetyPhrase] ADD  CONSTRAINT [DF_HazardousSubstanceSafetyPhrase_CreatedBy]  DEFAULT ('B03C83EE-39F2-4F88-B4C4-7C276B1AAD99') FOR [CreatedBy]
END
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	ADD [LastModifiedOn] DATETIME NULL
END
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	ADD [LastModifiedBy] UNIQUEIDENTIFIER NULL
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstanceSafetyPhrase] 
	DROP CONSTRAINT [DF_HazardousSubstanceSafetyPhrase_Deleted]
	
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	DROP COLUMN [Deleted] 
	
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstanceSafetyPhrase] 
	DROP CONSTRAINT [DF_HazardousSubstanceSafetyPhrase_CreatedOn]
	
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	DROP COLUMN [CreatedOn] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstanceSafetyPhrase] 
	DROP CONSTRAINT [DF_HazardousSubstanceSafetyPhrase_CreatedBy]
	
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	DROP COLUMN [CreatedBy] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	DROP COLUMN [LastModifiedOn] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	DROP COLUMN [LastModifiedBy] 
END
	