SELECT 
riskAssessment.ClientId AS [ClientId],
customer.CustomerKey AS [CAN],
riskAssessmentHazard.Id AS [RiskAssessmentHazard.Id],
riskAssessment.Id AS [RiskAssessment.Id],
riskAssessment.Reference AS [RiskAssessment.Reference],
riskAssessment.Title AS [RiskAssessment.Title],
hazard.Id AS [Hazard.Id],
hazard.Name AS [Hazard.Name],
ControlMeasure AS [ControlMeasure.ControlMeasure], 
COUNT(*) As [Number Of Duplicates],
COUNT(*) - SUM(CAST(controlMeasure.Deleted AS INT)) [Undeleted Records],
CAST(MAX(controlMeasure.CreatedOn) AS VARCHAR) AS [Last CreatedOn Date]
FROM MultiHazardRiskAssessmentControlMeasure controlMeasure
LEFT JOIN [MultiHazardRiskAssessmentHazard] riskAssessmentHazard
ON controlMeasure.MultiHazardRiskAssessmentHazardId = riskAssessmentHazard.Id
LEFT JOIN [RiskAssessment] riskAssessment
ON riskAssessmentHazard.RiskAssessmentId = riskAssessment.Id
LEFT JOIN [Hazard] hazard
ON riskAssessmentHazard.HazardId = hazard.Id
LEFT JOIN [Peninsula].[dbo].[TblCustomers] customer
ON riskAssessment.ClientId = customer.CustomerId
WHERE ControlMEasure.Deleted = 0
GROUP BY 
riskAssessment.ClientId,
customer.CustomerKey,
riskAssessmentHazard.Id,
riskAssessment.Id,
riskAssessment.Reference,
riskAssessment.Title,
hazard.Id,
hazard.Name,
ControlMeasure
HAVING COUNT(*) > 1
ORDER BY MAX(controlMeasure.CreatedOn) DESC




--SELECT ControlMeasure,
--COUNT(*) 
--FROM MultiHazardRiskAssessmentControlMeasure 
--GROUP BY ControlMeasure
--HAVING COUNT(*) > 1
--ORDER BY COUNT(*) DESC

--SELECT *
--FROM [dbo].[Audit]
--WITH (NOLOCK)
--WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentControlMeasure'
--AND [Type] = 'D'
--ORDER BY [EntityId] DESC

--SELECT *
--FROM [dbo].[Audit]
--WITH (NOLOCK)
--WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentControlMeasure'
--AND [Type] = 'I'
--AND EntityId IN (
--9447,
--9442
--)
--ORDER BY [EntityId] DESC


--SELECT ControlMeasure,
--COUNT(*) 
--FROM MultiHazardRiskAssessmentControlMeasure 
--GROUP BY ControlMeasure
--HAVING COUNT(*) > 1
--ORDER BY COUNT(*) DESC

--SELECT *
--FROM [dbo].[Audit]
--WITH (NOLOCK)
--WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentControlMeasure'
--AND [Type] = 'D'
--ORDER BY [EntityId] DESC

--SELECT *
--FROM [dbo].[Audit]
--WITH (NOLOCK)
--WHERE [EntityName] = 'BusinessSafe.Domain.Entities.MultiHazardRiskAssessmentControlMeasure'
--AND [Type] = 'I'
--AND EntityId IN (
--9447,
--9442
--)
--ORDER BY [EntityId] DESC

