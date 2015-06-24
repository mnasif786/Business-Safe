USE [BusinessSafe]
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'ReportLetterStatement')
BEGIN
	EXEC sp_RENAME 'SafeCheckQuestionResponse.[NonCompliance]', 'ReportLetterStatement'
END
GO

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'ReportLetterStatement')
BEGIN
	EXEC sp_RENAME 'SafeCheckQuestionResponse.[ReportLetterStatement]', 'NonCompliance'
END
GO
