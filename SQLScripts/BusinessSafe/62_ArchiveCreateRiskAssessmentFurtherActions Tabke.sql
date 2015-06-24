USE [BusinessSafe]
GO

print 'Create [ArchiveRiskAssessmentFurtherActionTasks]'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'ArchiveRiskAssessmentFurtherActionTasks' AND TYPE = 'U')
BEGIN
	CREATE TABLE [dbo].[ArchiveRiskAssessmentFurtherActionTasks](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[RiskAssessmentFurtherActionTaskId] [bigint],
		[RiskAssessmentHazardId] [bigint] NOT NULL,
		[Title] [nvarchar](50) NULL,
		[Description] [nvarchar](500) NULL,
		[Reference] [nvarchar](50) NULL,
		[Deleted] [bit] NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[LastModifiedOn] [datetime] NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[TaskAssignedToId] [uniqueidentifier] NULL,
		[TaskCompletionDueDate] [datetime] NULL,
		[TaskStatusId] [smallint] NULL,
		[TaskCompletedDate] [datetime] NULL,
		[TaskCompletedComments] [nvarchar](250) NULL,
		[ArchiveAction] [nvarchar] (50) NULL,
	 CONSTRAINT [PK_ArchiveRiskAssessmentFurtherActionTasks] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [ArchiveRiskAssessmentFurtherActionTasks] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'ArchiveRiskAssessmentFurtherActionTasks' AND TYPE = 'U')
BEGIN
	DROP TABLE [ArchiveRiskAssessmentFurtherActionTasks]
END