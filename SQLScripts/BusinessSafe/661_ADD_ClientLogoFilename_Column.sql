USE [BusinessSafe]
GO 

IF NOT EXISTS (SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('SafeCheckCheckList') 
AND c.name = 'ClientLogoFilename')
BEGIN
	ALTER TABLE [SafeCheckCheckList]
	ADD [ClientLogoFilename] NVARCHAR(500) NULL 
END

GO

