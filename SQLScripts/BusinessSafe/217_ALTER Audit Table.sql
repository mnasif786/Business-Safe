USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'Id' AND [DATA_TYPE] = 'int')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [Id] [bigint]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'Type' AND [DATA_TYPE] = 'varchar')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [Type] [nvarchar](1)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'EntityName' AND [DATA_TYPE] = 'varchar')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [EntityName] [nvarchar](200)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'EntityId' AND [DATA_TYPE] = 'varchar')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [EntityId] [nvarchar](200)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'FieldName' AND [DATA_TYPE] = 'varchar')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [FieldName] [nvarchar](200)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'OldValue' AND [DATA_TYPE] = 'varchar')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [OldValue] [nvarchar](MAX)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'NewValue' AND [CHARACTER_MAXIMUM_LENGTH] = '128')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [NewValue] [nvarchar](MAX)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'UpdateDate' AND [DATA_TYPE] = 'date')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [UpdateDate] [datetime]
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'UpdateId')
BEGIN
	ALTER TABLE [Audit]
	ADD [UpdateId] [uniqueIdentifier]
END

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'UpdateId')
BEGIN
	ALTER TABLE [Audit]
	DROP COLUMN [UpdateId] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'UpdateDate' AND [DATA_TYPE] = 'datetime')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [UpdateDate] [date]
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'NewValue' AND [CHARACTER_MAXIMUM_LENGTH] = '128')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [NewValue] [nvarchar](128)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'OldValue' AND [DATA_TYPE] = 'nvarchar')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [OldValue] [varchar](128)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'FieldName' AND [DATA_TYPE] = 'nvarchar')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [FieldName] [varchar](200)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'EntityId' AND [DATA_TYPE] = 'nvarchar')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [EntityId] [varchar](200)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'EntityName' AND [DATA_TYPE] = 'nvarchar')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [EntityName] [varchar](200)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'Type' AND [DATA_TYPE] = 'nvarchar')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [Type] [varchar](1)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'Id' AND [DATA_TYPE] = 'bigint')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [Id] [int]
END

