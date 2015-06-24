USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE SafeCheckCheckList ADD CreatedBy uniqueidentifier NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE SafeCheckCheckList ADD CreatedOn datetime NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE SafeCheckCheckList ADD LastModifiedBy uniqueidentifier NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE SafeCheckCheckList ADD LastModifiedOn datetime NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE SafeCheckCheckList ADD Deleted bit NULL
END

GO
--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE SafeCheckCheckList DROP COLUMN CreatedBy 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE SafeCheckCheckList DROP COLUMN CreatedOn 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE SafeCheckCheckList DROP COLUMN LastModifiedBy 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE SafeCheckCheckList DROP COLUMN LastModifiedOn
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE SafeCheckCheckList DROP COLUMN Deleted
END