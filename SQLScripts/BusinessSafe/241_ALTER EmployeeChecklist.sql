USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'FriendlyReference')
BEGIN
	ALTER TABLE EmployeeChecklist
	ADD FriendlyReference [nvarchar] (100) NULL
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'FriendlyReference')
BEGIN
	ALTER TABLE EmployeeChecklist
	DROP COLUMN FriendlyReference
END
GO