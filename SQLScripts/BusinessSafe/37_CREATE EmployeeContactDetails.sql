----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the Employees table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'EmployeeContactDetails' AND TYPE = 'U')
BEGIN

CREATE TABLE [dbo].[EmployeeContactDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Address1] [nvarchar](100) NULL,
	[Address2] [nvarchar](100) NULL,
	[Address3] [nvarchar](100) NULL,
	[Town] [nvarchar](100) NULL,
	[County] [nvarchar](100) NULL,
	[CountryId] [int] NULL,
	[PostCode] [nvarchar](20) NULL,
	[Telephone1] [nvarchar](20) NULL,
	[Telephone2] [nvarchar](20) NULL,
	[PreferedTelephone] [smallint] NULL,
	[Email] [nvarchar](100) NULL,
	[EmployeeId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL
 CONSTRAINT [PK_EmployeeContactDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END 
GO


GRANT SELECT, INSERT,DELETE, UPDATE ON [EmployeeContactDetails] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'EmployeeContactDetails' AND TYPE = 'U')
BEGIN
	DROP TABLE [EmployeeContactDetails]
END
