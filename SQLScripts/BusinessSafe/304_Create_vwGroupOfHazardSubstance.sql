IF EXISTS (SELECT * FROM sys.objects AS o
	WHERE name ='vwGroupOfHazardSubstance')
BEGIN
	DROP VIEW vwGroupOfHazardSubstance
END
Go
CREATE VIEW vwGroupOfHazardSubstance AS 

SELECT  a.Id AS HazardousSubstanceId
		,a.Code AS HazardGroupCode
FROM    ( SELECT    hs.id
                   ,hsg.Code
                   --,rp.*
                   ,ROW_NUMBER() OVER ( PARTITION BY hs.id ORDER BY code DESC ) AS RowNumber
          FROM      dbo.HazardousSubstance AS hs
                    INNER JOIN dbo.HazardousSubstanceRiskPhrase AS hsrp ON hs.id = hsrp.HazardousSubstanceId
                    INNER JOIN dbo.RiskPhrase AS rp ON hsrp.RiskPhraseId = rp.Id
                    INNER JOIN dbo.HazardousSubstanceGroup AS hsg ON rp.HazardousSubstanceGroupId = hsg.Id ) a
WHERE   a.RowNumber = 1

Go

GRANT SELECT ON vwGroupOfHazardSubstance TO AllowAll


