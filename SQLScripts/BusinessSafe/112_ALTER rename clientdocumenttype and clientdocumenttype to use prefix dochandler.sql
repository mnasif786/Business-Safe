USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClientDocumentType')
BEGIN
	EXEC sp_rename 'ClientDocumentType', 'DocHandlerDocumentType'
END

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DocHandlerDocumentType')
BEGIN
	EXEC sp_rename 'DocHandlerDocumentType', 'ClientDocumentType'
END