
USE [BusinessSafe]
GO 

IF NOT EXISTS (SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('SafecheckFavouriteChecklists') 
AND c.name = 'Title')
BEGIN
	ALTER TABLE [SafecheckFavouriteChecklists]
	ADD [Title] NVARCHAR(50) NOT NULL
END

GO