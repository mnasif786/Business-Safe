USE [BusinessSafe]

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'Password' AND IS_NULLABLE = 'NO')
BEGIN
	ALTER TABLE [EmployeeChecklist]
	ALTER COLUMN [password] [nvarchar](50) NULL
END

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'Password' AND IS_NULLABLE = 'YES')
BEGIN
	ALTER TABLE [EmployeeChecklist]
	ALTER COLUMN [password] [nvarchar](50) NOT NULL
END