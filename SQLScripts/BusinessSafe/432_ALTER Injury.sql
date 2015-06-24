USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Injury' AND COLUMN_NAME = 'CompanyId')
begin
	alter table Injury add CompanyId bigint null
end

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Injury' AND COLUMN_NAME = 'AccidentRecordId')
begin
	alter table Injury add AccidentRecordId bigint null
end

go

--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Injury' AND COLUMN_NAME = 'CompanyId')
begin
	alter table Injury drop column CompanyId
end

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Injury' AND COLUMN_NAME = 'AccidentRecordId')
begin
	alter table Injury drop column AccidentRecordId
end
go
