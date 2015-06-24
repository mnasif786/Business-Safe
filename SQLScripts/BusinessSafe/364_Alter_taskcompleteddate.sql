
IF NOT EXISTS ( SELECT  *
                FROM    sys.columns AS c
                WHERE   c.object_id = OBJECT_ID('Task')
                        AND c.name = 'TaskCompletedDate'
                        AND c.system_type_id = 43 ) 
    BEGIN

        ALTER TABLE dbo.Task ALTER COLUMN TaskCompletedDate DATETIMEOFFSET

        UPDATE  dbo.Task
        SET     TaskCompletedDate = TODATETIMEOFFSET(TaskCompletedDate, '+01:00')
        WHERE TaskCompletedDate IS NOT NULL
    END

