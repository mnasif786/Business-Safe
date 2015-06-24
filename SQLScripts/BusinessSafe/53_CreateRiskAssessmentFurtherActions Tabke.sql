USE [BusinessSafe]
GO

print 'Create [RiskAssessmentFurtherActionTasks]'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'RiskAssessmentFurtherActionTasks' AND TYPE = 'U')
BEGIN
	CREATE TABLE [dbo].[RiskAssessmentFurtherActionTasks](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[RiskAssessmentHazardId] [bigint] NOT NULL,
		[Title] [nvarchar](50) NULL,
		[Description] [nvarchar](50) NULL,
		[Reference] [nvarchar](50) NULL,
		[Deleted] [bit] NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[LastModifiedOn] [datetime] NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
	 CONSTRAINT [PK_RiskAssessmentFurtherActionTasks] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [RiskAssessmentFurtherActionTasks] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RiskAssessmentFurtherActionTasks' AND TYPE = 'U')
BEGIN
	DROP TABLE [RiskAssessmentFurtherActionTasks]
END