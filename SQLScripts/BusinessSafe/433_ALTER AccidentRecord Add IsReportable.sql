USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccidentRecord' AND COLUMN_NAME = 'IsReportable')
begin
	alter table AccidentRecord add IsReportable bit NOT NULL DEFAULT(0)
end
GO
	

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccidentRecord' AND COLUMN_NAME = 'IsReportable')
begin
	alter table AccidentRecord drop column IsReportable
end
go
