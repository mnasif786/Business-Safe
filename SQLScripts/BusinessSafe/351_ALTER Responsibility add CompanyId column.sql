IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Responsibility' AND COLUMN_NAME = 'CompanyId')
BEGIN
	ALTER TABLE [Responsibility]
	ADD [CompanyId] [bigint] NOT NULL
END
GO


--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Responsibility' AND COLUMN_NAME = 'CompanyId')
BEGIN
	ALTER TABLE [Responsibility]
	DROP COLUMN [CompanyId] 
END
GO