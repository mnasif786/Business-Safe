
SET XACT_ABORT ON

BEGIN TRANSACTION

UPDATE SafeCheckQuestionResponse
SET Title = 'Unacceptable'
	,ResponseType = 'Negative'
WHERE id IN ('CC9151BC-395C-4CAC-9FA2-E3A4876B0620')
AND Title <> 'Unacceptable'


UPDATE SafeCheckQuestionResponse
SET Title = 'Acceptable'
	,ResponseType = 'Positive'
WHERE id IN ('3A7A20A6-F1F3-4851-8157-F1F59F2E1DDA')

UPDATE dbo.SafeCheckQuestion
SET Mandatory = 0
	,Deleted = 1
WHERE id ='ADE2C5CD-BE9E-42BE-89C4-3EBB94CAD035'
AND Mandatory = 1
AND Deleted = 0

UPDATE dbo.SafeCheckCheckListAnswer
SET Deleted = 1
WHERE QuestionId IN ('ADE2C5CD-BE9E-42BE-89C4-3EBB94CAD035')
AND Deleted = 0

UPDATE dbo.SafeCheckCheckListQuestion
SET Deleted = 1
WHERE QuestionId IN ('ADE2C5CD-BE9E-42BE-89C4-3EBB94CAD035')
AND Deleted = 0

DELETE FROM dbo.SafeCheckIndustryQuestion
WHERE QuestionId IN ('ADE2C5CD-BE9E-42BE-89C4-3EBB94CAD035')


COMMIT TRANSACTION



/*
SELECT  qr.id,qr.Title,qr.ResponseType,q.Title ,* FROM dbo.SafeCheckQuestion AS q
	INNER JOIN dbo.SafeCheckQuestionResponse AS qr ON q.Id = qr.QuestionId
WHERE q.id IN ('D8705F59-AD79-456D-8847-10667C95B205','ADE2C5CD-BE9E-42BE-89C4-3EBB94CAD035','691C9048-414F-4BFE-BDE0-3E39A430B53A') 


SELECT qr.id,qr.Title,qr.ResponseType,q.Title ,cla.CheckListId,q.Id,cla.Deleted
FROM dbo.SafeCheckCheckListAnswer AS cla
	INNER JOIN dbo.SafeCheckQuestionResponse AS qr ON cla.ResponseId = qr.Id
	INNER JOIN dbo.SafeCheckQuestion AS q ON cla.QuestionId = q.Id
WHERE cla.QuestionId in ('D8705F59-AD79-456D-8847-10667C95B205','ADE2C5CD-BE9E-42BE-89C4-3EBB94CAD035','691C9048-414F-4BFE-BDE0-3E39A430B53A') 

*/