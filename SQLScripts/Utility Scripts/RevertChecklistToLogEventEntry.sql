USE [BusinessSafe]

--use utility script 'Query log event for post checklist events' to find the log ID you need.

DECLARE @logEventId AS BIGINT
DECLARE @checklistId AS UNIQUEIDENTIFIER;
SET @logEventId = 10076144;
--SET @logEventId = 5;

WITH XMLNAMESPACES ('http://schemas.datacontract.org/2004/07/EvaluationChecklist.Models' AS DYN )
(
	SELECT 
	@checklistId = objects.value('(/DYN:ChecklistViewModel/DYN:Id)[1]', 'uniqueidentifier')
	FROM dbo.LogEvent 
	WHERE LogEventId = @logEventId
);

--SELECT @checklistId;

BEGIN TRAN

DECLARE @questionId AS uniqueidentifier
DECLARE @actionRequired AS nvarchar(500)
DECLARE @assignedTo AS UNIQUEIDENTIFIER
DECLARE @AssignedToNonEmployeeName NVARCHAR(150)
DECLARE @guidanceNotes AS nvarchar(250)
DECLARE @qaComments AS nvarchar(500)
DECLARE @supportingEvidence AS nvarchar(500)
DECLARE @selectedResponseId AS UNIQUEIDENTIFIER
DECLARE @timeScaleId AS INT;
DECLARE @SupportingDocumentationDate VARCHAR(35)
DECLARE @SupportingDocumentationStatus VARCHAR(25)
DECLARE @AreaOfNonCompliance VARCHAR(1000)

DECLARE curs CURSOR FOR
WITH XMLNAMESPACES ('http://schemas.datacontract.org/2004/07/EvaluationChecklist.Models' AS DYN )
(
	SELECT 
	c.q.value('DYN:Answer[1]/DYN:QuestionId[1]', 'uniqueidentifier') AS QuestionId,
	c.q.value('DYN:Answer[1]/DYN:ActionRequired[1]', 'nvarchar(500)') AS ActionRequired,
	c.q.value('DYN:Answer[1]/DYN:AssignedTo[1]/DYN:Id[1]', 'nvarchar(50)') AS AssignedTo,
	c.q.value('DYN:Answer[1]/DYN:GuidanceNotes[1]', 'nvarchar(250)') AS GuidanceNotes,
	c.q.value('DYN:Answer[1]/DYN:QaComments[1]', 'nvarchar(500)') AS QaComments,
	c.q.value('DYN:Answer[1]/DYN:SupportingEvidence[1]', 'nvarchar(500)') AS SupportingEvidence,
	c.q.value('DYN:Answer[1]/DYN:SelectedResponseId[1]', 'uniqueidentifier') AS SelectedResponseId,	
	c.q.value('DYN:Answer[1]/DYN:Timescale[1]/DYN:Id[1]', 'int') AS TimeScaleId,
	c.q.value('DYN:Answer[1]/DYN:AssignedTo[1]/DYN:NonEmployeeName[1]', 'nvarchar(50)') AS AssignedToNonEmployeeName,
	c.q.value('DYN:Answer[1]/DYN:SupportingDocumentationDate[1]', 'varchar(35)') AS SupportingDocumentationDate,
    c.q.value('DYN:Answer[1]/DYN:SupportingDocumentationStatus[1]', 'varchar(25)') AS SupportingDocumentationStatus,
    c.q.value('DYN:Answer[1]/DYN:AreaOfNonCompliance[1]', 'varchar(1000)') AS AreaOfNonCompliance
	FROM dbo.LogEvent 
	CROSS APPLY [Objects].nodes('DYN:ChecklistViewModel/DYN:Questions/DYN:QuestionAnswerViewModel') as c(q)
	WHERE LogEventId = @logEventId
);
	
OPEN curs
FETCH NEXT FROM curs INTO @questionId, @actionRequired, @assignedTo, @guidanceNotes, @qaComments, @supportingEvidence, @selectedResponseId, @timeScaleId, @AssignedToNonEmployeeName, @SupportingDocumentationDate, @SupportingDocumentationStatus, @AreaOfNonCompliance
WHILE (@@FETCH_STATUS <> -1)
BEGIN

--PRINT @questionId
--Print @SupportingDocumentationDate
--PRINT @SupportingDocumentationStatus

	UPDATE [SafeCheckCheckListAnswer]
	SET 	
	[ActionRequired] = @actionRequired,
	[AssignedTo] = CASE @assignedTo WHEN '00000000-0000-0000-0000-000000000000' THEN NULL ELSE @assignedTo END,
	[GuidanceNotes] = @guidanceNotes,
	[QaComments] = @qaComments,
	[SupportingEvidence] = @supportingEvidence,
	[ResponseId] = @SelectedResponseId,
	[TimescaleId] = @timeScaleId, 
	[EmployeeNotListed]= @AssignedToNonEmployeeName, 
	[AreaOfNonCompliance] = @AreaOfNonCompliance,
	[SupportingDocumentationDate] = CONVERT(DATETIME, (CASE WHEN @SupportingDocumentationDate = '' THEN NULL ELSE @SupportingDocumentationDate END)),
	[SupportingDocumentationStatus] = @SupportingDocumentationStatus,
	[LastModifiedOn] = CURRENT_TIMESTAMP
	WHERE [QuestionId] = @questionId
	AND [ChecklistId] = @checklistId
	FETCH NEXT FROM curs INTO @questionId, @actionRequired, @assignedTo, @guidanceNotes, @qaComments, @supportingEvidence, @selectedResponseId, @timeScaleId, @AssignedToNonEmployeeName, @SupportingDocumentationDate, @SupportingDocumentationStatus, @AreaOfNonCompliance
END

CLOSE curs
DEALLOCATE curs


COMMIT TRAN
--ROLLBACK TRAN

/*

--USE THIS TO CHECK BEFORE COMMITTING.

SELECT  
*
FROM         SafeCheckCheckListAnswer WITH (NOLOCK)
WHERE ChecklistId = '448fccb7-04dd-3e4f-e5b2-cfd540e87ecc'
*/