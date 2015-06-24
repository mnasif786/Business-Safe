USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccidentRecordInjury' AND COLUMN_NAME = 'CompanyId')
begin
	alter table AccidentRecordInjury drop column CompanyId
end

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccidentRecordInjury' AND COLUMN_NAME = 'AccidentRecordInjuryId')
begin
	alter table AccidentRecordInjury drop column AccidentRecordInjuryId
end
go
