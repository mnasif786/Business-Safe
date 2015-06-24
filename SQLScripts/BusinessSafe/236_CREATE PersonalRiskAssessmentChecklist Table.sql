USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PersonalRiskAssessmentChecklist')
BEGIN
	CREATE TABLE [dbo].[PersonalRiskAssessmentChecklist](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[PersonalRiskAssessmentId] [bigint] NOT NULL,
		[CheckListId] [bigint] NOT NULL,
	CONSTRAINT [PK_PersonalRiskAssessmentChecklist] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE ON [PersonalRiskAssessmentChecklist] TO AllowAll
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PersonalRiskAssessmentChecklist')
BEGIN
	DROP TABLE [dbo].[PersonalRiskAssessmentChecklist]
END
GO
