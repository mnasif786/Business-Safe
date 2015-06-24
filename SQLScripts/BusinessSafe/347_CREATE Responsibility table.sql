
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'Responsibility' AND TYPE = 'U')
BEGIN

CREATE TABLE [dbo].[Responsibility](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ResponsibilityCategoryId] [bigint] NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[SiteId] [bigint] NOT NULL,
	[ResponsibilityReasonId] [bigint] NULL,
	[OwnerId] [uniqueidentifier] NOT NULL,
	[InitialTaskReoccurringTypeId] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[LastModifiedOn] [datetime] NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Responsibility] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

END
GO

GRANT SELECT, INSERT, DELETE, UPDATE ON [Responsibility] TO AllowAll
	

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Responsibility' AND TYPE = 'U')
BEGIN
	DROP TABLE [Responsibility]
END
