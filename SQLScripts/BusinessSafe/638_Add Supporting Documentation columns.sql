
IF NOT EXISTS (SELECT * FROM sys.columns AS c 
WHERE c.object_id = OBJECT_id('SafecheckChecklistAnswer')
AND c.name = 'SupportingDocumentationStatus'
)
BEGIN
	ALTER TABLE dbo.SafeCheckCheckListAnswer ADD SupportingDocumentationStatus VARCHAR(25)
	ALTER TABLE dbo.SafeCheckCheckListAnswer ADD SupportingDocumentationDate DATETIME
END


