----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the ResponsibilityTaskCategory table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessment')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessment', 'PREVIOUS_GeneralRiskAssessment'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment')
BEGIN
	EXEC sp_rename 'HazardousSubstanceRiskAssessment', 'PREVIOUS_HazardousSubstanceRiskAssessment'
END

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_HazardousSubstanceRiskAssessment')
BEGIN
	EXEC sp_rename 'PREVIOUS_HazardousSubstanceRiskAssessment', 'HazardousSubstanceRiskAssessment'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_GeneralRiskAssessment')
BEGIN
	EXEC sp_rename 'PREVIOUS_GeneralRiskAssessment', 'GeneralRiskAssessment'
END