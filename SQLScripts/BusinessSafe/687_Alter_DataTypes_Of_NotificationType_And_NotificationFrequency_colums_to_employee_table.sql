USE [BusinessSafe]
GO

DECLARE @ConstraintName nvarchar(200)
SELECT @ConstraintName = Name FROM SYS.DEFAULT_CONSTRAINTS WHERE PARENT_OBJECT_ID = OBJECT_ID('Employee') AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns WHERE NAME = N'NotificationType' AND object_id = OBJECT_ID(N'Employee'))
IF @ConstraintName IS NOT NULL
EXEC('ALTER TABLE Employee DROP CONSTRAINT ' + @ConstraintName)
IF EXISTS (SELECT * FROM syscolumns WHERE id=object_id('Employee') AND name='NotificationType')
EXEC('ALTER TABLE Employee DROP COLUMN NotificationType')
GO


IF NOT EXISTS (SELECT * FROM sys.columns AS c WHERE c.object_id = OBJECT_ID('Employee') AND c.name = 'NotificationType')
BEGIN
		
	ALTER TABLE dbo.[Employee]
	ADD [NotificationType] INT NOT NULL DEFAULT 1
END
go

IF EXISTS (SELECT * FROM sys.columns AS c WHERE c.object_id = OBJECT_ID('Employee') AND c.name = 'NotificationFrequecy')
BEGIN
	ALTER TABLE dbo.[Employee]
	ALTER COLUMN NotificationFrequecy INT NULL
END
go