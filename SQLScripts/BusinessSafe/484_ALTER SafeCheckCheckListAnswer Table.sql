USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListAnswer' AND COLUMN_NAME = 'EmployeeNotListed')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer		
	ADD EmployeeNotListed nvarchar(150) NULL
END

--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListAnswer' AND COLUMN_NAME = 'EmployeeNotListed')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer
	DROP COLUMN EmployeeNotListed
END