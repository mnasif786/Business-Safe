USE [BusinessSafe]

set dateformat ymd

IF NOT EXISTS (SELECT * FROM [BusinessSafe].[dbo].[Checklist] WHERE id = 5)
BEGIN
	SET IDENTITY_INSERT [BusinessSafe].[dbo].[Checklist] ON
	INSERT INTO [BusinessSafe].[dbo].[Checklist]
	([Id], [Title],[Description],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy],[ChecklistRiskAssessmentType])
	VALUES (5,'Fire Risk Assessment Checklist',null, 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', 2)
	SET IDENTITY_INSERT [BusinessSafe].[dbo].[Checklist] OFF
END
GO

IF NOT EXISTS (SELECT * FROM [BusinessSafe].[dbo].[Section] WHERE id > 34)
BEGIN
	SET IDENTITY_INSERT [BusinessSafe].[dbo].[Section] ON
	INSERT INTO [dbo].[Section] ([Id],[ChecklistId],[Title],[ListOrder],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
	VALUES (35, 4, 'Sources Of Fuel', 1, 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [dbo].[Section] ([Id],[ChecklistId],[Title],[ListOrder],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
	VALUES (36, 4, 'Sources Of Heat', 2, 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [dbo].[Section] ([Id],[ChecklistId],[Title],[ListOrder],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
	VALUES (37, 4, 'Staff and People at Risk', 3, 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [dbo].[Section] ([Id],[ChecklistId],[Title],[ListOrder],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
	VALUES (38, 4, 'Means of Escape', 4, 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [dbo].[Section] ([Id],[ChecklistId],[Title],[ListOrder],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
	VALUES (39, 4, 'Fire Fighting Equipment', 5, 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [dbo].[Section] ([Id],[ChecklistId],[Title],[ListOrder],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
	VALUES (40, 4, 'Fire Warning Systems', 6, 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [dbo].[Section] ([Id],[ChecklistId],[Title],[ListOrder],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
	VALUES (41, 4, 'Fire Safety Administration', 7, 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [dbo].[Section] ([Id],[ChecklistId],[Title],[ListOrder],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy])
	VALUES (42, 4, 'Revised or Reviewed', 8, 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	SET IDENTITY_INSERT [BusinessSafe].[dbo].[Section] OFF
END
GO

IF NOT EXISTS (SELECT * FROM [BusinessSafe].[dbo].[Question] WHERE [SectionId] > 34)
BEGIN
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (35, 4, 1, 1, 'Is there a system for controlling the amounts of combustible materials, flammable liquids or gases kept in the workplace?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (35, 4, 2, 1, 'Is the waste control system operating effectively?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (35, 4, 3, 1, 'Are all the combustible materials and flammable liquids or gases stored safely?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (35, 4, 4, 1, 'Is the upholstery of furniture in good condition?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (35, 4, 5, 1, 'Is the workplace free of rubbish and combustible waste materials?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (36, 4, 1, 1, 'Are all heaters fitted with suitable guards and fixed in position away from combustible materials?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (36, 4, 2, 1, 'Are all items of portable electrical equipment inspected regularly and fitted with correctly rated fuses?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (36, 4, 3, 1, 'Is the fixed electrical installation inspected periodically by a competent qualified electrician?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (36, 4, 4, 1, 'Is the use of electrical extension leads and multipoint adaptors kept to a minimum?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (36, 4, 5, 1, 'Are flexes run in safe places, where they will not be damaged?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (36, 4, 6, 1, 'Is there a designated external smoking area provided with adequate ashtrays?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (36, 4, 7, 1, 'Have suitable measures been taken to protect against the risk of arson?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (36, 4, 8, 1, 'Do procedures and practices avoid the use of combustible materials or processes that use heat?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (36, 4, 9, 1, 'Has consideration been given to all cost-effective measures that could be taken to prevent the occurrence of arson?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (37, 4, 1, 1, 'Has an emergency evacuation procedure been developed for use in the event of a fire or other emergency?  Has it been explained to all employees?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (37, 4, 2, 1, 'Does this emergency plan take account of any disabled or vulnerable people who may be in the building – whether as workers or visitors?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (37, 4, 3, 1, 'Do any of these disabled or vulnerable people need a Personal Emergency Evacuation Plan (PEEP)?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (37, 4, 4, 1, 'If the answer to Q17 is YES, have PEEPs been prepared and practiced for all who need them and all who will be required to give assistance during an evacuation?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (38, 4, 1, 1, 'Have measures been taken to ensure that smoke and flames cannot spread from one compartment within the building to another?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (38, 4, 2, 1, 'Is there a sufficient number of exits of suitable width for the people likely to be present? Are they within the maximum recommended travel distances?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (38, 4, 3, 1, 'Do the exits lead to a place of safety?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (38, 4, 4, 1, 'Are all gangways and escape routes free from obstructions?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (38, 4, 5, 1, 'Are the escape routes free from tripping and slipping hazards?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (38, 4, 6, 1, 'Are steps and stairs in a good state of repair?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (38, 4, 7, 1, 'Are final exits always unlocked when the premises are in use?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (38, 4, 8, 1, 'Can the final exits be immediately opened without the use of a key?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (38, 4, 9, 1, 'Do the doors on escape routes open in the direction of travel (i.e. towards the escape route)?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (38, 4, 10, 1, 'Are internal fire doors labelled, as such, and normally kept closed?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (38, 4, 11, 1, 'Are the self-closers on fire doors operating correctly?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (38, 4, 12, 1, 'Are escape routes clearly signed?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (38, 4, 13, 1, 'Are escape routes adequately lit?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (38, 4, 14, 1, 'Where escape lighting is installed is it in working order and is it maintained regularly?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (39, 4, 1, 1, 'Is an adequate number of suitable fire extinguishers provided?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (39, 4, 2, 1, 'Are the fire extinguishers and fire blankets located suitably and ready for use?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (39, 4, 3, 1, 'Are the fire extinguishers serviced annually by a competent company or person?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (40, 4, 1, 1, 'Is there an automatic fire detection and alarm system?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (40, 4, 2, 1, 'Is the fire alarm and any detection system in good working order?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (40, 4, 3, 1, ' Is the fire alarm tested weekly?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (40, 4, 4, 1, 'Can the fire alarm be raised without placing anyone in danger?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (40, 4, 5, 1, 'Are the fire alarm call points clearly visible and unobstructed?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (40, 4, 6, 1, 'Is the fire alarm system connected to a monitoring centre which calls the fire brigade?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (40, 4, 7, 1, 'Is the fire alarm system, and all its components, continuously monitored?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (40, 4, 8, 1, 'Is any fixed fire-fighting installation (e.g. sprinkler system) in working order?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (41, 4, 1, 1, 'Have all members of staff been trained in how to call the Fire Authority, the use of the fire extinguishers and basic fire prevention?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (41, 4, 2, 1, 'Have you asked your insurers for advice regarding the fire protection of your premises?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (41, 4, 3, 1, 'Have you told, will you tell your staff or their representatives about your findings?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (41, 4, 4, 1, 'If you have prepared a formal report, has it been shown to your staff or their representatives?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (41, 4, 5, 1, 'If you share the workplace with others, do they know about the risks that you have identified?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (41, 4, 6, 1, 'If you do not have direct control over the workplace have you made your findings known to the owner or landlord?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (41, 4, 7, 1, 'Has an emergency plan been drawn up in case of a major fire?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (41, 4, 8, 1, 'Is a copy of the emergency plan kept somewhere other than at the workplace?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (41, 4, 9, 1, 'Are Fire Action Notices displayed prominently throughout the workplace?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
	INSERT INTO [Question] ([SectionId], [QuestionType], [ListOrder], [IsRequired], [Text], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) VALUES (42, 4, 1, 1, 'Has a procedure been established to review the Fire Risk Assessment periodically?', 0, '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', '2013-02-14', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99')
END
GO

--//@UNDO

DELETE FROM [Question] WHERE [SectionId] > 34
DELETE FROM [Section] WHERE [Id] > 34
DELETE FROM [Checklist] WHERE [Id] = 5
GO