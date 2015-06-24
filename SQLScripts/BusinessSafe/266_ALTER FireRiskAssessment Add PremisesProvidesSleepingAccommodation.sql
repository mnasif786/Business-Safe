USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'PremisesProvidesSleepingAccommodation')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	ADD [PremisesProvidesSleepingAccommodation] [bit] NULL
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'PremisesProvidesSleepingAccommodationConfirmed')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	ADD [PremisesProvidesSleepingAccommodationConfirmed] [bit] NULL
END
GO

--//@UNDO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'PremisesProvidesSleepingAccommodation')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [PremisesProvidesSleepingAccommodation]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'PremisesProvidesSleepingAccommodationConfirmed')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [PremisesProvidesSleepingAccommodationConfirmed]
END
GO