use BusinessSafe
go

IF EXISTS (SELECT * FROM sys.columns AS c WHERE c.object_id = OBJECT_ID('SafeCheckCheckListAnswer') AND c.name = 'ActionRequired')
BEGIN
	ALTER TABLE dbo.[SafeCheckCheckListAnswer]
	ALTER COLUMN ActionRequired nvarchar(1000) NULL 
END
go

IF EXISTS (SELECT * FROM sys.columns AS c WHERE c.object_id = OBJECT_ID('SafeCheckCheckListAnswer') AND c.name = 'SupportingEvidence')
BEGIN
	ALTER TABLE dbo.[SafeCheckCheckListAnswer]
	ALTER COLUMN SupportingEvidence nvarchar(1000) NULL 
END
go

IF EXISTS (SELECT * FROM sys.columns AS c WHERE c.object_id = OBJECT_ID('SafeCheckCheckListAnswer') AND c.name = 'QaComments')
BEGIN
	ALTER TABLE dbo.[SafeCheckCheckListAnswer]
	ALTER COLUMN QaComments nvarchar(1000) NULL 
END
go

IF EXISTS (SELECT * FROM sys.columns AS c WHERE c.object_id = OBJECT_ID('SafeCheckCheckListAnswer') AND c.name = 'AreaOfNonCompliance')
BEGIN
	ALTER TABLE dbo.[SafeCheckCheckListAnswer]
	ALTER COLUMN AreaOfNonCompliance nvarchar(1000) NULL 
END
go