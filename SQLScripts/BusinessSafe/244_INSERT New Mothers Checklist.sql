
set dateformat ymd

IF NOT EXISTS (SELECT * FROM [BusinessSafe].[dbo].[Checklist] WHERE id = 4)
BEGIN

print 'creating checklist 4'

SET IDENTITY_INSERT [BusinessSafe].[dbo].[Checklist] ON
INSERT INTO [BusinessSafe].[dbo].[Checklist]
([Id], [Title],[Description],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (4,'New and Expectant Mothers','Important Notes: This assessment should be completed by both the pregnant woman and a supervisor, a copy should  be giver to the person subject to this assessment. It may help to refer to Guidance Note – New and Expectant Mothers during the completion of this form. You may also find it helpful to refer to other topic related Guidance Notes as you complete this checklist. The assessment may need to be reviewed more than once as the pregnancy or return to work develops.  It should always be reviewed at the request of the New and Expectant Mother. The assessment should clearly state what control measures are already in place and indicate the new control measures required – confirmation regarding the implementation of new control measures should be given in the comments section.', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

SET IDENTITY_INSERT [BusinessSafe].[dbo].[Checklist] OFF

declare @NewQuestions table (ChecklistName varchar(50), Section varchar(255), Question varchar(max), questiontype int, listorder int, sectionlistorder int)
INSERT INTO @NewQuestions
select 'New and Expectant Mothers' as ChecklistName,'About this assessment' as section, 'Is this the first assessment for this pregnancy? (if NO, when was the last assessment?)' as question , 3 as questiontype, 1 as listorder, 1 as sectionlistorder
UNION select 'New and Expectant Mothers', 'About this assessment', 'Has the mother named above taken part in this assessment?', 1, 2, 1
UNION select 'New and Expectant Mothers', 'Movement and posture', 'Does the job involve awkward twisting or stretching?', 3, 1, 2
UNION select 'New and Expectant Mothers', 'Movement and posture', 'Does the woman have to stand for periods of, for example more than two-three hours without a break?', 3, 2, 2
UNION select 'New and Expectant Mothers', 'Movement and posture', 'Does she have to sit for periods of more than two-three hours?', 3, 2, 2
UNION select 'New and Expectant Mothers', 'Movement and posture', 'Are there space restrictions (for example, working behind a desk)?', 3, 3, 2
UNION select 'New and Expectant Mothers', 'Movement and posture', 'If yes, will these cause more restricted movement as the pregnancy develops?', 1, 4, 2
UNION select 'New and Expectant Mothers', 'Manual Handling', 'Does the job involve twisting, stooping or stretching to lift objects?', 3, 1, 3
UNION select 'New and Expectant Mothers', 'Manual Handling', 'Does the job involve the lifting, pushing or pulling of heavy loads?', 3, 2, 3
UNION select 'New and Expectant Mothers', 'Manual Handling', 'Does the job involve rapid repetitive lifting (even of lighter objects)?', 3, 3, 3
UNION select 'New and Expectant Mothers', 'Manual Handling', 'Does the job involve lifting objects that are difficult to grasp or awkward to hold?', 3, 4, 3
UNION select 'New and Expectant Mothers', 'Protective equipment and uniforms', 'If the woman needs to wear protective aprons, overalls etc., are they provided in suitable sizes?', 3, 1, 4
UNION select 'New and Expectant Mothers', 'Protective equipment and uniforms', 'If uniforms are obligatory are they provided in maternity sizes?', 3, 2, 4
UNION select 'New and Expectant Mothers', 'Protective equipment and uniforms', 'Are the materials used comfortable for all pregnant women to wear?', 3, 3, 4
UNION select 'New and Expectant Mothers', 'Hazardous substances – infection risks & chemicals', 'Are there any infection risks in the work? For example: Clearing up spilled body fluids or disposing of used syringes.', 3, 1, 5
UNION select 'New and Expectant Mothers', 'Hazardous substances – infection risks & chemicals', 'Work with raw meats', 3, 2, 5
UNION select 'New and Expectant Mothers', 'Hazardous substances – infection risks & chemicals', 'If yes to either of the above, are hygiene precautions adequate?', 3, 3, 5
UNION select 'New and Expectant Mothers', 'Hazardous substances – infection risks & chemicals', 'Are any chemicals used at work known to be of risk to women of child bearing age?', 3, 4, 5
UNION select 'New and Expectant Mothers', 'Hazardous substances – infection risks & chemicals', 'If yes to above, are pregnant workers kept away from work that could increase exposure?', 1, 5, 5
UNION select 'New and Expectant Mothers', 'Working Time', 'Is the woman expected to work long hours or overtime?', 3, 1, 6
UNION select 'New and Expectant Mothers', 'Working Time', 'Does she have some flexibility or choice over her working hours?', 3, 2, 6
UNION select 'New and Expectant Mothers', 'Working Time', 'Does the work involve very early starts or late finishes?', 3, 3, 6
UNION select 'New and Expectant Mothers', 'Working Time', 'Does the work involve night work between the hours of, for example, 11pm to 7am?', 3, 4, 6
UNION select 'New and Expectant Mothers', 'Work-related stress', 'Are there tasks which are known to be particularly stressful, for example dealing with irate customers?', 3, 1, 7
UNION select 'New and Expectant Mothers', 'Work-related stress', 'Are colleagues and supervisors supportive toward the pregnant worker?', 3, 2, 7
UNION select 'New and Expectant Mothers', 'Work-related stress', 'Is the woman aware of what to do if she feels she is being bullied or victimised?', 3, 3, 7
UNION select 'New and Expectant Mothers', 'Work-related stress', 'Has the individual risk assessment taken into account any concerns the woman has about her own pregnancy?', 3, 4, 7
UNION select 'New and Expectant Mothers', 'Extremes of cold or heat', 'Does the work involve exposure to temperatures that are uncomfortably cold (below 16ºC) or hot (above 27ºC)?', 3, 1, 8
UNION select 'New and Expectant Mothers', 'Extremes of cold or heat', 'If protective clothing is provided against the cold is it suitable for the pregnant worker?', 3, 2, 8
UNION select 'New and Expectant Mothers', 'Extremes of cold or heat', 'Is the worker exposed to cold draughts even when the average temperature is acceptable?', 3, 3, 8
UNION select 'New and Expectant Mothers', 'Extremes of cold or heat', 'Are there arrangements for sufficient breaks and access to hot or cold drinks?', 3, 4, 8
UNION select 'New and Expectant Mothers', 'Work at heights', 'Does the work involve a lot of climbing up and down steps or ladders?', 3, 1, 9
UNION select 'New and Expectant Mothers', 'Work at heights', 'Does the work involve carrying items up or down stairs or ladders?', 3, 2, 9
UNION select 'New and Expectant Mothers', 'Work at heights', 'If a mobile platform is used to access higher levels, is there enough room for a pregnant worker to use it safely?', 3, 3, 9
UNION select 'New and Expectant Mothers', 'Work-related violence', 'Is the job one which is perceived to have a high risk of violence (for example security work, single staffing in a petrol station)?', 3, 1, 10
UNION select 'New and Expectant Mothers', 'Work-related violence', 'Is there always support at hand to help any staff who may be threatened or abused by customers?', 3, 2, 10
UNION select 'New and Expectant Mothers', 'Work-related violence', 'Are managers and supervisors aware of the extra risks posed by violence to pregnant women?', 3, 3, 10
UNION select 'New and Expectant Mothers', 'Welfare issues', 'Is there somewhere quiet for pregnant workers to rest or lie down?', 3, 1, 11
UNION select 'New and Expectant Mothers', 'Welfare issues', 'Are new or expectant mothers provided easy access to toilets and allowed sufficient breaks, where needed?', 3, 2, 11
UNION select 'New and Expectant Mothers', 'Welfare issues', 'Is there a clean, private area to express breast milk?', 3, 3, 11
UNION select 'New and Expectant Mothers', 'Welfare issues', 'Is there somewhere safe for them to store expressed milk?', 3, 4, 11
UNION select 'New and Expectant Mothers', 'Additional comments', 'Comments (please include confirmation regarding new control measures implemented):', 2, 1, 11

--select * from @NewQuestions
-- add sections
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

print 'finished creating checklist 4'

END

--//@UNDO
DELETE from question where sectionid in (
  select id from section where checklistid = 4
)
DELETE from section where checklistid = 4
DELETE from checklist where id = 4