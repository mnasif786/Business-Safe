USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'MultiHazardRiskAssessmentControlMeasure')
BEGIN
	EXEC SP_RENAME 'GeneralRiskAssessmentControlMeasure', 'MultiHazardRiskAssessmentControlMeasure'
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TaskCategory')
BEGIN
	EXEC SP_RENAME 'ResponsibilityTaskCategory', 'TaskCategory'
END
GO

IF EXISTS (SELECT * FROM [Task] WHERE [Discriminator] = 'GeneralRiskAssessmentFurtherControlMeasureTask')
BEGIN
	UPDATE [Task] 
	SET [Discriminator] = 'MultiHazardRiskAssessmentFurtherControlMeasureTask'
	WHERE [Discriminator] = 'GeneralRiskAssessmentFurtherControlMeasureTask'
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'RiskAssessmentHazardId')
BEGIN
	EXEC SP_RENAME 'Task.RiskAssessmentHazardId' , 'MultiHazardRiskAssessmentHazardId', 'COLUMN'
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'MultiHazardRiskAssessmentControlMeasure' AND COLUMN_NAME = 'RiskAssessmentHazardId')
BEGIN
	EXEC SP_RENAME 'MultiHazardRiskAssessmentControlMeasure.RiskAssessmentHazardId' , 'MultiHazardRiskAssessmentHazardId', 'COLUMN'
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'MultiHazardRiskAssessmentControlMeasure' AND COLUMN_NAME = 'MultiHazardRiskAssessmentHazardId')
BEGIN
	EXEC SP_RENAME 'MultiHazardRiskAssessmentControlMeasure.MultiHazardRiskAssessmentHazardId' , 'RiskAssessmentHazardId', 'COLUMN'
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'MultiHazardRiskAssessmentHazardId')
BEGIN
	EXEC SP_RENAME 'Task.MultiHazardRiskAssessmentHazardId' , 'RiskAssessmentHazardId', 'COLUMN'
END
GO

IF EXISTS (SELECT * FROM [Task] WHERE [Discriminator] = 'MultiHazardRiskAssessmentFurtherControlMeasureTask')
BEGIN
	UPDATE [Task] 
	SET [Discriminator] = 'GeneralRiskAssessmentFurtherControlMeasureTask'
	WHERE [Discriminator] = 'MultiHazardRiskAssessmentFurtherControlMeasureTask'
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TaskCategory')
BEGIN
	EXEC SP_RENAME 'TaskCategory', 'ResponsibilityTaskCategory'
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'MultiHazardRiskAssessmentControlMeasure')
BEGIN
	EXEC SP_RENAME 'MultiHazardRiskAssessmentControlMeasure', 'GeneralRiskAssessmentControlMeasure'
END
GO