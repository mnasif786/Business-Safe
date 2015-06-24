IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AuthorisationToken')
BEGIN
	CREATE TABLE [dbo].[AuthorisationToken](
		[Id] [uniqueidentifier] NOT NULL,
		[ApplicationTokenId] [uniqueidentifier] NOT NULL,
		[UserId] [uniqueidentifier] NOT NULL,
		[CreationDate] [datetime] NOT NULL,
		[LastAccessDate] [datetime] NOT NULL,
		[IsEnabled] [bit] NOT NULL,
		[ReasonForDeauthorisation] [nvarchar](50) NULL

		CONSTRAINT [PK_AuthorisationToken] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GRANT SELECT, INSERT, DELETE, UPDATE ON [AuthorisationToken] TO AllowAll

END

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AuthorisationToken')
BEGIN
	DROP TABLE [AuthorisationToken];
END
