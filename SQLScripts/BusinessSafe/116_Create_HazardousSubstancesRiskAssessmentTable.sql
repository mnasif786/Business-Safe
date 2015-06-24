----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the ResponsibilityTaskCategory table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'HazardousSubstanceRiskAssessment' AND TYPE = 'U')
BEGIN
	CREATE TABLE	[HazardousSubstanceRiskAssessment]
	(
		Id bigint IDENTITY(1,1) NOT NULL
		,Title varchar(200) NOT NULL 		
		,Reference varchar(50) NOT NULL
		,AssessmentDate datetime NULL DEFAULT GetDate()
		,ClientId bigint NOT NULL
		,Deleted bit NOT NULL DEFAULT 0
		,CreatedOn datetime NOT NULL DEFAULT GetDate()	
		,CreatedBy uniqueidentifier NULL
		,LastModifiedOn datetime NULL DEFAULT GetDate()
		,LastModifiedBy uniqueidentifier NULL
		,RiskAssessorEmployeeId uniqueidentifier NULL
		
	)
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [HazardousSubstanceRiskAssessment] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'HazardousSubstanceRiskAssessment' AND TYPE = 'U')
BEGIN
	DROP TABLE [HazardousSubstanceRiskAssessment]
END
