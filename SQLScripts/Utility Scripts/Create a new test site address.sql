--demo001
USE Peninsula_BusinessSafe


DECLARE @CustomerId int 
DECLARE @SiteName VARCHAR(100) 
SET @CustomerId  = (SELECT CustomerID FROM tblcustomers WHERE CustomerKey = 'Demo001')
SET @SiteName = 'Test ' + CONVERT(VARCHAR(max),GETDATE())


SET XACT_ABORT ON

BEGIN TRANSACTION

INSERT INTO dbo.TBLSiteAddresses
        ( SiteAddressTypeID
        ,CompanyName
        ,Address1
        ,Postcode
        ,flagHidden
        ,CustomerID
        ,LegalLocationID
        ,LockCurrentHSConsultant
        )
VALUES (9,@SiteName,@SiteName, 'TE10 7TE',0,@CustomerId,0,0)


DECLARE @SiteId INT
SET @SiteId = SCOPE_IDENTITY()

INSERT INTO dbo.TBLSiteAddressSiteAddressTypes
        ( SiteAddressID
        ,SiteAddressTypeID
        )
VALUES  ( @SiteId,9)
     
COMMIT TRAN

