
DECLARE @logEventId AS BIGINT
DECLARE @checklistId AS UNIQUEIDENTIFIER ;
DECLARE @CoveringLetterContent VARCHAR(MAX)
SET @logEventId = 11988614 ;
--SET @logEventId = 5;

WITH XMLNAMESPACES ('http://schemas.datacontract.org/2004/07/EvaluationChecklist.Models' AS DYN )
( SELECT    @checklistId = objects.value('(/DYN:ChecklistViewModel/DYN:Id)[1]', 'uniqueidentifier')
  FROM      dbo.LogEvent
  WHERE     LogEventId = @logEventId) ;

DECLARE @answeredQuestions TABLE
    (
     QuestionId UNIQUEIDENTIFIER
    ,ActionRequired NVARCHAR(500)
    ,AssignedToId UNIQUEIDENTIFIER
    ,AssignedToNonEmployeeName NVARCHAR(150)
    ,GuidanceNotes NVARCHAR(250)
    ,QaComments NVARCHAR(500)
    ,SupportingEvidence NVARCHAR(500)
    ,SelectedResponseId UNIQUEIDENTIFIER
    ,TimeScaleId INT
    ,SupportingDocumentationDate VARCHAR(35)
    ,SupportingDocumentationStatus VARCHAR(25)
    ,AreaOfNonCompliance VARCHAR(1000)
    ) ;
WITH XMLNAMESPACES  ('http://schemas.datacontract.org/2004/07/EvaluationChecklist.Models' AS DYN )

INSERT  INTO @answeredQuestions
        ( 
         QuestionId
        ,ActionRequired
        ,AssignedToId
        ,AssignedToNonEmployeeName
        ,GuidanceNotes
        ,QaComments
        ,SupportingEvidence
        ,SelectedResponseId
        ,TimeScaleId
        ,SupportingDocumentationDate
        ,SupportingDocumentationStatus
        ,AreaOfNonCompliance
         
        )
        SELECT  c.q.value('DYN:Answer[1]/DYN:QuestionId[1]', 'uniqueidentifier') AS QuestionId
               ,c.q.value('DYN:Answer[1]/DYN:ActionRequired[1]', 'nvarchar(500)') AS ActionRequired
               ,c.q.value('DYN:Answer[1]/DYN:AssignedTo[1]/DYN:Id[1]', 'uniqueidentifier') AS AssignedToId
               ,c.q.value('DYN:Answer[1]/DYN:AssignedTo[1]/DYN:NonEmployeeName[1]', 'nvarchar(50)') AS AssignedToNonEmployeeName
               ,c.q.value('DYN:Answer[1]/DYN:GuidanceNotes[1]', 'nvarchar(250)') AS GuidanceNotes
               ,c.q.value('DYN:Answer[1]/DYN:QaComments[1]', 'nvarchar(500)') AS QaComments
               ,c.q.value('DYN:Answer[1]/DYN:SupportingEvidence[1]', 'nvarchar(500)') AS SupportingEvidence
               ,c.q.value('DYN:Answer[1]/DYN:SelectedResponseId[1]', 'uniqueidentifier') AS SelectedResponseId
               ,c.q.value('DYN:Answer[1]/DYN:Timescale[1]/DYN:Id[1]', 'int') AS TimeScaleId
               ,c.q.value('DYN:Answer[1]/DYN:SupportingDocumentationDate[1]', 'varchar(35)') AS SupportingDocumentationDate
               ,c.q.value('DYN:Answer[1]/DYN:SupportingDocumentationStatus[1]', 'varchar(25)') AS SupportingDocumentationStatus
               ,c.q.value('DYN:Answer[1]/DYN:AreaOfNonCompliance[1]', 'varchar(1000)') AS AreaOfNonCompliance
        FROM    dbo.LogEvent
                CROSS APPLY [Objects].nodes('DYN:ChecklistViewModel/DYN:Questions/DYN:QuestionAnswerViewModel') AS c ( q )
        WHERE   LogEventId = @logEventId
                AND c.q.value('DYN:Answer[1]/DYN:SelectedResponseId[1]', 'nvarchar(50)') <> '' ;
                
WITH XMLNAMESPACES  ('http://schemas.datacontract.org/2004/07/EvaluationChecklist.Models' AS DYN )
SELECT  @CoveringLetterContent = objects.value('(/DYN:ChecklistViewModel/DYN:CoveringLetterContent)[1]', 'varchar(MAX)')
FROM    dbo.LogEvent AS le WITH ( NOLOCK )
WHERE   CallingMethod = 'PostChecklist'
        AND LogEventId = @logEventId
ORDER BY le.LogEventId     


 
SET XACT_ABORT ON
BEGIN TRANSACTION

--SELECT   aq.*
--FROM    @answeredQuestions AS aq
--        INNER JOIN [SafeCheckCheckListAnswer] sa ON aq.QuestionId = sa.QuestionId
--WHERE   sa.CheckListId = @checklistId
--        AND sa.ResponseId IS NULL



UPDATE  [SafeCheckCheckListAnswer]
SET     ResponseId = aq.SelectedResponseId
       ,SupportingEvidence = aq.SupportingEvidence
       ,ActionRequired = aq.ActionRequired
       ,AssignedTo = aq.AssignedToId
       ,GuidanceNotes = aq.GuidanceNotes
       ,TimeScaleId = aq.TimeScaleId
       ,EmployeeNotListed = aq.AssignedToNonEmployeeName
       ,AreaOfNonCompliance = aq.AreaOfNonCompliance
       ,SupportingDocumentationDate = CONVERT(DATETIME, ( CASE WHEN aq.SupportingDocumentationDate = '' THEN NULL
                                                               ELSE aq.SupportingDocumentationDate
                                                          END ))
       ,SupportingDocumentationStatus = aq.SupportingDocumentationStatus
       ,LastModifiedOn = CURRENT_TIMESTAMP
FROM    @answeredQuestions AS aq
        INNER JOIN [SafeCheckCheckListAnswer] sa ON aq.QuestionId = sa.QuestionId
WHERE   sa.CheckListId = @checklistId
        AND sa.ResponseId IS NULL
 
UPDATE  dbo.SafeCheckCheckList
SET     CoveringLetterContent = @CoveringLetterContent
WHERE   Id = @checklistId
        AND ( CoveringLetterContent IS NULL
              OR CoveringLetterContent = ''
            )
 
UPDATE  dbo.SafeCheckCheckList
SET     LastModifiedOn = CURRENT_TIMESTAMP
WHERE   Id = @checklistId

SELECT  sa.*
FROM    @answeredQuestions AS aq
        INNER JOIN [SafeCheckCheckListAnswer] sa ON aq.QuestionId = sa.QuestionId
WHERE   sa.CheckListId = @checklistId
ORDER BY sa.LastModifiedOn

ROLLBACK TRAN