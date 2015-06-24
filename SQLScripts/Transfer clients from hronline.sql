
--add HroPersonId column

--INSERT INTO [Employee] 
(
	[Id],
	--[HroPersonId],
	[Forename],
	[Surname],
	[Title],
	[PreviousSurname],
	[MiddleName],
	[DateOfBirth],
	[NationalityId],
	[Sex],
	[HasDisability],
	[NINumber],
	[DrivingLicenceNumber],
	[WorkVisaNumber],
	[WorkVisaExpirationDate],
	[PPSNumber],
	[PassportNumber],
	[HasCompanyVehicle],
	--[JobTitle],??????????????????
	[Deleted]
)




SELECT
NEWID(),
--[Employee].[PersonId],
[Person].[Forename],
[Person].[Surname],
[Person].[Title],
[Employee].[PreviousSurname],
[Person].[Middlename],
[Employee].[DateOfBirth],
[Employee].[NationalityId],
[Employee].[Sex],
[Employee].[Disabled],
[Employee].[NINumber],
[Employee].[DrivingLicenceNumber],
[Employee].[WorkVisaNumber],
[Employee].[WorkVisaExpiryDate],
[Employee].[PPSNumber],
[Employee].[PassportNumber],
[Employee].[CompanyCar],
[Person].[Deleted]
FROM [Employee]
INNER JOIN [Person]
ON [Employee].[PersonId] = [Person].[PersonId]
WHERE [ClientId] = 1917
ORDER BY [Employee].[PersonId]