USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'HazardousSubstancePictogram' AND TYPE = 'U')
BEGIN

CREATE TABLE [dbo].[HazardousSubstancePictogram] (
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[HazardousSubstanceId] [bigint] NOT NULL,
	[PictogramId] [bigint] NOT NULL,
 CONSTRAINT [PK_[HazardousSubstancePictogram] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

END

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'HazardousSubstancePictogram' AND TYPE = 'U')
BEGIN
	DROP TABLE [HazardousSubstancePictogram]
END