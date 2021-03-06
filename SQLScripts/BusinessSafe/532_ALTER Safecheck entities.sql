USE [BusinessSafe]
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQuestion' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [SafeCheckQuestion]
	ADD [CreatedBy] [uniqueidentifier] NOT NULL DEFAULT ('B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQuestion' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [SafeCheckQuestion]
	ADD [CreatedOn] [datetime] NOT NULL DEFAULT (GETDATE())
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQuestion' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [SafeCheckQuestion]
	ADD [LastModifiedBy] [uniqueidentifier] NOT NULL DEFAULT ('B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQuestion' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [SafeCheckQuestion]
	ADD [LastModifieddOn] [datetime] NOT NULL DEFAULT (GETDATE())
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQuestion' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [SafeCheckQuestion]
	ADD [Deleted] [bit] NOT NULL DEFAULT (0)
END
GO

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS TABLE_CATALOG = 'BusinessSafe' AND WHERE TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'ReportLetterStatement')
BEGIN
	EXEC sp_RENAME 'SafeCheckQuestionResponse.[ReportLetterStatement]', 'NonCompliance'
END
GO
