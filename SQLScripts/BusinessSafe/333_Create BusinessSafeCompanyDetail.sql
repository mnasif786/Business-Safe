IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'BusinessSafeCompanyDetail')
BEGIN

CREATE TABLE [dbo].[BusinessSafeCompanyDetail](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[ContactEmployeeId] [uniqueidentifier] NULL,
		[Deleted] [bit] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[LastModifiedOn] [datetime] NULL,
		[LastModifiedBy] [uniqueidentifier] NULL
 CONSTRAINT [PK_BusinessSafeCompanyDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GRANT SELECT, INSERT, DELETE, UPDATE ON [BusinessSafeCompanyDetail] TO AllowAll

END

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'BusinessSafeCompanyDetail')
BEGIN
	DROP TABLE [BusinessSafeCompanyDetail];
END
