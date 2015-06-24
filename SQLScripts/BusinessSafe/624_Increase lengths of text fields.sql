
IF EXISTS (SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('SafecheckChecklist') 
AND c.name = 'OtherEmailAddresses'
AND c.max_length = 1000)
BEGIN
	ALTER TABLE dbo.SafeCheckCheckList ALTER COLUMN OtherEmailAddresses NVARCHAR(1000) NULL
END


IF EXISTS (SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('SafecheckChecklist') 
AND c.name = 'AreasVisited'
AND c.max_length = 200)
BEGIN
	ALTER TABLE dbo.SafeCheckCheckList ALTER COLUMN AreasVisited VARCHAR(500) NULL
END

IF EXISTS (SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('SafecheckChecklist') 
AND c.name = 'AreasNotVisited'
AND c.max_length = 200)
BEGIN
	ALTER TABLE dbo.SafeCheckCheckList ALTER COLUMN AreasNotVisited VARCHAR(500) NULL
END


IF EXISTS (SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('SafecheckChecklist') 
AND c.name = 'PersonSeenName'
AND c.max_length = 50)
BEGIN
	ALTER TABLE dbo.SafeCheckCheckList ALTER COLUMN PersonSeenName VARCHAR(100) NULL
END