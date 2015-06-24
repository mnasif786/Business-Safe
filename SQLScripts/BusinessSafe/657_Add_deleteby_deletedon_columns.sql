
IF NOT EXISTS(SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('safecheckchecklist')
AND c.name = 'DeletedBy')

BEGIN
	ALTER TABLE dbo.SafeCheckCheckList ADD DeletedBy VARCHAR(100)
	ALTER TABLE dbo.SafeCheckCheckList ADD DeletedOn DATETIME
END