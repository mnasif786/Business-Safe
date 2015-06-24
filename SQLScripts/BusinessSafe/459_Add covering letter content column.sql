IF NOT EXISTS(SELECT * FROM sys.columns AS c
WHERE c.object_id =OBJECT_ID('SafeCheckCheckList')
AND c.name = 'CoveringLetterContent')
BEGIN

ALTER TABLE dbo.SafeCheckCheckList ADD CoveringLetterContent VARCHAR(max)

END


