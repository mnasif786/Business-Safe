USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'SendCompletedChecklistNotificationEmail')
BEGIN
	ALTER TABLE PersonalRiskAssessment
	ADD  SendCompletedChecklistNotificationEmail [bit]
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'CompletionDueDateForChecklists')
BEGIN
	ALTER TABLE PersonalRiskAssessment
	ADD  CompletionDueDateForChecklists [datetime]
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'CompletionNotificationEmailAddress')
BEGIN
	ALTER TABLE PersonalRiskAssessment
	ADD  CompletionNotificationEmailAddress [nvarchar](100)
END


--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'SendCompletedChecklistNotificationEmail')
BEGIN
	ALTER TABLE [PersonalRiskAssessment]
	DROP  COLUMN [SendCompletedChecklistNotificationEmail] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'CompletionDueDateForChecklists')
BEGIN
	ALTER TABLE [PersonalRiskAssessment]
	DROP  COLUMN [CompletionDueDateForChecklists] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'CompletionNotificationEmailAddress')
BEGIN
	ALTER TABLE [PersonalRiskAssessment]
	DROP  COLUMN [CompletionNotificationEmailAddress] 
END

