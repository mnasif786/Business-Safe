USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ActionPlan' AND COLUMN_NAME = 'AreasVisited')
BEGIN
	ALTER TABLE	ActionPlan		
		ADD AreasVisited nvarchar(200) null	
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ActionPlan' AND COLUMN_NAME = 'AreasNotVisited')
BEGIN
	ALTER TABLE	ActionPlan		
		ADD AreasNotVisited nvarchar(200) null
	
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ActionPlan' AND COLUMN_NAME = 'AreasVisited')
BEGIN
	ALTER TABLE	ActionPlan		
		ADD AreasVisited nvarchar(200) null
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ActionPlan' AND COLUMN_NAME = 'AreasNotVisited')
BEGIN
	ALTER TABLE	ActionPlan		
		ADD AreasNotVisited nvarchar(200) null	
END
GO