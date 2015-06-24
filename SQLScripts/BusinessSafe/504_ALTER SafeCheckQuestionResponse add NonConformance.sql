USE [BusinessSafe]
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'NonCompliance')
BEGIN
	ALTER TABLE [SafeCheckQuestionResponse]
	ADD [NonCompliance] [nvarchar](3000) NULL
END
GO

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'NonCompliance')
BEGIN
	ALTER TABLE [SafeCheckQuestionResponse]
	DROP COLUMN [NonCompliance]
END
GO
