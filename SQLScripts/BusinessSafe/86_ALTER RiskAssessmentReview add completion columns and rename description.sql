USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentReview' AND COLUMN_NAME = 'CompletedDate')
BEGIN
	ALTER TABLE [RiskAssessmentReview]
	ADD [CompletedDate] [datetime] NULL
	
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentReview' AND COLUMN_NAME = 'CompletedById')
BEGIN
	ALTER TABLE [RiskAssessmentReview]
	ADD [CompletedById] [uniqueidentifier] NULL
	
END
GO	

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentReview' AND COLUMN_NAME = 'Description')
BEGIN
	EXEC SP_RENAME 'RiskAssessmentReview.Description', 'Comments', 'COLUMN'

END
GO	

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentReview' AND COLUMN_NAME = 'Comments')
BEGIN
	ALTER TABLE RiskAssessmentReview
	ALTER COLUMN Comments NVARCHAR(500) NULL

END
GO	

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentReview' AND COLUMN_NAME = 'CompletedById')
BEGIN
	ALTER TABLE [RiskAssessmentReview]
	DROP COLUMN [CompletedById]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentReview' AND COLUMN_NAME = 'CompletedDate')
BEGIN
	ALTER TABLE [RiskAssessmentReview]
	DROP COLUMN [CompletedDate]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentReview' AND COLUMN_NAME = 'Comments')
BEGIN
	EXEC SP_RENAME 'RiskAssessmentReview.Comments', 'Description', 'COLUMN'
END
GO	

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentReview' AND COLUMN_NAME = 'Description')
BEGIN
	ALTER TABLE RiskAssessmentReview
	ALTER COLUMN [Description] NVARCHAR(150) NULL

END
GO	