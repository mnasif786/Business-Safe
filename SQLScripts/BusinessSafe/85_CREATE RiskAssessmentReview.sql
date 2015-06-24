IF NOT EXISTS (SELECT * FROM sys.objects o WHERE o.name = 'RiskAssessmentReview')
BEGIN

CREATE TABLE [dbo].[RiskAssessmentReview](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RiskAssessmentId] [bigint] NOT NULL,
	[Description] [nvarchar](150) NULL,
	[Deleted] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[CompletionDueDate] [datetime] NULL,
	[ReviewAssignedToId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_RiskAssessmentReview] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

END
GO

GRANT SELECT, INSERT, DELETE, UPDATE ON [RiskAssessmentReview] TO [AllowAll]
GO


