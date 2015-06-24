

UPDATE dbo.SafeCheckCheckList
SET Status = 'Assigned'
WHERE QaAdvisor IS NOT NULL
AND [Status] = 'Completed'







