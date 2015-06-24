USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessment')
BEGIN
	EXEC sp_rename 'RiskAssessment', 'GeneralRiskAssessment'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentHazards')
BEGIN
	EXEC sp_rename 'RiskAssessmentHazards', 'GeneralRiskAssessmentHazard'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentControlMeasures')
BEGIN
	EXEC sp_rename 'RiskAssessmentControlMeasures', 'GeneralRiskAssessmentControlMeasure'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentDocument')
BEGIN
	EXEC sp_rename 'RiskAssessmentDocument', 'GeneralRiskAssessmentDocument'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentEmployees')
BEGIN
	EXEC sp_rename 'RiskAssessmentEmployees', 'GeneralRiskAssessmentEmployee'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk')
BEGIN
	EXEC sp_rename 'RiskAssessmentPeopleAtRisk', 'GeneralRiskAssessmentPeopleAtRisk'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentReview')
BEGIN
	EXEC sp_rename 'RiskAssessmentReview', 'GeneralRiskAssessmentReview'
END

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessmentReview')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessmentReview', 'RiskAssessmentReview'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessmentPeopleAtRisk')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessmentPeopleAtRisk', 'RiskAssessmentPeopleAtRisk'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessmentEmployee')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessmentEmployee', 'RiskAssessmentEmployees'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessmentDocument')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessmentDocument', 'RiskAssessmentDocument'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessmentControlMeasure')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessmentControlMeasure', 'RiskAssessmentControlMeasures'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessmentHazard')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessmentHazard', 'RiskAssessmentHazards'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessment')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessment', 'RiskAssessment'
END