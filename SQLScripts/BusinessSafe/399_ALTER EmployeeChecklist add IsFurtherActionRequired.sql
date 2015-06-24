
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'IsFurtherActionRequired')
BEGIN
	ALTER TABLE [EmployeeChecklist]
	ADD [IsFurtherActionRequired] BIT NULL
END

--//@UNDO 


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'IsFurtherActionRequired')
BEGIN
	ALTER TABLE [EmployeeChecklist]
	DROP COLUMN [IsFurtherActionRequired] 
END