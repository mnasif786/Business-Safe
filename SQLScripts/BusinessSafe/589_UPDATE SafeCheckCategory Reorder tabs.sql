USE [BusinessSafe]
GO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCategory' AND COLUMN_NAME = 'TabTitle')
BEGIN
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 1 WHERE [Title] = 'Documentation'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 2 WHERE [Title] = 'Equipment'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 3 WHERE [Title] = 'Fire'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 4 WHERE [Title] = 'People Management'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 5 WHERE [Title] = 'Premises Management'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 6 WHERE [Title] = 'Risk Assessments'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 7 WHERE [Title] = 'Safety Arrangements'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 8 WHERE [Title] = 'Other subjects'
END
GO

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCategory' AND COLUMN_NAME = 'TabTitle')
BEGIN
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 1 WHERE [Title] = 'Documentation'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 7 WHERE [Title] = 'Equipment'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 4 WHERE [Title] = 'Fire'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 5 WHERE [Title] = 'People Management'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 6 WHERE [Title] = 'Premises Management'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 3 WHERE [Title] = 'Risk Assessments'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 2 WHERE [Title] = 'Safety Arrangements'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 8 WHERE [Title] = 'Other subjects'
END 
