USE [BusinessSafe]
GO
if not exists
(
	select top 1 EmployeeId ,count(EmployeeId) 
	from [user]
	group by EmployeeId
	having count(*) > 1
	order by count(*) desc
 )
begin
	alter table [user]
	add constraint UC_EmployeeId unique (employeeid)
end

--//@UNDO 

alter table [user] drop constraint UC_EmployeeId