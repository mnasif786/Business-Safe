USE BusinessSafe
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OthersInvolvedAccidentDetails')
BEGIN
	CREATE TABLE [dbo].[OthersInvolvedAccidentDetails](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](200) NOT NULL,		
		CONSTRAINT [PK_OthersInvolvedAccidentDetails] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GRANT SELECT, INSERT, DELETE, UPDATE ON [OthersInvolvedAccidentDetails] TO AllowAll

END
GO

GRANT SELECT, UPDATE,INSERT ON OthersInvolvedAccidentDetails TO AllowAll
GRANT SELECT, UPDATE,INSERT ON OthersInvolvedAccidentDetails TO AllowSelectInsertUpdate

GO
--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OthersInvolvedAccidentDetails')
BEGIN
	DROP TABLE [OthersInvolvedAccidentDetails];
END
GO
