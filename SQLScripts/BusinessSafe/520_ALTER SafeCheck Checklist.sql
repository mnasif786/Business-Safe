USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklist' AND COLUMN_NAME = 'ActionPlanId')
BEGIN
	ALTER TABLE	[dbo].[SafeCheckChecklist] 
	ADD [ActionPlanId] [bigint] NULL 			
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklist' AND COLUMN_NAME = 'ActionPlanId')
BEGIN
	ALTER TABLE	[dbo].[SafeCheckChecklist] 
	DROP [ActionPlanId] 		
END
GO