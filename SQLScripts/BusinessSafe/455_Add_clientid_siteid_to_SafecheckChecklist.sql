IF NOT EXISTS ( SELECT  *
                FROM    sys.columns AS c
                WHERE   c.object_id = OBJECT_ID('SafecheckChecklist')
                        AND c.name = 'ClientId' ) 
    BEGIN
        ALTER TABLE dbo.SafeCheckCheckList ADD ClientId INT NULL
    END

IF NOT EXISTS ( SELECT  *
                FROM    sys.columns AS c
                WHERE   c.object_id = OBJECT_ID('SafecheckChecklist')
                        AND c.name = 'SiteId' ) 
    BEGIN
        ALTER TABLE dbo.SafeCheckCheckList ADD SiteId INT NULL
    END




IF EXISTS ( SELECT  *
                FROM    sys.columns AS c
                WHERE   c.object_id = OBJECT_ID('SafecheckChecklist')
                        AND c.name = 'CompanyName' ) 
    BEGIN
        ALTER TABLE dbo.SafeCheckCheckList ALTER COLUMN CompanyName VARCHAR(3000) NULL
    END