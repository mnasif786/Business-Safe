
IF NOT EXISTS (SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('SafeCheckChecklist')
AND c.name = 'QAComments')
BEGIN
	
	ALTER TABLE dbo.SafeCheckCheckList ADD QAComments NVARCHAR(4000) NULL

END


