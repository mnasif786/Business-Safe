USE [BusinessSafe]
GO

DECLARE @ConstraintName nvarchar(200)
SELECT @ConstraintName = Name FROM SYS.DEFAULT_CONSTRAINTS WHERE PARENT_OBJECT_ID = OBJECT_ID('RiskAssessor') AND PARENT_COLUMN_ID = (SELECT column_id FROM sys.columns WHERE NAME = N'DoNotSendTaskDueTomorrowNotification' AND object_id = OBJECT_ID(N'RiskAssessor'))
IF @ConstraintName IS NOT NULL
EXEC('ALTER TABLE RiskAssessor DROP CONSTRAINT ' + @ConstraintName)
IF EXISTS (SELECT * FROM syscolumns WHERE id=object_id('RiskAssessor') AND name='DoNotSendTaskDueTomorrowNotification')
EXEC('ALTER TABLE RiskAssessor DROP COLUMN DoNotSendTaskDueTomorrowNotification')
GO


