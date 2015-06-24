USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentDocument')
BEGIN
	CREATE TABLE [dbo].[RiskAssessmentDocument](
		[Id] [bigint] NOT NULL,
		[RiskAssessmentId] [bigint] NULL
		CONSTRAINT [PK_RiskAssessmentDocument2] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
	) ON [PRIMARY]
END
GO
	
GRANT SELECT, INSERT,DELETE, UPDATE ON [RiskAssessmentDocument] TO [AllowAll]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FurtherControlMeasureDocument')
BEGIN
	CREATE TABLE [dbo].[FurtherControlMeasureDocument](
		[Id] [bigint]  NOT NULL,
		[FurtherControlMeasureId] [bigint] NULL,
		[DocumentOriginTypeId] [smallint] NULL
		CONSTRAINT [PK_FurtherControlMeasureDocument2] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
	) ON [PRIMARY]
END
GO
	
GRANT SELECT, INSERT,DELETE, UPDATE ON [FurtherControlMeasureDocument] TO [AllowAll]
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentDocument')
BEGIN
	DROP TABLE [RiskAssessmentDocument]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FurtherControlMeasureDocument')
BEGIN
	DROP TABLE [FurtherControlMeasureDocument]
END
GO
