USE BusinessSafe
declare @Id UNIQUEIDENTIFIER
set @Id = '99b364bf-19eb-29fd-2a6b-5c9db924a3dc' 

;WITH XMLNAMESPACES  ('http://schemas.datacontract.org/2004/07/EvaluationChecklist.Models' AS DYN )
SELECT 
	objects.value('count(/DYN:ChecklistViewModel/DYN:Questions/DYN:QuestionAnswerViewModel/DYN:Question)', 'int') AS QuestionCount
	,objects.value('count(/DYN:ChecklistViewModel/DYN:Questions/DYN:QuestionAnswerViewModel/DYN:Answer/DYN:SelectedResponseId[not(@xsi:nil = "true")])', 'int') AS AnswerCount
	,objects.value('(/DYN:ChecklistViewModel/DYN:CoveringLetterContent)[1]', 'varchar(MAX)') AS CoveringLetterContent
	,objects.value('(/DYN:ChecklistViewModel/DYN:Site/DYN:SiteName)[1]', 'varchar(MAX)') AS SiteName
	,objects.value('(/DYN:ChecklistViewModel/DYN:ClientId)[1]', 'varchar(MAX)') AS ClientId
	,objects.value('(/DYN:ChecklistViewModel/DYN:PostedBy)[1]', 'varchar(MAX)')
,* 
FROM dbo.LogEvent AS le WITH(NOLOCK)
WHERE CallingMethod = 'PostChecklist'
--AND objects.value('(/DYN:ChecklistViewModel/DYN:Id)[1]', 'UNIQUEIDENTIFIER' ) = @Id
AND objects.value('(/DYN:ChecklistViewModel/DYN:ClientId)[1]', 'varchar(MAX)') IN (9177,81872)
--AND le.logeventid IN (12138294,11989430)
--AND le.[User] = 'HQ\Paul.Jackson'
--AND le.Date > '2014-04-10 00:00:00.00'
ORDER BY objects.value('(/DYN:ChecklistViewModel/DYN:ClientId)[1]', 'int'),le.Date desc

9177
81872
MCK032- today at approx 1pm

EUR72 - monday around 3pm
