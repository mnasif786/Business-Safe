USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('Action')
AND c.name = 'NoLongerRequired')

BEGIN
	ALTER TABLE dbo.Action ADD NoLongerRequired BIT DEFAULT 0
	
END
GO

IF NOT EXISTS(SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('ActionPlan')
AND c.name = 'NoLongerRequired')

BEGIN
	ALTER TABLE dbo.ActionPlan ADD NoLongerRequired BIT DEFAULT 0
	
END

go