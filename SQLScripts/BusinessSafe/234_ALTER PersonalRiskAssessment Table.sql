USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'HasMultipleChecklistRecipients')
BEGIN
	ALTER TABLE [PersonalRiskAssessment]
	ADD [HasMultipleChecklistRecipients] [bit] NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'ChecklistGeneratorMessage')
BEGIN
	ALTER TABLE [PersonalRiskAssessment]
	ADD [ChecklistGeneratorMessage] [NVARCHAR](MAX) NULL
END

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'HasMultipleChecklistRecipients')
BEGIN
	ALTER TABLE [PersonalRiskAssessment]
	DROP COLUMN [HasMultipleChecklistRecipients]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'ChecklistGeneratorMessage')
BEGIN
	ALTER TABLE [PersonalRiskAssessment]
	DROP COLUMN [ChecklistGeneratorMessage]
END