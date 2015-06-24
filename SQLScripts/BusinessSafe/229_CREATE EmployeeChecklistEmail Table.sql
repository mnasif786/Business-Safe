USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EmployeeChecklistEmail')
BEGIN
	CREATE TABLE [dbo].[EmployeeChecklistEmail](
		[Id] [uniqueidentifier] NOT NULL,
		[EmailPusherId] [bigint] NULL,
		[Message] [nvarchar](max) NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[Deleted] [bit] NOT NULL,
		[LastModifiedOn] [datetime] NULL,
		[LastModifiedBy] [uniqueidentifier] NULL
	CONSTRAINT [PK_EmployeeChecklistEmail] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE ON [EmployeeChecklistEmail] TO AllowAll
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EmployeeChecklistEmployeeChecklistEmail')
BEGIN
	CREATE TABLE [dbo].[EmployeeChecklistEmployeeChecklistEmail](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[EmployeeCheckListEmailId] [uniqueidentifier] NOT NULL,
		[EmployeeCheckListId] [uniqueidentifier] NOT NULL,
	CONSTRAINT [PK_EmployeeChecklistEmployeeChecklistEmail] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE ON [EmployeeChecklistEmail] TO AllowAll
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EmployeeChecklistEmployeeChecklistEmail')
BEGIN
	DROP TABLE [dbo].[EmployeeChecklistEmployeeChecklistEmail]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EmployeeChecklistEmail')
BEGIN
	DROP TABLE [dbo].[EmployeeChecklistEmail]
END
GO
