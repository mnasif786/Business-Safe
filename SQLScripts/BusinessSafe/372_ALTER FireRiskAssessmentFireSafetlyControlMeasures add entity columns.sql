
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'PK_FireRiskAssessmentFireSafetlyControlMeasures' )
BEGIN
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures] 
	DROP CONSTRAINT [PK_FireRiskAssessmentFireSafetlyControlMeasures]
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'Id')
BEGIN
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	ADD [Id] BIGINT IDENTITY(1,1) NOT NULL 
END

ALTER TABLE dbo.FireRiskAssessmentFireSafetlyControlMeasures ADD CONSTRAINT PK_FireRiskAssessmentFireSafetlyControlMeasures2
PRIMARY KEY CLUSTERED (Id);

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	ADD [Deleted] [bit] NULL

	ALTER TABLE [dbo].[FireRiskAssessmentFireSafetlyControlMeasures] ADD  CONSTRAINT [DF_FireRiskAssessmentFireSafetlyControlMeasures_Deleted]  DEFAULT ((0)) FOR [Deleted]
END
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	ADD [CreatedOn] DATETIME NULL
	
	ALTER TABLE [dbo].[FireRiskAssessmentFireSafetlyControlMeasures] ADD  CONSTRAINT [DF_FireRiskAssessmentFireSafetlyControlMeasures_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
END
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	ADD [CreatedBy] UNIQUEIDENTIFIER NULL
		
	ALTER TABLE [dbo].[FireRiskAssessmentFireSafetlyControlMeasures] ADD  CONSTRAINT [DF_FireRiskAssessmentFireSafetlyControlMeasures_CreatedBy]  DEFAULT ('B03C83EE-39F2-4F88-B4C4-7C276B1AAD99') FOR [CreatedBy]
END
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	ADD [LastModifiedOn] DATETIME NULL
END
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	ADD [LastModifiedBy] UNIQUEIDENTIFIER NULL
END
GO

--//@UNDO 


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'PK_RiskAssessmentsNonEmployees2' )
BEGIN
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures] 
	DROP CONSTRAINT [PK_FireRiskAssessmentFireSafetlyControlMeasures2] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'Id')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessmentFireSafetlyControlMeasures] 
	DROP CONSTRAINT [DF_FireRiskAssessmentFireSafetlyControlMeasures_Deleted]
	
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	DROP COLUMN [Id] 
	
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessmentFireSafetlyControlMeasures] 
	DROP CONSTRAINT [DF_FireRiskAssessmentFireSafetlyControlMeasures_Deleted]
	
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	DROP COLUMN [Deleted] 
	
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessmentFireSafetlyControlMeasures] 
	DROP CONSTRAINT [DF_FireRiskAssessmentFireSafetlyControlMeasures_CreatedOn]
	
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	DROP COLUMN [CreatedOn] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessmentFireSafetlyControlMeasures] 
	DROP CONSTRAINT [DF_FireRiskAssessmentFireSafetlyControlMeasures_CreatedBy]
	
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	DROP COLUMN [CreatedBy] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	DROP COLUMN [LastModifiedOn] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	DROP COLUMN [LastModifiedBy] 
END
	