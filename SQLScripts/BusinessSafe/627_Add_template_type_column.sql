
IF NOT EXISTS(SELECT *
FROM sys.columns AS c
WHERE c.object_id = object_id('SafeCheckIndustry')
AND c.name = 'TemplateType')
BEGIN
	ALTER TABLE dbo.SafeCheckIndustry ADD TemplateType TINYINT NULL
END

go
UPDATE dbo.SafeCheckIndustry
SET TemplateType = 1
WHERE TemplateType IS NULL


