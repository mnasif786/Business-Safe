USE [BusinessSafe]
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' 
AND TABLE_NAME = 'AccidentRecordNotificationMember'  AND COLUMN_NAME = 'NonEmployeeEmail')
BEGIN
	ALTER TABLE [AccidentRecordNotificationMember]
	ADD [NonEmployeeEmail] NVARCHAR(100) NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' 
AND TABLE_NAME = 'AccidentRecordNotificationMember' AND COLUMN_NAME = 'NonEmployeeName')
BEGIN
	ALTER TABLE [AccidentRecordNotificationMember]
	ADD [NonEmployeeName] NVARCHAR(400) NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' 
AND TABLE_NAME = 'AccidentRecordNotificationMember' AND COLUMN_NAME = 'Discriminator')
BEGIN
	ALTER TABLE [AccidentRecordNotificationMember]
	ADD [Discriminator] VARCHAR(150) NOT NULL DEFAULT 'BusinessSafe.Domain.Entities.AccidentRecordNotificationEmployeeMember'
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' 
AND TABLE_NAME = 'AccidentRecordNotificationMember' AND COLUMN_NAME = 'EmployeeId')
BEGIN			
	ALTER TABLE [AccidentRecordNotificationMember]		
	ALTER COLUMN [EmployeeId] UNIQUEIDENTIFIER NULL
END
GO

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' 
AND TABLE_NAME = 'AccidentRecordNotificationMember' AND COLUMN_NAME = 'NonEmployeeEmail')
BEGIN
	ALTER TABLE [AccidentRecordNotificationMember]
	DROP COLUMN [NonEmployeeEmail]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' 
AND TABLE_NAME = 'AccidentRecordNotificationMember' AND COLUMN_NAME = 'NonEmployeeName')
BEGIN
	ALTER TABLE [AccidentRecordNotificationMember]
	DROP COLUMN [NonEmployeeName]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' 
AND TABLE_NAME = 'AccidentRecordNotificationMember' AND COLUMN_NAME = 'Discriminator')
BEGIN
	ALTER TABLE [AccidentRecordNotificationMember]
	DROP COLUMN [Discriminator]
END
GO