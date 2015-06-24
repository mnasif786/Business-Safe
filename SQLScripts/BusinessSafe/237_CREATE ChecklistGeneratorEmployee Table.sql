USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ChecklistGeneratorEmployee')
BEGIN
	CREATE TABLE [dbo].[ChecklistGeneratorEmployee](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[PersonalRiskAssessmentId] [bigint] NOT NULL,
		[EmployeeId] [uniqueidentifier] NOT NULL,
	CONSTRAINT [PK_ChecklistGeneratorEmployee] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE ON [ChecklistGeneratorEmployee] TO AllowAll
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ChecklistGeneratorEmployee')
BEGIN
	DROP TABLE [dbo].[ChecklistGeneratorEmployee]
END
GO
