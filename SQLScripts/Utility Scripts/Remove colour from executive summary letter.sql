DECLARE @checklistId UNIQUEIDENTIFIER

SET @checklistId = 'dc58f062-e3aa-dc5c-2ffc-206c997fe364'

SET XACT_ABORT ON

BEGIN TRANSACTION

UPDATE dbo.SafeCheckCheckList
SET CoveringLetterContent = REPLACE(CoveringLetterContent,'<span style="color: #ff00ff;">','<span style="color: #000000;">')
WHERE Id = @checklistId

UPDATE dbo.SafeCheckCheckList
SET CoveringLetterContent = REPLACE(CoveringLetterContent,'<span style="color: #ff0000;">','<span style="color: #000000;">')
WHERE Id = @checklistId

UPDATE dbo.SafeCheckCheckList
SET CoveringLetterContent = REPLACE(CoveringLetterContent,'<span style="color: #0000ff;">','<span style="color: #000000;">')
WHERE Id = @checklistId

COMMIT TRANSACTION