USE [BusinessSafe]

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FireAnswerSet')
BEGIN
	DROP TABLE [FireAnswerSet]
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FireRiskAssessmentChecklist')
BEGIN
	CREATE TABLE [dbo].[FireRiskAssessmentChecklist](
		[Id] bigint IDENTITY(1,1) NOT NULL,
		[FireRiskAssessmentId] [bigint] NOT NULL,
		[ChecklistId] [bigint] NOT NULL,
		[CompletedDate] [datetime] NULL,
		[ReviewGeneratedFromId] [bigint] NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
	CONSTRAINT [PK_FireRiskAssessmentChecklist] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, DELETE, UPDATE ON [FireRiskAssessmentChecklist] TO AllowAll
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FireRiskAssessmentChecklist')
BEGIN
	DROP TABLE [dbo].[FireRiskAssessmentChecklist]
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FireAnswerSet')
BEGIN
	CREATE TABLE [dbo].[FireAnswerSet](
		[Id] bigint IDENTITY(1,1) NOT NULL,
		[FireRiskAssessmentId] [bigint] NOT NULL,
		[SubmittedOn] [datetime] NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
	CONSTRAINT [PK_FireAnswerSet] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, DELETE, UPDATE ON [FireAnswerSet] TO AllowAll
END
GO