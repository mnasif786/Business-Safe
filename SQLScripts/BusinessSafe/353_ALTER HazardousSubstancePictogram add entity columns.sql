IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstancePictogram' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [HazardousSubstancePictogram]
	ADD [Deleted] [bit] NULL

	ALTER TABLE [dbo].[HazardousSubstancePictogram] ADD  CONSTRAINT [DF_HazardousSubstancePictogram_Deleted]  DEFAULT ((0)) FOR [Deleted]
END
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstancePictogram' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [HazardousSubstancePictogram]
	ADD [CreatedOn] DATETIME NULL
	
	ALTER TABLE [dbo].[HazardousSubstancePictogram] ADD  CONSTRAINT [DF_HazardousSubstancePictogram_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
END
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstancePictogram' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [HazardousSubstancePictogram]
	ADD [CreatedBy] UNIQUEIDENTIFIER NULL
		
	ALTER TABLE [dbo].[HazardousSubstancePictogram] ADD  CONSTRAINT [DF_HazardousSubstancePictogram_CreatedBy]  DEFAULT ('B03C83EE-39F2-4F88-B4C4-7C276B1AAD99') FOR [CreatedBy]
END
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstancePictogram' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [HazardousSubstancePictogram]
	ADD [LastModifiedOn] DATETIME NULL
END
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstancePictogram' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [HazardousSubstancePictogram]
	ADD [LastModifiedBy] UNIQUEIDENTIFIER NULL
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstancePictogram' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstancePictogram] 
	DROP CONSTRAINT [DF_HazardousSubstancePictogram_Deleted]
	
	ALTER TABLE [HazardousSubstancePictogram]
	DROP COLUMN [Deleted] 
	
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstancePictogram' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstancePictogram] 
	DROP CONSTRAINT [DF_HazardousSubstancePictogram_CreatedOn]
	
	ALTER TABLE [HazardousSubstancePictogram]
	DROP COLUMN [CreatedOn] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstancePictogram' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstancePictogram] 
	DROP CONSTRAINT [DF_HazardousSubstancePictogram_CreatedBy]
	
	ALTER TABLE [HazardousSubstancePictogram]
	DROP COLUMN [CreatedBy] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstancePictogram' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [HazardousSubstancePictogram]
	DROP COLUMN [LastModifiedOn] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstancePictogram' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [HazardousSubstancePictogram]
	DROP COLUMN [LastModifiedBy] 
END
	