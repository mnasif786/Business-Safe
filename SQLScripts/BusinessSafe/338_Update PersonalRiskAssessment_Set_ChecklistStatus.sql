use businesssafe
go

if exists (select top 1 id from personalriskassessment where id = 1262 and PersonalRiskAssessementEmployeeChecklistStatusId = 1)
begin
	update personalriskassessment
	set PersonalRiskAssessementEmployeeChecklistStatusId = 2
	where id = 1262
end

--//@UNDO 
use businesssafe
go

if exists (select top 1 id from personalriskassessment where id = 1262 and PersonalRiskAssessementEmployeeChecklistStatusId = 2)
begin
	update personalriskassessment
	set PersonalRiskAssessementEmployeeChecklistStatusId = 1
	where id = 1262
end
