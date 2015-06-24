USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'Location')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	ADD [Location] [nvarchar](500) NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'BuildingUse')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	ADD [BuildingUse] [nvarchar](200) NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'ElectricityEmergencyShutOff')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	ADD [ElectricityEmergencyShutOff] [nvarchar](200) NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'GasEmergencyShutOff')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	ADD [GasEmergencyShutOff] [nvarchar](200) NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'WaterEmergencyShutOff')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	ADD [WaterEmergencyShutOff] [nvarchar](200) NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'OtherEmergencyShutOff')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	ADD [OtherEmergencyShutOff] [nvarchar](200) NULL
END
GO

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'Location')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [Location]
END
GO


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'BuildingUse')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [BuildingUse]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'ElectricityEmergencyShutOff')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [ElectricityEmergencyShutOff]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'GasEmergencyShutOff')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [GasEmergencyShutOff]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'WaterEmergencyShutOff')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [WaterEmergencyShutOff]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'OtherEmergencyShutOff')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [OtherEmergencyShutOff]
END
GO
