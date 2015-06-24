USE [BusinessSafe]
GO

IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessor')
BEGIN
	DROP TABLE [RiskAssessor]
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessor')
BEGIN
	CREATE TABLE [dbo].[RiskAssessor](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[EmployeeId] [uniqueidentifier] NULL,
		[HasAccessToAllSites] [bit] NOT NULL,
		[SiteId] [bigint] NULL,
		[DoNotSendTaskOverdueNotifications] [bit] NOT NULL,
		[DoNotSendTaskCompletedNotifications] [bit] NOT NULL,
		[DoNotSendReviewDueNotification] [bit] NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
	CONSTRAINT [PK_RiskAssessor] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, DELETE, UPDATE ON [RiskAssessor] TO AllowAll
END

--//@UNDO

