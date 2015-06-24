IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EscalationOverdueReview')
BEGIN
	CREATE TABLE [dbo].[EscalationOverdueReview](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[TaskId] [bigint] NOT NULL,
		[OverdueEmailSentDate] [datetime] NOT NULL,
		

		CONSTRAINT [PK_EscalationOverdueReview] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GRANT SELECT, INSERT, DELETE, UPDATE ON [EscalationOverdueReview] TO AllowAll

END

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EscalationOverdueReview')
BEGIN
	DROP TABLE [EscalationOverdueReview];
END