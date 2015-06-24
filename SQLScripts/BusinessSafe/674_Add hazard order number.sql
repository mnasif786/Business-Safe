IF NOT EXISTS(SELECT * FROM sys.columns AS c
	WHERE c.object_id = object_id('MultiHazardRiskAssessmentHazard')
	AND c.name = 'OrderNumber')
BEGIN
	ALTER TABLE MultiHazardRiskAssessmentHazard ADD OrderNumber SMALLINT
END	