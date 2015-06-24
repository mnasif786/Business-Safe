IF NOT EXISTS ( SELECT  *
                FROM    sys.columns AS c
                WHERE   c.object_id = OBJECT_ID('SafecheckChecklistAnswer')
                        AND c.name = 'ResponseId' ) 
    BEGIN
        ALTER TABLE dbo.SafecheckChecklistAnswer ADD ResponseId UNIQUEIDENTIFIER NULL	REFERENCES dbo.SafeCheckQuestionResponse (Id)
    END
