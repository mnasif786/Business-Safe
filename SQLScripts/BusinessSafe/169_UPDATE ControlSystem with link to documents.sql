USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ControlSystem' AND COLUMN_NAME = 'Url')
BEGIN
	ALTER TABLE [ControlSystem]
	ALTER COLUMN [Url] VARCHAR(150) NOT NULL

	UPDATE [ControlSystem] SET [Url] = '/Content/HazardousSubstanceControlSystems/Hazardous_Substances_Control_System_General_Ventilation.docx' WHERE [Id] = 1
	UPDATE [ControlSystem] SET [Url] = '/Content/HazardousSubstanceControlSystems/Hazardous_Substances_Control_System_Engineering_Control.docx' WHERE [Id] = 2
	UPDATE [ControlSystem] SET [Url] = '/Content/HazardousSubstanceControlSystems/Hazardous_Substances_Control_System_Containment.docx' WHERE [Id] = 3
	UPDATE [ControlSystem] SET [Url] = '/Content/HazardousSubstanceControlSystems/Hazardous_Substances_Control_System_Special_Measures.docx' WHERE [Id] = 4
END

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ControlSystem' AND COLUMN_NAME = 'Url')
BEGIN
	UPDATE [ControlSystem]
	SET [Url] = ''
	
	ALTER TABLE [ControlSystem]
	ALTER COLUMN [Url] VARCHAR(100) NOT NULL
END