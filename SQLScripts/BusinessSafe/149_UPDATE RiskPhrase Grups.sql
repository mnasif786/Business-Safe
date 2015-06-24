USE [BusinessSafe]
GO

UPDATE [RiskPhrase] 
SET [HazardousSubstanceGroupId] = 5 
WHERE [ReferenceNumber] IN ('H334', 'H341', 'H340', 'H350', 'R40', 'R42', 'R42/43', 'R45', 'R46', 'R49', 'R68')
GO

UPDATE [RiskPhrase] 
SET [HazardousSubstanceGroupId] = 4 
WHERE [ReferenceNumber] IN ('H300', 'H310', 'H330', 'H351', 'H360', 'H361', 'H362', 'H372', 'R26', 'R26/27', 'R26/27/28', 'R27/28', 'R28', 'R39/26/27/28',
'R48/23', 'R48/23/24', 'R48/23/24/25', 'R48/23/25', 'R48/24', 'R48/24/25', 'R48/25', 'R60', 'R61', 'R62', 'R63', 'R64')
GO

UPDATE [RiskPhrase] 
SET [HazardousSubstanceGroupId] = 3 
WHERE [ReferenceNumber] IN ('H301', 'H311', 'H314', 'H317', 'H318', 'H331', 'H335', 'H370', 'H373', 'R23', 'R23/24', 'R23/24/25', 'R23/25', 'R24', 'R24/25', 
'R25', 'R34', 'R35', 'R36/37', 'R36/37/38', 'R37', 'R37/38', 'R39/23/24/25', 'R41', 'R43', 'R48', 'R48/20', 'R48/20/21', 'R48/20/21/22', 'R48/22', 
'R48/21/22', 'R48/22', 'R68/23/24/25')
GO

UPDATE [RiskPhrase] 
SET [HazardousSubstanceGroupId] = 2 
WHERE [ReferenceNumber] IN ('H302', 'H312', 'H332', 'H371', 'R20', 'R20/21', 'R20/22', 'R20/21/22', 'R21', 'R21/22', 'R22', 'R68/20/21/22')
GO

UPDATE [RiskPhrase] 
SET [HazardousSubstanceGroupId] = 1 
WHERE [HazardousSubstanceGroupId] IS NULL
GO

--//@UNDO 

UPDATE [RiskPhrase] 
SET [HazardousSubstanceGroupId] = NULL