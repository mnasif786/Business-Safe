USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'ImpressionTypeId')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	ADD ImpressionTypeId [uniqueidentifier] NULL	
END

GO
--//@UNDO 
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'ImpressionTypeId')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	DROP COLUMN ImpressionTypeId 	
END

GO 

