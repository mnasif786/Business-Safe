USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('SafeCheckChecklist')
AND c.name = 'Jurisdiction')
BEGIN
	
	ALTER TABLE dbo.SafeCheckCheckList ADD Jurisdiction VARCHAR(5) NULL

END

