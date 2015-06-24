USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EmployeeChecklist')
BEGIN
	CREATE TABLE [dbo].[EmployeeChecklist](
		[Id] [uniqueidentifier] NOT NULL,
		[EmployeeId] [uniqueidentifier] NOT NULL,
		[ChecklistId] [bigint] NOT NULL,
		[StartDate] [datetime] NULL,
		[CompletedDate] [datetime] NULL,
		[password] [nvarchar](50) NOT NULL
			,Deleted bit NOT NULL DEFAULT 0
			,CreatedOn datetime NOT NULL DEFAULT GetDate()	
			,CreatedBy uniqueidentifier NULL
			,LastModifiedOn datetime NULL DEFAULT GetDate()
			,LastModifiedBy uniqueidentifier NULL
	 CONSTRAINT [PK_EmployeeChecklist] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE ON [EmployeeChecklist] TO AllowAll
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Checklist')
BEGIN
	CREATE TABLE [dbo].[Checklist](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[Title] [nvarchar](255) NULL,
		[Description] [nvarchar](1000) NULL
			,Deleted bit NOT NULL DEFAULT 0
			,CreatedOn datetime NOT NULL DEFAULT GetDate()	
			,CreatedBy uniqueidentifier NULL
			,LastModifiedOn datetime NULL DEFAULT GetDate()
			,LastModifiedBy uniqueidentifier NULL
	 CONSTRAINT [PK_Questionnaire] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GRANT SELECT, INSERT, UPDATE ON [Checklist] TO AllowAll
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Section')
BEGIN
	CREATE TABLE [dbo].[Section](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[ChecklistId] [bigint] NOT NULL,
		[Title] [nvarchar](255) NULL,
		[ListOrder] [smallint] NOT NULL
			,Deleted bit NOT NULL DEFAULT 0
			,CreatedOn datetime NOT NULL DEFAULT GetDate()	
			,CreatedBy uniqueidentifier NULL
			,LastModifiedOn datetime NULL DEFAULT GetDate()
			,LastModifiedBy uniqueidentifier NULL
	 CONSTRAINT [PK_Section] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GRANT SELECT, INSERT, UPDATE ON [Section] TO AllowAll
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Question')
BEGIN
	CREATE TABLE [dbo].[Question](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[SectionId] [bigint] NOT NULL,
		[QuestionType] [int] NOT NULL,
		[ListOrder] [smallint] NULL,
		[IsRequired] [bit] NOT NULL,
		[Text] [nvarchar](255) NOT NULL
			,Deleted bit NOT NULL DEFAULT 0
			,CreatedOn datetime NOT NULL DEFAULT GetDate()	
			,CreatedBy uniqueidentifier NULL
			,LastModifiedOn datetime NULL DEFAULT GetDate()
			,LastModifiedBy uniqueidentifier NULL
	 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GRANT SELECT, INSERT, UPDATE ON [Question] TO AllowAll
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Answer')
BEGIN
	CREATE TABLE [dbo].[Answer](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[EmployeeChecklistId] [bigint] NOT NULL,
		[BooleanResponse] [bit] NULL,
		[AdditionalInfo] [nvarchar](max) NULL,
		[QuestionId] [bigint] NOT NULL
			,Deleted bit NOT NULL DEFAULT 0
			,CreatedOn datetime NOT NULL DEFAULT GetDate()	
			,CreatedBy uniqueidentifier NULL
			,LastModifiedOn datetime NULL DEFAULT GetDate()
			,LastModifiedBy uniqueidentifier NULL
	 CONSTRAINT [PK_Answer] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GRANT SELECT, INSERT, UPDATE ON [Answer] TO AllowAll
END
GO


--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EmployeeChecklist')
BEGIN
	DROP TABLE [dbo].[EmployeeChecklist]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Checklist')
BEGIN
	DROP TABLE [dbo].[Checklist]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Section')
BEGIN
	DROP TABLE [dbo].[Section]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Question')
BEGIN
	DROP TABLE [dbo].[Question]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Answer')
BEGIN
	DROP TABLE [dbo].[Answer]
END
GO