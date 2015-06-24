IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EscalationNextReoccurringLiveTask')
BEGIN
	CREATE TABLE [dbo].[EscalationNextReoccurringLiveTask](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[TaskId] [bigint] NOT NULL,
		[NextReoccurringLiveTaskEmailSentDate] [datetime] NOT NULL,
		

		CONSTRAINT [PK_EscalationNextReoccurringLiveTask] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GRANT SELECT, INSERT, DELETE, UPDATE ON [EscalationNextReoccurringLiveTask] TO AllowAll

END

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EscalationNextReoccurringLiveTask')
BEGIN
	DROP TABLE [EscalationNextReoccurringLiveTask];
END
