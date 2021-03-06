USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[ActionPlanId]')
BEGIN
	ALTER TABLE	[dbo].[Action] ADD [ActionPlanId] [bigint] NOT NULL			
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[Reference]')
BEGIN
	ALTER TABLE	[dbo].[Action] ADD [Reference] [nvarchar](250) NULL	
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[SignificantHazardIdentified]')
BEGIN
	ALTER TABLE	[dbo].[Action] ADD [SignificantHazardIdentified] [BIT] NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[RecommendedImmediateAction]')
BEGIN
	ALTER TABLE	[dbo].[Action] ADD [RecommendedImmediateAction] [nvarchar](250) NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[AreaOfNonCompliance]')
BEGIN

	ALTER TABLE	[dbo].[Action] ADD [AreaOfNonCompliance] [nvarchar](250) NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[ActionRequired]')
BEGIN
	ALTER TABLE	[dbo].[Action] ADD [ActionRequired] [nvarchar](250) NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[GuidanceNotes]')
BEGIN
	ALTER TABLE	[dbo].[Action] ADD [GuidanceNotes] [nvarchar](250) NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[TargetTimescale] ')
BEGIN
	ALTER TABLE	[dbo].[Action] ADD [TargetTimescale] [nvarchar](250) NULL
END
GO
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[AssignedTo]')
BEGIN
	ALTER TABLE	[dbo].[Action] ADD [AssignedTo] [uniqueidentifier] NOT NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[DueDate] ')
BEGIN
	ALTER TABLE	[dbo].[Action] ADD [DueDate] [datetime] NOT NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[Status] ')
BEGIN
	ALTER TABLE	[dbo].[Action] ADD [Status] [nvarchar](100) NULL	
END
GO
	
	
--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[ActionPlanId]')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN [ActionPlanId]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[Reference]')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN [Reference]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[SignificantHazardIdentified]')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [SignificantHazardIdentified] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[RecommendedImmediateAction]')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [RecommendedImmediateAction] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[AreaOfNonCompliance]')
BEGIN

	ALTER TABLE	[dbo].[Action] DROP COLUMN  [AreaOfNonCompliance] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[ActionRequired]')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [ActionRequired] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[GuidanceNotes]')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [GuidanceNotes] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[TargetTimescale] ')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [TargetTimescale] 
END
GO
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[AssignedTo]')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [AssignedTo] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[DueDate] ')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [DueDate]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[Status] ')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [Status] 
END
GO
	