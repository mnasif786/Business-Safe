USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AccidentRecordInjury')
BEGIN
	CREATE TABLE [dbo].[AccidentRecordInjury](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[AccidentRecordId] [bigint] NOT NULL,
		[InjuryId] [bigint] NOT NULL,
		[AdditionalInformation] [nvarchar](250) NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL
	CONSTRAINT [PK_AccidentRecordInjury] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE, DELETE ON [AccidentRecordInjury] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [AccidentRecordInjury] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AccidentRecordInjury')
BEGIN
	DROP TABLE [dbo].[AccidentRecordInjury]
END
GO