USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccidentRecord' AND COLUMN_NAME = 'CompanyId')
begin
	alter table AccidentRecord add CompanyId bigint null
end

go

--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccidentRecord' AND COLUMN_NAME = 'CompanyId')
begin
	alter table AccidentRecord drop column CompanyId
end
go
