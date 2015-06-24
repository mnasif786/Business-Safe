SELECT 
MultiHazardRiskAssessmentHazardId,
ControlMeasure,
COUNT(*) As [Number Of Duplicates],
MAX(Id) As [Max Id]
FROM MultiHazardRiskAssessmentControlMeasure
WHERE Deleted = 0
GROUP BY 
MultiHazardRiskAssessmentHazardId,
ControlMeasure
HAVING COUNT(*) > 1

CREATE TABLE #DeletedRecords
(
	Id BIGINT
)

BEGIN TRAN

DECLARE @multiHazardRiskAssessmentHazardId BIGINT
DECLARE @controlMeasureText NVARCHAR(300)
DECLARE @maxControlMeasureId BIGINT

DECLARE curDuplicateControlMeasures CURSOR FOR
SELECT 
MultiHazardRiskAssessmentHazardId,
ControlMeasure,
MAX(Id)
FROM MultiHazardRiskAssessmentControlMeasure
WHERE Deleted = 0
GROUP BY 
MultiHazardRiskAssessmentHazardId,
ControlMeasure
HAVING COUNT(*) > 1

OPEN curDuplicateControlMeasures
FETCH NEXT FROM curDuplicateControlMeasures INTO @multiHazardRiskAssessmentHazardId, @controlMeasureText, @maxControlMeasureId

WHILE (@@FETCH_STATUS <> -1)
BEGIN
	INSERT INTO #DeletedRecords (Id) 
	SELECT Id 
	FROM MultiHazardRiskAssessmentControlMeasure
	WHERE MultiHazardRiskAssessmentHazardId = @multiHazardRiskAssessmentHazardId
	AND ControlMeasure = @controlMeasureText
	AND Id <> @maxControlMeasureId
	AND Deleted = 0
	
	UPDATE MultiHazardRiskAssessmentControlMeasure
	SET Deleted = 1
	WHERE MultiHazardRiskAssessmentHazardId = @multiHazardRiskAssessmentHazardId
	AND ControlMeasure = @controlMeasureText
	AND Id <> @maxControlMeasureId
	AND Deleted = 0

	FETCH NEXT FROM curDuplicateControlMeasures INTO @multiHazardRiskAssessmentHazardId, @controlMeasureText, @maxControlMeasureId
END

CLOSE curDuplicateControlMeasures
DEALLOCATE curDuplicateControlMeasures

SELECT * FROM #DeletedRecords
DROP TABLE #DeletedRecords


--COMMIT TRAN
--ROLLBACK TRAN




