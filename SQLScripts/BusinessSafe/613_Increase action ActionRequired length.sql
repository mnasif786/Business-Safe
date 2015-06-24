
IF EXISTS (
SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('Action')
AND c.max_length = 500
AND c.name = 'ActionRequired')

BEGIN

ALTER TABLE [Action] ALTER COLUMN ActionRequired NVARCHAR(1000)

END

