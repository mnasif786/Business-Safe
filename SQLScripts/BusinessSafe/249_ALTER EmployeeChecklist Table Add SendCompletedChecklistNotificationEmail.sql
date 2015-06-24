USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'SendCompletedChecklistNotificationEmail')
BEGIN
	ALTER TABLE EmployeeChecklist
	ADD  SendCompletedChecklistNotificationEmail [bit]
END

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'SendCompletedChecklistNotificationEmail')
BEGIN
	ALTER TABLE [EmployeeChecklist]
	DROP  COLUMN [SendCompletedChecklistNotificationEmail] 
END
