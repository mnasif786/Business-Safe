IF EXISTS ( SELECT  *
            FROM    sys.columns AS c
            WHERE   c.object_id = OBJECT_ID('SafeCheckCategory')
                    AND c.name = 'Title'
                    AND system_type_id = 35 ) -- text
    BEGIN
	
        ALTER TABLE dbo.SafeCheckCategory ALTER COLUMN Title VARCHAR(250)
    END


IF EXISTS ( SELECT  *
            FROM    sys.columns AS c
            WHERE   c.object_id = OBJECT_ID('SafeCheckCategory')
                    AND c.name = 'ReportTitle'
                    AND system_type_id = 35 ) -- text
    BEGIN
        ALTER TABLE dbo.SafeCheckCategory ALTER COLUMN ReportTitle VARCHAR(250)
    END
    

IF EXISTS ( SELECT  *
            FROM    sys.columns AS c
            WHERE   c.object_id = OBJECT_ID('SafeCheckCheckListAnswer')
                    AND c.name = 'Comment'
                    AND system_type_id = 35 ) -- text
    BEGIN
        ALTER TABLE dbo.SafeCheckCheckListAnswer ALTER COLUMN Comment VARCHAR(max)
    END

IF EXISTS ( SELECT  *
            FROM    sys.columns AS c
            WHERE   c.object_id = OBJECT_ID('SafeCheckClientType')
                    AND c.name = 'Name'
                    AND system_type_id = 35 ) -- text
    BEGIN
        ALTER TABLE dbo.SafeCheckClientType ALTER COLUMN Name VARCHAR(250)
    END
    
IF EXISTS ( SELECT  *
        FROM    sys.columns AS c
        WHERE   c.object_id = OBJECT_ID('SafeCheckSector')
                AND c.name = 'Name'
                AND system_type_id = 35 ) -- text
BEGIN
    ALTER TABLE dbo.SafeCheckSector ALTER COLUMN Name VARCHAR(250)
END

