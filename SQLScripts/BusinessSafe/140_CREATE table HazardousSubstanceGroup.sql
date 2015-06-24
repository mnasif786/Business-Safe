USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'HazardousSubstanceGroup')
BEGIN
	CREATE TABLE [dbo].[HazardousSubstanceGroup](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[Code] [nvarchar](10) NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
		CONSTRAINT [PK_HazardousSubstanceGroup] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT ON [HazardousSubstanceGroup] TO AllowAll
	GRANT INSERT ON [HazardousSubstanceGroup] TO AllowAll
	GRANT UPDATE ON [HazardousSubstanceGroup] TO AllowAll
END

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'HazardousSubstanceGroup')
BEGIN
	DROP TABLE [HazardousSubstanceGroup]
END
GO
