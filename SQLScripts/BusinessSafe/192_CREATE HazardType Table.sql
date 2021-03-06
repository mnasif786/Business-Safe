----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* Create the HazardType table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'HazardType' AND TYPE = 'U')
BEGIN
	CREATE TABLE [dbo].[HazardType](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](250) NULL,
		[Deleted] [bit] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[LastModifiedBy] [uniqueidentifier] NULL
	) ON [PRIMARY]
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [HazardType] TO [AllowAll]
GO

--//@UNDO 
IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'HazardType' AND TYPE = 'U')
BEGIN
	DROP TABLE [HazardType]
END