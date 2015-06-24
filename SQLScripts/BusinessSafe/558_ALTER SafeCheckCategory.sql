USE [BusinessSafe]
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCategory' AND COLUMN_NAME = 'TabTitle')
BEGIN
	ALTER TABLE [SafeCheckCategory]
	ADD [TabTitle] [varchar](20) NULL 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCategory' AND COLUMN_NAME = 'TabTitle')
BEGIN
	UPDATE [SafeCheckCategory] SET [TabTitle] = 'DOCS' WHERE [Title] = 'Documentation'
	UPDATE [SafeCheckCategory] SET [TabTitle] = 'EQUIPMENT' WHERE [Title] = 'Equipment'
	UPDATE [SafeCheckCategory] SET [TabTitle] = 'FIRE' WHERE [Title] = 'Fire'
	UPDATE [SafeCheckCategory] SET [TabTitle] = 'PEOPLE' WHERE [Title] = 'People Management'
	UPDATE [SafeCheckCategory] SET [TabTitle] = 'PREMISES' WHERE [Title] = 'Premises Management'
	UPDATE [SafeCheckCategory] SET [TabTitle] = 'RAs' WHERE [Title] = 'Risk Assessments'
	UPDATE [SafeCheckCategory] SET [TabTitle] = 'SAs' WHERE [Title] = 'Safety Arrangements'
	UPDATE [SafeCheckCategory] SET [TabTitle] = 'OTHER' WHERE [Title] = 'Other subjects'
END
GO

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCategory' AND COLUMN_NAME = 'TabTitle')
BEGIN
	ALTER TABLE [SafeCheckCategory]
	DROP COLUMN [TabTitle]
END 
