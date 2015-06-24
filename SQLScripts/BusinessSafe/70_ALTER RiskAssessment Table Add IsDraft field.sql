----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the DocumentType table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessment' AND COLUMN_NAME = 'IsDraft')
BEGIN
	ALTER TABLE [RiskAssessment]
	ADD [IsDraft] [bit] NULL	
END
GO	


--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessment' AND COLUMN_NAME = 'IsDraft')
BEGIN
	ALTER TABLE [RiskAssessment]
	DROP COLUMN [IsDraft]
END
GO
