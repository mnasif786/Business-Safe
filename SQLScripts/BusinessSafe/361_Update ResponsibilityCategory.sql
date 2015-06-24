use businesssafe
go

if exists (select top 1 id from Responsibilitycategory where id <> 11 and sequence is null)
begin
	update Responsibilitycategory 
		set sequence = 0 
		where id <> 11
end		

if exists (select top 1 id from Responsibilitycategory where id = 11 and sequence is null)
begin
	update Responsibilitycategory 
		set sequence = 99 
		where id = 11
end		

--//@UNDO 
use businesssafe
go

if exists (select top 1 id from Responsibilitycategory where id <> 11 and sequence = 0)
begin
	update Responsibilitycategory 
		set sequence = null
		where id <> 11
end		

if exists (select top 1 id from Responsibilitycategory where id = 11 and sequence = 99)
begin
	update Responsibilitycategory 
		set sequence = null 
		where id = 11
end	