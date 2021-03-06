----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the ResponsibilityTaskCategory table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'RiskAssessment' AND TYPE = 'U')
BEGIN
	CREATE TABLE	[RiskAssessment]
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
		,SiteId bigint NULL
		,RiskAssessorEmployeeId uniqueidentifier NULL
		,StatusId smallint
		,CONSTRAINT [PK_RiskAssessment2] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [RiskAssessment] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RiskAssessment' AND TYPE = 'U')
BEGIN
	DROP TABLE [RiskAssessment]
END
