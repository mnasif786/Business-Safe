----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the HazardousSubstance and  table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'HazardousSubstance' AND TYPE = 'U')
BEGIN
	CREATE TABLE	HazardousSubstance
	(
		Id bigint IDENTITY(1,1) NOT NULL
		,[Name] varchar(200) NOT NULL 		
		,Reference varchar(50) NOT NULL
		,SupplierId bigint NULL
		,HazardousSubstanceStandardId bigint NOT NULL
		,SDSDate datetime NOT NULL
		,DetailsOfUse varchar(500) NOT NULL
		,AssessmentRequired bit NOT NULL
		,CompanyId bigint NOT NULL
		,Deleted bit NOT NULL DEFAULT 0
		,CreatedOn datetime NOT NULL DEFAULT GetDate()	
		,CreatedBy uniqueidentifier NULL
		,LastModifiedOn datetime NULL DEFAULT GetDate()
		,LastModifiedBy uniqueidentifier NULL
	)
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [HazardousSubstance] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'HazardousSubstance' AND TYPE = 'U')
BEGIN
	DROP TABLE [HazardousSubstance]
END
