USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ArchiveRiskAssessmentFurtherControlMeasureTask')
BEGIN
	EXEC sp_rename 'ArchiveRiskAssessmentFurtherControlMeasureTask', 'ArchivedFurtherControlMeasureTask'
END

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ArchivedFurtherControlMeasureTask')
BEGIN
	EXEC sp_rename 'ArchivedFurtherControlMeasureTask', 'ArchiveRiskAssessmentFurtherControlMeasureTask'
END