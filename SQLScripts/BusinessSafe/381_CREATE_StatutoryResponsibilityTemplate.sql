USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'StatutoryResponsibilityTemplate')
BEGIN
	CREATE TABLE [dbo].[StatutoryResponsibilityTemplate](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[SectorId] [bigint] NULL,
		[ResponsibilityCategoryId] [bigint]  NULL,
		[Title] [nvarchar](250) NULL,
		[Description] [nvarchar](500) NULL,
		[GuidanceNote] [nvarchar](250) NULL,
		[ResponsibilityReasonId] [bigint] NULL,
		[TaskReoccurringTypeId] [int] NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit] NOT NULL
	 CONSTRAINT [PK_StatutoryResponsibilityTemplate] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'StatutoryResponsibilityTemplate')
BEGIN
	DROP TABLE [dbo].[StatutoryResponsibilityTemplate]
END
GO