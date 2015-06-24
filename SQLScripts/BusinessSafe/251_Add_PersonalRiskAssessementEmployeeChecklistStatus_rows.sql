PRINT 'Inserting PersonalRiskAssessementEmployeeChecklistStatus rows'

INSERT INTO dbo.PersonalRiskAssessementEmployeeChecklistStatus
        ( Id, Name )
SELECT a.*
FROM (SELECT 0 AS id, 'Not set' AS [Name]
	UNION SELECT 1, 'Generating'
	UNION SELECT 2, 'Generated') a
	LEFT JOIN dbo.PersonalRiskAssessementEmployeeChecklistStatus AS praecs ON a.id = praecs.Id
WHERE praecs.Id IS NULL


--//@UNDO 
--Nothing to undo. Would cause more issues attempting to rollback