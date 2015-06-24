USE BusinessSafe


DECLARE @Dups TABLE (
	QuestionId UNIQUEIDENTIFIER
	,Title VARCHAR(1000)
	,RowNumber INT
	)

INSERT INTO @Dups
        ( QuestionId, Title, RowNumber )
SELECT q.Id, q.Title, ROW_NUMBER() OVER(PARTITION BY q.Title  ORDER BY qr.QuestionId desc, q.Id)
FROM dbo.SafeCheckQuestion AS q
	LEFT JOIN (SELECT DISTINCT QuestionId FROM dbo.SafeCheckQuestionResponse) AS qr ON q.Id = qr.QuestionId
WHERE q.Title IN (SELECT Title
FROM dbo.SafeCheckQuestion AS q
GROUP BY q.Title
HAVING COUNT(*) > 1)


DECLARE @DuplicateResolver TABLE( QuestionIdToKeep UNIQUEIDENTIFIER, QuestionIdToRemove UNIQUEIDENTIFIER)


INSERT INTO @DuplicateResolver
        ( QuestionIdToKeep
        ,QuestionIdToRemove
        )
SELECT tokeep.QuestionId, toDelete.QuestionId
FROM
(SELECT * FROM @dups 
WHERE rownumber = 1) toKeep
	INNER JOIN 
(
SELECT * FROM @dups 
WHERE rownumber > 1) toDelete ON toKeep.Title = toDelete.Title


UPDATE dbo.SafeCheckCheckListQuestion
SET Deleted = 1
FROM dbo.SafeCheckCheckListQuestion AS clq
	INNER JOIN @DuplicateResolver dups ON clq.QuestionId = dups.QuestionIdToRemove
WHERE clq.Deleted = 0
	
DELETE FROM SafeCheckCheckListAnswer
FROM dbo.SafeCheckCheckListAnswer AS clq
	INNER JOIN @DuplicateResolver dups ON clq.QuestionId = dups.QuestionIdToRemove

	
UPDATE dbo.SafeCheckQuestion
SET Deleted = 1
FROM dbo.SafeCheckQuestion AS q
	INNER JOIN @DuplicateResolver dups ON q.Id = dups.QuestionIdToRemove
WHERE q.Deleted = 0
	
		


	