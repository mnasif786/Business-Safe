USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AccidentRecordBodyPart')
BEGIN
	CREATE TABLE [dbo].[AccidentRecordBodyPart](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[AccidentRecordId] [bigint] NOT NULL,
		[BodyPartId] [bigint] NOT NULL,
		[AdditionalInformation] [nvarchar](250) NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL
	CONSTRAINT [PK_AccidentRecordBodyPart] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE, DELETE ON [AccidentRecordBodyPart] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [AccidentRecordBodyPart] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AccidentRecordBodyPart')
BEGIN
	DROP TABLE [dbo].[AccidentRecordBodyPart]
END
GO