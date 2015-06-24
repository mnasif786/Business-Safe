 
IF NOT EXISTS (SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('SafeCheckIndustry') 
AND c.name = 'Draft')
BEGIN
	ALTER TABLE [SafeCheckIndustry]
	ADD [Draft] BIT NOT NULL DEFAULT 0
END