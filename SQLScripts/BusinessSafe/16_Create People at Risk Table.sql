----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the CompanyDefaults table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

print 'Create [PeopleAtRisk]'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'PeopleAtRisk' AND TYPE = 'U')
BEGIN
	CREATE TABLE [PeopleAtRisk]
	(
		Id bigint IDENTITY(1,1) NOT NULL
		,Name varchar(200) NOT NULL 				
		,RiskAssessmentId bigint NULL
		,CompanyId bigint NULL
		,Deleted bit NULL DEFAULT 0
		,CreatedOn datetime NOT NULL DEFAULT GetDate()	
		,CreatedBy uniqueidentifier NULL
		,LastModifiedOn datetime NULL DEFAULT GetDate()
		,LastModifiedBy uniqueidentifier NULL
	)
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [PeopleAtRisk] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'PeopleAtRisk' AND TYPE = 'U')
BEGIN
	DROP TABLE [PeopleAtRisk]
END
