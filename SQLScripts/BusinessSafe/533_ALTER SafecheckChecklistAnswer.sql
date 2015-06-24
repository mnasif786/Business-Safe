USE [BusinessSafe]
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistAnswer' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [SafeCheckChecklistAnswer]
	ADD [CreatedBy] [uniqueidentifier] NOT NULL DEFAULT ('B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistAnswer' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [SafeCheckChecklistAnswer]
	ADD [CreatedOn] [datetime] NOT NULL DEFAULT (GETDATE())
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistAnswer' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [SafeCheckChecklistAnswer]
	ADD [LastModifiedBy] [uniqueidentifier] NOT NULL DEFAULT ('B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistAnswer' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [SafeCheckChecklistAnswer]
	ADD [LastModifiedOn] [datetime] NOT NULL DEFAULT (GETDATE())
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistAnswer' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [SafeCheckChecklistAnswer]
	ADD [Deleted] [bit] NOT NULL DEFAULT (0)
END
GO

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistAnswer' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [SafeCheckChecklistAnswer]
	DROP COLUMN [CreatedBy]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistAnswer' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [SafeCheckChecklistAnswer]
	DROP COLUMN [CreatedOn]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistAnswer' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [SafeCheckChecklistAnswer]
	DROP COLUMN [LastModifiedBy] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistAnswer' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [SafeCheckChecklistAnswer]
	DROP COLUMN [LastModifiedOn] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistAnswer' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [SafeCheckChecklistAnswer]
	DROP COLUMN [Deleted]
END
GO

