USE [BusinessSafe]

UPDATE [dbo].[Section]
SET [Title] = 'Hazardous substances - infection risks & chemicals'
WHERE [Id] = 26
GO

UPDATE [dbo].[Question]
SET [Text] = 'Does the work involve exposure to temperatures that are uncomfortably cold (below 16ºC) or hot (above 27ºC)?'
WHERE [Id] = 109
GO

