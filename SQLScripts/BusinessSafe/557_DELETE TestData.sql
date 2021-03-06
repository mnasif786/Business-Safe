USE [BusinessSafe]

DELETE FROM [BusinessSafe].[dbo].[Action]
GO

DELETE FROM [BusinessSafe].[dbo].[ActionPlan]
GO

DELETE FROM [BusinessSafe].[dbo].[SafeCheckCheckList]
GO

DELETE FROM [BusinessSafe].[dbo].[SafeCheckCheckListAnswer]
GO

DELETE FROM [BusinessSafe].[dbo].[SafeCheckCheckListQuestion]
GO

DELETE FROM [BusinessSafe].[dbo].[SafeCheckSector]
GO

DELETE FROM [BusinessSafe].[dbo].[SafeCheckSectorQuestion]
GO

DELETE FROM [BusinessSafe].[dbo].[SafeCheckImmediateRiskNotification]
GO

DELETE FROM [BusinessSafe].[dbo].[SafeCheckClientQuestion]
GO

update [BusinessSafe].[dbo].[Task]
set Deleted = 1
where Discriminator = 'ActionTask'
go

update [BusinessSafe].[dbo].[Document]
set Deleted = 1
where DocumentTypeId = 18 -- type 18 is Action documents
go
