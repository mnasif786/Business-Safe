USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE	[SafeCheckCheckListQuestion]			
	ADD [Deleted] [Bit] NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE	[SafeCheckCheckListQuestion]			
	ADD [CreatedBy] [uniqueidentifier] NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE	[SafeCheckCheckListQuestion]			
	ADD [CreatedOn] [datetime] NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE	[SafeCheckCheckListQuestion]			
	ADD [LastModifiedBy] [uniqueidentifier] NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE	[SafeCheckCheckListQuestion]			
	ADD [LastModifiedOn] [datetime] NULL
END

--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE	[SafeCheckCheckListQuestion]			
	DROP COLUMN [Deleted] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE	[SafeCheckCheckListQuestion]			
	DROP COLUMN [CreatedBy]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE	[SafeCheckCheckListQuestion]			
	DROP COLUMN [CreatedOn] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE	[SafeCheckCheckListQuestion]			
	DROP COLUMN [LastModifiedBy]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE	[SafeCheckCheckListQuestion]			
	DROP COLUMN [LastModifiedOn] 
END
