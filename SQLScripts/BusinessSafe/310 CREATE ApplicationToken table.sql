IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ApplicationToken')
BEGIN
	CREATE TABLE [dbo].[ApplicationToken](
		[Id] [uniqueidentifier] NOT NULL,
		[IsEnabled] [bit] NOT NULL,
		[AppName] [nvarchar](50) NOT NULL,
	 CONSTRAINT [PK_ApplicationToken] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[ApplicationToken] ADD  CONSTRAINT [DF_ApplicationToken_IsEnabled]  DEFAULT ((0)) FOR [IsEnabled]
	
	GRANT SELECT, INSERT, DELETE, UPDATE ON [ApplicationToken] TO AllowAll

	INSERT INTO [BusinessSafe].[dbo].[ApplicationToken]
           ([Id]
           ,[IsEnabled]
           ,[AppName])
     VALUES
           ('f219c8ae-3df6-4486-9a11-353c42fc4419'
           ,1
           ,'BusinessSafeMobile')
END

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ApplicationToken')
BEGIN
	DROP TABLE [ApplicationToken];
END
