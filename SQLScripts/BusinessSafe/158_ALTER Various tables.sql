USE [BusinessSafe]
GO

DELETE FROM GeneralRiskAssessmentControlMeasure
DELETE FROM GeneralRiskAssessmentHazard
DELETE FROM HazardousSubstanceRiskAssessmentControlMeasure
DELETE FROM RiskAssessmentsNonEmployees
DELETE FROM Task
DELETE FROM GeneralRiskAssessmentPeopleAtRisk

DELETE FROM GeneralRiskAssessmentDocument
DELETE FROM GeneralRiskAssessmentReview
DELETE FROM GeneralRiskAssessmentEmployee

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessmentDocument')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessmentDocument', 'RiskAssessmentDocument'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessmentReview')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessmentReview', 'RiskAssessmentReview'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessmentEmployee')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessmentEmployee', 'RiskAssessmentEmployee'
END

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentDocument')
BEGIN
	EXEC sp_rename 'RiskAssessmentDocument', 'GeneralRiskAssessmentDocument'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentReview')
BEGIN
	EXEC sp_rename 'RiskAssessmentReview', 'GeneralRiskAssessmentReview'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentEmployee')
BEGIN
	EXEC sp_rename 'RiskAssessmentEmployee', 'GeneralRiskAssessmentEmployee'
END
