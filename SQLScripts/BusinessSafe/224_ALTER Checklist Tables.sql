USE [BusinessSafe]

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Answer' AND COLUMN_NAME = 'EmployeeChecklistId' AND DATA_TYPE = 'bigint')
BEGIN
	ALTER TABLE [Answer]
	DROP COLUMN [EmployeeChecklistId] 
	
	ALTER TABLE [Answer]
	ADD [EmployeeChecklistId] [uniqueidentifier] NULL
END

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Answer' AND COLUMN_NAME = 'EmployeeChecklistId' AND DATA_TYPE = 'uniqueidentifier')
BEGIN
	ALTER TABLE [Answer]
	DROP COLUMN [EmployeeChecklistId] 
	
	ALTER TABLE [Answer]
	ADD [EmployeeChecklistId] [bigint] NOT NULL
END