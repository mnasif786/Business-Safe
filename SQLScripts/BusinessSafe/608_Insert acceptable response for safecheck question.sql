INSERT  INTO dbo.SafeCheckQuestionResponse
        ( 
         Id
        ,Title
        ,ResponseType
        ,QuestionId
        ,SupportingEvidence
        ,GuidanceNotes
        ,CreatedBy
        ,CreatedOn
        ,LastModifiedBy
        ,LastModifiedOn
        ,Deleted
        )
        SELECT  '9DF62838-7296-4BB3-B409-F2A906130531'
               ,'Acceptable'
               ,'Positive'
               ,'691C9048-414F-4BFE-BDE0-3E39A430B53A'
               ,'Suitable arrangements are in place.'
               ,'1.19'
               ,'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99'
               ,CURRENT_TIMESTAMP
               ,'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99'
               ,CURRENT_TIMESTAMP
               ,0
        WHERE   NOT EXISTS ( SELECT *
                             FROM   dbo.SafeCheckQuestionResponse AS qr
                             WHERE  QuestionId = '691C9048-414F-4BFE-BDE0-3E39A430B53A'
                                    AND Title = 'Acceptable' )