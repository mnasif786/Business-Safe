----------------------------------------------------------------------------------------------------------------------------------------------------------

			/* CREATE the Peninsula_BS tables */

----------------------------------------------------------------------------------------------------------------------------------------------------------

--	tblCustomers	<-- Practice Name, Practice Code

--	tblSiteAddresses	<-- Address, Postcode, Tel No, Fax No, Email(?)
--	tblSiteAddressTypes	<-- list of Address types
--	tblSiteAddressSiteAddressTypes	<-- the link between Addresses & Address Types

--	tblCompanyContacts	<-- Forename, Surname, Email, Contact Type(?)
--	tblContactTypes	<-- the link between Contacts & Contact Types
--	tblSiteAddressCompanyContacts	<-- the link between Addresses & Contacts
--	tblContactsContactType	<-- list of Contact types

--	tblTaxWiseCustomers <-- Practice Code

----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* FKs required for tblCustomers */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TBLCustomerGroups](
	[CustomerGroupID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](300) NOT NULL,
	[Description] [varchar](200) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](200) NULL,
 CONSTRAINT [PK_TBLCustomerGroups] PRIMARY KEY CLUSTERED 
(
	[CustomerGroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TBLINDUSTRYTYPES](
	[ID] [int] NOT NULL,
	[INDUSTRYTYPE] [varchar](100) NULL,
 CONSTRAINT [PK_TBLINDUSTRYTYPES] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TBLLegalLocations](
	[LegalLocationID] [int] IDENTITY(6,1) NOT NULL,
	[Title] [varchar](20) NOT NULL,
	[Description] [varchar](100) NULL,
	[CreatedBy] [varchar](30) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[CountryCode] [varchar](50) NULL,
	[CurrencySymbol] [varchar](10) NOT NULL,
	[CurrencyCode] [char](3) NOT NULL,
	[VATCodeRegistered] [char](1) NOT NULL,
	[VATCodeUnRegistered] [char](1) NOT NULL,
	[IPTRate] [decimal](4, 2) NOT NULL,
	[IPTFixed] [decimal](4, 2) NOT NULL,
	[IsLitigation] [bit] NULL,
	[CultureName] [nvarchar](50) NULL,
 CONSTRAINT [PK_TBLLegalLocations] PRIMARY KEY CLUSTERED 
(
	[LegalLocationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TBLLegalLocations] ADD  CONSTRAINT [DF_TBLLegalLocations_CurrencySymbol]  DEFAULT ('?') FOR [CurrencySymbol]
GO

ALTER TABLE [dbo].[TBLLegalLocations] ADD  CONSTRAINT [DF_TBLLegalLocations_CurrencyCode]  DEFAULT (' ') FOR [CurrencyCode]
GO

ALTER TABLE [dbo].[TBLLegalLocations] ADD  CONSTRAINT [DF_TBLLegalLocations_VATCodeRegistered]  DEFAULT (' ') FOR [VATCodeRegistered]
GO

ALTER TABLE [dbo].[TBLLegalLocations] ADD  CONSTRAINT [DF_TBLLegalLocations_VATCodeUnRegistered]  DEFAULT (' ') FOR [VATCodeUnRegistered]
GO

ALTER TABLE [dbo].[TBLLegalLocations] ADD  CONSTRAINT [DF_TBLLegalLocations_IPTRate]  DEFAULT ((0)) FOR [IPTRate]
GO

ALTER TABLE [dbo].[TBLLegalLocations] ADD  CONSTRAINT [DF_TBLLegalLocations_IPTFixed]  DEFAULT ((0)) FOR [IPTFixed]
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TBLPaymentMethods](
	[PaymentMethodID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](20) NOT NULL,
	[Description] [varchar](200) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
 CONSTRAINT [PK_TBLPaymentMethods] PRIMARY KEY CLUSTERED 
(
	[PaymentMethodID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TBLIndustryTypeCategoryMainGroups](
	[MainCatID] [int] NOT NULL,
	[MainCatDescription] [varchar](100) NULL,
 CONSTRAINT [PK_TBLIndustryTypeCategoryMainGroups] PRIMARY KEY CLUSTERED 
(
	[MainCatID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TBLIndustryTypesAccounts](
	[AccountsIndTypeID] [int] IDENTITY(1,1) NOT NULL,
	[AccountsSicCode] [varchar](9) NULL,
	[AccountsIndustryTypeDesc] [varchar](150) NULL,
 CONSTRAINT [PK_tblAccountsIndustryTypes] PRIMARY KEY CLUSTERED 
(
	[AccountsIndTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TBLIndustryTypesCustomerCare](
	[CustomerCareIndTypeID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerCareSICCode] [varchar](6) NULL,
	[CustomerCareIndTypeDesc] [varchar](100) NULL,
 CONSTRAINT [PK_tblCustomerCareIndustryTypes] PRIMARY KEY CLUSTERED 
(
	[CustomerCareIndTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TBLIndustryTypesTaxwise](
	[TaxwiseIndTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TaxwiseIndTypeDesc] [varchar](100) NULL,
 CONSTRAINT [PK_TBLIndustryTypesTaxwise] PRIMARY KEY CLUSTERED 
(
	[TaxwiseIndTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TBLIndustrytypesTelesales](
	[TeleSalesIndTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TelesalesSICCode] [varchar](6) NULL,
	[TeleSalesIndTypeDesc] [varchar](100) NULL,
 CONSTRAINT [PK_TBLIndustrytypesTelesales] PRIMARY KEY CLUSTERED 
(
	[TeleSalesIndTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TBLPBSIndustryTypes](
	[PBSIndustryTypeID] [int] IDENTITY(1,1) NOT NULL,
	[MainCatID] [int] NULL,
	[SubCatID] [int] NULL,
	[IndustryCatID] [int] NULL,
	[Title] [varchar](100) NOT NULL,
	[Description] [varchar](200) NULL,
	[UKSICCode] [varchar](20) NULL,
	[CreatedBy] [varchar](30) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[TeleSalesIndTypeID] [int] NULL,
	[TaxwiseIndTypeID] [int] NULL,
	[CustomerCareIndTypeID] [int] NULL,
	[AccountsIndTypeID] [int] NULL,
 CONSTRAINT [PK_TBLPBSIndustryTypes] PRIMARY KEY CLUSTERED 
(
	[PBSIndustryTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TBLPBSIndustryTypes]  WITH CHECK ADD  CONSTRAINT [FK_TBLPBSIndustryTypes_TBLIndustryTypeCategoryMainGroups] FOREIGN KEY([MainCatID])
REFERENCES [dbo].[TBLIndustryTypeCategoryMainGroups] ([MainCatID])
GO

ALTER TABLE [dbo].[TBLPBSIndustryTypes] CHECK CONSTRAINT [FK_TBLPBSIndustryTypes_TBLIndustryTypeCategoryMainGroups]
GO

ALTER TABLE [dbo].[TBLPBSIndustryTypes]  WITH CHECK ADD  CONSTRAINT [FK_TBLPBSIndustryTypes_TBLIndustryTypesAccounts] FOREIGN KEY([AccountsIndTypeID])
REFERENCES [dbo].[TBLIndustryTypesAccounts] ([AccountsIndTypeID])
GO

ALTER TABLE [dbo].[TBLPBSIndustryTypes] CHECK CONSTRAINT [FK_TBLPBSIndustryTypes_TBLIndustryTypesAccounts]
GO

ALTER TABLE [dbo].[TBLPBSIndustryTypes]  WITH CHECK ADD  CONSTRAINT [FK_TBLPBSIndustryTypes_TBLIndustryTypesCustomerCare] FOREIGN KEY([CustomerCareIndTypeID])
REFERENCES [dbo].[TBLIndustryTypesCustomerCare] ([CustomerCareIndTypeID])
GO

ALTER TABLE [dbo].[TBLPBSIndustryTypes] CHECK CONSTRAINT [FK_TBLPBSIndustryTypes_TBLIndustryTypesCustomerCare]
GO

ALTER TABLE [dbo].[TBLPBSIndustryTypes]  WITH CHECK ADD  CONSTRAINT [FK_TBLPBSIndustryTypes_TBLIndustryTypesTaxwise] FOREIGN KEY([TaxwiseIndTypeID])
REFERENCES [dbo].[TBLIndustryTypesTaxwise] ([TaxwiseIndTypeID])
GO

ALTER TABLE [dbo].[TBLPBSIndustryTypes] CHECK CONSTRAINT [FK_TBLPBSIndustryTypes_TBLIndustryTypesTaxwise]
GO

ALTER TABLE [dbo].[TBLPBSIndustryTypes]  WITH CHECK ADD  CONSTRAINT [FK_TBLPBSIndustryTypes_TBLIndustrytypesTelesales] FOREIGN KEY([TeleSalesIndTypeID])
REFERENCES [dbo].[TBLIndustrytypesTelesales] ([TeleSalesIndTypeID])
GO

ALTER TABLE [dbo].[TBLPBSIndustryTypes] CHECK CONSTRAINT [FK_TBLPBSIndustryTypes_TBLIndustrytypesTelesales]
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* tblCustomers */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TBLCustomers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerKey] [varchar](100) NOT NULL,
	[CreatedBy] [varchar](30) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [varchar](30) NULL,
	[ModifiedOn] [datetime] NULL,
	[CompanyName] [varchar](300) NULL,
	[Password] [varchar](200) NULL,
	[CompanyRegistrationNumber] [varchar](15) NOT NULL,
	[CompanyVATNumber] [varchar](15) NULL,
	[NumberOfStaff] [int] NULL,
	[Payroll] [money] NULL,
	[BankName] [varchar](200) NULL,
	[AccountNumber] [varchar](100) NULL,
	[SortCode] [int] NULL,
	[CompanyMainEmail] [varchar](1000) NULL,
	[CompanyWebsite] [varchar](1000) NULL,
	[DefaultLegalLocationID] [int] NOT NULL,
	[CurrentSalesRepUserName] [varchar](30) NULL,
	[CurrentHSConsultant] [varchar](30) NULL,
	[CurrentPersonnelConsultant] [varchar](30) NULL,
	[PrincipleBusinessTypeID] [int] NULL,
	[CompanyNameAKA] [varchar](15) NULL,
	[PBSIndustryTypeID] [int] NULL,
	[CustomerGroupID] [int] NULL,
	[DefaultPaymentMethodID] [int] NOT NULL,
	[PrivateAndConfidential] [bit] NULL,
	[AssociatedClientNotes] [varchar](2000) NULL,
	[AssociatedClient] [varchar](20) NULL,
	[OperaSalesLedger] [varchar](50) NULL,
	[flagHidden] [bit] NULL,
	[DummyAccount] [bit] NULL,
	[ClientTypeID] [int] NULL,
	[HighPriority] [bit] NULL,
	[LeadSaleID] [int] NULL,
	[SyncronisationStatus] [int] NULL,
	[HoldAllAgreements] [bit] NULL,
	[ServiceDeskID] [int] NULL,
	[TradingAs] [varchar](100) NULL,
	[IsVATRegistered] [bit] NULL,
	[NumberOfERMUpdates] [int] NULL,
 CONSTRAINT [PK_TBLCustomers] PRIMARY KEY CLUSTERED 
(
	[CustomerKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TBLCustomers]  WITH NOCHECK ADD  CONSTRAINT [FK_TBLCustomers_TBLCustomerGroups] FOREIGN KEY([CustomerGroupID])
REFERENCES [dbo].[TBLCustomerGroups] ([CustomerGroupID])
GO

ALTER TABLE [dbo].[TBLCustomers] CHECK CONSTRAINT [FK_TBLCustomers_TBLCustomerGroups]
GO

ALTER TABLE [dbo].[TBLCustomers]  WITH CHECK ADD  CONSTRAINT [FK_TBLCustomers_TBLINDUSTRYTYPES] FOREIGN KEY([PrincipleBusinessTypeID])
REFERENCES [dbo].[TBLINDUSTRYTYPES] ([ID])
GO

ALTER TABLE [dbo].[TBLCustomers] CHECK CONSTRAINT [FK_TBLCustomers_TBLINDUSTRYTYPES]
GO

ALTER TABLE [dbo].[TBLCustomers]  WITH NOCHECK ADD  CONSTRAINT [FK_TBLCustomers_TBLLegalLocations] FOREIGN KEY([DefaultLegalLocationID])
REFERENCES [dbo].[TBLLegalLocations] ([LegalLocationID])
GO

ALTER TABLE [dbo].[TBLCustomers] CHECK CONSTRAINT [FK_TBLCustomers_TBLLegalLocations]
GO

ALTER TABLE [dbo].[TBLCustomers]  WITH NOCHECK ADD  CONSTRAINT [FK_TBLCustomers_TBLPaymentMethods] FOREIGN KEY([DefaultPaymentMethodID])
REFERENCES [dbo].[TBLPaymentMethods] ([PaymentMethodID])
GO

ALTER TABLE [dbo].[TBLCustomers] CHECK CONSTRAINT [FK_TBLCustomers_TBLPaymentMethods]
GO

ALTER TABLE [dbo].[TBLCustomers]  WITH CHECK ADD  CONSTRAINT [FK_TBLCustomers_TBLPBSIndustryTypes] FOREIGN KEY([PBSIndustryTypeID])
REFERENCES [dbo].[TBLPBSIndustryTypes] ([PBSIndustryTypeID])
GO

ALTER TABLE [dbo].[TBLCustomers] CHECK CONSTRAINT [FK_TBLCustomers_TBLPBSIndustryTypes]
GO

ALTER TABLE [dbo].[TBLCustomers] ADD  CONSTRAINT [DF_TBLCustomers_CompanyRegistrationNumber]  DEFAULT (0) FOR [CompanyRegistrationNumber]
GO

ALTER TABLE [dbo].[TBLCustomers] ADD  CONSTRAINT [DF_TBLCustomers_NumberOfStaff]  DEFAULT (0) FOR [NumberOfStaff]
GO

ALTER TABLE [dbo].[TBLCustomers] ADD  CONSTRAINT [DF_TBLCustomers_Payroll]  DEFAULT (0) FOR [Payroll]
GO

ALTER TABLE [dbo].[TBLCustomers] ADD  CONSTRAINT [DF_TBLCustomers_LegalLocationID]  DEFAULT (1) FOR [DefaultLegalLocationID]
GO

ALTER TABLE [dbo].[TBLCustomers] ADD  CONSTRAINT [DF_TBLCustomers_PrincipleBusinessTypeID]  DEFAULT (0) FOR [PrincipleBusinessTypeID]
GO

ALTER TABLE [dbo].[TBLCustomers] ADD  CONSTRAINT [DF_TBLCustomers_DefaultPaymentMethodID]  DEFAULT (0) FOR [DefaultPaymentMethodID]
GO

ALTER TABLE [dbo].[TBLCustomers] ADD  DEFAULT (0) FOR [PrivateAndConfidential]
GO

ALTER TABLE [dbo].[TBLCustomers] ADD  CONSTRAINT [TBLCustomersDefaultOpera]  DEFAULT ('UK') FOR [OperaSalesLedger]
GO

ALTER TABLE [dbo].[TBLCustomers] ADD  DEFAULT (0) FOR [flagHidden]
GO

ALTER TABLE [dbo].[TBLCustomers] ADD  DEFAULT (0) FOR [DummyAccount]
GO

ALTER TABLE [dbo].[TBLCustomers] ADD  DEFAULT (0) FOR [HighPriority]
GO

ALTER TABLE [dbo].[TBLCustomers] ADD  DEFAULT (0) FOR [SyncronisationStatus]
GO

ALTER TABLE [dbo].[TBLCustomers] ADD  DEFAULT (0) FOR [HoldAllAgreements]
GO

ALTER TABLE [dbo].[TBLCustomers] ADD  DEFAULT ((0)) FOR [IsVATRegistered]
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* tblSiteAddresses */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

SET ARITHABORT ON
GO

CREATE TABLE [dbo].[TBLSiteAddresses](
	[SiteAddressID] [int] IDENTITY(1,1) NOT NULL,
	[SiteAddressTypeID] [int] NOT NULL,
	[CustomerKey] [varchar](100) NULL,
	[CompanyName] [varchar](100) NOT NULL,
	[Address1] [varchar](40) NULL,
	[Address2] [varchar](40) NULL,
	[Address3] [varchar](40) NULL,
	[Address4] [varchar](40) NULL,
	[Address5] [varchar](40) NULL,
	[County] [varchar](40) NULL,
	[Postcode] [varchar](15) NULL,
	[PhoneMain] [varchar](20) NULL,
	[FaxMain] [varchar](20) NULL,
	[HSConsultantCode] [varchar](3) NULL,
	[PersonnelConsultantCode] [varchar](3) NULL,
	[SalesConsultantCode] [varchar](3) NULL,
	[flagHidden] [bit] NULL,
	[CustomerID] [int] NULL,
	[SoundXCompanyName] [char](4) NULL,
	[SoundXAddress1] [char](4) NULL,
	[SoundXPostCode] [char](4) NULL,
	[CurrentHSConsultant] [varchar](30) NULL,
	[CurrentPersonnelConsultant] [varchar](30) NULL,
	[CurrentSalesRepUserName] [varchar](30) NULL,
	[LegalLocationID] [int] NOT NULL,
	[BankName] [varchar](200) NULL,
	[AccountNumber] [varchar](100) NULL,
	[SortCode] [int] NULL,
	[IndustryTypeID] [int] NULL,
	[PrincipleBusinessTypeID] [int] NULL,
	[PrivateAndConfidential] [bit] NULL,
	[HopewiserIndustryType] [varchar](200) NULL,
	[CompanyNo] [int] NULL,
	[Latitude] [decimal](9, 6) NULL,
	[Longitude] [decimal](9, 6) NULL,
	[LockCurrentHSConsultant] [bit] NOT NULL,
	[CompanyNameJustAlphaNumeric]  AS ([dbo].[JustAlphaNumeric]([companyname])) PERSISTED NOT NULL,
	[OperaCompanyName] [nvarchar](40) NULL,
 CONSTRAINT [PK_TBLSiteAddresses] PRIMARY KEY NONCLUSTERED 
(
	[SiteAddressID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TBLSiteAddresses]  WITH NOCHECK ADD  CONSTRAINT [FK_TBLSiteAddresses_TBLLegalLocations] FOREIGN KEY([LegalLocationID])
REFERENCES [dbo].[TBLLegalLocations] ([LegalLocationID])
GO

ALTER TABLE [dbo].[TBLSiteAddresses] CHECK CONSTRAINT [FK_TBLSiteAddresses_TBLLegalLocations]
GO

ALTER TABLE [dbo].[TBLSiteAddresses] ADD  CONSTRAINT [DF_TBLSiteAddresses_flagHidden]  DEFAULT (0) FOR [flagHidden]
GO

ALTER TABLE [dbo].[TBLSiteAddresses] ADD  CONSTRAINT [DF_TBLSiteAddresses_LegalLocationID]  DEFAULT (0) FOR [LegalLocationID]
GO

ALTER TABLE [dbo].[TBLSiteAddresses] ADD  CONSTRAINT [DF_TBLSiteAddresses_IndustryTypeID]  DEFAULT (0) FOR [IndustryTypeID]
GO

ALTER TABLE [dbo].[TBLSiteAddresses] ADD  DEFAULT (0) FOR [PrivateAndConfidential]
GO

ALTER TABLE [dbo].[TBLSiteAddresses] ADD  DEFAULT (0) FOR [LockCurrentHSConsultant]
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* tblSiteAddressTypes */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TBLSiteAddressTypes](
	[SiteAddressTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](30) NULL,
	[Description] [varchar](100) NULL,
	[DateAdded] [datetime] NOT NULL,
	[AddedBy] [varchar](30) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](30) NULL,
	[UserRight] [varchar](50) NULL,
	[OneOnly] [bit] NOT NULL,
	[ListOrder] [int] NULL,
	[ImageFile] [varchar](500) NULL,
	[CustomerCareExclude] [bit] NULL,
 CONSTRAINT [PK_TBLSiteAddressTypes] PRIMARY KEY CLUSTERED 
(
	[SiteAddressTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Only allow one of this type' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TBLSiteAddressTypes', @level2type=N'COLUMN',@level2name=N'OneOnly'
GO

ALTER TABLE [dbo].[TBLSiteAddressTypes] ADD  CONSTRAINT [DF_TBLSiteAddressTypes_UserRight]  DEFAULT ('') FOR [UserRight]
GO

ALTER TABLE [dbo].[TBLSiteAddressTypes] ADD  CONSTRAINT [DF_TBLSiteAddressTypes_OneOnly]  DEFAULT (0) FOR [OneOnly]
GO

ALTER TABLE [dbo].[TBLSiteAddressTypes] ADD  CONSTRAINT [DF_TBLSiteAddressTypes_ListOrder]  DEFAULT (0) FOR [ListOrder]
GO

ALTER TABLE [dbo].[TBLSiteAddressTypes] ADD  DEFAULT (0) FOR [CustomerCareExclude]
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* tblSiteAddressSiteAddressTypes */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBLSiteAddressSiteAddressTypes](
	[SiteAddressID] [int] NOT NULL,
	[SiteAddressTypeID] [int] NOT NULL,
 CONSTRAINT [PK_TBLSiteAddressSiteAddressTypes] PRIMARY KEY CLUSTERED 
(
	[SiteAddressID] ASC,
	[SiteAddressTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TBLSiteAddressSiteAddressTypes]  WITH NOCHECK ADD  CONSTRAINT [FK_TBLSiteAddressSiteAddressTypes_TBLSiteAddresses] FOREIGN KEY([SiteAddressID])
REFERENCES [dbo].[TBLSiteAddresses] ([SiteAddressID])
GO

ALTER TABLE [dbo].[TBLSiteAddressSiteAddressTypes] CHECK CONSTRAINT [FK_TBLSiteAddressSiteAddressTypes_TBLSiteAddresses]
GO

ALTER TABLE [dbo].[TBLSiteAddressSiteAddressTypes]  WITH NOCHECK ADD  CONSTRAINT [FK_TBLSiteAddressSiteAddressTypes_TBLSiteAddressTypes] FOREIGN KEY([SiteAddressTypeID])
REFERENCES [dbo].[TBLSiteAddressTypes] ([SiteAddressTypeID])
GO

ALTER TABLE [dbo].[TBLSiteAddressSiteAddressTypes] CHECK CONSTRAINT [FK_TBLSiteAddressSiteAddressTypes_TBLSiteAddressTypes]
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* FKs required for tblCompanyContacts */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TBLContactPositions](
	[PositionID] [int] IDENTITY(1,1) NOT NULL,
	[PositionCode] [varchar](6) NULL,
	[Title] [varchar](75) NULL,
	[Description] [varchar](100) NULL,
	[DateAdded] [datetime] NOT NULL,
	[AddedBy] [varchar](30) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](30) NULL,
 CONSTRAINT [PK_TBLContactPositions] PRIMARY KEY CLUSTERED 
(
	[PositionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* tblCompanyContacts */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TBLCompanyContacts](
	[CompanyContactID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NULL,
	[SiteAddressID] [int] NOT NULL,
	[CustomerKey] [varchar](100) NULL,
	[ContactTypeID] [int] NULL,
	[PositionID] [int] NULL,
	[Title] [varchar](20) NULL,
	[Forename] [varchar](30) NULL,
	[Initial] [varchar](10) NULL,
	[Surname] [varchar](30) NOT NULL,
	[TelNoMain] [varchar](20) NULL,
	[TelNoAlt] [varchar](20) NULL,
	[EMail] [varchar](50) NULL,
	[NameSoundX] [varchar](6) NOT NULL,
	[flagHidden] [bit] NULL,
	[PrivateAndConfidential] [bit] NULL,
	[FaxNumber] [varchar](20) NULL,
	[ContactHrsSpanStart] [char](5) NULL,
	[ContactHrsSpanEnd] [char](5) NULL,
	[CompanyNo] [int] NULL,
	[ContactNo] [int] NULL,
	[Notes] [varchar](500) NULL,
 CONSTRAINT [PK_TBLCompanyContacts] PRIMARY KEY NONCLUSTERED 
(
	[CompanyContactID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TBLCompanyContacts]  WITH NOCHECK ADD  CONSTRAINT [FK_TBLCompanyContacts_TBLContactPositions] FOREIGN KEY([PositionID])
REFERENCES [dbo].[TBLContactPositions] ([PositionID])
GO

ALTER TABLE [dbo].[TBLCompanyContacts] CHECK CONSTRAINT [FK_TBLCompanyContacts_TBLContactPositions]
GO

ALTER TABLE [dbo].[TBLCompanyContacts] ADD  CONSTRAINT [DF_TBLCompanyContacts_flagHidden]  DEFAULT (0) FOR [flagHidden]
GO

ALTER TABLE [dbo].[TBLCompanyContacts] ADD  DEFAULT (0) FOR [PrivateAndConfidential]
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* tblContactTypes */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TBLContactTypes](
	[ContactTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](30) NOT NULL,
	[Description] [varchar](100) NULL,
	[DateAdded] [datetime] NOT NULL,
	[AddedBy] [varchar](30) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](30) NULL,
	[UserRight] [varchar](50) NULL,
	[OneOnly] [bit] NULL,
	[ListOrder] [int] NOT NULL,
	[ImageFile] [varchar](500) NULL,
	[CustomerCareExclude] [bit] NULL,
 CONSTRAINT [PK_TBLContactTypes] PRIMARY KEY CLUSTERED 
(
	[ContactTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TBLContactTypes] ADD  CONSTRAINT [DF_TBLContactTypes_UserRight]  DEFAULT ('') FOR [UserRight]
GO

ALTER TABLE [dbo].[TBLContactTypes] ADD  CONSTRAINT [DF_TBLContactTypes_OneOnly]  DEFAULT (0) FOR [OneOnly]
GO

ALTER TABLE [dbo].[TBLContactTypes] ADD  CONSTRAINT [DF_TBLContactTypes_ListOrder]  DEFAULT (0) FOR [ListOrder]
GO

ALTER TABLE [dbo].[TBLContactTypes] ADD  DEFAULT (null) FOR [ImageFile]
GO

ALTER TABLE [dbo].[TBLContactTypes] ADD  DEFAULT (0) FOR [CustomerCareExclude]
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* tblSiteAddressCompanyContacts */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBLSiteAddressCompanyContacts](
	[SiteAddressID] [int] NOT NULL,
	[CompanyContactID] [int] NOT NULL,
 CONSTRAINT [PK_TBLSiteAddressCompanyContacts] PRIMARY KEY CLUSTERED 
(
	[SiteAddressID] ASC,
	[CompanyContactID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TBLSiteAddressCompanyContacts]  WITH NOCHECK ADD  CONSTRAINT [FK_TBLSiteAddressCompanyContacts_TBLCompanyContacts] FOREIGN KEY([CompanyContactID])
REFERENCES [dbo].[TBLCompanyContacts] ([CompanyContactID])
GO

ALTER TABLE [dbo].[TBLSiteAddressCompanyContacts] CHECK CONSTRAINT [FK_TBLSiteAddressCompanyContacts_TBLCompanyContacts]
GO

ALTER TABLE [dbo].[TBLSiteAddressCompanyContacts]  WITH NOCHECK ADD  CONSTRAINT [FK_TBLSiteAddressCompanyContacts_TBLSiteAddresses] FOREIGN KEY([SiteAddressID])
REFERENCES [dbo].[TBLSiteAddresses] ([SiteAddressID])
GO

ALTER TABLE [dbo].[TBLSiteAddressCompanyContacts] CHECK CONSTRAINT [FK_TBLSiteAddressCompanyContacts_TBLSiteAddresses]
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* tblContactsContactType */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBLContactsContactTypes](
	[CompanyContactID] [int] NOT NULL,
	[ContactTypeID] [int] NOT NULL,
	[SiteAddressID] [int] NULL,
 CONSTRAINT [PK_TBLContactsContactTypes] PRIMARY KEY CLUSTERED 
(
	[CompanyContactID] ASC,
	[ContactTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TBLContactsContactTypes]  WITH NOCHECK ADD  CONSTRAINT [FK_TBLContactsContactTypes_TBLCompanyContacts] FOREIGN KEY([CompanyContactID])
REFERENCES [dbo].[TBLCompanyContacts] ([CompanyContactID])
GO

ALTER TABLE [dbo].[TBLContactsContactTypes] CHECK CONSTRAINT [FK_TBLContactsContactTypes_TBLCompanyContacts]
GO

ALTER TABLE [dbo].[TBLContactsContactTypes]  WITH NOCHECK ADD  CONSTRAINT [FK_TBLContactsContactTypes_TBLContactTypes] FOREIGN KEY([ContactTypeID])
REFERENCES [dbo].[TBLContactTypes] ([ContactTypeID])
GO

ALTER TABLE [dbo].[TBLContactsContactTypes] CHECK CONSTRAINT [FK_TBLContactsContactTypes_TBLContactTypes]
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* tblTaxWiseCustomers */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE	[dbo].[TBLTaxWiseCustomers](
	CustomerID bigint NOT NULL
)
GO

SET ANSI_PADDING OFF
GO




----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* tblTitles */
----------------------------------------------------------------------------------------------------------------------------------------------------------

USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TBLTitles](
	[Title] [varchar](20) NOT NULL,
	[OrderBy] [int] NOT NULL,
 CONSTRAINT [PK_TBLTitles] PRIMARY KEY CLUSTERED 
(
	[Title] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* TBLOperaSalesLedgers */
----------------------------------------------------------------------------------------------------------------------------------------------------------

USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TBLOperaSalesLedgers](
	[OperaSalesLedgerID] [varchar](5) NOT NULL,
	[Description] [varchar](30) NULL,
	[CreatedBy] [varchar](30) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[OperaOutputFolder] [varchar](255) NOT NULL,
 CONSTRAINT [PK_TBLOperaSalesLedgers] PRIMARY KEY CLUSTERED 
(
	[OperaSalesLedgerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TBLOperaSalesLedgers] ADD  CONSTRAINT [DF_TBLOperaSalesLedgers_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[TBLOperaSalesLedgers] ADD  CONSTRAINT [DF_TBLOperaSalesLedgers_OperaOutputFolder]  DEFAULT (' ') FOR [OperaOutputFolder]
GO


USE [Peninsula_BS]
GO
----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* ChangeLog */
----------------------------------------------------------------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ChangeLog](
	[change_number] [int] NOT NULL,
	[delta_set] [varchar](10) NOT NULL,
	[start_dt] [datetime] NOT NULL,
	[complete_dt] [datetime] NULL,
	[applied_by] [varchar](100) NOT NULL,
	[description] [varchar](500) NOT NULL,
 CONSTRAINT [Pkchangelog] PRIMARY KEY CLUSTERED 
(
	[change_number] ASC,
	[delta_set] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO