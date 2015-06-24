USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccidentRecordInjury' AND COLUMN_NAME = 'CompanyId')
begin
	alter table AccidentRecordInjury add CompanyId bigint null
end

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccidentRecordInjury' AND COLUMN_NAME = 'AccidentRecordInjury')
begin
	alter table AccidentRecordInjury add AccidentRecordInjuryId bigint null
end

go

--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccidentRecordInjury' AND COLUMN_NAME = 'CompanyId')
begin
	alter table AccidentRecordInjury drop column CompanyId
end

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccidentRecordInjury' AND COLUMN_NAME = 'AccidentRecordId')
begin
	alter table AccidentRecordInjury drop column AccidentRecordInjuryId
end
go
