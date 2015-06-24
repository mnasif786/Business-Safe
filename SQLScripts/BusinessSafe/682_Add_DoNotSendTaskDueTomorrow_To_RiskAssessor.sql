USE BusinessSafe
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'RiskAssessor' AND COLUMN_NAME = 'DoNotSendTaskDueTomorrowNotification') 

BEGIN
	ALTER TABLE [RiskAssessor]
	ADD [DoNotSendTaskDueTomorrowNotification] BIT NOT NULL DEFAULT 1
END
GO

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'RiskAssessor' AND COLUMN_NAME = 'DoNotSendTaskDueTomorrowNotification')
BEGIN
	ALTER TABLE [RiskAssessor]
	DROP [DoNotSendTaskDueTomorrowNotification] 
END
GO