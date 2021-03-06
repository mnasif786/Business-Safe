USE [BusinessSafe]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'HazardHazardType' AND TYPE = 'U')BEGIN
	CREATE TABLE [dbo].[HazardHazardType](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[HazardId] [bigint] NOT NULL,
		[HazardTypeId] [bigint] NOT NULL
	) ON [PRIMARY]
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [HazardHazardType] TO [AllowAll]
GO

--//@UNDO
IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'HazardHazardType' AND TYPE = 'U')
BEGIN
	DROP TABLE [HazardHazardType]
END