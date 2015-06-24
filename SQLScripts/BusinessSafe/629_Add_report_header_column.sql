
IF NOT EXISTS(SELECT *
FROM sys.columns AS c
WHERE c.object_id = object_id('SafeCheckChecklist')
AND c.name = 'ReportHeaderType')
BEGIN
	ALTER TABLE dbo.SafeCheckChecklist ADD ReportHeaderType TINYINT NULL
END

go
UPDATE dbo.SafeCheckChecklist
SET ReportHeaderType = 1
WHERE ReportHeaderType IS NULL


