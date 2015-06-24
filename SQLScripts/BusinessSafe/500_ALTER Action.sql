IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = 'SignificantHazardIdentified')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [SignificantHazardIdentified] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = 'RecommendedImmediateAction')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [RecommendedImmediateAction] 
END
GO


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE	[dbo].[Action] ADD CreatedBy [uniqueidentifier] NULL
END
GO


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE	[dbo].[Action] ADD CreatedOn [datetime] NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE	[dbo].[Action] ADD LastModifiedBy [uniqueidentifier] NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE	[dbo].[Action] ADD LastModifiedOn [datetime] NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE	[dbo].[Action] ADD Deleted [bit] NULL DEFAULT 0
END
GO

update	[dbo].[Action]
set deleted = 0
where deleted is null

--update userid to employee id
UPDATE [Action]
Set ActionPlanId = 2
where ActionPlanId = 1 

--update userid to employee id
UPDATE [Action]
Set AssignedTo = 'D2122FFF-1DCD-4A3C-83AE-E3503B394EB4'
where AssignedTo = '16AC58FB-4EA4-4482-AC3D-000D607AF67C'











	
	
	