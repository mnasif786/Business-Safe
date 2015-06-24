USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ArchiveRiskAssessmentFurtherControlMeasureTask' AND COLUMN_NAME = 'FurtherControlMeasureTaskCategoryId')
BEGIN
	ALTER TABLE [ArchiveRiskAssessmentFurtherControlMeasureTask]
	ADD [FurtherControlMeasureTaskCategoryId] [smallint]
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ArchiveRiskAssessmentFurtherControlMeasureTask' AND COLUMN_NAME = 'TaskReoccurringTypeId')
BEGIN
	ALTER TABLE [ArchiveRiskAssessmentFurtherControlMeasureTask]
	ADD [TaskReoccurringTypeId] [smallint] NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ArchiveRiskAssessmentFurtherControlMeasureTask' AND COLUMN_NAME = 'TaskReoccurringEndDate')
BEGIN
	ALTER TABLE [ArchiveRiskAssessmentFurtherControlMeasureTask]
	ADD [TaskReoccurringEndDate] DATETIME NULL
END
GO

--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ArchiveRiskAssessmentFurtherControlMeasureTask' AND COLUMN_NAME = 'FurtherControlMeasureTaskCategoryId')
BEGIN
	ALTER TABLE [ArchiveRiskAssessmentFurtherControlMeasureTask]
	DROP COLUMN [FurtherControlMeasureTaskCategoryId]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ArchiveRiskAssessmentFurtherControlMeasureTask' AND COLUMN_NAME = 'TaskReoccurringTypeId')
BEGIN
	ALTER TABLE [ArchiveRiskAssessmentFurtherControlMeasureTask]
	DROP COLUMN [TaskReoccurringTypeId]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ArchiveRiskAssessmentFurtherControlMeasureTask' AND COLUMN_NAME = 'TaskReoccurringEndDate')
BEGIN
	ALTER TABLE [ArchiveRiskAssessmentFurtherControlMeasureTask]
	DROP COLUMN [TaskReoccurringEndDate]
END
GO