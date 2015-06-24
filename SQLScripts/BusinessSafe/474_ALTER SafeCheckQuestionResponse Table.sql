USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckQuestionResponse]' AND COLUMN_NAME = 'SupportingEvidence')
BEGIN
	ALTER TABLE	SafeCheckQuestionResponse		
	ADD SupportingEvidence nvarchar(250) NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckQuestionResponse]' AND COLUMN_NAME = 'ActionRequired')
BEGIN
	ALTER TABLE	SafeCheckQuestionResponse		
	ADD ActionRequired nvarchar(250) NULL
END

GO

UPDATE SafeCheckQuestionResponse set SupportingEvidence = Comment where ResponseType = 'Positive'
UPDATE SafeCheckQuestionResponse set ActionRequired = Comment where ResponseType = 'Negative'

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckQuestionResponse]' AND COLUMN_NAME = 'Comment')
BEGIN
	ALTER TABLE	SafeCheckQuestionResponse
	DROP COLUMN Comment
END

--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckQuestionResponse]' AND COLUMN_NAME = 'SupportingEvidence')
BEGIN
	ALTER TABLE	SafeCheckQuestionResponse
	DROP COLUMN SupportingEvidence
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckQuestionResponse]' AND COLUMN_NAME = 'ActionRequired')
BEGIN
	ALTER TABLE	SafeCheckQuestionResponse
	DROP COLUMN ActionRequired
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckQuestionResponse]' AND COLUMN_NAME = 'Comment')
BEGIN
	ALTER TABLE	SafeCheckQuestionResponse		
	ADD Comment nvarchar(250) NULL
END


