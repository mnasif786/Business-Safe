SET XACT_ABORT ON

BEGIN TRANSACTION

DELETE FROM dbo.SafeCheckIndustryQuestion
WHERE id IN (
SELECT MAX(CONVERT(VARCHAR(36),iq.Id)) AS LatestDuplicateId
FROM dbo.SafeCheckIndustryQuestion AS iq
WHERE iq.Deleted = 0
GROUP BY iq.IndustryId,iq.QuestionId
HAVING COUNT(*) > 1)

--execute a second time to get rid of the third duplicate
DELETE FROM dbo.SafeCheckIndustryQuestion
WHERE id IN (
SELECT MAX(CONVERT(VARCHAR(36),iq.Id)) AS LatestDuplicateId
FROM dbo.SafeCheckIndustryQuestion AS iq
WHERE iq.Deleted = 0
GROUP BY iq.IndustryId,iq.QuestionId
HAVING COUNT(*) > 1)


DELETE FROM dbo.SafeCheckIndustryQuestion
WHERE Deleted = 1

COMMIT TRANSACTION