USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'DueDateForCompletion')
BEGIN
	ALTER TABLE EmployeeChecklist
	ADD  DueDateForCompletion [datetime]
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'CompletionNotificationEmailAddress')
BEGIN
	ALTER TABLE EmployeeChecklist
	ADD  CompletionNotificationEmailAddress [nvarchar](100)
END


--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'DueDateForCompletion')
BEGIN
	ALTER TABLE [EmployeeChecklist]
	DROP  COLUMN [DueDateForCompletion] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'CompletionNotificationEmailAddress')
BEGIN
	ALTER TABLE [EmployeeChecklist]
	DROP  COLUMN [CompletionNotificationEmailAddress] 
END