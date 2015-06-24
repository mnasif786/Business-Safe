USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PersonalRiskAssessmentEmployee')
BEGIN
	CREATE TABLE [dbo].[PersonalRiskAssessmentEmployee](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[PersonalRiskAssessmentId] [bigint] NOT NULL,
		[EmployeeId] [bigint] NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
	CONSTRAINT [PK_PersonalRiskAssessmentEmployee] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE ON [PersonalRiskAssessmentEmployee] TO AllowAll
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PersonalRiskAssessmentEmployee')
BEGIN
	DROP TABLE [dbo].[PersonalRiskAssessmentEmployee]
END
GO
