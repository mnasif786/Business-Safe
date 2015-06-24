SET XACT_ABORT ON
BEGIN TRANSACTION

UPDATE dbo.SafetyPhrase
SET Title = REPLACE(Title,'â€“','-')
WHERE title LIKE '%â€“%'

UPDATE dbo.SafetyPhrase
SET Title = REPLACE(Title,'–','-')
WHERE title LIKE '%–%'

COMMIT TRANSACTION