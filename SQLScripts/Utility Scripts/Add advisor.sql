
USE BusinessSafe
SELECT * FROM dbo.SafeCheckQaAdvisor AS qa


INSERT INTO dbo.SafeCheckQaAdvisor
        ( Id
        ,Forename
        ,Surname
        ,Email
        ,Deleted
        ,InRotation
        )
VALUES  ( NEWID()
        , -- Id - uniqueidentifier
         'Alastair'
        , -- Forename - varchar(100)
         'Polden'
        , -- Surname - varchar(100)
         'alastair.polden@peninsula-uk.com'
        , -- Email - varchar(100)
         0
        , -- Deleted - bit
         0  -- InRotation - bit
        )