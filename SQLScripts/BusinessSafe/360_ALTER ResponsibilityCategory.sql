IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ResponsibilityCategory' AND COLUMN_NAME = 'Sequence')
BEGIN
	ALTER TABLE Responsibilitycategory
	ADD [Sequence] [int] NULL

	ALTER TABLE [dbo].[ResponsibilityCategory] ADD  CONSTRAINT [DF_ResponsibilityCategory_Sequence]  DEFAULT ((0)) FOR [Sequence]
END

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ResponsibilityCategory' AND COLUMN_NAME = 'Sequence')
BEGIN
	ALTER TABLE [dbo].[ResponsibilityCategory] 
	DROP CONSTRAINT [DF_ResponsibilityCategory_Sequence]
	
	ALTER TABLE [ResponsibilityCategory]
	DROP COLUMN [Sequence] 
	
END
