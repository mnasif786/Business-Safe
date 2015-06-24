use businesssafe
go
if exists(select top 1 id from injury where description = 'Other known injuries' and deleted = 0)
begin
	update injury
	set deleted = 1
	where description = 'Other known injuries'
	and deleted = 0
end

if exists(select top 1 id from injury where description = 'Multiple injuries' and deleted = 0)
begin
	update injury
	set deleted = 1
	where description = 'Multiple injuries'
	and deleted = 0
end
go

--//@UNDO 

use businesssafe
go
if exists(select top 1 id from injury where description = 'Other known injuries' and deleted = 1)
begin
	update injury
	set deleted = 0
	where description = 'Other known injuries'
	and deleted = 1
end

if exists(select top 1 id from injury where description = 'Multiple injuries' and deleted = 1)
begin
	update injury
	set deleted = 0
	where description = 'Multiple injuries'
	and deleted = 1 
end
go

