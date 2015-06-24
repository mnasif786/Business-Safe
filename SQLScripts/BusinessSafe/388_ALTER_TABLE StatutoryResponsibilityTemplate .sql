use businesssafe
go
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'StatutoryResponsibilityTemplate' AND COLUMN_NAME = 'SectorId')
BEGIN
    ALTER TABLE StatutoryResponsibilityTemplate
    DROP COLUMN SectorId 
END
GO

--//@UNDO 

IF  NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'StatutoryResponsibilityTemplate' AND COLUMN_NAME = 'SectorId')
BEGIN
    ALTER TABLE StatutoryResponsibilityTemplate
    ADD SectorId [bigint] NULL
END
GO
