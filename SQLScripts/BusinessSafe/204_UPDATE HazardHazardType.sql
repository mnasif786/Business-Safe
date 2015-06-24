USE [BusinessSafe]
GO

INSERT INTO [dbo].[HazardHazardType] (HazardId, HazardTypeId)
SELECT [Id], 1 FROM [dbo].[Hazard]

--//@UNDO

DELETE FROM [dbo].[HazardHazardType]