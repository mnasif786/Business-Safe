USE [BusinessSafe]	
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'FriendlyReference')
BEGIN
	ALTER TABLE [dbo].[EmployeeChecklist] 
	DROP COLUMN	[FriendlyReference]
END
GO
	
DECLARE @currentEmployeeChecklistId AS UNIQUEIDENTIFIER
DECLARE @currentEmployeeId AS UNIQUEIDENTIFIER 
DECLARE @referencePrefix AS VARCHAR(30)
DECLARE @referenceIncremental AS BIGINT

DECLARE currentEmployeeChecklist CURSOR
FOR SELECT [Id], [EmployeeId] FROM [EmployeeChecklist] WHERE [ReferencePrefix] IS NULL OR [ReferenceIncremental] IS NULL

OPEN currentEmployeeChecklist
FETCH NEXT FROM currentEmployeeChecklist INTO @currentEmployeeChecklistId, @currentEmployeeId

WHILE @@FETCH_STATUS = 0
BEGIN
	SELECT @referencePrefix = UPPER(REPLACE(REPLACE(REPLACE([Surname], ' ', ''), '-', ''), '''', '')) FROM [Employee] WHERE [Id] = @currentEmployeeId
	SELECT @referenceIncremental = ISNULL(MAX([ReferenceIncremental]), 0) + 1 FROM [EmployeeChecklist] WHERE [ReferencePrefix] = @referencePrefix
	UPDATE [EmployeeChecklist] SET [ReferencePrefix] = @referencePrefix, [ReferenceIncremental] = @referenceIncremental WHERE [Id] = @currentEmployeeChecklistId
	FETCH NEXT FROM currentEmployeeChecklist INTO @currentEmployeeChecklistId, @currentEmployeeId
END

CLOSE currentEmployeeChecklist
DEALLOCATE currentEmployeeChecklist
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'ReferencePrefix' AND IS_NULLABLE = 'YES')
BEGIN
	ALTER TABLE [dbo].[EmployeeChecklist] 
	ALTER COLUMN [ReferencePrefix] [nvarchar](30) NOT NULL
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'ReferenceIncremental' AND IS_NULLABLE = 'YES')
BEGIN
	ALTER TABLE [dbo].[EmployeeChecklist] 
	ALTER COLUMN [ReferenceIncremental] [bigint] NOT NULL
END
GO

IF NOT EXISTS(SELECT * FROM SYS.INDEXES WHERE NAME = 'IX_EmployeeChecklistReference')
BEGIN
	CREATE UNIQUE NONCLUSTERED INDEX IX_EmployeeChecklistReference ON [EmployeeChecklist] ([ReferencePrefix], [ReferenceIncremental])
END
GO

--//@UNDO 

IF EXISTS(SELECT * FROM SYS.INDEXES WHERE NAME = 'IX_EmployeeChecklistReference')
BEGIN
	DROP INDEX [IX_EmployeeChecklistReference] ON [EmployeeChecklist]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'ReferencePrefix' AND IS_NULLABLE = 'NO')
BEGIN
	ALTER TABLE [dbo].[EmployeeChecklist] 
	ALTER COLUMN [ReferencePrefix] [nvarchar](30) NULL
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'ReferenceIncremental' AND IS_NULLABLE = 'NO')
BEGIN
	ALTER TABLE [dbo].[EmployeeChecklist] 
	ALTER COLUMN [ReferenceIncremental] [bigint] NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'FriendlyReference')
BEGIN
	ALTER TABLE [dbo].[EmployeeChecklist] 
	ADD [FriendlyReference] [nvarchar](100) NULL
END
GO
