USE [BusinessSafe]
GO 

IF NOT EXISTS (SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('SafeCheckCheckList') 
AND c.name = 'IncludeActionPlan')
BEGIN
	ALTER TABLE [SafeCheckCheckList]
	ADD [IncludeActionPlan] BIT NOT NULL DEFAULT 1
END


IF NOT EXISTS (SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('SafeCheckCheckList') 
AND c.name = 'IncludeComplianceReview')
BEGIN
	ALTER TABLE [SafeCheckCheckList]
	ADD [IncludeComplianceReview] BIT NOT NULL DEFAULT 1
END

GO

