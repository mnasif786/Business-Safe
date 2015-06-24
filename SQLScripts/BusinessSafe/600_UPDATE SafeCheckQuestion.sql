USE [BusinessSafe]
GO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQuestion' AND COLUMN_NAME = 'Title')
BEGIN
	UPDATE SafeCheckQuestion SET Title='Are there arrangements in place to ensure that hot water outlet temperatures are lower than 43°C with testing records retained?'
	WHERE Id='ACA7EFE5-5DE2-4B2F-958A-31FAD471689C'
END
GO

