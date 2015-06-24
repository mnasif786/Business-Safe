USE [Peninsula_BusinessSafe]

SET XACT_ABORT ON

BEGIN TRAN

DECLARE @can AS VARCHAR(10)
SET @can = 'Cou176'
DECLARE @customerId AS INT
SELECT @customerId = CustomerId FROM [PBSPROD2SQL\PROD2].[Peninsula].[dbo].[TblCustomers] WHERE CustomerKey = @can



SET IDENTITY_INSERT [TblCustomers] ON

INSERT INTO [TblCustomers] 
(
	[CustomerID],
	[CustomerKey],
	[CreatedBy],
	[CreatedOn],
	[ModifiedBy],
	[ModifiedOn],
	[CompanyName],
	[Password],
	[CompanyRegistrationNumber],
	[CompanyVATNumber],
	[NumberOfStaff],
	[Payroll],
	[BankName],
	[AccountNumber],
	[SortCode],
	[CompanyMainEmail],
	[CompanyWebsite],
	[DefaultLegalLocationID],
	[CurrentSalesRepUserName],
	[CurrentHSConsultant],
	[CurrentPersonnelConsultant],
	[PrincipleBusinessTypeID],
	[CompanyNameAKA],
	[PBSIndustryTypeID],
	[CustomerGroupID],
	[DefaultPaymentMethodID],
	[PrivateAndConfidential],
	[AssociatedClientNotes],
	[AssociatedClient],
	[OperaSalesLedger],
	[flagHidden],
	[DummyAccount],
	[ClientTypeID],
	[HighPriority],
	[LeadSaleID],
	[SyncronisationStatus],
	[HoldAllAgreements],
	[ServiceDeskID],
	[TradingAs],
	[IsVATRegistered],
	[NumberOfERMUpdates]
)
SELECT 
	[CustomerID],
	[CustomerKey],
	[CreatedBy],
	[CreatedOn],
	[ModifiedBy],
	[ModifiedOn],
	[CompanyName],
	[Password],
	[CompanyRegistrationNumber],
	[CompanyVATNumber],
	[NumberOfStaff],
	[Payroll],
	[BankName],
	[AccountNumber],
	[SortCode],
	[CompanyMainEmail],
	[CompanyWebsite],
	[DefaultLegalLocationID],
	[CurrentSalesRepUserName],
	[CurrentHSConsultant],
	[CurrentPersonnelConsultant],
	[PrincipleBusinessTypeID],
	[CompanyNameAKA],
	[PBSIndustryTypeID],
	[CustomerGroupID],
	[DefaultPaymentMethodID],
	[PrivateAndConfidential],
	[AssociatedClientNotes],
	[AssociatedClient],
	[OperaSalesLedger],
	[flagHidden],
	[DummyAccount],
	[ClientTypeID],
	[HighPriority],
	[LeadSaleID],
	[SyncronisationStatus],
	[HoldAllAgreements],
	[ServiceDeskID],
	[TradingAs],
	[IsVATRegistered],
	[NumberOfERMUpdates]
FROM [PBSPROD2SQL\PROD2].[Peninsula].[dbo].[TblCustomers]
WHERE CustomerId = @customerId

SET IDENTITY_INSERT [TblCustomers] OFF

SET IDENTITY_INSERT [TblCompanyContacts] ON

INSERT INTO [TblCompanyContacts]
(
	[CompanyContactID],
	[CompanyID],
	[SiteAddressID],
	[CustomerKey],
	[ContactTypeID],
	[PositionID],
	[Title],
	[Forename],
	[Initial],
	[Surname],
	[TelNoMain],
	[TelNoAlt],
	[EMail],
	[NameSoundX],
	[flagHidden],
	[PrivateAndConfidential],
	[FaxNumber],
	[ContactHrsSpanStart],
	[ContactHrsSpanEnd],
	[CompanyNo],
	[ContactNo],
	[Notes]
) 
SELECT 
	[CompanyContactID],
	[CompanyID],
	[SiteAddressID],
	[CustomerKey],
	[ContactTypeID],
	[PositionID],
	[Title],
	[Forename],
	[Initial],
	[Surname],
	[TelNoMain],
	[TelNoAlt],
	[EMail],
	[NameSoundX],
	[flagHidden],
	[PrivateAndConfidential],
	[FaxNumber],
	[ContactHrsSpanStart],
	[ContactHrsSpanEnd],
	[CompanyNo],
	[ContactNo],
	[Notes]
