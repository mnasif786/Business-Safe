

TRUNCATE TABLE [SafeCheckCategoryQuestion]
  
INSERT  INTO dbo.SafeCheckCategoryQuestion
        ( 
         Id
        ,CategoryId
        ,QuestionId 
        )
        SELECT  NEWID()
               ,scq.RelatedCategoryId
               ,scq.Id
        FROM    dbo.SafeCheckQuestion AS scq
                LEFT JOIN dbo.SafeCheckCategoryQuestion AS cq ON scq.RelatedCategoryId = cq.CategoryId
                                                                 AND scq.Id = cq.QuestionId
        WHERE   cq.Id IS NULL
  
  
