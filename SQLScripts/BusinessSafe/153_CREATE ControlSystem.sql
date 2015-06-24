USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'ControlSystem')
BEGIN
	CREATE TABLE [dbo].[ControlSystem](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[Description] [nvarchar](100) NOT NULL,
		[Url] [nvarchar](100) NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_ControlSystem] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT ON [ControlSystem] TO AllowAll
	GRANT INSERT ON [ControlSystem] TO AllowAll
	GRANT UPDATE ON [ControlSystem] TO AllowAll
END
GO

IF NOT EXISTS (SELECT * FROM [ControlSystem])
BEGIN
	SET IDENTITY_INSERT [ControlSystem] ON
	INSERT INTO [ControlSystem] ([Id], [Description], [Url], [CreatedBy], [CreatedOn], [Deleted]) VALUES (0, 'None', '', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0FD0112D6BC AS DateTime), 0)
	INSERT INTO [ControlSystem] ([Id], [Description], [Url], [CreatedBy], [CreatedOn], [Deleted]) VALUES (1, 'General', '', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0FD0112D6BC AS DateTime), 0)
	INSERT INTO [ControlSystem] ([Id], [Description], [Url], [CreatedBy], [CreatedOn], [Deleted]) VALUES (2, 'Engineering Controls', '', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0FD0112D6BC AS DateTime), 0)
	INSERT INTO [ControlSystem] ([Id], [Description], [Url], [CreatedBy], [CreatedOn], [Deleted]) VALUES (3, 'Containment', '', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0FD0112D6BC AS DateTime), 0)
	INSERT INTO [ControlSystem] ([Id], [Description], [Url], [CreatedBy], [CreatedOn], [Deleted]) VALUES (4, 'Special', '', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0FD0112D6BC AS DateTime), 0)
	SET IDENTITY_INSERT [ControlSystem] OFF
END
GO

--//@UNDO 
IF EXISTS (SELECT * FROM [ControlSystem])
BEGIN
	DELETE FROM [ControlSystem]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ControlSystem')
BEGIN
	DROP TABLE [ControlSystem]
END
GO
