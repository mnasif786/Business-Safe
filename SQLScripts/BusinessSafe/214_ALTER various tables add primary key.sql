USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_AddedDocument')
BEGIN
	ALTER TABLE [AddedDocument]
	ADD CONSTRAINT PK_AddedDocument PRIMARY KEY (Id)
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_DocumentType')
BEGIN
	ALTER TABLE [DocumentType]
	ADD CONSTRAINT PK_DocumentType PRIMARY KEY (Id)
END
GO

IF((SELECT IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'GeneralRiskAssessment' AND COLUMN_NAME = 'Id') = 'YES')
BEGIN 
	ALTER TABLE [GeneralRiskAssessment]
	ALTER COLUMN [Id] bigint NOT NULL
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_GeneralRiskAssessment')
BEGIN
	ALTER TABLE [GeneralRiskAssessment]
	ADD CONSTRAINT PK_GeneralRiskAssessment PRIMARY KEY (Id)
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_GeneralRiskAssessmentControlMeasure')
BEGIN
	ALTER TABLE [GeneralRiskAssessmentControlMeasure]
	ADD CONSTRAINT PK_GeneralRiskAssessmentControlMeasure PRIMARY KEY (Id)
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_Hazard')
BEGIN
	ALTER TABLE [Hazard]
	ADD CONSTRAINT PK_Hazard PRIMARY KEY (Id)
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_NonEmployee')
BEGIN
	ALTER TABLE [NonEmployee]
	ADD CONSTRAINT PK_NonEmployee PRIMARY KEY (Id)
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_PeopleAtRisk')
BEGIN
	ALTER TABLE [PeopleAtRisk]
	ADD CONSTRAINT PK_PeopleAtRisk PRIMARY KEY (Id)
END
GO

IF((SELECT IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'Id') = 'YES')
BEGIN 
	ALTER TABLE [PersonalRiskAssessment]
	ALTER COLUMN [Id] bigint NOT NULL
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_PersonalRiskAssessment')
BEGIN
	ALTER TABLE [PersonalRiskAssessment]
	ADD CONSTRAINT PK_PersonalRiskAssessment PRIMARY KEY (Id)
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_SiteStructureElement')
BEGIN
	ALTER TABLE [SiteStructureElement]
	ADD CONSTRAINT PK_SiteStructureElement PRIMARY KEY (Id)
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardHazardType' AND COLUMN_NAME = 'Id')
BEGIN
	ALTER TABLE [HazardHazardType]
	DROP COLUMN [Id]
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_HazardHazardType')
BEGIN
	ALTER TABLE [HazardHazardType]
	ADD CONSTRAINT PK_HazardHazardType PRIMARY KEY (HazardId, HazardTypeId)
END
GO

--//@UNDO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_HazardHazardType')
BEGIN
	ALTER TABLE [HazardHazardType]
	DROP CONSTRAINT PK_HazardHazardType
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_SiteStructureElement')
BEGIN
	ALTER TABLE [SiteStructureElement]
	DROP CONSTRAINT PK_SiteStructureElement
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardHazardType' AND COLUMN_NAME = 'Id')
BEGIN
	ALTER TABLE [HazardHazardType]
	ADD [Id] bigint NULL
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_PersonalRiskAssessment')
BEGIN
	ALTER TABLE [PersonalRiskAssessment]
	DROP CONSTRAINT PK_PersonalRiskAssessment
END
GO

IF((SELECT IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'Id') = 'NO')
BEGIN 
	ALTER TABLE [PersonalRiskAssessment]
	ALTER COLUMN [Id] bigint NULL
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_PeopleAtRisk')
BEGIN
	ALTER TABLE [PeopleAtRisk]
	DROP CONSTRAINT PK_PeopleAtRisk
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_NonEmployee')
BEGIN
	ALTER TABLE [NonEmployee]
	DROP CONSTRAINT PK_NonEmployee 
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_Hazard')
BEGIN
	ALTER TABLE [Hazard]
	DROP CONSTRAINT PK_Hazard 
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_GeneralRiskAssessmentControlMeasure')
BEGIN
	ALTER TABLE [GeneralRiskAssessmentControlMeasure]
	DROP CONSTRAINT PK_GeneralRiskAssessmentControlMeasure
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_GeneralRiskAssessment')
BEGIN
	ALTER TABLE [GeneralRiskAssessment]
	DROP CONSTRAINT PK_GeneralRiskAssessment
END
GO

IF((SELECT IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'GeneralRiskAssessment' AND COLUMN_NAME = 'Id') = 'NO')
BEGIN 
	ALTER TABLE [GeneralRiskAssessment]
	ALTER COLUMN [Id] bigint NULL
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_DocumentType')
BEGIN
	ALTER TABLE [DocumentType]
	DROP CONSTRAINT PK_DocumentType
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_AddedDocument')
BEGIN
	ALTER TABLE [AddedDocument]
	DROP CONSTRAINT PK_AddedDocument
END
GO