USE [BusinessSafe]
GO

ALTER TABLE dbo.RiskAssessment ALTER COLUMN Title NVARCHAR(200) NULL
GO

--//@UNDO

ALTER TABLE dbo.RiskAssessment ALTER COLUMN Title NVARCHAR(200) NOT NULL
GO