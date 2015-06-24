use BusinessSafe
go

IF EXISTS (SELECT * FROM sys.columns AS c WHERE c.object_id = OBJECT_ID('Action') AND c.name = 'AreaOfNonCompliance')
BEGIN
	ALTER TABLE dbo.[Action]
	ALTER COLUMN AreaOfNonCompliance nvarchar(1000) NULL 
END
go