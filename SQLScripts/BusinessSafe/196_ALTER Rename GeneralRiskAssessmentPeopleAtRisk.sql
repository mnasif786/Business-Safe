USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessmentPeopleAtRisk')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessmentPeopleAtRisk', 'RiskAssessmentPeopleAtRisk'
END

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk')
BEGIN
	EXEC sp_rename 'RiskAssessmentPeopleAtRisk', 'GeneralRiskAssessmentPeopleAtRisk'
END