use businesssafe
go

if exists (select top 1 id from Responsibilitycategory where id = 4)
begin
	update Responsibilitycategory
	set Category = 'Management of Health & Safety'
	where id =  4
end

--//@UNDO 
use businesssafe
go

if exists (select top 1 id from Responsibilitycategory where id = 4)
begin
	update Responsibilitycategory
	set Category = 'Management of Health and Safety'
	where id =  4
end
