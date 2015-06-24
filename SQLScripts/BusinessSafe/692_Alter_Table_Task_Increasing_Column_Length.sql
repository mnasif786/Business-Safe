use BusinessSafe
go

IF EXISTS (SELECT * FROM sys.columns AS c WHERE c.object_id = OBJECT_ID('Task') AND c.name = 'Description')
BEGIN
	ALTER TABLE dbo.[Task]
	ALTER COLUMN Description nvarchar(1000) NULL 
END
go