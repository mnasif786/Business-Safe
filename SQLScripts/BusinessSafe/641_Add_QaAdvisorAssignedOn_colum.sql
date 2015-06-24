USE BusinessSafe
GO
IF NOT EXISTS ( SELECT  * FROM    sys.columns AS c WHERE   c.object_id = OBJECT_ID('SafeCheckChecklist') AND c.name = 'QaAdvisorAssignedOn' ) 
BEGIN
    ALTER TABLE dbo.SafeCheckChecklist ADD QaAdvisorAssignedOn DATETIME NULL
     
END
GO

UPDATE dbo.SafeCheckCheckList SET QaAdvisorAssignedOn = ChecklistCompletedOn where Status = 'Assigned' and QaAdvisorAssignedOn is null

--//@UNDO
IF EXISTS ( SELECT  * FROM    sys.columns AS c WHERE   c.object_id = OBJECT_ID('SafeCheckChecklist') AND c.name = 'QaAdvisorAssignedOn' ) 
BEGIN
    ALTER TABLE dbo.SafeCheckChecklist DROP COLUMN QaAdvisorAssignedOn
END
GO



