USE [BusinessSafe]


IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafecheckQuestionResponse' AND COLUMN_NAME = 'ReportLetterStatementCategoryId')
BEGIN
	ALTER TABLE [SafecheckQuestionResponse]
	ADD [ReportLetterStatementCategoryId] [uniqueidentifier] NULL
END
GO

IF EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafecheckQuestionResponse' AND COLUMN_NAME = 'ReportLetterStatementCategoryId')
BEGIN
	UPDATE [SafecheckQuestionResponse] SET [ReportLetterStatementCategoryId] = 'a3afdfe3-eb69-4bd6-ab7e-68317adf0d22' WHERE [ReportLetterStatement] = 'Monitoring system in need of improvement'
	UPDATE [SafecheckQuestionResponse] SET [ReportLetterStatementCategoryId] = 'a3afdfe3-eb69-4bd6-ab7e-68317adf0d22' WHERE [ReportLetterStatement] = 'Hazard reporting system needs improvement'
	UPDATE [SafecheckQuestionResponse] SET [ReportLetterStatementCategoryId] = '652043c9-eddf-414d-9204-62eb76e3f86d' WHERE [ReportLetterStatement] = 'Inadequate health and safety induction training'
	UPDATE [SafecheckQuestionResponse] SET [ReportLetterStatementCategoryId] = '652043c9-eddf-414d-9204-62eb76e3f86d' WHERE [ReportLetterStatement] = 'Insufficient first aid materials and personnel provision'
	UPDATE [SafecheckQuestionResponse] SET [ReportLetterStatementCategoryId] = '9548df42-716f-43ea-a446-269f7668326c' WHERE [ReportLetterStatement] = 'Insufficient arrangements for the disposal of medical waste and sharps'
	UPDATE [SafecheckQuestionResponse] SET [ReportLetterStatementCategoryId] = '9548df42-716f-43ea-a446-269f7668326c' WHERE [ReportLetterStatement] = 'Inadequate arrangements for fork lift truck activities'
	UPDATE [SafecheckQuestionResponse] SET [ReportLetterStatementCategoryId] = 'c0f7fb70-7351-4b4e-891b-fadbe1a08f38' WHERE [ReportLetterStatement] = 'Unsuitable or no fitted window restriction'
	UPDATE [SafecheckQuestionResponse] SET [ReportLetterStatementCategoryId] = 'c0f7fb70-7351-4b4e-891b-fadbe1a08f38' WHERE [ReportLetterStatement] = 'No arrangements for servicing of roller doors'
END
GO

--//@UNDO 

IF  EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafecheckQuestionResponse' AND COLUMN_NAME = 'ReportLetterStatementCategoryId')
BEGIN
	ALTER TABLE [SafecheckQuestionResponse]
	DROP COLUMN [ReportLetterStatementCategoryId] 
END
