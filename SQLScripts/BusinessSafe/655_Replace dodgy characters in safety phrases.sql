SET XACT_ABORT ON
BEGIN TRANSACTION

UPDATE dbo.SafetyPhrase
SET Title = REPLACE(Title,'–','-')
WHERE title LIKE '%–%'

UPDATE dbo.SafetyPhrase
SET Title = REPLACE(Title,'�','-')
WHERE title LIKE '%�%'

COMMIT TRANSACTION