UPDATE    SafeCheckQuestion
SET       RelatedCategoryId = cq.CategoryId
FROM      dbo.SafeCheckQuestion AS q
        INNER JOIN SafeCheckCategoryQuestion cq ON q.id = cq.QuestionId
WHERE     RelatedCategoryId = '00000000-0000-0000-0000-000000000000'