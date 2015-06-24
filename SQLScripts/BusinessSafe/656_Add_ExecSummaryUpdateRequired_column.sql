USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM sys.columns AS c 
WHERE c.object_id = OBJECT_id('SafeCheckChecklist')
AND c.name = 'ExecutiveSummaryUpdateRequired')
BEGIN
	ALTER TABLE dbo.SafeCheckCheckList ADD ExecutiveSummaryUpdateRequired bit not null DEFAULT 0	
END

IF NOT EXISTS (SELECT * FROM sys.columns AS c 
WHERE c.object_id = OBJECT_id('SafeCheckChecklist')
AND c.name = 'ExecutiveSummaryQACommentsResolved')
BEGIN
	ALTER TABLE dbo.SafeCheckCheckList ADD ExecutiveSummaryQACommentsResolved bit not null DEFAULT 0		
END

IF NOT EXISTS (SELECT * FROM sys.columns AS c 
WHERE c.object_id = OBJECT_id('SafeCheckChecklist')
AND c.name = 'ExecutiveSummaryQACommentsSignedOffBy')
BEGIN
	ALTER TABLE dbo.SafeCheckCheckList ADD ExecutiveSummaryQACommentsSignedOffBy varchar(100) null
END

IF NOT EXISTS (SELECT * FROM sys.columns AS c 
WHERE c.object_id = OBJECT_id('SafeCheckChecklist')
AND c.name = 'ExecutiveSummaryQACommentsSignedOffDate')
BEGIN
	ALTER TABLE dbo.SafeCheckCheckList ADD ExecutiveSummaryQACommentsSignedOffDate datetime null 
END

