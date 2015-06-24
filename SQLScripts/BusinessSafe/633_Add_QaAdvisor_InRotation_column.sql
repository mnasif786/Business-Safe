
IF NOT EXISTS ( SELECT  *
                FROM    sys.columns AS c
                WHERE   c.object_id = OBJECT_ID('SafeCheckQaAdvisor')
                        AND c.name = 'InRotation' ) 
    BEGIN
        ALTER TABLE dbo.SafeCheckQaAdvisor ADD InRotation BIT DEFAULT(0)
    END

GO

UPDATE  SafeCheckQaAdvisor
SET     InRotation = 1
WHERE   InRotation IS NULL



