----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the Employees table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'Country' AND TYPE = 'U')
BEGIN

CREATE TABLE [dbo].[Country](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Deleted] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[LastModifiedOn] [datetime] NOT NULL,
	[LastModifiedBy] [uniqueidentifier] NOT NULL
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO


GRANT SELECT, INSERT,DELETE, UPDATE ON [Country] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Country' AND TYPE = 'U')
BEGIN
	DROP TABLE [Country]
END