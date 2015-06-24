
SELECT response.Id,qr.ActionRequired,qr.SupportingEvidence,qr.GuidanceNotes 
FROM dbo.SafeCheckCheckListAnswer AS cla
	INNER JOIN
	(SELECT MIN(CONVERT(VARCHAR(36),qr.Id)) AS Id , QuestionId
	FROM dbo.SafeCheckQuestionResponse AS qr
	GROUP BY questionid
	) response ON cla.questionId = response.questionId
	INNER JOIN dbo.SafeCheckQuestionResponse AS qr ON response.Id = qr.Id
WHERE CheckListId = 'd30ac0ea-3981-c6ab-c3d3-ef570790104c'
AND ResponseId IS NULL


DECLARE @Id UNIQUEIDENTIFIER

SET @Id = 'd30ac0ea-3981-c6ab-c3d3-ef570790104c'

UPDATE dbo.SafeCheckCheckListAnswer
SET ResponseId = response.Id
	,ActionRequired = qr.ActionRequired
	,SupportingEvidence = qr.SupportingEvidence
	,GuidanceNotes = qr.GuidanceNotes
	,EmployeeNotListed = 'test employee'
	,TimescaleId = 3
FROM dbo.SafeCheckCheckListAnswer AS cla
	INNER JOIN
	(SELECT MIN(CONVERT(VARCHAR(36),qr.Id)) AS Id , QuestionId
	FROM dbo.SafeCheckQuestionResponse AS qr
	WHERE qr.Deleted = 0
	GROUP BY questionid
	) response ON cla.questionId = response.questionId
	INNER JOIN dbo.SafeCheckQuestionResponse AS qr ON response.Id = qr.Id
WHERE CheckListId = @Id
AND ResponseId IS NULL

UPDATE dbo.SafeCheckCheckList
SET LastModifiedOn = CURRENT_TIMESTAMP
WHERE Id = @Id




