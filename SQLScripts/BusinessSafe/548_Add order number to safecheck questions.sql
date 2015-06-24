IF NOT EXISTS ( SELECT  *
                FROM    sys.columns AS c
                WHERE   c.object_id = OBJECT_ID('SafeCheckQuestion')
                        AND c.name = 'OrderNumber' ) 
    BEGIN
        ALTER TABLE dbo.SafeCheckQuestion ADD OrderNumber INT NULL
    END
    
    
IF NOT EXISTS ( SELECT  *
            FROM    sys.columns AS c
            WHERE   c.object_id = OBJECT_ID('SafeCheckCategory')
                    AND c.name = 'OrderNumber' ) 
BEGIN
    ALTER TABLE dbo.SafeCheckCategory ADD OrderNumber INT NULL
END