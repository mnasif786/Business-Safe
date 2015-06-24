USE [BusinessSafe]

UPDATE [dbo].[Section]
SET [Title] = 'Hazardous substances - infection risks & chemicals'
where [title] like '%Hazardous substances%'
and [title] like '%infection risks%'
GO



UPDATE [dbo].[Question]
SET [Text] = 'Does the work involve exposure to temperatures that are uncomfortably cold (below 16&deg;C) or hot (above 27&deg;C)?'
where [Text] like '%Does the work involve exposure to temperatures that are uncomfortably cold%'
and [Text] like '%or hot (above%'
GO

