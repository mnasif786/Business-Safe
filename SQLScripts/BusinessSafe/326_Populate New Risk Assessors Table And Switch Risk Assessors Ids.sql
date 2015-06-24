

-- Add Risk Assessor Id field
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessment' AND COLUMN_NAME = 'RiskAssessorId')
BEGIN
	ALTER TABLE [RiskAssessment]
	ADD [RiskAssessorId] BIGINT NULL
END
GO

-- Delete risk assessors
DELETE FROM [BusinessSafe].[dbo].[RiskAssessor]

-- Insert into risk assessors
INSERT INTO [BusinessSafe].[dbo].[RiskAssessor]
           ([EmployeeId]
           ,[HasAccessToAllSites]
           ,[SiteId]
           ,[DoNotSendTaskOverdueNotifications]
           ,[DoNotSendTaskCompletedNotifications]
           ,[DoNotSendReviewDueNotification]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[LastModifiedBy]
           ,[LastModifiedOn]
           ,[Deleted])
SELECT 
			DISTINCT([RiskAssessorEmployeeId]), 
			1,
			null,
			0,
			0,
			0,
			'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',
			GetDate(),
			null,
			null,
			0      
			FROM [BusinessSafe].[dbo].[RiskAssessment]
			WHERE RiskAssessorEmployeeId is not null
GO

-- Update RiskAssessorId in RiskAssessment table
UPDATE [BusinessSafe].[dbo].[RiskAssessment]
SET [RiskAssessment].[RiskAssessorId] = ( SELECT TOP 1 Id FROM 
						 [BusinessSafe].[dbo].[RiskAssessor] 
						  WHERE EmployeeId = [RiskAssessment].RiskAssessorEmployeeId)


-- To trigger failure
--UPDATE RiskAssessment SET [RiskAssessorId] = null where Id = 1

-- Guard make sure all risk assessments that have risk assessor have not got a new risk assessor id
DECLARE @NotAssignedResultCount TABLE
(
   Result bigint
);

INSERT INTO 
    @NotAssignedResultCount 
SELECT Count([Id]) 
FROM [BusinessSafe].[dbo].[RiskAssessment]
WHERE RiskAssessorEmployeeId is not null
AND RiskAssessorId is null

IF EXISTS (SELECT * FROM @NotAssignedResultCount WHERE Result > 0) 
BEGIN
	RAISERROR ('Not all risk assessments have been assigned a new risk assessor id', 16, 2) WITH SETERROR
END