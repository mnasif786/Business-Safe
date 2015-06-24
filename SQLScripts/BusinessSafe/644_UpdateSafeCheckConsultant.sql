IF NOT EXISTS ( SELECT  *
                FROM    sys.columns AS c
                WHERE   c.object_id = OBJECT_ID('SafeCheckConsultant')
                        AND c.name = 'QaAdvisorAssigned' ) 
    BEGIN
        ALTER TABLE dbo.SafeCheckConsultant ADD QaAdvisorAssigned uniqueidentifier DEFAULT null
    END
GO




