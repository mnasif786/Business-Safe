----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the CompanyDefaults table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

print 'Create [Hazard]'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'Hazard' AND TYPE = 'U')
BEGIN
	CREATE TABLE [Hazard]
	(
		Id bigint IDENTITY(1,1) NOT NULL
		,Name varchar(200) NOT NULL 				
		,RiskAssessmentId bigint NULL
		,CompanyId bigint NULL
		,Deleted bit NOT NULL DEFAULT 0
		,CreatedOn datetime NOT NULL DEFAULT GetDate()	
		,CreatedBy uniqueidentifier NULL
		,LastModifiedOn datetime NULL DEFAULT GetDate()
		,LastModifiedBy uniqueidentifier NULL
	)
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [Hazard] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Hazard' AND TYPE = 'U')
BEGIN
	DROP TABLE [Hazard]
END
