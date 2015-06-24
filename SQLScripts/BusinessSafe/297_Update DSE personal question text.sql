UPDATE dbo.Question
SET Text = 'Have you ever suffered from work related aches or pains in your ' + LOWER(q.text) + '?' 
FROM dbo.Question AS q
	INNER JOIN dbo.Section AS s ON q.SectionId = s.Id
WHERE q.Text in ('Wrists','Forearms','Neck','Eyes','Back')
AND s.ChecklistId =1

UPDATE dbo.Section 
SET Title = 'Personal'
WHERE Title = 'Personal - Have you ever suffered from work related aches or pains in your:'