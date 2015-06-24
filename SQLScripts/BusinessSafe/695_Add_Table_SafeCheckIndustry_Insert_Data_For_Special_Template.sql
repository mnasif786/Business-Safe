USE BusinessSafe
GO

IF NOT EXISTS (SELECT * FROM sys.columns AS c WHERE c.object_id = OBJECT_ID('SafeCheckIndustry') AND c.name = 'SpecialTemplate')
BEGIN
	ALTER TABLE [SafeCheckIndustry]
	ADD [SpecialTemplate] [BIT] NOT NULL DEFAULT 0
END
GO

UPDATE SafeCheckIndustry SET SpecialTemplate = 1 WHERE Name='Special Report' AND TemplateType=2

GO 

DECLARE @CatagoryId UNIQUEIDENTIFIER, @QuestionId UNIQUEIDENTIFIER, @QuestionOrderNumber INT, @IndustryId UNIQUEIDENTIFIER
SET @CatagoryId = NEWID()
SET @QuestionId = NEWID()
SET @QuestionOrderNumber = (SELECT MAX(OrderNumber)+1 FROM SafeCheckQuestion)
SET @IndustryId = (SELECT Id FROM SafeCheckIndustry WHERE Name='Special Report' AND SpecialTemplate=1)

INSERT INTO SafeCheckCategory VALUES(@CatagoryId,'Special Report Template',NULL,0,'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',GETDATE(),'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',GETDATE(),0,9,'SPECIAL')

INSERT INTO SafeCheckQuestion VALUES(@QuestionId,0,'Is it acceptable to generate a special report for this client?',@CatagoryId,1,NULL,'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',GETDATE(),'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',GETDATE(),0,@QuestionOrderNumber)

INSERT INTO SafeCheckQuestionResponse VALUES(NEWID(),'Acceptable',NULL,'Positive',@QuestionId,NULL,NULL,NULL,NULL,NULL,NULL,'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',GETDATE(),'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',GETDATE(),0)

INSERT INTO SafeCheckIndustryQuestion VALUES(NEWID(),@IndustryId,@QuestionId,'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',GETDATE(),'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',GETDATE(),0)

GO