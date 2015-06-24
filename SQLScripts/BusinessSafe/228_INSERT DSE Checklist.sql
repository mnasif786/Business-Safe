USE [BusinessSafe]


Print 'Add DSE Checklist'



IF NOT EXISTS (SELECT * FROM [BusinessSafe].[dbo].[Checklist] WHERE id = 1)
BEGIN

SET IDENTITY_INSERT [BusinessSafe].[dbo].[Checklist] ON
--Checklist
INSERT INTO [BusinessSafe].[dbo].[Checklist]
([Id], [Title],[Description],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (1,'Display Screen Equipment','See also Guidance Note 5-11', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

SET IDENTITY_INSERT [BusinessSafe].[dbo].[Checklist] OFF

SET IDENTITY_INSERT [BusinessSafe].[dbo].[Section] ON

Print 'Add DSE Checklist - Section 1'
INSERT INTO [BusinessSafe].[dbo].[Section] 
([Id], [ChecklistId],[Title],[ListOrder],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (1,1,'DSE Use',1,0,'2013-02-13','b03c83ee-39f2-4f88-b4c4-7c276b1aad99','2013-02-14','b03c83ee-39f2-4f88-b4c4-7c276b1aad99')


Print 'Add DSE Checklist - Section 2'

INSERT INTO [BusinessSafe].[dbo].[Section] 
([Id], [ChecklistId],[Title],[ListOrder],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (2,1,'Screen',2,0,'2013-02-13','b03c83ee-39f2-4f88-b4c4-7c276b1aad99','2013-02-14','b03c83ee-39f2-4f88-b4c4-7c276b1aad99')


Print 'Add DSE Checklist - Section 3'

INSERT INTO [BusinessSafe].[dbo].[Section] 
([Id], [ChecklistId],[Title],[ListOrder],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (3,1,'Keyboard and Mouse',3,0,'2013-02-13','b03c83ee-39f2-4f88-b4c4-7c276b1aad99','2013-02-14','b03c83ee-39f2-4f88-b4c4-7c276b1aad99')


Print 'Add DSE Checklist - Section 4'

INSERT INTO [BusinessSafe].[dbo].[Section] 
([Id], [ChecklistId],[Title],[ListOrder],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (4,1,'Chair',4,0,'2013-02-13','b03c83ee-39f2-4f88-b4c4-7c276b1aad99','2013-02-14','b03c83ee-39f2-4f88-b4c4-7c276b1aad99')


Print 'Add DSE Checklist - Section 5'

INSERT INTO [BusinessSafe].[dbo].[Section] 
([Id], [ChecklistId],[Title],[ListOrder],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (5,1,'Desk',5,0,'2013-02-13','b03c83ee-39f2-4f88-b4c4-7c276b1aad99','2013-02-14','b03c83ee-39f2-4f88-b4c4-7c276b1aad99')


Print 'Add DSE Checklist - Section 6'

INSERT INTO [BusinessSafe].[dbo].[Section] 
([Id], [ChecklistId],[Title],[ListOrder],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (6,1,'General',6,0,'2013-02-13','b03c83ee-39f2-4f88-b4c4-7c276b1aad99','2013-02-14','b03c83ee-39f2-4f88-b4c4-7c276b1aad99')


Print 'Add DSE Checklist - Section 7'

INSERT INTO [BusinessSafe].[dbo].[Section] 
([Id], [ChecklistId],[Title],[ListOrder],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (7,1,'Posture',7,0,'2013-02-13','b03c83ee-39f2-4f88-b4c4-7c276b1aad99','2013-02-14','b03c83ee-39f2-4f88-b4c4-7c276b1aad99')


Print 'Add DSE Checklist - Section 8'

INSERT INTO [BusinessSafe].[dbo].[Section] 
([Id], [ChecklistId],[Title],[ListOrder],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (8,1,'Eye Tests',8,0,'2013-02-13','b03c83ee-39f2-4f88-b4c4-7c276b1aad99','2013-02-14','b03c83ee-39f2-4f88-b4c4-7c276b1aad99')


Print 'Add DSE Checklist - Section 9'

INSERT INTO [BusinessSafe].[dbo].[Section] 
([Id], [ChecklistId],[Title],[ListOrder],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (9,1,'Personal - Have you ever suffered from work related aches or pains in your:',9,0,'2013-02-13','b03c83ee-39f2-4f88-b4c4-7c276b1aad99','2013-02-14','b03c83ee-39f2-4f88-b4c4-7c276b1aad99')


SET IDENTITY_INSERT [BusinessSafe].[dbo].[Section] OFF


SET IDENTITY_INSERT [BusinessSafe].[dbo].[Question] ON


Print 'Add DSE Checklist - Question 0'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (36,1,2,0,1,'Location of Workstation',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')


Print 'Add DSE Checklist - Question 1'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (1,1,3,1,1,'Is the use of DSE a requirement of your work on a daily basis?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 2'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (2,1,3,2,1,'Do you use the DSE for continuous or near continuous spells of an hour or more at a time?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 3'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (3,1,3,3,1,'Does the majority of your display screen work involve the use of a stand-alone portable laptop computer?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 4'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (4,2,3,4,1,'Is the screen located in front of you when using the equipment?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 5'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (5,2,3,5,1,'Is the top of the screen level with your eye level?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 6'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (6,2,3,6,1,'Can the screen be tilted and adjusted to a comfortable position?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 7'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (7,2,3,7,1,'Is the screen free of reflections e.g. windows, overhead lighting?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 8'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (8,3,3,8,1,'Can the keyboard be moved to a comfortable position in front of you?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 9'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (9,3,3,9,1,'Is there sufficient room in front of the keyboard to rest your wrists when not using the keyboard?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 10'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (10,3,3,10,1,'Are the keyboard symbols clear and legible?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 11'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (11,3,3,11,1,'Can you operate the mouse or trackball without reaching?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 12'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (12,3,3,12,1,'Can you operate the mouse or trackball with your hand and wrist resting on the desk?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 13'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (13,3,3,13,1,'Is there adequate space to manoeuvre the mouse?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 14'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (14,4,3,14,1,'Is the height of the chair adjustable?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 15'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (15,4,3,15,1,'Is the backrest adjustable for height and tilt?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 16'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (16,4,3,16,1,'Do you know how to adjust the height of the chair and or the backrest?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 17'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (17,4,3,17,1,'Is the chair fitted with arms?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 18'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (18,4,3,18,1,'If YES: When the chair is correctly adjusted do the arms of the chair come into contact with the desk?  See section on Posture.',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 19'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (19,5,3,19,1,'Is there adequate work surface to allow a flexible arrangement for the screen, keyboard and mouse operation?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 20'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (20,5,3,20,1,'Is there adequate knee room to obtain a comfortable position?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 21'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (21,6,3,21,1,'Is there adequate lighting?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 22'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (22,6,3,22,1,'Is there adequate humidity in the atmosphere?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 23'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (23,6,3,23,1,'Is the work arranged so that there are breaks away from the DSE?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 24'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (24,7,3,24,1,'When positioned to use the keyboard are your upper arms in line with your upper body?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 25'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (25,7,3,25,1,'With your fingers on the keys are your wrists straight?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 26'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (26,7,3,26,1,'When in this position is your back supported by the chair''s backrest?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 27'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (27,7,3,27,1,'When in this position do your feet rest comfortably on the floor without the seat digging into the back of your knees / thighs?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 28'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (28,8,3,28,1,'Have you had your eyes tested for use with DSE?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 30'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (30,9,3,30,1,'Wrists',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 31'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (31,9,3,31,1,'Forearms',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 32'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (32,9,3,32,1,'Neck',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 33'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (33,9,3,33,1,'Eyes',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 34'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (34,9,3,34,1,'Back',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')

Print 'Add DSE Checklist - Question 35'

INSERT INTO [BusinessSafe].[dbo].[Question] ([Id],[SectionId],[QuestionType],[ListOrder],[IsRequired],[Text],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
VALUES (35,9,3,35,1,'Have you ever suffered from epilepsy?',0,'2013-02-13','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99','2013-02-14','B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')


SET IDENTITY_INSERT [BusinessSafe].[dbo].[Question] OFF


END

--//@UNDO
DELETE FROM [BusinessSafe].[dbo].[Checklist]
DELETE FROM [BusinessSafe].[dbo].[Section]
DELETE FROM [BusinessSafe].[dbo].[Question]