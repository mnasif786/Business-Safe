IF NOT EXISTS(SELECT * FROM sys.objects AS o WHERE o.name = 'SafeCheckChecklistUpdatesRequired')
BEGIN

CREATE TABLE SafeCheckChecklistUpdatesRequired
    (
		Id [bigint] IDENTITY(1,1) NOT NULL
		,ChecklistId UNIQUEIDENTIFIER NOT NULL
		,QaAdvisorId UNIQUEIDENTIFIER NOT NULL
		,UpdatesRequiredOn DATETIME NOT NULL
		,CONSTRAINT PK_SafeCheckChecklistUpdatesRequired PRIMARY KEY ( Id )
    )
    
END 
GO

GRANT SELECT,UPDATE,INSERT ON SafeCheckChecklistUpdatesRequired TO AllowAll
GRANT SELECT,UPDATE,INSERT ON SafeCheckChecklistUpdatesRequired TO AllowSelectInsertUpdate

