USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'Status')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	ADD Status VARCHAR(50) NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'PersonSeenJobTitle')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	ADD PersonSeenJobTitle VARCHAR(50) NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'PersonSeenName')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	ADD PersonSeenName VARCHAR(50) NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'PersonSeenSalutation')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	ADD PersonSeenSalutation VARCHAR(50) NULL
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'AreasVisited')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	ADD AreasVisited VARCHAR(200) NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'AreasNotVisited')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	ADD AreasNotVisited VARCHAR(200) NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'EmailAddress')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	ADD EmailAddress VARCHAR(200) NULL
END

--//@UNDO 
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'Status')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	DROP COLUMN Status 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'PersonSeenJobTitle')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	DROP COLUMN PersonSeenJobTitle
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'PersonSeenName')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	DROP COLUMN PersonSeenName
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'PersonSeenSalutation')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	DROP COLUMN PersonSeenSalutation 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'AreasVisited')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	DROP COLUMN AreasVisited
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'AreasNotVisited')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	DROP COLUMN AreasNotVisited
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'EmailAddress')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	DROP COLUMN EmailAddress
END
