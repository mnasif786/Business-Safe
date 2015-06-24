IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AuthenticationToken')
BEGIN
	CREATE TABLE [dbo].[AuthenticationToken](
		[Id] [uniqueidentifier] NOT NULL,
		[ApplicationTokenId] [uniqueidentifier] NOT NULL,
		[UserId] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastAccessDate] [datetime] NOT NULL,
		[IsEnabled] [bit] NOT NULL,
		[ReasonForDeAuthorisation] [nvarchar](50) NULL

		CONSTRAINT [PK_AuthenticationToken] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [AuthenticationToken]
	ADD CONSTRAINT [DF_AuthenticationToken_CreatedOn] DEFAULT (getdate()) FOR [CreatedOn]

	GRANT SELECT, INSERT, DELETE, UPDATE ON [AuthenticationToken] TO AllowAll

END

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AuthenticationToken')
BEGIN
	DROP TABLE [AuthenticationToken];
END
