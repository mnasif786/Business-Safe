----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the PermissionTarget table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'PermissionTarget' AND TYPE = 'U')
BEGIN
	CREATE TABLE	[PermissionTarget]
	(
		[Id] [bigint] IDENTITY(1,1) NOT NULL
		,Name nvarchar(50) NULL
		,DisplayOrder int NULL
		,PermissionGroupId int NOT NULL
		,[CreatedBy] [uniqueidentifier] NOT NULL
		,[CreatedOn] [datetime] NOT NULL
		,[LastModifiedBy] [uniqueidentifier] NULL
		,[LastModifiedOn] [datetime] NULL
		,[Deleted] [bit]	NOT NULL
		,CONSTRAINT [PK_PermissionTarget] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

GRANT SELECT, INSERT, DELETE, UPDATE ON [PermissionTarget] TO [AllowAll]
GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'PermissionTarget' AND TYPE = 'U')
BEGIN
	DROP TABLE [PermissionTarget]
END