USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccidentRecord' AND COLUMN_NAME = 'JusridictionId' AND DATA_TYPE = 'bigint')
begin
	alter table AccidentRecord drop column JusridictionId
	alter table AccidentRecord add JurisdictionId bigint null
end

go

--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccidentRecord' AND COLUMN_NAME = 'JurisdictionId' AND DATA_TYPE = 'bigint')
begin
	alter table AccidentRecord drop column JurisdictionId
	alter table AccidentRecord add JusridictionId bigint null
end
go
