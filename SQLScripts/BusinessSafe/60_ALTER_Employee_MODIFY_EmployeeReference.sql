USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Employee' AND COLUMN_NAME = 'EmployeeReference' AND DATA_TYPE = 'nvarchar' AND IS_NULLABLE = 'NO')
BEGIN
	ALTER TABLE Employee
	ALTER COLUMN EmployeeReference NVARCHAR(100) NULL
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Employee' AND COLUMN_NAME = 'EmployeeReference' AND DATA_TYPE = 'nvarchar' AND IS_NULLABLE = 'YES')
BEGIN
	ALTER TABLE Employee
	ALTER COLUMN EmployeeReference NVARCHAR(100)
END
GO