FROM [PBSPROD2SQL\PROD2].[Peninsula].[dbo].[TblCompanyContacts]
WHERE CompanyId = @customerId

SET IDENTITY_INSERT [TblCompanyContacts] OFF

--SET IDENTITY_INSERT [TblContactsContactTypes] ON

INSERT INTO [TblContactsContactTypes]
(
	[CompanyContactID],
	[ContactTypeID],
	[SiteAddressID]
)
SELECT 
	[CompanyContactID],
	[ContactTypeID],
	[SiteAddressID]
FROM [PBSPROD2SQL\PROD2].[Peninsula].[dbo].[TblContactsContactTypes]
WHERE CompanyContactId IN
(
	SELECT CompanyContactId 
	FROM [PBSPROD2SQL\PROD2].[Peninsula].[dbo].[TblCompanyContacts]
	WHERE CompanyId = @customerId
)

--SET IDENTITY_INSERT [TblContactsContactTypes] OFF

SET IDENTITY_INSERT [TblSiteAddresses] ON

INSERT INTO [TblSiteAddresses]
(
	[SiteAddressID],
	[SiteAddressTypeID],
	[CustomerKey],
	[CompanyName],
	[Address1],
	[Address2],
	[Address3],
	[Address4],
	[Address5],
	[County],
	[Postcode],
	[PhoneMain],
	[FaxMain],
	[HSConsultantCode],
	[PersonnelConsultantCode],
	[SalesConsultantCode],
	[flagHidden],
	[CustomerID],
	[SoundXCompanyName],
	[SoundXAddress1],
	[SoundXPostCode],
	[CurrentHSConsultant],
	[CurrentPersonnelConsultant],
	[CurrentSalesRepUserName],
	[LegalLocationID],
	[BankName],
	[AccountNumber],
	[SortCode],
	[IndustryTypeID],
	[PrincipleBusinessTypeID],
	[PrivateAndConfidential],
	[HopewiserIndustryType],
	[CompanyNo],
	[Latitude],
	[Longitude],
	[LockCurrentHSConsultant],
	[OperaCompanyName],
	[CompanyNameJustAlphaNumeric]
)
SELECT 
	[SiteAddressID],
	[SiteAddressTypeID],
	[CustomerKey],
	[CompanyName],
	[Address1],
	[Address2],
	[Address3],
	[Address4],
	[Address5],
	[County],
	[Postcode],
	[PhoneMain],
	[FaxMain],
	[HSConsultantCode],
	[PersonnelConsultantCode],
	[SalesConsultantCode],
	[flagHidden],
	[CustomerID],
	[SoundXCompanyName],
	[SoundXAddress1],
	[SoundXPostCode],
	[CurrentHSConsultant],
	[CurrentPersonnelConsultant],
	[CurrentSalesRepUserName],
	[LegalLocationID],
	[BankName],
	[AccountNumber],
	[SortCode],
	[IndustryTypeID],
	[PrincipleBusinessTypeID],
	[PrivateAndConfidential],
	[HopewiserIndustryType],
	[CompanyNo],
	[Latitude],
	[Longitude],
	[LockCurrentHSConsultant],
	[OperaCompanyName],
	[CompanyNameJustAlphaNumeric]
FROM [PBSPROD2SQL\PROD2].[Peninsula].[dbo].[TblSiteAddresses]
WHERE [CustomerID] = @customerId

SET IDENTITY_INSERT [TblSiteAddresses] OFF

--SET IDENTITY_INSERT [TblSiteAddressSiteAddressTypes] ON

INSERT INTO [TblSiteAddressSiteAddressTypes]
(
	[SiteAddressID],
	[SiteAddressTypeID]
)
SELECT 
	[SiteAddressID],
	[SiteAddressTypeID]
FROM [PBSPROD2SQL\PROD2].[Peninsula].[dbo].[TblSiteAddressSiteAddressTypes]
WHERE SiteAddressId IN
(
	SELECT SiteAddressId
	FROM [PBSPROD2SQL\PROD2].[Peninsula].[dbo].[TblSiteAddresses]
	WHERE [CustomerID] = @customerId
)

--SET IDENTITY_INSERT [TblSiteAddressSiteAddressTypes] OFF

COMMIT TRAN
--ROLLBACK TRAN