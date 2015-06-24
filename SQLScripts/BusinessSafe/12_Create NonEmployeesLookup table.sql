----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the ResponsibilityTaskCategory table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'NonEmployee' AND TYPE = 'U')
BEGIN
	CREATE TABLE	[NonEmployee]
	(
		Id bigint IDENTITY(1,1) NOT NULL
		,Name varchar(200) NOT NULL 		
		,Company varchar(50) NULL
		,Position varchar(50) NULL 
		,ClientId bigint NULL
		,RiskAssessmentId bigint NULL
		,Deleted bit NOT NULL DEFAULT 0
		,CreatedOn datetime NOT NULL DEFAULT GetDate()	
		,CreatedBy uniqueidentifier NULL
		,LastModifiedOn datetime NULL DEFAULT GetDate()
		,LastModifiedBy uniqueidentifier NULL
	)
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [NonEmployee] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'NonEmployee' AND TYPE = 'U')
BEGIN
	DROP TABLE [NonEmployee]
END
