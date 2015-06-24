USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks')
BEGIN
	EXEC SP_RENAME 'RiskAssessmentFurtherActionTasks', 'RiskAssessmentFurtherControlMeasureTask'
END
GO	

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ArchiveRiskAssessmentFurtherActionTasks')
BEGIN
	EXEC SP_RENAME 'ArchiveRiskAssessmentFurtherActionTasks', 'ArchiveRiskAssessmentFurtherControlMeasureTask'
END
GO	

--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherControlMeasureTask')
BEGIN
	EXEC SP_RENAME 'RiskAssessmentFurtherControlMeasureTask', 'RiskAssessmentFurtherActionTasks'
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ArchiveRiskAssessmentFurtherControlMeasureTask')
BEGIN
	EXEC SP_RENAME 'ArchiveRiskAssessmentFurtherControlMeasureTask', 'ArchiveRiskAssessmentFurtherActionTasks'
END
GO