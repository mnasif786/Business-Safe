USE [BusinessSafe]

IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckImmediateRiskNotification')
BEGIN
	DROP TABLE [dbo].[SafeCheckImmediateRiskNotification]
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckImmediateRiskNotification')
BEGIN
	CREATE TABLE [dbo].[SafeCheckImmediateRiskNotification](
		[Id] [uniqueidentifier] NOT NULL,
		[Reference] [nvarchar](100) NULL,
        [Title] [nvarchar](250) NULL,
        [SignificantHazardIdentified] [nvarchar](MAX) NULL,
        [RecommendedImmediateAction] [nvarchar](MAX) NULL,
		[ChecklistId] [uniqueidentifier] NULL,
		--[CreatedBy] [uniqueidentifier] NOT NULL,
		--[CreatedOn] [datetime] NOT NULL,
		--[LastModifiedBy] [uniqueidentifier] NULL,
		--[LastModifiedOn] [datetime] NULL,
		--[Deleted] [bit]	NOT NULL
	CONSTRAINT [PK_SafeCheckImmediateRiskNotification] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE, DELETE ON [SafeCheckImmediateRiskNotification] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckImmediateRiskNotification] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckImmediateRiskNotification')
BEGIN
	DROP TABLE [dbo].[SafeCheckImmediateRiskNotification]
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckImmediateRiskNotification')
BEGIN
	CREATE TABLE [dbo].[SafeCheckImmediateRiskNotification](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[Reference] [nvarchar](100) NULL,
        [Title] [nvarchar](250) NULL,
        [SignificantHazardIdentified] [nvarchar](MAX) NULL,
        [RecommendedImmediateAction] [nvarchar](MAX) NULL,
		[ChecklistId] [uniqueidentifier] NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL
	CONSTRAINT [PK_SafeCheckImmediateRiskNotification] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE, DELETE ON [SafeCheckImmediateRiskNotification] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckImmediateRiskNotification] TO AllowSelectInsertUpdate
END
GO
