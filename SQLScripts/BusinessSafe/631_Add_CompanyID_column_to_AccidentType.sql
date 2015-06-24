
IF NOT EXISTS(SELECT *
FROM sys.columns AS c
WHERE c.object_id = object_id('AccidentType')
AND c.name = 'CompanyId')
BEGIN
	ALTER TABLE AccidentType 
	ADD CompanyId bigint NULL
END
go


