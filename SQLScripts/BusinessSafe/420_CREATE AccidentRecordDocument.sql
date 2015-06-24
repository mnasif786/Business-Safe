USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AccidentRecordDocument')
BEGIN
	CREATE TABLE [dbo].[AccidentRecordDocument](
		[Id] [bigint] NOT NULL,
		[AccidentRecordId] [bigint] NOT NULL,
	CONSTRAINT [PK_AccidentRecordDocument] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE, DELETE ON [AccidentRecordDocument] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [AccidentRecordDocument] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AccidentRecordDocument')
BEGIN
	DROP TABLE [dbo].[AccidentRecordDocument]
END
GO