print 'Create LogEvent Table'

USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'LogEvent')
BEGIN
	CREATE TABLE [dbo].[LogEvent](
		[LogEventId] [bigint] IDENTITY(1,1) NOT NULL,
		[Date] [datetime] NOT NULL,
		[Level] [int] NOT NULL,
		[Message] [varchar](100) COLLATE Latin1_General_CI_AS NULL,
		[User] [varchar](50) COLLATE Latin1_General_CI_AS NULL,
		[Exception] [text] COLLATE Latin1_General_CI_AS NULL,
		[Objects] [xml] NULL,
		[ExecutingMachine] [varchar](50) COLLATE Latin1_General_CI_AS NULL,
		[CallingAssembly] [varchar](50) COLLATE Latin1_General_CI_AS NULL,
		[CallingClass] [varchar](50) COLLATE Latin1_General_CI_AS NULL,
		[CallingMethod] [varchar](50) COLLATE Latin1_General_CI_AS NULL,
		[ContextGuid] [varchar](50) COLLATE Latin1_General_CI_AS NULL,
	 CONSTRAINT [PK_SyncLogEvent] PRIMARY KEY CLUSTERED 
	(
		[LogEventId] ASC
	)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END 
GO

GRANT INSERT ON [LogEvent] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'LogEvent')
BEGIN
	DROP TABLE [LogEvent]
END
GO