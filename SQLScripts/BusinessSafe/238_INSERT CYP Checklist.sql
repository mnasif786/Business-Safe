
set dateformat ymd

SET IDENTITY_INSERT [BusinessSafe].[dbo].[Checklist] ON
INSERT INTO [BusinessSafe].[dbo].[Checklist]
([Id], [Title],[Description],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (3,'Children and Young Persons Information Checklist','See also Guidance Note, Manual Handling', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
GO
SET IDENTITY_INSERT [BusinessSafe].[dbo].[Checklist] OFF

declare @NewQuestions table (ChecklistName varchar(255), Section varchar(255), Question varchar(max), questiontype int, listorder int, sectionlistorder int)
INSERT INTO @NewQuestions
select 'Children and Young Persons Information Checklist' as ChecklistName,'Task Information' as section, 'Task Location' as question , 2 as questiontype, 1 as listorder, 1 as sectionlistorder
UNION select 'Children and Young Persons Information Checklist','Task Information',  'Task Undertaken', 2,2,1

UNION select 'Children and Young Persons Information Checklist','Is the person a:',  'Young Person (under 18)?', 3, 1,2
UNION select 'Children and Young Persons Information Checklist','Is the person a:',  'Child (under school leaving age)?', 3, 2,2

UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:',  'Use of dangerous machinery?', 3, 1,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:', 'Use of other machinery?', 3, 2,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:', 'Lifting and carrying activities?',3,3 ,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:','Repetitive handling of articles?',3,4  ,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:','Hot Work (welding etc)?' ,3,5 ,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:','Use of fixed electrical equipment?',3,6 ,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:','Use of portable electrical equipment?' ,3,7 ,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:','Use of air powered or pressure equipment?' ,3,8 ,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:','Driving of vehiciles?' ,3,9,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:','Use of flammable substances?' ,3,10,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:','Use of hazardous substances?' ,3,11,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:','Exposure to toxic substances?' ,3,12,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:','Exposure to other dangerous substances?' ,3,13,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:','Work with animals?' ,3,14,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:','Work in places where articles may fall?' ,3,15,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:','Maitenance of equipment?' ,3,16,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:','Work with display screen or visual display unit?' ,3,17,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:','Pre-determined work rates?' ,3,18,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:','Work in high-pressure atmospheres?' ,3,19,3
UNION select 'Children and Young Persons Information Checklist','The Task - does it involve:','Work where structural collapse is possible?' ,3,20,3

UNION select 'Children and Young Persons Information Checklist','Environmental factors - does the work involve:','Work in hot conditions?' ,3,1,4
UNION select 'Children and Young Persons Information Checklist','Environmental factors - does the work involve:','Work in cold conditions?' ,3,2,4
UNION select 'Children and Young Persons Information Checklist','Environmental factors - does the work involve:','Work in the open air?' ,3,3,4
UNION select 'Children and Young Persons Information Checklist','Environmental factors - does the work involve:','Work in wet conditions?' ,3,4,4
UNION select 'Children and Young Persons Information Checklist','Environmental factors - does the work involve:','Work in confined spaces?' ,3,5,4
UNION select 'Children and Young Persons Information Checklist','Environmental factors - does the work involve:','Work at height?' ,3,10,4
UNION select 'Children and Young Persons Information Checklist','Environmental factors - does the work involve:','Work near open pits, vats, reservoirs or tanks?' ,3,7,4
UNION select 'Children and Young Persons Information Checklist','Environmental factors - does the work involve:','Work in excavated areas?' ,3,8,4
UNION select 'Children and Young Persons Information Checklist','Environmental factors - does the work involve:','Work in noisy conditions?' ,3,9,4
UNION select 'Children and Young Persons Information Checklist','Environmental factors - does the work involve:','Work with ionising or non-ionising radiation?' ,3,9,4

UNION select 'Children and Young Persons Information Checklist','Use of personal protective equipment - does the task require the YP to wear PPE:','Occasionally?' ,3,1,5
UNION select 'Children and Young Persons Information Checklist','Use of personal protective equipment - does the task require the YP to wear PPE:','All the time?' ,3,2,5

UNION select 'Children and Young Persons Information Checklist','Individual capability - does the task require:','Unusual capability?' ,3,1,6
UNION select 'Children and Young Persons Information Checklist','Individual capability - does the task require:','Special training (e.g. safety passport, manual handling etc.)?' ,3,2,6

UNION select 'Children and Young Persons Information Checklist','Other Factors:','Is there a possibility of hand-arm vibration or full body vibration due to use of equipment?' ,3,2,7


--select * from @NewQuestions
INSERT INTO [dbo].[Section]
           ([ChecklistId]
           ,[Title]
           ,[ListOrder]
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy]
           ,[LastModifiedOn]
           ,[LastModifiedBy])
select distinct chk.id as checklistid,nq.section,nq.sectionlistorder, 0,'2013-02-13','b03c83ee-39f2-4f88-b4c4-7c276b1aad99','2013-02-14','b03c83ee-39f2-4f88-b4c4-7c276b1aad99'
from @newquestions nq 
  inner join Checklist chk on nq.checklistname = chk.title
  left join section s on chk.id = s.checklistid
    and s.title = nq.section
where s.id is null

--add questions
INSERT INTO [BusinessSafe].[dbo].[Question] ([SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
select  s.id as sectionid, nq.questiontype, nq.listorder, 1, nq.question,0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99'
from @newquestions nq 
  inner join Checklist chk on nq.checklistname = chk.title
  INNER join section s on chk.id = s.checklistid
    and s.title = nq.section
  LEFT JOIN question q on s.id = q.sectionid
    and q.text = nq.question
where q.id is null

--//@UNDO
DELETE from question where sectionid in (
  select id from section where checklistid = 3
)
DELETE from section where checklistid = 3
DELETE from checklist where id = 3