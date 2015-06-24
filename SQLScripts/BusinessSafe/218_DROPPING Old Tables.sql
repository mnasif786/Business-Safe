USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'ArchivedEmployee' AND TYPE = 'U')BEGIN
	DROP TABLE ArchivedEmployee;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'ArchivedFurtherControlMeasureTask' AND TYPE = 'U')BEGIN
	DROP TABLE ArchivedFurtherControlMeasureTask;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'PREVIOUS_FurtherControlMeasureDocument' AND TYPE = 'U')BEGIN
	DROP TABLE PREVIOUS_FurtherControlMeasureDocument;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'PREVIOUS_RiskAssessmentDocument' AND TYPE = 'U')BEGIN
	DROP TABLE PREVIOUS_RiskAssessmentDocument
END
GO



--//@UNDO
USE [BusinessSafe]
GO

-- These tables need to go so let's not go back hey code does not reference them!



