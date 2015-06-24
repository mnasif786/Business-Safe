----------------------------------------------------------------------------------------------------------------------------------------------------------

			/* INSERT INTO the Peninsula_BS DDL tables */
			/* Insert just enough data to support TaxWise practice management */

----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

IF NOT EXISTS (SELECT * FROM dbo.TBLINDUSTRYTYPES WHERE ID > 0)
	BEGIN
		INSERT INTO	dbo.TBLINDUSTRYTYPES(ID, INDUSTRYTYPE)
		SELECT	0, 'NOT SET'
	END
GO

IF NOT EXISTS (SELECT * FROM dbo.TBLPBSIndustryTypes WHERE PBSIndustryTypeID > 0)
	SET IDENTITY_INSERT dbo.TBLPBSIndustryTypes ON;
	BEGIN
		INSERT INTO	dbo.TBLPBSIndustryTypes(PBSIndustryTypeID, Title, CreatedBy, CreatedOn)
		SELECT	123, 'Accountants', 'SQLScript', GetDate()
	END
	SET IDENTITY_INSERT dbo.TBLPBSIndustryTypes OFF;
GO

IF NOT EXISTS (SELECT * FROM dbo.TBLLegalLocations WHERE LegalLocationID > 0)
	SET IDENTITY_INSERT dbo.TBLLegalLocations ON;
	BEGIN
		INSERT INTO	dbo.TBLLegalLocations(LegalLocationID, Title, CreatedBy, CreatedOn)
		SELECT	1, 'UK', 'SQLScript', GetDate()
	END
	SET IDENTITY_INSERT dbo.TBLLegalLocations OFF;
GO

IF NOT EXISTS (SELECT * FROM dbo.TBLPaymentMethods WHERE PaymentMethodID > 0)
	SET IDENTITY_INSERT dbo.TBLPaymentMethods ON;
	BEGIN
		INSERT INTO	dbo.TBLPaymentMethods(PaymentMethodID, Title, CreatedBy, CreatedOn)
		SELECT	4, 'Cheque', 'SQLScript', GetDate() UNION
		SELECT	6, 'BACS', 'SQLScript', GetDate()
	END
	SET IDENTITY_INSERT dbo.TBLPaymentMethods OFF;
GO

IF NOT EXISTS (SELECT * FROM dbo.TBLSiteAddressTypes WHERE SiteAddressTypeID > 0)
	SET IDENTITY_INSERT dbo.TBLSiteAddressTypes ON;
	BEGIN
		INSERT INTO	dbo.TBLSiteAddressTypes(SiteAddressTypeID, Title, DateAdded, AddedBy)
		SELECT	1, 'Main', GetDate(), 'SQLScript'
	END
	SET IDENTITY_INSERT dbo.TBLSiteAddressTypes OFF;
GO

IF NOT EXISTS (SELECT * FROM dbo.TBLCustomerGroups WHERE CustomerGroupID > 0)
	SET IDENTITY_INSERT dbo.TBLCustomerGroups ON;
	BEGIN
		INSERT INTO	dbo.TBLCustomerGroups(CustomerGroupID, Title, CreatedOn, CreatedBy)
		SELECT	1, 'Not Selected', GetDate(), 'SQLScript'
	END
	SET IDENTITY_INSERT dbo.TBLCustomerGroups OFF;
GO

IF NOT EXISTS (SELECT * FROM dbo.TBLContactPositions WHERE PositionID > 0)
	SET IDENTITY_INSERT dbo.TBLContactPositions ON;
	BEGIN
		INSERT INTO	dbo.TBLContactPositions(PositionID, Title, DateAdded, AddedBy)
		SELECT 	1	, 'Managing Director', GetDate(), 'SQLScript' UNION
		SELECT 	2	, 'Financial Director', GetDate(), 'SQLScript' UNION
		SELECT 	3	, 'Chief Exec Officer', GetDate(), 'SQLScript' UNION
		SELECT 	4	, 'Chief Op Officer', GetDate(), 'SQLScript' UNION
		SELECT 	5	, 'Unknown Position', GetDate(), 'SQLScript' UNION
		SELECT 	6	, 'Company Secretary', GetDate(), 'SQLScript' UNION
		SELECT 	7	, 'Director', GetDate(), 'SQLScript' UNION
		SELECT 	8	, 'Secretary', GetDate(), 'SQLScript' UNION
		SELECT 	9	, 'Principal', GetDate(), 'SQLScript' UNION
		SELECT 	10	, 'Senior Partner', GetDate(), 'SQLScript' UNION
		SELECT 	11	, 'Partner', GetDate(), 'SQLScript' UNION
		SELECT 	12	, 'Business Manager', GetDate(), 'SQLScript' UNION
		SELECT 	13	, 'Proprietor', GetDate(), 'SQLScript' UNION
		SELECT 	14	, 'Manager', GetDate(), 'SQLScript' UNION
		SELECT 	15	, 'Head Of Tax Department', GetDate(), 'SQLScript' UNION
		SELECT 	16	, 'Directors P.A', GetDate(), 'SQLScript' UNION
		SELECT 	17	, 'Tax Partner', GetDate(), 'SQLScript' UNION
		SELECT 	18	, 'Office Manager', GetDate(), 'SQLScript' UNION
		SELECT 	20	, 'Managing Partner', GetDate(), 'SQLScript' UNION
		SELECT	30, 'Senior Manager', GetDate(), 'SQLScript' UNION
		SELECT 	465	, 'Manciple', GetDate(), 'SQLScript' UNION
		SELECT 	1307	, 'Directors daughter', GetDate(), 'SQLScript' UNION
		SELECT	1480, 'Clerical Officer', GetDate(), 'SQLScript' UNION
		SELECT	2043, 'Partner', GetDate(), 'SQLScript' UNION
		SELECT	2045, 'Partner', GetDate(), 'SQLScript'
 
	END
	SET IDENTITY_INSERT dbo.TBLContactPositions OFF;
