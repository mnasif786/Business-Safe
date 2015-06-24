USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'BusinessSafeCompanyDetail' AND COLUMN_NAME = 'CompanyId')
BEGIN
	alter table BusinessSafeCompanyDetail
	add CompanyId bigint not null
END

--//@UNDO 
USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'BusinessSafeCompanyDetail' AND COLUMN_NAME = 'CompanyId')
BEGIN
	alter table BusinessSafeCompanyDetail
	drop column CompanyId 
END
