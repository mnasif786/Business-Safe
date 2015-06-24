USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AccidentRecord')
BEGIN
	CREATE TABLE [dbo].[AccidentRecord](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[Title] [nvarchar](250) NULL,
		[Reference] [nvarchar](50) NULL,
		[JusridictionId] [bigint] NULL,
		[PersonInvolvedId] [int] NULL,
		[PersonInvolvedOtherDescription] [nvarchar](250) NULL,
		[EmployeeInjuredId] [uniqueidentifier] NULL,
		[NonEmployeeInjuredForename] [nvarchar](100) NULL,
		[NonEmployeeInjuredSurname] [nvarchar](100) NULL,
		[NonEmployeeInjuredAddress1] [nvarchar](100) NULL,
		[NonEmployeeInjuredAddress2] [nvarchar](100) NULL,
		[NonEmployeeInjuredAddress3] [nvarchar](100) NULL,
		[NonEmployeeInjuredCountyState] [nvarchar](100) NULL,
		[NonEmployeeInjuredCountryId] [bigint] NULL,
		[NonEmployeeInjuredPostcode] [nvarchar](20) NULL,
		[NonEmployeeInjuredContactNumber] [nvarchar](20) NULL,
		[NonEmployeeInjuredOccupation] [nvarchar](100) NULL,
		[DateAndTimeOfAccident] [datetime] NULL,
		[SiteWhereHappenedId] [bigint] NULL,
		[OffSiteSpecifics] [nvarchar](100) NULL,
		[Location] [nvarchar](100) NULL,
		[AccidentTypeId] [bigint] NULL,
		[AccidentTypeOther] [nvarchar](100) NULL,
		[CauseOfAccidentId] [bigint] NULL,
		[CauseOfAccidentOther] [nvarchar](100) NULL,
		[FirstAidAdministered] [bit] NULL,
		[EmployeeFirstAiderId] [uniqueidentifier] NULL,
		[NonEmployeeFirstAiderSpecifics] [nvarchar](100) NULL,
		[DetailsOfFirstAidTreatment] [nvarchar](250) NULL,
		[SeverityOfInjuryId] [int] NULL,
		[InjuredPersonWasTakenToHospital] [bit] NULL,
		[InjuredPersonAbleToCarryOutWorkId] [int] NULL,
		[LengthOfTimeUnableToCarryOutWorkId] [int] NULL,
		[DescriptionHowAccidentHappened] [nvarchar](max) NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL
	CONSTRAINT [PK_AccidentRecord] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE, DELETE ON [AccidentRecord] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [AccidentRecord] TO AllowSelectInsertUpdate
END
GO

--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AccidentRecord')
BEGIN
	DROP TABLE [dbo].[AccidentRecord]
END
GO
