USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeEmergencyContactDetails' AND COLUMN_NAME = 'SameAddressAsEmployee')
BEGIN
	ALTER TABLE [EmployeeEmergencyContactDetails]
	ADD [SameAddressAsEmployee] [bit] NULL
END
GO

UPDATE [EmployeeEmergencyContactDetails] 
SET [SameAddressAsEmployee] = 0
WHERE [SameAddressAsEmployee] IS NULL

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeEmergencyContactDetails' AND COLUMN_NAME = 'SameAddressAsEmployee')
BEGIN
	ALTER TABLE [EmployeeEmergencyContactDetails]
	DROP COLUMN [SameAddressAsEmployee] 
END
GO