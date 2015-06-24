
set dateformat ymd

IF NOT EXISTS (SELECT * FROM [BusinessSafe].[dbo].[Checklist] WHERE id = 2)
BEGIN

SET IDENTITY_INSERT [BusinessSafe].[dbo].[Checklist] ON
INSERT INTO [BusinessSafe].[dbo].[Checklist]
([Id], [Title],[Description],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (2,'Manual Handling Self Assessment Questionnaire','See also Guidance Note, Manual Handling', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

SET IDENTITY_INSERT [BusinessSafe].[dbo].[Checklist] OFF

declare @NewQuestions table (ChecklistName varchar(50), Section varchar(50), Question varchar(max), questiontype int, listorder int, sectionlistorder int)
INSERT INTO @NewQuestions
select 'Manual Handling Self Assessment Questionnaire' as ChecklistName,'Task Information' as section, 'Task Location' as question , 2 as questiontype, 1 as listorder, 1 as sectionlistorder
UNION select 'Manual Handling Self Assessment Questionnaire','Task Information',  'Task Undertaken', 2,2,1
UNION select 'Manual Handling Self Assessment Questionnaire','The Task - does it involve:',  'Holding loads away from the trunk?', 3, 1,2
UNION select 'Manual Handling Self Assessment Questionnaire','The Task - does it involve:', 'Twisting or stooping?', 3, 2,2
UNION select 'Manual Handling Self Assessment Questionnaire','The Task - does it involve:', 'Reaching upwards?',3,3 ,2
UNION select 'Manual Handling Self Assessment Questionnaire','The Task - does it involve:','Large vertical movement?',3,4  ,2
UNION select 'Manual Handling Self Assessment Questionnaire','The Task - does it involve:','Long carrying distances?' ,3,5 ,2
UNION select 'Manual Handling Self Assessment Questionnaire','The Task - does it involve:','Strenuous pushing or pulling?',3,6 ,2
UNION select 'Manual Handling Self Assessment Questionnaire','The Task - does it involve:','Unpredictable movement of loads?' ,3,7 ,2
UNION select 'Manual Handling Self Assessment Questionnaire','The Task - does it involve:','Repetitive handling?' ,3,8 ,2
UNION select 'Manual Handling Self Assessment Questionnaire','The Task - does it involve:','Insufficient rest and recovery?' ,3,9,2
UNION select 'Manual Handling Self Assessment Questionnaire','The Task - does it involve:','A work rate imposed by a process?' ,3,10,2
UNION select 'Manual Handling Self Assessment Questionnaire','The Individual''s capability -  does the job:','Require unusual capability??' ,3,1,3
UNION select 'Manual Handling Self Assessment Questionnaire','The Individual''s capability -  does the job:','Endanger those with a health problem?' ,3,2,3
UNION select 'Manual Handling Self Assessment Questionnaire','The Individual''s capability -  does the job:','Endanger those who are pregnant?'  ,3,3,3
UNION select 'Manual Handling Self Assessment Questionnaire','The Individual''s capability -  does the job:','Call for special information / training?' ,3,4,3
UNION select 'Manual Handling Self Assessment Questionnaire','The Individual''s capability -  does the job:','Endanger those who are pregnant?'  ,3,5,3
UNION select 'Manual Handling Self Assessment Questionnaire','The Loads - are they:','Heavy?  What is the weight and frequency?',3,6,4
UNION select 'Manual Handling Self Assessment Questionnaire','The Loads - are they:','Bulky / unwieldy?' ,3,1,4
UNION select 'Manual Handling Self Assessment Questionnaire','The Loads - are they:','Difficult to grasp?',3,2,4
UNION select 'Manual Handling Self Assessment Questionnaire','The Loads - are they:','Unstable / unpredictable?',3,3,4
UNION select 'Manual Handling Self Assessment Questionnaire','The Loads - are they:','Intrinsically harmful (e.g. sharp / hot)?',3,4,4
UNION select 'Manual Handling Self Assessment Questionnaire','The Environment: are there:','Constraints on posture?', 3,1,5
UNION select 'Manual Handling Self Assessment Questionnaire','The Environment: are there:','Poor floors?'  ,3,2,5
UNION select 'Manual Handling Self Assessment Questionnaire','The Environment: are there:','Variations in levels?' ,3,3,5
UNION select 'Manual Handling Self Assessment Questionnaire','The Environment: are there:','Hot / cold / humid conditions?'  ,3,4,5
UNION select 'Manual Handling Self Assessment Questionnaire','The Environment: are there:','Strong air movements?' ,3,5,5
UNION select 'Manual Handling Self Assessment Questionnaire','The Environment: are there:','Poor lighting conditions?' ,3,6,5
UNION select 'Manual Handling Self Assessment Questionnaire','Other factors:','Is movement or posture hindered by clothing or personal protective equipment?' ,3,1, 6

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

END

--//@UNDO
DELETE from question where sectionid in (
  select id from section where checklistid = 2
)
DELETE from section where checklistid = 2
DELETE from checklist where id = 2