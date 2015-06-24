USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'RiskAssessmentControlMeasures' AND TYPE = 'U')
BEGIN
	CREATE TABLE [RiskAssessmentControlMeasures](
		[Id] bigint IDENTITY(1,1) NOT NULL,
		[RiskAssessmentHazardId] [bigint] NOT NULL,		
		[ControlMeasure] [nvarchar](150) NULL,
		[Deleted] [bit] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
	) 
END
GO


GRANT SELECT, INSERT,DELETE, UPDATE ON [RiskAssessmentControlMeasures] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RiskAssessmentControlMeasures' AND TYPE = 'U')
BEGIN
	DROP TABLE [RiskAssessmentControlMeasures]
END
