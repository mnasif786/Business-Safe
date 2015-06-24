USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ActionPlan')
BEGIN
	CREATE TABLE [dbo].[ActionPlan](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[Title] [nvarchar](250) NULL,
		[SiteId] [bigint] NULL,
		[DateOfVisit] [datetime] NULL,
		[VisitBy] [nvarchar](100) NULL,
		[SubmittedOn] [datetime] NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL
	CONSTRAINT [PK_ActionPlan] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE, DELETE ON [ActionPlan] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [ActionPlan] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ActionPlan')
BEGIN
	DROP TABLE [dbo].[ActionPlan]
END
GO
