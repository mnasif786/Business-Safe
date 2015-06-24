use BusinessSafe
go

IF NOT EXISTS (SELECT * FROM sys.columns AS c WHERE c.object_id = OBJECT_ID('SafeCheckChecklist') AND c.name = 'EmailReportToPerson')
BEGIN
	ALTER TABLE dbo.SafeCheckCheckList ADD EmailReportToPerson BIT NOT NULL DEFAULT 0
END

IF NOT EXISTS (SELECT * FROM sys.columns AS c WHERE c.object_id = OBJECT_ID('SafeCheckChecklist') AND c.name = 'EmailReportToOthers')
BEGIN
	ALTER TABLE dbo.SafeCheckCheckList ADD EmailReportToOthers BIT NOT NULL DEFAULT 0
END

IF NOT EXISTS (SELECT * FROM sys.columns AS c WHERE c.object_id = OBJECT_ID('SafeCheckChecklist') AND c.name = 'PostReport')
BEGIN
	ALTER TABLE dbo.SafeCheckCheckList ADD PostReport BIT NOT NULL DEFAULT 0
END


IF NOT EXISTS (SELECT * FROM sys.columns AS c WHERE c.object_id = OBJECT_ID('SafeCheckChecklist') AND c.name = 'OtherEmailAddresses')
BEGIN
	ALTER TABLE dbo.SafeCheckCheckList ADD OtherEmailAddresses NVARCHAR(500)
END
