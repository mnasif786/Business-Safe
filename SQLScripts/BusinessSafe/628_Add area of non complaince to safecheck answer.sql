IF NOT EXISTS(SELECT *
FROM sys.columns AS c
WHERE c.object_id = object_id('SafeCheckCheckListAnswer')
AND c.name = 'AreaOfNonCompliance')
BEGIN
	ALTER TABLE dbo.SafeCheckCheckListAnswer ADD AreaOfNonCompliance NVARCHAR(500) NULL
END

GO