GO

IF NOT EXISTS (SELECT * FROM dbo.TBLContactTypes WHERE ContactTypeID > 0)
	SET IDENTITY_INSERT dbo.TBLContactTypes ON;
	BEGIN
		INSERT INTO	dbo.TBLContactTypes(ContactTypeID, Title, ListOrder, DateAdded, AddedBy)
		SELECT	1	, 'Main Contact', 	1	, GetDate(), 'SQLScript' UNION
		SELECT	2	, 'H&S Contact', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	3	, 'Personnel Contact', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	5	, 'Bottom Line', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	8	, 'ERM Contact', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	9	, 'AuthorisedCaller', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	10	, 'InternalStaff', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	12	, 'Ex Authorised Caller', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	13	, 'CD UK', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	14	, 'Insurance Broker', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	15	, 'Accountant', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	18	, 'Lead', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	19	, 'Primary Lead Contact', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	20	, 'Quality Care', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	21	, 'Personnel Training', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	22	, 'H&S Training', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	23	, 'TEST MAIN', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	24	, 'Accounts', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	25	, 'Magic Personnel', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	26	, 'Magic Health&Safety', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	27	, 'Broker Referral', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	28	, 'Accountant Referral', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	29	, 'Seminar Referral', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	30	, 'Client Referral', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	31	, 'Taxwise', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	32	, 'Training', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	33	, 'Unconfirmed', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	34	, 'Other', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	35	, 'Credit Control', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	36	, 'Monthly advice correspondence', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	37	, 'Correspondence', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	38	, 'General Contact', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	39	, 'Authorised Caller - HS', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	40	, 'Authorised Caller - ES', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	41	, 'TaxWise Admin Contact', 	0	, GetDate(), 'SQLScript' UNION
		SELECT	42	, 'TaxWise Claims Contact', 	0	, GetDate(), 'SQLScript'
	END
	SET IDENTITY_INSERT dbo.TBLContactTypes OFF;
GO

IF NOT EXISTS (SELECT * FROM dbo.TBLTitles)
	BEGIN
		INSERT INTO	dbo.TBLTitles(Title, OrderBy)
				  SELECT	'',	13
			UNION SELECT 'Brother', 17
			UNION SELECT 'CPL', 8
			UNION SELECT 'CPT', 10
			UNION SELECT 'Dr', 5
			UNION SELECT 'Father', 12
			UNION SELECT 'Lady', 9
			UNION SELECT 'LNT', 9
			UNION SELECT 'Miss', 3
			UNION SELECT 'Mother', 16
			UNION SELECT 'Mr', 1
			UNION SELECT 'Mrs', 2
			UNION SELECT 'Ms', 4
			UNION SELECT 'Prof', 6
			UNION SELECT 'Rev', 7
			UNION SELECT 'Revd', 18
			UNION SELECT 'SGT', 11
			UNION SELECT 'Sir', 16
			UNION SELECT 'Sister', 15;
	END
GO

IF NOT EXISTS (SELECT * FROM dbo.[TBLOperaSalesLedgers])
	BEGIN
		INSERT INTO	dbo.[TBLOperaSalesLedgers](OperaSalesLedgerID,Description,CreatedBy,CreatedOn,OperaOutputFolder)
		SELECT 'EIR',	'Irish Ledger','Mike.Mengell',GetDate(),'\\pbsunipaas\apps\platinum\penin\ireland' UNION
		SELECT 'EMP',	'EmployerLine','Chris.Bain',GetDate(),'\\pbsunipaas\apps\platinum\penin\emp' UNION
		SELECT 'JER',	'Jersey Ledger','David.Cordwell',GetDate(),'\\pbsunipaas\apps\platinum\penin\jersey' UNION
		SELECT 'QDOS',	'QDOS Ledger','Mike.Mengell',GetDate(),'\\pbsunipaas\apps\platinum\penin\qdos' UNION
		SELECT 'TAX',	'Taxwise Ledger','Chris.Bain',GetDate(),'\\pbsunipaas\apps\platinum\penin\tax' UNION
		SELECT 'UK',	'UK Ledger','Mike.Mengell',GetDate(),'\\pbsunipaas\apps\platinum\penin\uk'
	END
GO
