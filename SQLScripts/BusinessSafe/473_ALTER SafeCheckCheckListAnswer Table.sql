USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckCheckListAnswer]' AND COLUMN_NAME = 'SupportingEvidence')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer		
	ADD SupportingEvidence nvarchar(250) NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckCheckListAnswer]' AND COLUMN_NAME = 'ActionRequired')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer		
	ADD ActionRequired nvarchar(250) NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckCheckListAnswer]' AND COLUMN_NAME = 'AssignedTo')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer		
	ADD AssignedTo uniqueidentifier NULL
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckCheckListAnswer]' AND COLUMN_NAME = 'Comment')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer
	DROP COLUMN Comment
END


--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckCheckListAnswer]' AND COLUMN_NAME = 'SupportingEvidence')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer
	DROP COLUMN SupportingEvidence
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckCheckListAnswer]' AND COLUMN_NAME = 'ActionRequired')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer
	DROP COLUMN ActionRequired
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckCheckListAnswer]' AND COLUMN_NAME = 'AssignedTo')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer
	DROP COLUMN AssignedTo
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckCheckListAnswer]' AND COLUMN_NAME = 'Comment')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer		
	ADD Comment nvarchar(250) NULL
END


