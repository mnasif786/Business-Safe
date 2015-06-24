USE BusinessSafe

IF EXISTS ( SELECT  *
            FROM    sys.columns AS c
            WHERE   c.object_id = OBJECT_ID('MultiHazardRiskAssessmentHazard')
                    AND c.name = 'Description'
                    AND c.max_length < 600 ) 
    BEGIN

        ALTER TABLE dbo.MultiHazardRiskAssessmentHazard ALTER COLUMN [Description] NVARCHAR(300)

    END