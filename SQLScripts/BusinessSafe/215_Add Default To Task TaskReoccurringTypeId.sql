USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.default_constraints 
    				WHERE NAME ='DF_Task_TaskReoccurringTypeId')
BEGIN
	ALTER TABLE dbo.Task 
  	ADD CONSTRAINT DF_Task_TaskReoccurringTypeId
    DEFAULT 0 FOR TaskReoccurringTypeId
END
GO


IF NOT EXISTS (SELECT * FROM sys.default_constraints 
    				WHERE NAME ='DF_SafetyPhrase_RequiresAdditionalInformation')
BEGIN
	ALTER TABLE dbo.SafetyPhrase
  	ADD CONSTRAINT DF_SafetyPhrase_RequiresAdditionalInformation
  	DEFAULT 0 FOR RequiresAdditionalInformation
END
GO

UPDATE dbo.SafetyPhrase
SET RequiresAdditionalInformation = 0
WHERE RequiresAdditionalInformation IS NULL




--//@UNDO
IF EXISTS (SELECT * FROM sys.default_constraints 
    				WHERE NAME ='DF_Task_TaskReoccurringTypeId')
BEGIN

	ALTER TABLE dbo.Task 
  	DROP CONSTRAINT DF_Task_TaskReoccurringTypeId
END
GO

IF EXISTS (SELECT * FROM sys.default_constraints 
    				WHERE NAME ='DF_SafetyPhrase_RequiresAdditionalInformation')
BEGIN

	ALTER TABLE dbo.SafetyPhrase 
  	DROP CONSTRAINT DF_SafetyPhrase_RequiresAdditionalInformation
END
GO