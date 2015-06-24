INSERT INTO dbo.SafeCheckQuestion
        ( Id
        ,CustomQuestion
        ,Title
        ,RelatedCategoryId
        ,Mandatory
        ,SpecificToClientId
        ,CreatedBy
        ,CreatedOn
        ,LastModifiedBy
        ,LastModifiedOn
        ,Deleted
        ,OrderNumber
        )
SELECT q.*
FROM [PBSPROD2SQL\PROD2].BusinessSafe.dbo.SafeCheckQuestion AS q
	LEFT JOIN dbo.SafeCheckQuestion AS q2 ON q.Id = q2.Id
WHERE q.CustomQuestion =0
AND q2.Id IS NULL

INSERT INTO dbo.SafeCheckQuestionResponse
        ( Id
        ,Title
        ,Date
        ,ResponseType
        ,QuestionId
        ,SupportingEvidence
        ,ActionRequired
        ,GuidanceNotes
        ,TimescaleId
        ,ReportLetterStatement
        ,ReportLetterStatementCategoryId
        ,CreatedBy
        ,CreatedOn
        ,LastModifiedBy
        ,LastModifiedOn
        ,Deleted
        )
SELECT qr.* FROM [PBSPROD2SQL\PROD2].BusinessSafe.dbo.SafeCheckQuestionResponse AS qr
	LEFT JOIN dbo.SafeCheckQuestionResponse AS qr2 ON qr.QuestionId = qr2.QuestionId
WHERE qr2.Id IS NULL

INSERT INTO dbo.SafeCheckIndustry
        ( Id
        ,Name
        ,CreatedBy
        ,CreatedOn
        ,LastModifiedBy
        ,LastModifiedOn
        ,Deleted
        ,Draft
        ,TemplateType
        )
SELECT i.* FROM [PBSPROD2SQL\PROD2].BusinessSafe.dbo.SafeCheckIndustry AS i
	LEFT JOIN dbo.SafeCheckIndustry AS i2 ON i.Id = i2.Id
WHERE i2.Id IS NULL
AND i.Deleted = 0


INSERT INTO dbo.SafeCheckIndustryQuestion
        ( Id
        ,IndustryId
        ,QuestionId
        ,CreatedBy
        ,CreatedOn
        ,LastModifiedBy
        ,LastModifiedOn
        ,Deleted
        )
SELECT  iq.*
FROM    [PBSPROD2SQL\PROD2].BusinessSafe.dbo.SafeCheckIndustryQuestion AS iq
        LEFT JOIN dbo.SafeCheckIndustryQuestion iq2 ON iq.IndustryId = iq2.IndustryId
                                                       AND iq.QuestionId = iq2.QuestionId
		INNER JOIN dbo.SafeCheckIndustry AS i ON iq.IndustryId = i.Id
		INNER JOIN dbo.SafeCheckQuestion AS q ON iq.QuestionId = q.Id
WHERE iq2.Id IS NULL