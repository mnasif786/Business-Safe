

UPDATE dbo.Question
SET Text = 'Require unusual capability?'
FROM dbo.Question AS q
	INNER JOIN dbo.Section AS s ON q.SectionId = s.Id
	INNER JOIN dbo.Checklist AS c ON s.ChecklistId = c.Id
WHERE c.Title ='Manual Handling'
AND s.title = 'The Individual''s capability -  does the job:'
AND q.Text = 'Require unusual capability??'

DELETE FROM dbo.Question
FROM dbo.Question AS q
	INNER JOIN dbo.Section AS s ON q.SectionId = s.Id
	INNER JOIN dbo.Checklist AS c ON s.ChecklistId = c.Id
WHERE c.Title ='Manual Handling'
AND s.title = 'The Individual''s capability -  does the job:'
AND q.Text = 'Endanger those who are pregnant?'
AND q.ListOrder = 5

UPDATE dbo.Question
SET ListOrder = 0
FROM dbo.Question AS q
	INNER JOIN dbo.Section AS s ON q.SectionId = s.Id
	INNER JOIN dbo.Checklist AS c ON s.ChecklistId = c.Id
WHERE c.Title ='Manual Handling'
AND s.title = 'The Loads - are they:'
AND q.Text = 'Heavy?  What is the weight and frequency?'
AND q.ListOrder = 6