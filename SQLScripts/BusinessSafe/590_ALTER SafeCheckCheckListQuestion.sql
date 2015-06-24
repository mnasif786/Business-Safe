USE [BusinessSafe]
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'QuestionNumber')
BEGIN
	ALTER TABLE SafeCheckCheckListQuestion
	ADD QuestionNumber [int] NULL 
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'CategoryNumber')
BEGIN
	ALTER TABLE SafeCheckCheckListQuestion
	ADD CategoryNumber [int] NULL 
END
GO

--//@UNDO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'QuestionNumber')
BEGIN
	ALTER TABLE SafeCheckCheckListQuestion
	DROP COLUMN [QuestionNumber]
END 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'CategoryNumber')
BEGIN
	ALTER TABLE SafeCheckCheckListQuestion
	DROP COLUMN [CategoryNumber]
END 
