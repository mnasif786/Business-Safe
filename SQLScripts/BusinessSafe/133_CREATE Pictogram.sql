USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'Pictogram' AND TYPE = 'U')
BEGIN

	CREATE TABLE [dbo].[Pictogram](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[Title] [nvarchar](50) NOT NULL,
		[HazardousSubstanceStandardId] [bigint] NOT NULL,
	 CONSTRAINT [PK_Pictogram] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

END

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Pictogram' AND TYPE = 'U')
BEGIN
	DROP TABLE [Pictogram]
END
