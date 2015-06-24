----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the ResponsibilityTaskCategory table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'GeneralRiskAssessment' AND TYPE = 'U')
BEGIN
	CREATE TABLE	[GeneralRiskAssessment]
	(
		Id bigint NOT NULL
		,Location nvarchar(500) NULL
		,TaskProcessDescription	nvarchar(500) NULL
		,CONSTRAINT [PK_GeneralRiskAssessment2] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [GeneralRiskAssessment] TO [AllowAll]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'HazardousSubstanceRiskAssessment' AND TYPE = 'U')
BEGIN
	CREATE TABLE	[HazardousSubstanceRiskAssessment]
	(
		Id bigint NOT NULL
		,IsInhalationRouteOfEntry bit NULL
		,IsIngestionRouteOfEntry bit NULL
		,IsAbsorptionRouteOfEntry bit NULL
		,WorkspaceExposureLimits nvarchar(100) NULL
		,HazardousSubstanceId bigint NULL
		,Quantity int NULL
		,MatterState int NULL
		,DustinessOrVolatility int NULL
		,HealthSurveillanceRequired	bit NULL
		,CONSTRAINT [PK_HazardousSubstanceRiskAssessment2] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [HazardousSubstanceRiskAssessment] TO [AllowAll]
GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'HazardousSubstanceRiskAssessment' AND TYPE = 'U')
BEGIN
	DROP TABLE [HazardousSubstanceRiskAssessment]
END

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'GeneralRiskAssessment' AND TYPE = 'U')
BEGIN
	DROP TABLE [GeneralRiskAssessment]
END
