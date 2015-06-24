USE BusinessSafe
GO

IF NOT EXISTS (SELECT * FROM SafeCheckIndustry WHERE Name='Special Report')
BEGIN
	INSERT INTO SafeCheckIndustry (Id, Name, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Deleted, Draft, TemplateType) 
	VALUES (NEWID(), 'Special Report','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',GETDATE(),'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', GETDATE(),0,0,2)
END 
GO

IF NOT EXISTS (SELECT * FROM sys.columns AS c WHERE c.object_id = OBJECT_ID('SafeCheckCheckList') AND c.name = 'SpecialReport')
BEGIN
	ALTER TABLE [SafeCheckCheckList]
	ADD [SpecialReport] [BIT] NOT NULL DEFAULT 0
END
GO

--//@UNDO
IF EXISTS (SELECT * FROM SafeCheckIndustry WHERE Name='Special Report')
BEGIN
	DELETE SafeCheckIndustry WHERE Name='Special Report' 	
END 
GO

IF EXISTS (SELECT * FROM sys.columns AS c WHERE c.object_id = OBJECT_ID('SafeCheckCheckList') AND c.name = 'SpecialReport')
BEGIN
	ALTER TABLE [SafeCheckCheckList]
	DROP COLUMN [SpecialReport]
END