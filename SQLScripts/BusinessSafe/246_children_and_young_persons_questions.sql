
UPDATE dbo.Question
SET ListOrder = 6
FROM dbo.Question AS q
	INNER JOIN dbo.Section AS s ON q.SectionId = s.Id
	INNER JOIN dbo.Checklist AS c ON s.ChecklistId = c.Id
WHERE c.Title = 'Children and Young Persons'
AND s.title = 'Environmental factors - does the work involve:'
AND q.Text = 'Work at height?'
AND q.ListOrder = 10

UPDATE dbo.Question
SET ListOrder = 10
FROM dbo.Question AS q
	INNER JOIN dbo.Section AS s ON q.SectionId = s.Id
	INNER JOIN dbo.Checklist AS c ON s.ChecklistId = c.Id
WHERE c.Title = 'Children and Young Persons'
AND s.title = 'Environmental factors - does the work involve:'
AND q.Text = 'Work with ionising or non-ionising radiation?'
AND q.ListOrder = 9