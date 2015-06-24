USE [BusinessSafe]
GO 

-- VisitBy field has not been completed. This takes the value from the related checklist.
-- In future, this will be set on creation
begin transaction

update ap 
set ap.VisitBy = cl.ChecklistCompletedBy
FROM [dbo].[SafeCheckCheckList] as cl
JOIN 
[dbo].[ActionPlan] as  ap
on cl.ActionPlanId = ap.Id

commit transaction