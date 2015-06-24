use businesssafe
go
update StatutoryResponsibilityTemplate
set title = ltrim(rtrim(title)), description=ltrim(rtrim(description))

update StatutoryResponsibilityTaskTemplate
set title = ltrim(rtrim(title)), description=ltrim(rtrim(description))
go

if exists (select top 1 id from StatutoryResponsibilityTemplate where id = 5)
begin
	update StatutoryResponsibilityTemplate 
		set description = N'Check that completed Personal Emergency Evacuation Plans are in place and up to date for all staff and residents that need them and that they are known to staff who may have to put them into practice.'
	where id = 5
end

if exists (select top 1 id from StatutoryResponsibilityTaskTemplate where id = 8)
begin	
	update StatutoryResponsibilityTaskTemplate 
		set description = N'Check that completed Personal Emergency Evacuation Plans are in place and up to date for all staff and residents that need them and that they are known to staff who may have to put them into practice.'
	where id  = 8
end	
go

if exists (select top 1 id from StatutoryResponsibilityTemplate where id = 13)
begin
	update StatutoryResponsibilityTemplate 
		set description = N'Check licences for endorsements and private vehicles for current insurance and MoT (NCT) certificates.'
	where id  = 13
end

if exists (select top 1 id from StatutoryResponsibilityTaskTemplate where id = 16)
begin		
	update StatutoryResponsibilityTaskTemplate 
		set description = N'Check licences for endorsements and private vehicles for current insurance and MoT (NCT) certificates.'
	where id  = 16
end	
go

if exists (select top 1 id from StatutoryResponsibilityTemplate where id = 15)
begin
	update StatutoryResponsibilityTemplate 
		set title = N'Annual health and safety review'
	where id  = 15
end
	
if exists (select top 1 id from StatutoryResponsibilityTaskTemplate where id = 18)
begin		
	update StatutoryResponsibilityTaskTemplate 
		set title = N'Annual health and safety review'
	where id  = 18
end	
go

if exists (select top 1 id from StatutoryResponsibilityTemplate where id = 17)
begin
	delete from StatutoryResponsibilityTemplate where id = 17
end

if exists (select top 1 id from StatutoryResponsibilityTaskTemplate where id = 20)
begin	
	delete from StatutoryResponsibilityTaskTemplate where id = 20
end	
go

if exists (select top 1 id from StatutoryResponsibilityTemplate where id = 20)
begin
	update StatutoryResponsibilityTemplate 
		set description = N'Check that the annual oil fired equipment safety checks have been carried out by an OFTEC engineer and the equipment is safe to use.'
	where id  = 20
end	
	
if exists (select top 1 id from StatutoryResponsibilityTaskTemplate where id = 23)
begin		
	update StatutoryResponsibilityTaskTemplate 
		set description = N'Check that the annual oil fired equipment safety checks have been carried out by an OFTEC engineer and the equipment is safe to use.'
	where id  = 23
end	
go

if exists (select top 1 id from StatutoryResponsibilityTemplate where id = 27)
begin
	update StatutoryResponsibilityTemplate 
		set description = N'Statutory examinations carried out and equipment remains fit for use.'
	where id  = 22
end	
go

if exists (select top 1 id from StatutoryResponsibilityTemplate where id = 24)
begin
	update StatutoryResponsibilityTemplate 
		set description = N'Check that statutory examination and test of equipemnt that operates under pressure including boilers, compressed air systems, autoclaves, etc are tested in accordance with a written scheme of examination. Typical intervals are 12 months for steam equipment and 24 months for compressed air systems.'
	where id  = 24
end

if exists (select top 1 id from StatutoryResponsibilityTaskTemplate where id = 28)
begin		
	update StatutoryResponsibilityTaskTemplate 
		set description = N'Check that statutory examination and test of equipemnt that operates under pressure including boilers, compressed air systems, autoclaves, etc are tested in accordance with a written scheme of examination. Typical intervals are 12 months for steam equipment and 24 months for compressed air systems.'
	where id  = 28
end	
go

if exists (select top 1 id from StatutoryResponsibilityTemplate where id = 33)
begin
	update StatutoryResponsibilityTemplate 
		set description = N'Servicing and maintainance of forklift trucks. Inspection of lifting chains to statutory requirements.'
	where id  = 33
end	

if exists (select top 1 id from StatutoryResponsibilityTaskTemplate where id = 37)
begin		
	update StatutoryResponsibilityTaskTemplate 
		set description = N'Servicing and maintainance of forklift trucks. Inspection of lifting chains to statutory requirements.'
	where id  = 37
end	
go

if exists (select top 1 id from StatutoryResponsibilityTemplate where id = 36)
begin
	update StatutoryResponsibilityTemplate 
		set description = N'Statutory inspections completed and machine fit for use.'
	where id  = 36
end	
go

if exists (select top 1 id from StatutoryResponsibilityTemplate where id = 47)
begin
	update StatutoryResponsibilityTemplate 
		set description = N'Drivers licences checked for convictions (in regard of insurance) and for CPD in respect of LGV and PSV drivers.'
	where id  = 47
end	

if exists (select top 1 id from StatutoryResponsibilityTaskTemplate where id = 52)
begin		
	update StatutoryResponsibilityTaskTemplate 
		set description = N'Drivers licences checked for convictions (in regard of insurance) and for CPD in respect of LGV and PSV drivers.'
	where id  = 52
end	
go
