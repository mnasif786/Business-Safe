IF NOT EXISTS ( SELECT  *
                FROM    sys.columns AS c
                WHERE   c.object_id = OBJECT_ID('RiskAssessment')
                        AND c.name = 'NextReviewDate' ) 
    BEGIN
        ALTER TABLE dbo.RiskAssessment ADD NextReviewDate DATETIME NULL
    END


GO
UPDATE dbo.RiskAssessment
SET NextReviewDate = nextReview.NextReviewDate
FROM dbo.RiskAssessment AS ra
	INNER JOIN (SELECT RiskAssessmentId, MAX(CompletionDueDate) NextReviewDate 
FROM dbo.RiskAssessmentReview AS rar
WHERE rar.Deleted = 0
GROUP BY RiskAssessmentId) nextReview ON ra.id = nextReview.RiskAssessmentId