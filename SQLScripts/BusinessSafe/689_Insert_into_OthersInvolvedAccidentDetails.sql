USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OthersInvolvedAccidentDetails')
BEGIN
	INSERT INTO OthersInvolvedAccidentDetails VALUES ('Customer')
	INSERT INTO OthersInvolvedAccidentDetails VALUES ('Guest')
	INSERT INTO OthersInvolvedAccidentDetails VALUES ('Member')
	INSERT INTO OthersInvolvedAccidentDetails VALUES ('Passer-by')
	INSERT INTO OthersInvolvedAccidentDetails VALUES ('Patient')
	INSERT INTO OthersInvolvedAccidentDetails VALUES ('Resident')
	INSERT INTO OthersInvolvedAccidentDetails VALUES ('Student')
	INSERT INTO OthersInvolvedAccidentDetails VALUES ('Visitor')
	INSERT INTO OthersInvolvedAccidentDetails VALUES ('Other (specify)...')
END
GO

--//@UNDO 

DELETE FROM [OthersInvolvedAccidentDetails]