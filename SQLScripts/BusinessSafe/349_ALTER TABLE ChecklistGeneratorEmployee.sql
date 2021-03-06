USE [BusinessSafe]
GO


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ChecklistGeneratorEmployee' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [ChecklistGeneratorEmployee]
	ADD [CreatedOn] DATETIME NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ChecklistGeneratorEmployee' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [ChecklistGeneratorEmployee]
	ADD [CreatedBy] UNIQUEIDENTIFIER NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ChecklistGeneratorEmployee' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [ChecklistGeneratorEmployee]
	ADD [LastModifiedOn] DATETIME NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ChecklistGeneratorEmployee' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [ChecklistGeneratorEmployee]
	ADD [LastModifiedBy] UNIQUEIDENTIFIER NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ChecklistGeneratorEmployee' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [ChecklistGeneratorEmployee]
	ADD [Deleted] BIT NOT NULL DEFAULT(0)
END
GO

DECLARE @now AS DATETIME
SET @now = GETDATE()

UPDATE [ChecklistGeneratorEmployee]
SET [CreatedBy] = 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',
[CreatedOn] = @now

UPDATE [ChecklistGeneratorEmployee]
SET [CreatedBy] = 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',
[CreatedOn] = @now
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ChecklistGeneratorEmployee' AND COLUMN_NAME = 'CreatedOn' AND IS_NULLABLE = 'NO')
BEGIN
	ALTER TABLE [ChecklistGeneratorEmployee]
	ALTER COLUMN [CreatedOn] DATETIME NOT NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ChecklistGeneratorEmployee' AND COLUMN_NAME = 'CreatedBy' AND IS_NULLABLE = 'NO')
BEGIN
	ALTER TABLE [ChecklistGeneratorEmployee]
	ALTER COLUMN [CreatedBy] UNIQUEIDENTIFIER NOT NULL
END

--//@UNDO 
USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ChecklistGeneratorEmployee' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [ChecklistGeneratorEmployee]
	DROP COLUMN [CreatedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ChecklistGeneratorEmployee' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [ChecklistGeneratorEmployee]
	DROP COLUMN [CreatedBy] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ChecklistGeneratorEmployee' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [ChecklistGeneratorEmployee]
	DROP COLUMN [LastModifiedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ChecklistGeneratorEmployee' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [ChecklistGeneratorEmployee]
	DROP COLUMN [LastModifiedBy]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ChecklistGeneratorEmployee' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [ChecklistGeneratorEmployee]
	DROP COLUMN [Deleted]
END
