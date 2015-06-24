USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'Id')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	ADD [Id] BIGINT IDENTITY(1,1) NOT NULL 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	ADD [CreatedOn] DATETIME NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	ADD [CreatedBy] UNIQUEIDENTIFIER NULL
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	ADD [LastModifiedOn] DATETIME NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	ADD [LastModifiedBy] UNIQUEIDENTIFIER NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	ADD [Deleted] BIT NOT NULL DEFAULT(0)
END
GO

DECLARE @now AS DATETIME
SET @now = GETDATE()

UPDATE [RiskAssessmentPeopleAtRisk]
SET [CreatedBy] = 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',
[CreatedOn] = @now

UPDATE [RiskAssessmentPeopleAtRisk]
SET [CreatedBy] = 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',
[CreatedOn] = @now
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'CreatedOn' AND IS_NULLABLE = 'NO')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	ALTER COLUMN [CreatedOn] DATETIME NOT NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'CreatedBy' AND IS_NULLABLE = 'NO')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	ALTER COLUMN [CreatedBy] UNIQUEIDENTIFIER NOT NULL
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'PK_RiskAssessmentPeopleAtRisk' )
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk] 
	DROP CONSTRAINT [PK_RiskAssessmentPeopleAtRisk]
END

--//@UNDO 
USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'PK_RiskAssessmentPeopleAtRisk2' )
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk] 
	DROP CONSTRAINT [PK_RiskAssessmentPeopleAtRisk2] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'Id')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	DROP COLUMN [Id]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	DROP COLUMN [CreatedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	DROP COLUMN [CreatedBy] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	DROP COLUMN [LastModifiedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	DROP COLUMN [LastModifiedBy]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	DROP COLUMN [Deleted]
END
