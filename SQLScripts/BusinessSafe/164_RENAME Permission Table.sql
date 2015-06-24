USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_Permission')
BEGIN
	EXEC  SP_RENAME 'Permission', 'PREVIOUS_Permission'
	EXEC SP_RENAME 'PK_Permission' ,'PK_PREVIOUS_Permission'

	CREATE TABLE [dbo].[Permission](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[PermissionTargetId] [bigint] NOT NULL,
		[PermissionActivityId] [int] NOT NULL,
		[Deleted] [bit] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[LastModifiedOn] [datetime] NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
	 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	GRANT SELECT ON [Permission] TO AllowAll
	GRANT INSERT ON [Permission] TO AllowAll
	GRANT UPDATE ON [Permission] TO AllowAll
	GRANT DELETE ON [Permission] TO AllowAll

END
GO

--//@UNDO 
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_Permission')
BEGIN
	DROP TABLE [dbo].Permission
	EXEC SP_RENAME 'PREVIOUS_Permission', 'Permission'
	EXEC SP_RENAME 'PK_PREVIOUS_Permission', 'PK_Permission'
END
GO