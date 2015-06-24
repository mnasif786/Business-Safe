IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Responsibility' AND COLUMN_NAME = 'IsCreatedByWizard')
BEGIN
	ALTER TABLE [Responsibility]
	ADD [IsCreatedByWizard] [bit] NOT NULL CONSTRAINT [DF_Responsibility_IsCreatedByWizard]  DEFAULT ((0))
END
	

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Responsibility' AND COLUMN_NAME = 'IsCreatedByWizard')
BEGIN
	ALTER TABLE [dbo].[Responsibility] 
	DROP CONSTRAINT [DF_Responsibility_IsCreatedByWizard]
	
	ALTER TABLE [Responsibility]
	DROP COLUMN [IsCreatedByWizard] 
	
END