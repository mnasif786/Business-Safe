IF NOT EXISTS ( SELECT  *
            FROM    sys.columns AS c
            WHERE   c.object_id = OBJECT_ID('SafeCheckChecklist')
                    AND c.name = 'VisitDate')
    BEGIN
        ALTER TABLE dbo.SafeCheckChecklist ADD VisitDate DATETIME
    END

IF NOT EXISTS ( SELECT  *
            FROM    sys.columns AS c
            WHERE   c.object_id = OBJECT_ID('SafeCheckChecklist')
                    AND c.name = 'VisitBy')
    BEGIN
        ALTER TABLE dbo.SafeCheckChecklist ADD VisitBy VARCHAR(100)
    END

IF NOT EXISTS ( SELECT  *
            FROM    sys.columns AS c
            WHERE   c.object_id = OBJECT_ID('SafeCheckChecklist')
                    AND c.name = 'VisitType')
    BEGIN
        ALTER TABLE dbo.SafeCheckChecklist ADD VisitType VARCHAR(50)
    END
   
