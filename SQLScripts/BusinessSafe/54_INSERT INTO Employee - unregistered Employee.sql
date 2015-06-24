SET DATEFORMAT YMD

INSERT INTO [BusinessSafe].[dbo].[Employee]
(
	[Id],
	[Forename],
	[Surname],
	[Title],
	[PreviousSurname],
	[MiddleName],
	[DateOfBirth],
	[NationalityId],
	[Sex],
	[HasDisability],
	[DisabilityDescription],
	[NINumber],
	[DrivingLicenseNumber],
	[DrivingLicenseExpirationDate],
	[WorkVisaNumber],
	[WorkVisaExpirationDate],
	[PPSNumber],
	[PassportNumber],
	[HasCompanyVehicle],
	[CompanyVehicleRegistration],
	[CompanyVehicleTypeId],
	[SiteId],
	[OrganisationalUnitId],
	[JobTitle],
	[EmploymentStatusId],
	[Deleted],
	[CreatedOn],
	[CreatedBy],
	[LastModifiedOn],
	[LastModifiedBy],
	[EmployeeReference],
	[ClientId]
)
VALUES
(
	'4d91b7e6-5e25-4620-bfab-d5d4b598cbf7',
	'Gary',
	'Green',
	'Mr',
	null,
	null,
	null,
	null,
	'Male',
	0,
	null,
	null,
	null,
	null,
	null,
	null,
	null,
	null,
	0,
	null,
	null,
	null,
	null,
	'Business Analyst',
	null,
	0,
	'2012-07-24 10:19:33.000',
	'16ac58fb-4ea4-4482-ac3d-000d607af67c',
	null,
	null,
	'KLTO1',
	31028
)
GO

--//@UNDO 
USE [BusinessSafe]
GO  

DELETE FROM [Employee] WHERE [Id] = '4d91b7e6-5e25-4620-bfab-d5d4b598cbf7'
GO
