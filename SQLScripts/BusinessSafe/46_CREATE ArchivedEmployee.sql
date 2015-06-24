----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the ArchivedEmployee table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'ArchivedEmployee' AND TYPE = 'U')
BEGIN

CREATE TABLE [dbo].[ArchivedEmployee](
	[Id] [uniqueidentifier] NOT NULL,
	[EmployeeId] [uniqueidentifier] NOT NULL,
	[Forename] [nvarchar](200) NOT NULL,
	[Surname] [nvarchar](200) NOT NULL,
	[Title] [nvarchar](20) NULL,
	[PreviousSurname] [nvarchar](200) NULL,
	[MiddleName] [nvarchar](200) NULL,
	[DateOfBirth] [datetime] NULL,
	[NationalityId] [int] NULL,
	[Sex] [nvarchar](20) NULL,
	[HasDisability] [bit] NULL,
	[DisabilityDescription] [nvarchar](max) NULL,
	[NINumber] [nvarchar](20) NULL,
	[DrivingLicenseNumber] [nvarchar](50) NULL,
	[DrivingLicenseExpirationDate] [datetime] NULL,
	[WorkVisaNumber] [nvarchar](100) NULL,
	[WorkVisaExpirationDate] [datetime] NULL,
	[PPSNumber] [nvarchar](20) NULL,
	[PassportNumber] [nvarchar](50) NULL,
	[HasCompanyVehicle] [bit] NULL,
	[CompanyVehicleRegistration] [nvarchar](50) NULL,
	[CompanyVehicleTypeId] [int] NULL,
	[SiteId] [bigint] NULL,
	[OrganisationalUnitId] [bigint] NULL,
	[JobTitle] [nvarchar](100) NULL,
	[EmploymentStatusId] [int] NULL,
	[Deleted] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastModifiedOn] [datetime] NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[EmployeeReference] [nvarchar] (100) NOT NULL,
	[ClientId] [bigint] NOT NULL
 CONSTRAINT [PK_ArchivedEmployee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END 
GO


GRANT SELECT, INSERT,DELETE, UPDATE ON [ArchivedEmployee] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'ArchivedEmployee' AND TYPE = 'U')
BEGIN
	DROP TABLE [ArchivedEmployee]
END
