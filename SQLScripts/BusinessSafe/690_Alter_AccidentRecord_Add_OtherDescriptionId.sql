USE [BusinessSafe]
GO 

IF NOT EXISTS (SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('AccidentRecord') 
AND c.name = 'PersonInvolvedOtherDescriptionId')
BEGIN
	ALTER TABLE [AccidentRecord]
	ADD [PersonInvolvedOtherDescriptionId] int
END

GO