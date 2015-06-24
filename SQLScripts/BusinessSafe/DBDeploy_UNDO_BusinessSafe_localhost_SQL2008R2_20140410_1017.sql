-- Script generated at 2014-04-10T10:17:48



--------------- Fragment begins: #673: 673_Add_NonEmployee_details_to_AccidentRecordNotificationMembers.sql ---------------

-- Change script: #673: 673_Add_NonEmployee_details_to_AccidentRecordNotificationMembers.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' 
AND TABLE_NAME = 'AccidentRecordNotificationMember' AND COLUMN_NAME = 'NonEmployeeEmail')
BEGIN
	ALTER TABLE [AccidentRecordNotificationMember]
	DROP COLUMN [NonEmployeeEmail]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' 
AND TABLE_NAME = 'AccidentRecordNotificationMember' AND COLUMN_NAME = 'NonEmployeeName')
BEGIN
	ALTER TABLE [AccidentRecordNotificationMember]
	DROP COLUMN [NonEmployeeName]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' 
AND TABLE_NAME = 'AccidentRecordNotificationMember' AND COLUMN_NAME = 'Discriminator')
BEGIN
	ALTER TABLE [AccidentRecordNotificationMember]
	DROP COLUMN [Discriminator]
END
GO

DELETE FROM changelog WHERE change_number = 673 AND delta_set = 'Main'
GO

--------------- Fragment ends: #673: 673_Add_NonEmployee_details_to_AccidentRecordNotificationMembers.sql ---------------

--------------- Fragment begins: #672: 672_Add_Email_Notification_Sent_Parameter.sql ---------------

-- Change script: #672: 672_Add_Email_Notification_Sent_Parameter.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'AccidentRecord' AND COLUMN_NAME = 'EmailNotificationSent')
BEGIN
	ALTER TABLE [AccidentRecord]
	DROP COLUMN [EmailNotificationSent]
END
GO

DELETE FROM changelog WHERE change_number = 672 AND delta_set = 'Main'
GO

--------------- Fragment ends: #672: 672_Add_Email_Notification_Sent_Parameter.sql ---------------

--------------- Fragment begins: #671: 671_Add_Accident_Email_Notification_Parameter.sql ---------------

-- Change script: #671: 671_Add_Accident_Email_Notification_Parameter.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'AccidentRecord' AND COLUMN_NAME = 'DoNotSendEmailNotification')
BEGIN
	ALTER TABLE [AccidentRecord]
	DROP COLUMN [DoNotSendEmailNotification]
END
GO

DELETE FROM changelog WHERE change_number = 671 AND delta_set = 'Main'
GO

--------------- Fragment ends: #671: 671_Add_Accident_Email_Notification_Parameter.sql ---------------

--------------- Fragment begins: #670: 670_Add identity colum to AR notification member table.sql ---------------

-- Change script: #670: 670_Add identity colum to AR notification member table.sql

DELETE FROM changelog WHERE change_number = 670 AND delta_set = 'Main'
GO

--------------- Fragment ends: #670: 670_Add identity colum to AR notification member table.sql ---------------

--------------- Fragment begins: #669: 669_Create AR notification member table.sql ---------------

-- Change script: #669: 669_Create AR notification member table.sql

DELETE FROM changelog WHERE change_number = 669 AND delta_set = 'Main'
GO

--------------- Fragment ends: #669: 669_Create AR notification member table.sql ---------------

--------------- Fragment begins: #668: 668_ALTER_Timescales.sql ---------------

-- Change script: #668: 668_ALTER_Timescales.sql

DELETE FROM changelog WHERE change_number = 668 AND delta_set = 'Main'
GO

--------------- Fragment ends: #668: 668_ALTER_Timescales.sql ---------------

--------------- Fragment begins: #667: 667_Insert_Timescales.sql ---------------

-- Change script: #667: 667_Insert_Timescales.sql

DELETE FROM changelog WHERE change_number = 667 AND delta_set = 'Main'
GO

--------------- Fragment ends: #667: 667_Insert_Timescales.sql ---------------

--------------- Fragment begins: #666: 666_ALTER_SafecheckFavouriteChecklist_Add_Columns_Title.sql ---------------

-- Change script: #666: 666_ALTER_SafecheckFavouriteChecklist_Add_Columns_Title.sql

DELETE FROM changelog WHERE change_number = 666 AND delta_set = 'Main'
GO

--------------- Fragment ends: #666: 666_ALTER_SafecheckFavouriteChecklist_Add_Columns_Title.sql ---------------

--------------- Fragment begins: #665: 665_Create_SafeCheckFavouriteChecklists_table.sql ---------------

-- Change script: #665: 665_Create_SafeCheckFavouriteChecklists_table.sql

DELETE FROM changelog WHERE change_number = 665 AND delta_set = 'Main'
GO

--------------- Fragment ends: #665: 665_Create_SafeCheckFavouriteChecklists_table.sql ---------------

--------------- Fragment begins: #664: 664_ALTER_SafecheckChecklistAnswer_Columns_Max_Length.sql ---------------

-- Change script: #664: 664_ALTER_SafecheckChecklistAnswer_Columns_Max_Length.sql

DELETE FROM changelog WHERE change_number = 664 AND delta_set = 'Main'
GO

--------------- Fragment ends: #664: 664_ALTER_SafecheckChecklistAnswer_Columns_Max_Length.sql ---------------

--------------- Fragment begins: #663: 663_INSERT_ImpressionType_For_IRNServed.sql ---------------

-- Change script: #663: 663_INSERT_ImpressionType_For_IRNServed.sql

DELETE FROM changelog WHERE change_number = 663 AND delta_set = 'Main'
GO

--------------- Fragment ends: #663: 663_INSERT_ImpressionType_For_IRNServed.sql ---------------

--------------- Fragment begins: #662: 662_Update_OtherEmailAddress.sql ---------------

-- Change script: #662: 662_Update_OtherEmailAddress.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistOtherEmails' AND COLUMN_NAME = 'Name')
BEGIN
	ALTER TABLE SafeCheckChecklistOtherEmails
	DROP COLUMN Name  
END 

DELETE FROM changelog WHERE change_number = 662 AND delta_set = 'Main'
GO

--------------- Fragment ends: #662: 662_Update_OtherEmailAddress.sql ---------------

--------------- Fragment begins: #661: 661_ADD_ClientLogoFilename_Column.sql ---------------

-- Change script: #661: 661_ADD_ClientLogoFilename_Column.sql

DELETE FROM changelog WHERE change_number = 661 AND delta_set = 'Main'
GO

--------------- Fragment ends: #661: 661_ADD_ClientLogoFilename_Column.sql ---------------

--------------- Fragment begins: #660: 660_Add_NoLongerRequired_Column_to_Action.sql ---------------

-- Change script: #660: 660_Add_NoLongerRequired_Column_to_Action.sql

DELETE FROM changelog WHERE change_number = 660 AND delta_set = 'Main'
GO

--------------- Fragment ends: #660: 660_Add_NoLongerRequired_Column_to_Action.sql ---------------

--------------- Fragment begins: #659: 659_Permissions_ChecklistOtherEmail_table.sql ---------------

-- Change script: #659: 659_Permissions_ChecklistOtherEmail_table.sql

DELETE FROM changelog WHERE change_number = 659 AND delta_set = 'Main'
GO

--------------- Fragment ends: #659: 659_Permissions_ChecklistOtherEmail_table.sql ---------------

--------------- Fragment begins: #658: 658_Create_ChecklistOtherEmail_table.sql ---------------

-- Change script: #658: 658_Create_ChecklistOtherEmail_table.sql

DELETE FROM changelog WHERE change_number = 658 AND delta_set = 'Main'
GO

--------------- Fragment ends: #658: 658_Create_ChecklistOtherEmail_table.sql ---------------

--------------- Fragment begins: #657: 657_Add_deleteby_deletedon_columns.sql ---------------

-- Change script: #657: 657_Add_deleteby_deletedon_columns.sql

DELETE FROM changelog WHERE change_number = 657 AND delta_set = 'Main'
GO

--------------- Fragment ends: #657: 657_Add_deleteby_deletedon_columns.sql ---------------

--------------- Fragment begins: #656: 656_Add_ExecSummaryUpdateRequired_column.sql ---------------

-- Change script: #656: 656_Add_ExecSummaryUpdateRequired_column.sql

DELETE FROM changelog WHERE change_number = 656 AND delta_set = 'Main'
GO

--------------- Fragment ends: #656: 656_Add_ExecSummaryUpdateRequired_column.sql ---------------

--------------- Fragment begins: #655: 655_Replace dodgy characters in safety phrases.sql ---------------

-- Change script: #655: 655_Replace dodgy characters in safety phrases.sql

DELETE FROM changelog WHERE change_number = 655 AND delta_set = 'Main'
GO

--------------- Fragment ends: #655: 655_Replace dodgy characters in safety phrases.sql ---------------

--------------- Fragment begins: #654: 654_Replace degree symbols in safety phrases.sql ---------------

-- Change script: #654: 654_Replace degree symbols in safety phrases.sql

DELETE FROM changelog WHERE change_number = 654 AND delta_set = 'Main'
GO

--------------- Fragment ends: #654: 654_Replace degree symbols in safety phrases.sql ---------------

--------------- Fragment begins: #653: 653_Add_Jurisdiction_colum.sql ---------------

-- Change script: #653: 653_Add_Jurisdiction_colum.sql

DELETE FROM changelog WHERE change_number = 653 AND delta_set = 'Main'
GO

--------------- Fragment ends: #653: 653_Add_Jurisdiction_colum.sql ---------------

--------------- Fragment begins: #652: 652_Increase_length_of_fields_in_ActionPlam.sql ---------------

-- Change script: #652: 652_Increase_length_of_fields_in_ActionPlam.sql

DELETE FROM changelog WHERE change_number = 652 AND delta_set = 'Main'
GO

--------------- Fragment ends: #652: 652_Increase_length_of_fields_in_ActionPlam.sql ---------------

--------------- Fragment begins: #651: 651_ADD_IncludeActionPlan_and_IncludeComplianceReview_Columns.sql ---------------

-- Change script: #651: 651_ADD_IncludeActionPlan_and_IncludeComplianceReview_Columns.sql

DELETE FROM changelog WHERE change_number = 651 AND delta_set = 'Main'
GO

--------------- Fragment ends: #651: 651_ADD_IncludeActionPlan_and_IncludeComplianceReview_Columns.sql ---------------

--------------- Fragment begins: #650: 650_UPDATE_Permissions.sql ---------------

-- Change script: #650: 650_UPDATE_Permissions.sql

DELETE FROM changelog WHERE change_number = 650 AND delta_set = 'Main'
GO

--------------- Fragment ends: #650: 650_UPDATE_Permissions.sql ---------------

--------------- Fragment begins: #649: 649_Update_SafeCheckReportLetterStatementCategory.sql ---------------

-- Change script: #649: 649_Update_SafeCheckReportLetterStatementCategory.sql

update SafeCheckReportLetterStatementCategory set Name = 'Management of Practices and Procedures' where Name = 'Work Processes'
update SafeCheckReportLetterStatementCategory set Name = 'Health and Safety Risk Management' where Name = 'Risk Management'
update SafeCheckReportLetterStatementCategory set Name = 'Management of Health and Safety Documentation' where Name = 'Documentation'
update SafeCheckReportLetterStatementCategory set Name = 'Management of the Premises' where Name = 'Work Premises'

delete from SafeCheckReportLetterStatementCategory where name = 'Work Equipment'

GO

DELETE FROM changelog WHERE change_number = 649 AND delta_set = 'Main'
GO

--------------- Fragment ends: #649: 649_Update_SafeCheckReportLetterStatementCategory.sql ---------------

--------------- Fragment begins: #648: 648_Create SafeCheckMaintenanceUser_Table.sql ---------------

-- Change script: #648: 648_Create SafeCheckMaintenanceUser_Table.sql

DELETE FROM changelog WHERE change_number = 648 AND delta_set = 'Main'
GO

--------------- Fragment ends: #648: 648_Create SafeCheckMaintenanceUser_Table.sql ---------------

--------------- Fragment begins: #647: 647_Create_PersonSeen_table.sql ---------------

-- Change script: #647: 647_Create_PersonSeen_table.sql

DELETE FROM changelog WHERE change_number = 647 AND delta_set = 'Main'
GO

--------------- Fragment ends: #647: 647_Create_PersonSeen_table.sql ---------------

--------------- Fragment begins: #646: 646_Remove_duplicate_template_questions.sql ---------------

-- Change script: #646: 646_Remove_duplicate_template_questions.sql

DELETE FROM changelog WHERE change_number = 646 AND delta_set = 'Main'
GO

--------------- Fragment ends: #646: 646_Remove_duplicate_template_questions.sql ---------------

--------------- Fragment begins: #645: 645_Insert_SafeCheckQaAdvisors.sql ---------------

-- Change script: #645: 645_Insert_SafeCheckQaAdvisors.sql

DELETE FROM changelog WHERE change_number = 645 AND delta_set = 'Main'
GO

--------------- Fragment ends: #645: 645_Insert_SafeCheckQaAdvisors.sql ---------------

--------------- Fragment begins: #644: 644_UpdateSafeCheckConsultant.sql ---------------

-- Change script: #644: 644_UpdateSafeCheckConsultant.sql

DELETE FROM changelog WHERE change_number = 644 AND delta_set = 'Main'
GO

--------------- Fragment ends: #644: 644_UpdateSafeCheckConsultant.sql ---------------

--------------- Fragment begins: #643: 643_UPDATE_UserAdminPermissions.sql ---------------

-- Change script: #643: 643_UPDATE_UserAdminPermissions.sql

DELETE FROM changelog WHERE change_number = 643 AND delta_set = 'Main'
GO

--------------- Fragment ends: #643: 643_UPDATE_UserAdminPermissions.sql ---------------

--------------- Fragment begins: #642: 642_Create SafeCheckChecklistUpdatesRequired_Table.sql ---------------

-- Change script: #642: 642_Create SafeCheckChecklistUpdatesRequired_Table.sql

DELETE FROM changelog WHERE change_number = 642 AND delta_set = 'Main'
GO

--------------- Fragment ends: #642: 642_Create SafeCheckChecklistUpdatesRequired_Table.sql ---------------

--------------- Fragment begins: #641: 641_Add_QaAdvisorAssignedOn_colum.sql ---------------

-- Change script: #641: 641_Add_QaAdvisorAssignedOn_colum.sql
IF EXISTS ( SELECT  * FROM    sys.columns AS c WHERE   c.object_id = OBJECT_ID('SafeCheckChecklist') AND c.name = 'QaAdvisorAssignedOn' ) 
BEGIN
    ALTER TABLE dbo.SafeCheckChecklist DROP COLUMN QaAdvisorAssignedOn
END
GO




DELETE FROM changelog WHERE change_number = 641 AND delta_set = 'Main'
GO

--------------- Fragment ends: #641: 641_Add_QaAdvisorAssignedOn_colum.sql ---------------

--------------- Fragment begins: #640: 640_UPDATE_UserAdminPermissions.sql ---------------

-- Change script: #640: 640_UPDATE_UserAdminPermissions.sql

DELETE FROM changelog WHERE change_number = 640 AND delta_set = 'Main'
GO

--------------- Fragment ends: #640: 640_UPDATE_UserAdminPermissions.sql ---------------

--------------- Fragment begins: #639: 639_UPDATE_UserAdminPermissions.sql ---------------

-- Change script: #639: 639_UPDATE_UserAdminPermissions.sql

DELETE FROM changelog WHERE change_number = 639 AND delta_set = 'Main'
GO

--------------- Fragment ends: #639: 639_UPDATE_UserAdminPermissions.sql ---------------

--------------- Fragment begins: #638: 638_Add Supporting Documentation columns.sql ---------------

-- Change script: #638: 638_Add Supporting Documentation columns.sql

DELETE FROM changelog WHERE change_number = 638 AND delta_set = 'Main'
GO

--------------- Fragment ends: #638: 638_Add Supporting Documentation columns.sql ---------------

--------------- Fragment begins: #637: 637_Create index Ix_Employee_Name_Title.sql ---------------

-- Change script: #637: 637_Create index Ix_Employee_Name_Title.sql

DELETE FROM changelog WHERE change_number = 637 AND delta_set = 'Main'
GO

--------------- Fragment ends: #637: 637_Create index Ix_Employee_Name_Title.sql ---------------

--------------- Fragment begins: #636: 636_Create lastQaAdvisor table.sql ---------------

-- Change script: #636: 636_Create lastQaAdvisor table.sql

DELETE FROM changelog WHERE change_number = 636 AND delta_set = 'Main'
GO

--------------- Fragment ends: #636: 636_Create lastQaAdvisor table.sql ---------------

--------------- Fragment begins: #635: 635_Insert_HSConsultants.sql ---------------

-- Change script: #635: 635_Insert_HSConsultants.sql

DELETE FROM changelog WHERE change_number = 635 AND delta_set = 'Main'
GO

--------------- Fragment ends: #635: 635_Insert_HSConsultants.sql ---------------

--------------- Fragment begins: #634: 634_Create_consultants_table.sql ---------------

-- Change script: #634: 634_Create_consultants_table.sql

DELETE FROM changelog WHERE change_number = 634 AND delta_set = 'Main'
GO

--------------- Fragment ends: #634: 634_Create_consultants_table.sql ---------------

--------------- Fragment begins: #633: 633_Add_QaAdvisor_InRotation_column.sql ---------------

-- Change script: #633: 633_Add_QaAdvisor_InRotation_column.sql

DELETE FROM changelog WHERE change_number = 633 AND delta_set = 'Main'
GO

--------------- Fragment ends: #633: 633_Add_QaAdvisor_InRotation_column.sql ---------------

--------------- Fragment begins: #632: 632_INSERT_AccidentTypes_for_BAN065.sql ---------------

-- Change script: #632: 632_INSERT_AccidentTypes_for_BAN065.sql

DELETE FROM changelog WHERE change_number = 632 AND delta_set = 'Main'
GO

--------------- Fragment ends: #632: 632_INSERT_AccidentTypes_for_BAN065.sql ---------------

--------------- Fragment begins: #631: 631_Add_CompanyID_column_to_AccidentType.sql ---------------

-- Change script: #631: 631_Add_CompanyID_column_to_AccidentType.sql

DELETE FROM changelog WHERE change_number = 631 AND delta_set = 'Main'
GO

--------------- Fragment ends: #631: 631_Add_CompanyID_column_to_AccidentType.sql ---------------

--------------- Fragment begins: #630: 630_INSERT_SafeCheckQaAdvisor.sql ---------------

-- Change script: #630: 630_INSERT_SafeCheckQaAdvisor.sql

IF EXISTS( SELECT * FROM SafeCheckQaAdvisor WHERE Email = 'george.cooper@peninsula-uk.com')
BEGIN
	DELETE FROM [dbo].[SafeCheckQaAdvisor] WHERE [Email] = 'george.cooper@peninsula-uk.com'
END


select * from [SafeCheckQaAdvisor]


DELETE FROM changelog WHERE change_number = 630 AND delta_set = 'Main'
GO

--------------- Fragment ends: #630: 630_INSERT_SafeCheckQaAdvisor.sql ---------------

--------------- Fragment begins: #629: 629_Add_report_header_column.sql ---------------

-- Change script: #629: 629_Add_report_header_column.sql

DELETE FROM changelog WHERE change_number = 629 AND delta_set = 'Main'
GO

--------------- Fragment ends: #629: 629_Add_report_header_column.sql ---------------

--------------- Fragment begins: #628: 628_Add area of non complaince to safecheck answer.sql ---------------

-- Change script: #628: 628_Add area of non complaince to safecheck answer.sql

DELETE FROM changelog WHERE change_number = 628 AND delta_set = 'Main'
GO

--------------- Fragment ends: #628: 628_Add area of non complaince to safecheck answer.sql ---------------

--------------- Fragment begins: #627: 627_Add_template_type_column.sql ---------------

-- Change script: #627: 627_Add_template_type_column.sql

DELETE FROM changelog WHERE change_number = 627 AND delta_set = 'Main'
GO

--------------- Fragment ends: #627: 627_Add_template_type_column.sql ---------------

--------------- Fragment begins: #626: 626_Add_Draft_Column_to_Industry.sql ---------------

-- Change script: #626: 626_Add_Draft_Column_to_Industry.sql

DELETE FROM changelog WHERE change_number = 626 AND delta_set = 'Main'
GO

--------------- Fragment ends: #626: 626_Add_Draft_Column_to_Industry.sql ---------------

--------------- Fragment begins: #625: 625_Add_QaCommentsResolved.sql ---------------

-- Change script: #625: 625_Add_QaCommentsResolved.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCheckListAnswer' AND COLUMN_NAME = 'QaCommentsResolved')
BEGIN
	ALTER TABLE [SafeCheckCheckListAnswer]
	DROP COLUMN [QaCommentsResolved] 
END
GO

DELETE FROM changelog WHERE change_number = 625 AND delta_set = 'Main'
GO

--------------- Fragment ends: #625: 625_Add_QaCommentsResolved.sql ---------------

--------------- Fragment begins: #624: 624_Increase lengths of text fields.sql ---------------

-- Change script: #624: 624_Increase lengths of text fields.sql

DELETE FROM changelog WHERE change_number = 624 AND delta_set = 'Main'
GO

--------------- Fragment ends: #624: 624_Increase lengths of text fields.sql ---------------

--------------- Fragment begins: #623: 623_Update_report_letter_statements.sql ---------------

-- Change script: #623: 623_Update_report_letter_statements.sql

DELETE FROM changelog WHERE change_number = 623 AND delta_set = 'Main'
GO

--------------- Fragment ends: #623: 623_Update_report_letter_statements.sql ---------------

--------------- Fragment begins: #622: 622_Add_qaSignedOffDate.sql ---------------

-- Change script: #622: 622_Add_qaSignedOffDate.sql

DELETE FROM changelog WHERE change_number = 622 AND delta_set = 'Main'
GO

--------------- Fragment ends: #622: 622_Add_qaSignedOffDate.sql ---------------

--------------- Fragment begins: #621: 621_Add_ExecutiveSummaryDocumentLibraryId_Column_to_checklist.sql ---------------

-- Change script: #621: 621_Add_ExecutiveSummaryDocumentLibraryId_Column_to_checklist.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'ExecutiveSummaryDocumentLibraryId')
BEGIN
	ALTER TABLE [SafeCheckCheckList]
	DROP [ExecutiveSummaryDocumentLibraryId]
END
GO

DELETE FROM changelog WHERE change_number = 621 AND delta_set = 'Main'
GO

--------------- Fragment ends: #621: 621_Add_ExecutiveSummaryDocumentLibraryId_Column_to_checklist.sql ---------------

--------------- Fragment begins: #620: 620_UPDATE_ActionPlan_VisitBy_value.sql ---------------

-- Change script: #620: 620_UPDATE_ActionPlan_VisitBy_value.sql

DELETE FROM changelog WHERE change_number = 620 AND delta_set = 'Main'
GO

--------------- Fragment ends: #620: 620_UPDATE_ActionPlan_VisitBy_value.sql ---------------

--------------- Fragment begins: #619: 619_UpdatePersonSeen.sql ---------------

-- Change script: #619: 619_UpdatePersonSeen.sql

DELETE FROM changelog WHERE change_number = 619 AND delta_set = 'Main'
GO

--------------- Fragment ends: #619: 619_UpdatePersonSeen.sql ---------------

--------------- Fragment begins: #618: 618_ALTER_Action_AreaOfNonCompliance.sql ---------------

-- Change script: #618: 618_ALTER_Action_AreaOfNonCompliance.sql

DELETE FROM changelog WHERE change_number = 618 AND delta_set = 'Main'
GO

--------------- Fragment ends: #618: 618_ALTER_Action_AreaOfNonCompliance.sql ---------------

--------------- Fragment begins: #617: 617_UPDATE_LastModifiedBy_and_LastModifiedOn_for_Action_and_ActionPlan.sql ---------------

-- Change script: #617: 617_UPDATE_LastModifiedBy_and_LastModifiedOn_for_Action_and_ActionPlan.sql

DELETE FROM changelog WHERE change_number = 617 AND delta_set = 'Main'
GO

--------------- Fragment ends: #617: 617_UPDATE_LastModifiedBy_and_LastModifiedOn_for_Action_and_ActionPlan.sql ---------------

--------------- Fragment begins: #616: 616_Drop_Action_Status_Column.sql ---------------

-- Change script: #616: 616_Drop_Action_Status_Column.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'Action' AND COLUMN_NAME = 'Status')
BEGIN
	ALTER TABLE Action
	ADD Status NVARCHAR(200) NULL
END
GO

DELETE FROM changelog WHERE change_number = 616 AND delta_set = 'Main'
GO

--------------- Fragment ends: #616: 616_Drop_Action_Status_Column.sql ---------------

--------------- Fragment begins: #615: 615_Add_ExecutiveSummaryDocumentLibraryId_Column.sql ---------------

-- Change script: #615: 615_Add_ExecutiveSummaryDocumentLibraryId_Column.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'ActionPlan' AND COLUMN_NAME = 'ExecutiveSummaryDocumentLibraryId')
BEGIN
	ALTER TABLE [ActionPlan]
	DROP [ExecutiveSummaryDocumentLibraryId]
END
GO

DELETE FROM changelog WHERE change_number = 615 AND delta_set = 'Main'
GO

--------------- Fragment ends: #615: 615_Add_ExecutiveSummaryDocumentLibraryId_Column.sql ---------------

--------------- Fragment begins: #614: 614_SafeCheckCheckListAnswer_Index.sql ---------------

-- Change script: #614: 614_SafeCheckCheckListAnswer_Index.sql

DELETE FROM changelog WHERE change_number = 614 AND delta_set = 'Main'
GO

--------------- Fragment ends: #614: 614_SafeCheckCheckListAnswer_Index.sql ---------------

--------------- Fragment begins: #613: 613_Increase action ActionRequired length.sql ---------------

-- Change script: #613: 613_Increase action ActionRequired length.sql

DELETE FROM changelog WHERE change_number = 613 AND delta_set = 'Main'
GO

--------------- Fragment ends: #613: 613_Increase action ActionRequired length.sql ---------------

--------------- Fragment begins: #612: 612_Add_PersonSeenEmployeeId_Column.sql ---------------

-- Change script: #612: 612_Add_PersonSeenEmployeeId_Column.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'PersonSeenId')
BEGIN
	ALTER TABLE [SafeCheckCheckList]
	DROP [PersonSeenId]
END
GO

DELETE FROM changelog WHERE change_number = 612 AND delta_set = 'Main'
GO

--------------- Fragment ends: #612: 612_Add_PersonSeenEmployeeId_Column.sql ---------------

--------------- Fragment begins: #611: 611_ALTER SafecheckChecklist.sql ---------------

-- Change script: #611: 611_ALTER SafecheckChecklist.sql

DELETE FROM changelog WHERE change_number = 611 AND delta_set = 'Main'
GO

--------------- Fragment ends: #611: 611_ALTER SafecheckChecklist.sql ---------------

--------------- Fragment begins: #610: 610_Add_UpdatesRequired_Column.sql ---------------

-- Change script: #610: 610_Add_UpdatesRequired_Column.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'UpdatesRequired')
BEGIN
	ALTER TABLE [SafeCheckCheckList]
	DROP [UpdatesRequired] 
END
GO
  

DELETE FROM changelog WHERE change_number = 610 AND delta_set = 'Main'
GO

--------------- Fragment ends: #610: 610_Add_UpdatesRequired_Column.sql ---------------

--------------- Fragment begins: #609: 609_Add qaComments column.sql ---------------

-- Change script: #609: 609_Add qaComments column.sql

DELETE FROM changelog WHERE change_number = 609 AND delta_set = 'Main'
GO

--------------- Fragment ends: #609: 609_Add qaComments column.sql ---------------

--------------- Fragment begins: #608: 608_Insert acceptable response for safecheck question.sql ---------------

-- Change script: #608: 608_Insert acceptable response for safecheck question.sql

DELETE FROM changelog WHERE change_number = 608 AND delta_set = 'Main'
GO

--------------- Fragment ends: #608: 608_Insert acceptable response for safecheck question.sql ---------------

--------------- Fragment begins: #607: 607_Remove duplicate safecheck question.sql ---------------

-- Change script: #607: 607_Remove duplicate safecheck question.sql

DELETE FROM changelog WHERE change_number = 607 AND delta_set = 'Main'
GO

--------------- Fragment ends: #607: 607_Remove duplicate safecheck question.sql ---------------

--------------- Fragment begins: #606: 606_ALTER SafeCheckCheckListAnswer.sql ---------------

-- Change script: #606: 606_ALTER SafeCheckCheckListAnswer.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCheckListAnswer' AND COLUMN_NAME = 'QaSignedOffBy')
BEGIN
	ALTER TABLE [SafeCheckCheckListAnswer]
	DROP [QaSignedOffBy] 
END
GO

DELETE FROM changelog WHERE change_number = 606 AND delta_set = 'Main'
GO

--------------- Fragment ends: #606: 606_ALTER SafeCheckCheckListAnswer.sql ---------------

--------------- Fragment begins: #605: 605_UPDATE SafeCheck status to assigned.sql ---------------

-- Change script: #605: 605_UPDATE SafeCheck status to assigned.sql

DELETE FROM changelog WHERE change_number = 605 AND delta_set = 'Main'
GO

--------------- Fragment ends: #605: 605_UPDATE SafeCheck status to assigned.sql ---------------

--------------- Fragment begins: #604: 604_UPDATE SafeCheckCategory.sql ---------------

-- Change script: #604: 604_UPDATE SafeCheckCategory.sql

DELETE FROM changelog WHERE change_number = 604 AND delta_set = 'Main'
GO

--------------- Fragment ends: #604: 604_UPDATE SafeCheckCategory.sql ---------------

--------------- Fragment begins: #603: 603_Update_SafeCheckH&SQAAdvisor.sql ---------------

-- Change script: #603: 603_Update_SafeCheckH&SQAAdvisor.sql

IF EXISTS( SELECT * FROM SafeCheckQaAdvisor where Id =  '3A204FB3-1956-4EFC-BE34-89F7897570DB')
BEGIN		
	UPDATE [dbo].[SafeCheckQaAdvisor] set Forename = 'H&SReports' where Id = '3A204FB3-1956-4EFC-BE34-89F7897570DB'
	UPDATE [dbo].[SafeCheckQaAdvisor] set Surname = '' where Id = '3A204FB3-1956-4EFC-BE34-89F7897570DB'
	UPDATE [dbo].[SafeCheckQaAdvisor] set email = 'H&SReports@Peninsula-uk.com'  where Id = '3A204FB3-1956-4EFC-BE34-89F7897570DB'
END
GO

DELETE FROM changelog WHERE change_number = 603 AND delta_set = 'Main'
GO

--------------- Fragment ends: #603: 603_Update_SafeCheckH&SQAAdvisor.sql ---------------

--------------- Fragment begins: #602: 602_INSERT_SafeCheckH&SQaAdvisor.sql ---------------

-- Change script: #602: 602_INSERT_SafeCheckH&SQaAdvisor.sql

IF EXISTS( SELECT * FROM SafeCheckQaAdvisor where Id =  '3A204FB3-1956-4EFC-BE34-89F7897570DB')
BEGIN		
	DELETE FROM [dbo].[SafeCheckQaAdvisor] WHERE Id = '3A204FB3-1956-4EFC-BE34-89F7897570DB'
END
GO

DELETE FROM changelog WHERE change_number = 602 AND delta_set = 'Main'
GO

--------------- Fragment ends: #602: 602_INSERT_SafeCheckH&SQaAdvisor.sql ---------------

--------------- Fragment begins: #601: 601_UPDATE SafeCheckQuestion remove degree symbol.sql ---------------

-- Change script: #601: 601_UPDATE SafeCheckQuestion remove degree symbol.sql

DELETE FROM changelog WHERE change_number = 601 AND delta_set = 'Main'
GO

--------------- Fragment ends: #601: 601_UPDATE SafeCheckQuestion remove degree symbol.sql ---------------

--------------- Fragment begins: #600: 600_UPDATE SafeCheckQuestion.sql ---------------

-- Change script: #600: 600_UPDATE SafeCheckQuestion.sql

DELETE FROM changelog WHERE change_number = 600 AND delta_set = 'Main'
GO

--------------- Fragment ends: #600: 600_UPDATE SafeCheckQuestion.sql ---------------

--------------- Fragment begins: #599: 599_INSERT_SafeCheckQuestionResponse.sql ---------------

-- Change script: #599: 599_INSERT_SafeCheckQuestionResponse.sql

DELETE FROM changelog WHERE change_number = 599 AND delta_set = 'Main'
GO

--------------- Fragment ends: #599: 599_INSERT_SafeCheckQuestionResponse.sql ---------------

--------------- Fragment begins: #598: 598_INSERT_SafeCheckQuestionResponse.sql ---------------

-- Change script: #598: 598_INSERT_SafeCheckQuestionResponse.sql

DELETE FROM changelog WHERE change_number = 598 AND delta_set = 'Main'
GO

--------------- Fragment ends: #598: 598_INSERT_SafeCheckQuestionResponse.sql ---------------

--------------- Fragment begins: #597: 597_Safecheck indexes.sql ---------------

-- Change script: #597: 597_Safecheck indexes.sql

DELETE FROM changelog WHERE change_number = 597 AND delta_set = 'Main'
GO

--------------- Fragment ends: #597: 597_Safecheck indexes.sql ---------------

--------------- Fragment begins: #596: 596_ALTER SafecheckChecklist.sql ---------------

-- Change script: #596: 596_ALTER SafecheckChecklist.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklist' AND COLUMN_NAME = 'IndustryId')
BEGIN
	ALTER TABLE [SafeCheckChecklist]
	DROP [IndustryId] 
END
GO

DELETE FROM changelog WHERE change_number = 596 AND delta_set = 'Main'
GO

--------------- Fragment ends: #596: 596_ALTER SafecheckChecklist.sql ---------------

--------------- Fragment begins: #595: 595_ALTER SafecheckChecklist.sql ---------------

-- Change script: #595: 595_ALTER SafecheckChecklist.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklist' AND COLUMN_NAME = 'QaAdvisor')
BEGIN
	ALTER TABLE [SafeCheckChecklist]
	DROP [QaAdvisor] 
END
GO

DELETE FROM changelog WHERE change_number = 595 AND delta_set = 'Main'
GO

--------------- Fragment ends: #595: 595_ALTER SafecheckChecklist.sql ---------------

--------------- Fragment begins: #594: 594_ALTER SafeCheckCheckListAnswer.sql ---------------

-- Change script: #594: 594_ALTER SafeCheckCheckListAnswer.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCheckListAnswer' AND COLUMN_NAME = 'QaComments')
BEGIN
	ALTER TABLE [SafeCheckCheckListAnswer]
	DROP COLUMN [QaComments]
END 

DELETE FROM changelog WHERE change_number = 594 AND delta_set = 'Main'
GO

--------------- Fragment ends: #594: 594_ALTER SafeCheckCheckListAnswer.sql ---------------

--------------- Fragment begins: #593: 593_INSERT_SafeCheckQaAdvisor.sql ---------------

-- Change script: #593: 593_INSERT_SafeCheckQaAdvisor.sql


IF EXISTS(SELECT * FROM SafeCheckQaAdvisor where Email =  'David.Brierley@peninsula-uk.com')
BEGIN		
	DELETE FROM SafeCheckQaAdvisor where Email =  'David.Brierley@peninsula-uk.com'
END

IF EXISTS(SELECT * FROM [SafeCheckQaAdvisor] where [Email] = 'Diane.Smith@peninsula-uk.com')
BEGIN		
	DELETE FROM [SafeCheckQaAdvisor] where [Email] = 'Diane.Smith@peninsula-uk.com'
END

IF EXISTS(SELECT * FROM [SafeCheckQaAdvisor] where [Email] = 'Sinead.Lewis@peninsula-uk.com')
BEGIN		
	DELETE FROM [SafeCheckQaAdvisor] where [Email] = 'Sinead.Lewis@peninsula-uk.com'
END

IF EXISTS(SELECT * FROM [SafeCheckQaAdvisor] where [Email] = 'Gary.Armitt@peninsula-uk.com')
BEGIN		
	DELETE FROM [SafeCheckQaAdvisor] where [Email] = 'Gary.Armitt@peninsula-uk.com'
END


IF EXISTS(SELECT * FROM [SafeCheckQaAdvisor] where [Email] = 'Paul.Leather@peninsula-uk.com')
BEGIN		
	DELETE FROM [SafeCheckQaAdvisor] where [Email] = 'Paul.Leather@peninsula-uk.com'
END
GO

DELETE FROM changelog WHERE change_number = 593 AND delta_set = 'Main'
GO

--------------- Fragment ends: #593: 593_INSERT_SafeCheckQaAdvisor.sql ---------------

--------------- Fragment begins: #592: 592_Soft Delete duplicate SafeCheck Questions.sql ---------------

-- Change script: #592: 592_Soft Delete duplicate SafeCheck Questions.sql

DELETE FROM changelog WHERE change_number = 592 AND delta_set = 'Main'
GO

--------------- Fragment ends: #592: 592_Soft Delete duplicate SafeCheck Questions.sql ---------------

--------------- Fragment begins: #591: 591_CREATE_SafeCheckQaAdvisor.sql ---------------

-- Change script: #591: 591_CREATE_SafeCheckQaAdvisor.sql

IF  EXISTS( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQaAdvisor')
BEGIN
	DROP TABLE [SafeCheckQaAdvisor] 
END

DELETE FROM changelog WHERE change_number = 591 AND delta_set = 'Main'
GO

--------------- Fragment ends: #591: 591_CREATE_SafeCheckQaAdvisor.sql ---------------

--------------- Fragment begins: #590: 590_ALTER SafeCheckCheckListQuestion.sql ---------------

-- Change script: #590: 590_ALTER SafeCheckCheckListQuestion.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'QuestionNumber')
BEGIN
	ALTER TABLE SafeCheckCheckListQuestion
	DROP COLUMN [QuestionNumber]
END 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'CategoryNumber')
BEGIN
	ALTER TABLE SafeCheckCheckListQuestion
	DROP COLUMN [CategoryNumber]
END 

DELETE FROM changelog WHERE change_number = 590 AND delta_set = 'Main'
GO

--------------- Fragment ends: #590: 590_ALTER SafeCheckCheckListQuestion.sql ---------------

--------------- Fragment begins: #589: 589_UPDATE SafeCheckCategory Reorder tabs.sql ---------------

-- Change script: #589: 589_UPDATE SafeCheckCategory Reorder tabs.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCategory' AND COLUMN_NAME = 'TabTitle')
BEGIN
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 1 WHERE [Title] = 'Documentation'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 7 WHERE [Title] = 'Equipment'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 4 WHERE [Title] = 'Fire'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 5 WHERE [Title] = 'People Management'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 6 WHERE [Title] = 'Premises Management'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 3 WHERE [Title] = 'Risk Assessments'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 2 WHERE [Title] = 'Safety Arrangements'
	UPDATE [SafeCheckCategory] SET [OrderNumber] = 8 WHERE [Title] = 'Other subjects'
END 

DELETE FROM changelog WHERE change_number = 589 AND delta_set = 'Main'
GO

--------------- Fragment ends: #589: 589_UPDATE SafeCheckCategory Reorder tabs.sql ---------------

--------------- Fragment begins: #558: 558_ALTER SafeCheckCategory.sql ---------------

-- Change script: #558: 558_ALTER SafeCheckCategory.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCategory' AND COLUMN_NAME = 'TabTitle')
BEGIN
	ALTER TABLE [SafeCheckCategory]
	DROP COLUMN [TabTitle]
END 

DELETE FROM changelog WHERE change_number = 558 AND delta_set = 'Main'
GO

--------------- Fragment ends: #558: 558_ALTER SafeCheckCategory.sql ---------------

--------------- Fragment begins: #557: 557_DELETE TestData.sql ---------------

-- Change script: #557: 557_DELETE TestData.sql

DELETE FROM changelog WHERE change_number = 557 AND delta_set = 'Main'
GO

--------------- Fragment ends: #557: 557_DELETE TestData.sql ---------------

--------------- Fragment begins: #556: 556_InsertsafeCheckIndustries.sql ---------------

-- Change script: #556: 556_InsertsafeCheckIndustries.sql

DELETE FROM changelog WHERE change_number = 556 AND delta_set = 'Main'
GO

--------------- Fragment ends: #556: 556_InsertsafeCheckIndustries.sql ---------------

--------------- Fragment begins: #555: 555_InsertQuestionResponses.sql ---------------

-- Change script: #555: 555_InsertQuestionResponses.sql

DELETE FROM changelog WHERE change_number = 555 AND delta_set = 'Main'
GO

--------------- Fragment ends: #555: 555_InsertQuestionResponses.sql ---------------

--------------- Fragment begins: #554: 554_InsertQuestionsScript.sql ---------------

-- Change script: #554: 554_InsertQuestionsScript.sql

DELETE FROM changelog WHERE change_number = 554 AND delta_set = 'Main'
GO

--------------- Fragment ends: #554: 554_InsertQuestionsScript.sql ---------------

--------------- Fragment begins: #553: 553_InsertsafeCheckCategories.sql ---------------

-- Change script: #553: 553_InsertsafeCheckCategories.sql

DELETE FROM changelog WHERE change_number = 553 AND delta_set = 'Main'
GO

--------------- Fragment ends: #553: 553_InsertsafeCheckCategories.sql ---------------

--------------- Fragment begins: #552: 552_Insert safeCheckIndustries.sql ---------------

-- Change script: #552: 552_Insert safeCheckIndustries.sql

DELETE FROM changelog WHERE change_number = 552 AND delta_set = 'Main'
GO

--------------- Fragment ends: #552: 552_Insert safeCheckIndustries.sql ---------------

--------------- Fragment begins: #551: 551_Insert safeCheckCategories.sql ---------------

-- Change script: #551: 551_Insert safeCheckCategories.sql

DELETE FROM changelog WHERE change_number = 551 AND delta_set = 'Main'
GO

--------------- Fragment ends: #551: 551_Insert safeCheckCategories.sql ---------------

--------------- Fragment begins: #550: 550_Insert QuestionsScript.sql ---------------

-- Change script: #550: 550_Insert QuestionsScript.sql

DELETE FROM changelog WHERE change_number = 550 AND delta_set = 'Main'
GO

--------------- Fragment ends: #550: 550_Insert QuestionsScript.sql ---------------

--------------- Fragment begins: #549: 549_Insert QuestionResponses.sql ---------------

-- Change script: #549: 549_Insert QuestionResponses.sql

DELETE FROM changelog WHERE change_number = 549 AND delta_set = 'Main'
GO

--------------- Fragment ends: #549: 549_Insert QuestionResponses.sql ---------------

--------------- Fragment begins: #548: 548_Add order number to safecheck questions.sql ---------------

-- Change script: #548: 548_Add order number to safecheck questions.sql

DELETE FROM changelog WHERE change_number = 548 AND delta_set = 'Main'
GO

--------------- Fragment ends: #548: 548_Add order number to safecheck questions.sql ---------------

--------------- Fragment begins: #547: 547_ALTER SafecheckChecklist add ChecklistAudit.sql ---------------

-- Change script: #547: 547_ALTER SafecheckChecklist add ChecklistAudit.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklist' AND COLUMN_NAME = 'ChecklistLastModifiedBy')
BEGIN
	ALTER TABLE [SafeCheckChecklist]
	DROP [ChecklistLastModifiedBy] 
END
GO

DELETE FROM changelog WHERE change_number = 547 AND delta_set = 'Main'
GO

--------------- Fragment ends: #547: 547_ALTER SafecheckChecklist add ChecklistAudit.sql ---------------

--------------- Fragment begins: #546: 546_AUPDATE SafecheckChecklist.sql ---------------

-- Change script: #546: 546_AUPDATE SafecheckChecklist.sql

DELETE FROM changelog WHERE change_number = 546 AND delta_set = 'Main'
GO

--------------- Fragment ends: #546: 546_AUPDATE SafecheckChecklist.sql ---------------

--------------- Fragment begins: #545: 545_ALTER SafecheckCategory.sql ---------------

-- Change script: #545: 545_ALTER SafecheckCategory.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCategory' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [SafeCheckCategory]
	DROP COLUMN [CreatedBy]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCategory' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [SafeCheckCategory]
	DROP COLUMN [CreatedOn]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCategory' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [SafeCheckCategory]
	DROP COLUMN [LastModifiedBy] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCategory' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [SafeCheckCategory]
	DROP COLUMN [LastModifiedOn] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckCategory' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [SafeCheckCategory]
	DROP COLUMN [Deleted]
END
GO


DELETE FROM changelog WHERE change_number = 545 AND delta_set = 'Main'
GO

--------------- Fragment ends: #545: 545_ALTER SafecheckCategory.sql ---------------

--------------- Fragment begins: #544: 544_ALTER Timescale.sql ---------------

-- Change script: #544: 544_ALTER Timescale.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'Timescale' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [Timescale]
	DROP COLUMN [CreatedBy]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'Timescale' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [Timescale]
	DROP COLUMN [CreatedOn]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'Timescale' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [Timescale]
	DROP COLUMN [LastModifiedBy] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'Timescale' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [Timescale]
	DROP COLUMN [LastModifiedOn] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'Timescale' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [Timescale]
	DROP COLUMN [Deleted]
END
GO


DELETE FROM changelog WHERE change_number = 544 AND delta_set = 'Main'
GO

--------------- Fragment ends: #544: 544_ALTER Timescale.sql ---------------

--------------- Fragment begins: #543: 543_ALTER SafecheckReportLetterStatementCategory.sql ---------------

-- Change script: #543: 543_ALTER SafecheckReportLetterStatementCategory.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckReportLetterStatementCategory' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [SafeCheckReportLetterStatementCategory]
	DROP COLUMN [CreatedBy]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckReportLetterStatementCategory' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [SafeCheckReportLetterStatementCategory]
	DROP COLUMN [CreatedOn]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckReportLetterStatementCategory' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [SafeCheckReportLetterStatementCategory]
	DROP COLUMN [LastModifiedBy] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckReportLetterStatementCategory' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [SafeCheckReportLetterStatementCategory]
	DROP COLUMN [LastModifiedOn] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckReportLetterStatementCategory' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [SafeCheckReportLetterStatementCategory]
	DROP COLUMN [Deleted]
END
GO


DELETE FROM changelog WHERE change_number = 543 AND delta_set = 'Main'
GO

--------------- Fragment ends: #543: 543_ALTER SafecheckReportLetterStatementCategory.sql ---------------

--------------- Fragment begins: #542: 542_ALTER SafecheckIndustryQuestion.sql ---------------

-- Change script: #542: 542_ALTER SafecheckIndustryQuestion.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckIndustryQuestion' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [SafeCheckIndustryQuestion]
	DROP COLUMN [CreatedBy]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckIndustryQuestion' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [SafeCheckIndustryQuestion]
	DROP COLUMN [CreatedOn]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckIndustryQuestion' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [SafeCheckIndustryQuestion]
	DROP COLUMN [LastModifiedBy] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckIndustryQuestion' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [SafeCheckIndustryQuestion]
	DROP COLUMN [LastModifiedOn] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckIndustryQuestion' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [SafeCheckIndustryQuestion]
	DROP COLUMN [Deleted]
END
GO


DELETE FROM changelog WHERE change_number = 542 AND delta_set = 'Main'
GO

--------------- Fragment ends: #542: 542_ALTER SafecheckIndustryQuestion.sql ---------------

--------------- Fragment begins: #541: 541_ALTER SafecheckIndustry.sql ---------------

-- Change script: #541: 541_ALTER SafecheckIndustry.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckIndustry' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [SafeCheckIndustry]
	DROP COLUMN [CreatedBy]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckIndustry' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [SafeCheckIndustry]
	DROP COLUMN [CreatedOn]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckIndustry' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [SafeCheckIndustry]
	DROP COLUMN [LastModifiedBy] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckIndustry' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [SafeCheckIndustry]
	DROP COLUMN [LastModifiedOn] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckIndustry' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [SafeCheckIndustry]
	DROP COLUMN [Deleted]
END
GO


DELETE FROM changelog WHERE change_number = 541 AND delta_set = 'Main'
GO

--------------- Fragment ends: #541: 541_ALTER SafecheckIndustry.sql ---------------

--------------- Fragment begins: #540: 540_ALTER SafecheckImpressionType.sql ---------------

-- Change script: #540: 540_ALTER SafecheckImpressionType.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckImpressionType' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [SafeCheckImpressionType]
	DROP COLUMN [CreatedBy]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckImpressionType' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [SafeCheckImpressionType]
	DROP COLUMN [CreatedOn]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckImpressionType' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [SafeCheckImpressionType]
	DROP COLUMN [LastModifiedBy] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckImpressionType' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [SafeCheckImpressionType]
	DROP COLUMN [LastModifiedOn] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckImpressionType' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [SafeCheckImpressionType]
	DROP COLUMN [Deleted]
END
GO


DELETE FROM changelog WHERE change_number = 540 AND delta_set = 'Main'
GO

--------------- Fragment ends: #540: 540_ALTER SafecheckImpressionType.sql ---------------

--------------- Fragment begins: #539: 539_ALTER SafecheckImmediateRiskNotification.sql ---------------

-- Change script: #539: 539_ALTER SafecheckImmediateRiskNotification.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckImmediateRiskNotification' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [SafeCheckImmediateRiskNotification]
	DROP COLUMN [CreatedBy]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckImmediateRiskNotification' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [SafeCheckImmediateRiskNotification]
	DROP COLUMN [CreatedOn]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckImmediateRiskNotification' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [SafeCheckImmediateRiskNotification]
	DROP COLUMN [LastModifiedBy] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckImmediateRiskNotification' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [SafeCheckImmediateRiskNotification]
	DROP COLUMN [LastModifiedOn] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckImmediateRiskNotification' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [SafeCheckImmediateRiskNotification]
	DROP COLUMN [Deleted]
END
GO


DELETE FROM changelog WHERE change_number = 539 AND delta_set = 'Main'
GO

--------------- Fragment ends: #539: 539_ALTER SafecheckImmediateRiskNotification.sql ---------------

--------------- Fragment begins: #538: 538_ALTER SafecheckClientQuestion.sql ---------------

-- Change script: #538: 538_ALTER SafecheckClientQuestion.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckClientQuestion' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [SafeCheckClientQuestion]
	DROP COLUMN [CreatedBy]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckClientQuestion' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [SafeCheckClientQuestion]
	DROP COLUMN [CreatedOn]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckClientQuestion' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [SafeCheckClientQuestion]
	DROP COLUMN [LastModifiedBy] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckClientQuestion' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [SafeCheckClientQuestion]
	DROP COLUMN [LastModifiedOn] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckClientQuestion' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [SafeCheckClientQuestion]
	DROP COLUMN [Deleted]
END
GO


DELETE FROM changelog WHERE change_number = 538 AND delta_set = 'Main'
GO

--------------- Fragment ends: #538: 538_ALTER SafecheckClientQuestion.sql ---------------

--------------- Fragment begins: #537: 537_ALTER SafecheckChecklistQuestion.sql ---------------

-- Change script: #537: 537_ALTER SafecheckChecklistQuestion.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistQuestion' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [SafeCheckChecklistQuestion]
	DROP COLUMN [CreatedBy]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistQuestion' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [SafeCheckChecklistQuestion]
	DROP COLUMN [CreatedOn]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistQuestion' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [SafeCheckChecklistQuestion]
	DROP COLUMN [LastModifiedBy] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistQuestion' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [SafeCheckChecklistQuestion]
	DROP COLUMN [LastModifiedOn] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistQuestion' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [SafeCheckChecklistQuestion]
	DROP COLUMN [Deleted]
END
GO


DELETE FROM changelog WHERE change_number = 537 AND delta_set = 'Main'
GO

--------------- Fragment ends: #537: 537_ALTER SafecheckChecklistQuestion.sql ---------------

--------------- Fragment begins: #536: 536_INSERT DocumentType.sql ---------------

-- Change script: #536: 536_INSERT DocumentType.sql
USE [BusinessSafe]
GO

IF EXISTS(SELECT * FROM [BusinessSafe].[dbo].[DocumentType] WHERE [Name] = 'Action')
BEGIN
	DELETE FROM [BusinessSafe].[dbo].[DocumentType] WHERE [Name] = 'Action'
END
GO

DELETE FROM changelog WHERE change_number = 536 AND delta_set = 'Main'
GO

--------------- Fragment ends: #536: 536_INSERT DocumentType.sql ---------------

--------------- Fragment begins: #535: 535_ALTER SafecheckChecklistQuestionResponse.sql ---------------

-- Change script: #535: 535_ALTER SafecheckChecklistQuestionResponse.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [SafeCheckQuestionResponse]
	DROP COLUMN [CreatedBy]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [SafeCheckQuestionResponse]
	DROP COLUMN [CreatedOn]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [SafeCheckQuestionResponse]
	DROP COLUMN [LastModifiedBy] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [SafeCheckQuestionResponse]
	DROP COLUMN [LastModifiedOn] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [SafeCheckQuestionResponse]
	DROP COLUMN [Deleted]
END
GO


DELETE FROM changelog WHERE change_number = 535 AND delta_set = 'Main'
GO

--------------- Fragment ends: #535: 535_ALTER SafecheckChecklistQuestionResponse.sql ---------------

--------------- Fragment begins: #534: 534_ALTER Safecheck entities.sql ---------------

-- Change script: #534: 534_ALTER Safecheck entities.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckQuestion' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	EXEC sp_rename 'SafeCheckQuestion.[LastModifiedOn]', 'LastModifieddOn', 'COLUMN'
END
GO

DELETE FROM changelog WHERE change_number = 534 AND delta_set = 'Main'
GO

--------------- Fragment ends: #534: 534_ALTER Safecheck entities.sql ---------------

--------------- Fragment begins: #533: 533_ALTER SafecheckChecklistAnswer.sql ---------------

-- Change script: #533: 533_ALTER SafecheckChecklistAnswer.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistAnswer' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [SafeCheckChecklistAnswer]
	DROP COLUMN [CreatedBy]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistAnswer' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [SafeCheckChecklistAnswer]
	DROP COLUMN [CreatedOn]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistAnswer' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [SafeCheckChecklistAnswer]
	DROP COLUMN [LastModifiedBy] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistAnswer' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [SafeCheckChecklistAnswer]
	DROP COLUMN [LastModifiedOn] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistAnswer' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [SafeCheckChecklistAnswer]
	DROP COLUMN [Deleted]
END
GO


DELETE FROM changelog WHERE change_number = 533 AND delta_set = 'Main'
GO

--------------- Fragment ends: #533: 533_ALTER SafecheckChecklistAnswer.sql ---------------

--------------- Fragment begins: #532: 532_ALTER Safecheck entities.sql ---------------

-- Change script: #532: 532_ALTER Safecheck entities.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS TABLE_CATALOG = 'BusinessSafe' AND WHERE TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'ReportLetterStatement')
BEGIN
	EXEC sp_RENAME 'SafeCheckQuestionResponse.[ReportLetterStatement]', 'NonCompliance'
END
GO

DELETE FROM changelog WHERE change_number = 532 AND delta_set = 'Main'
GO

--------------- Fragment ends: #532: 532_ALTER Safecheck entities.sql ---------------

--------------- Fragment begins: #531: 531_INSERT INTO DocHandlerDocumentType.sql ---------------

-- Change script: #531: 531_INSERT INTO DocHandlerDocumentType.sql
USE [BusinessSafe]
GO

IF EXISTS(SELECT * FROM [DocHandlerDocumentType] WHERE [Id] = 131 AND [DocumentGroupId] = 1)
BEGIN
	DELETE FROM [BusinessSafe].[dbo].[DocHandlerDocumentType] WHERE [Id] = 131 AND [DocumentGroupId] = 1
END
GO

DELETE FROM changelog WHERE change_number = 531 AND delta_set = 'Main'
GO

--------------- Fragment ends: #531: 531_INSERT INTO DocHandlerDocumentType.sql ---------------

--------------- Fragment begins: #530: 530_ALTER Action TimeScale.sql ---------------

-- Change script: #530: 530_ALTER Action TimeScale.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = 'ActionPlanId')
BEGIN
	
	ALTER TABLE [Action] ALTER COLUMN ActionPlanId bigint NOT NULL
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = 'AssignedTo')
BEGIN
	
	ALTER TABLE [Action] ALTER COLUMN AssignedTo uniqueidentifier NOT NULL
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = 'DueDate')
BEGIN
	
	ALTER TABLE [Action] ALTER COLUMN DueDate datetime NOT NULL
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Timescale' AND COLUMN_NAME = 'Name')
BEGIN	
	UPDATE Timescale SET Name = 'Mone Month' where Id = 1
END

DELETE FROM changelog WHERE change_number = 530 AND delta_set = 'Main'
GO

--------------- Fragment ends: #530: 530_ALTER Action TimeScale.sql ---------------

--------------- Fragment begins: #529: 529_CREATE_SafeCheckReportLetterCategory.sql ---------------

-- Change script: #529: 529_CREATE_SafeCheckReportLetterCategory.sql

IF  EXISTS( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckReportLetterStatementCategory')
BEGIN
	DROP TABLE [SafeCheckReportLetterStatementCategory] 
END

DELETE FROM changelog WHERE change_number = 529 AND delta_set = 'Main'
GO

--------------- Fragment ends: #529: 529_CREATE_SafeCheckReportLetterCategory.sql ---------------

--------------- Fragment begins: #528: 528_INSERT_SafeCheckClientQuestion.sql ---------------

-- Change script: #528: 528_INSERT_SafeCheckClientQuestion.sql

DELETE FROM changelog WHERE change_number = 528 AND delta_set = 'Main'
GO

--------------- Fragment ends: #528: 528_INSERT_SafeCheckClientQuestion.sql ---------------

--------------- Fragment begins: #527: 527_INSERT_SafeCheckIndustryChecklist.sql ---------------

-- Change script: #527: 527_INSERT_SafeCheckIndustryChecklist.sql

DELETE FROM changelog WHERE change_number = 527 AND delta_set = 'Main'
GO

--------------- Fragment ends: #527: 527_INSERT_SafeCheckIndustryChecklist.sql ---------------

--------------- Fragment begins: #526: 526_ALTER SafeCheckCheckList .sql ---------------

-- Change script: #526: 526_ALTER SafeCheckCheckList .sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'ChecklistCreatedBy')
BEGIN
	ALTER TABLE SafeCheckCheckList DROP COLUMN ChecklistCreatedBy 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'ChecklistCreatedOn')
BEGIN
	ALTER TABLE SafeCheckCheckList DROP COLUMN ChecklistCreatedOn 
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'ChecklistCompletedBy')
BEGIN
	ALTER TABLE SafeCheckCheckList DROP COLUMN ChecklistCompletedBy 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'ChecklistCompletedOn')
BEGIN
	ALTER TABLE SafeCheckCheckList DROP COLUMN ChecklistCompletedOn 
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'ChecklistSubmittedBy')
BEGIN
	ALTER TABLE SafeCheckCheckList DROP COLUMN ChecklistSubmittedBy 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'ChecklistSubmittedOn')
BEGIN
	ALTER TABLE SafeCheckCheckList DROP COLUMN ChecklistSubmittedOn 
END

GO

DELETE FROM changelog WHERE change_number = 526 AND delta_set = 'Main'
GO

--------------- Fragment ends: #526: 526_ALTER SafeCheckCheckList .sql ---------------

--------------- Fragment begins: #525: 525_ALTER_SafecheckQuestionResponse_Add_ReportLetterStatementCategory.sql ---------------

-- Change script: #525: 525_ALTER_SafecheckQuestionResponse_Add_ReportLetterStatementCategory.sql

IF  EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafecheckQuestionResponse' AND COLUMN_NAME = 'ReportLetterStatementCategoryId')
BEGIN
	ALTER TABLE [SafecheckQuestionResponse]
	DROP COLUMN [ReportLetterStatementCategoryId] 
END

DELETE FROM changelog WHERE change_number = 525 AND delta_set = 'Main'
GO

--------------- Fragment ends: #525: 525_ALTER_SafecheckQuestionResponse_Add_ReportLetterStatementCategory.sql ---------------

--------------- Fragment begins: #524: 524_CREATE_SafeCheckReportLetterCategory.sql ---------------

-- Change script: #524: 524_CREATE_SafeCheckReportLetterCategory.sql

IF  EXISTS( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'ReportLetterStatementCategory')
BEGIN
	DROP TABLE [ReportLetterStatementCategory] 
END

DELETE FROM changelog WHERE change_number = 524 AND delta_set = 'Main'
GO

--------------- Fragment ends: #524: 524_CREATE_SafeCheckReportLetterCategory.sql ---------------

--------------- Fragment begins: #523: 523_INSERT_SafecheckQuestionResponse.sql ---------------

-- Change script: #523: 523_INSERT_SafecheckQuestionResponse.sql

DELETE FROM changelog WHERE change_number = 523 AND delta_set = 'Main'
GO

--------------- Fragment ends: #523: 523_INSERT_SafecheckQuestionResponse.sql ---------------

--------------- Fragment begins: #522: 522_INSERT_SafeCheckQuestion.sql ---------------

-- Change script: #522: 522_INSERT_SafeCheckQuestion.sql

DELETE FROM changelog WHERE change_number = 522 AND delta_set = 'Main'
GO

--------------- Fragment ends: #522: 522_INSERT_SafeCheckQuestion.sql ---------------

--------------- Fragment begins: #521: 521_Delete ActionPlan.sql ---------------

-- Change script: #521: 521_Delete ActionPlan.sql

DELETE FROM changelog WHERE change_number = 521 AND delta_set = 'Main'
GO

--------------- Fragment ends: #521: 521_Delete ActionPlan.sql ---------------

--------------- Fragment begins: #520: 520_ALTER SafeCheck Checklist.sql ---------------

-- Change script: #520: 520_ALTER SafeCheck Checklist.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklist' AND COLUMN_NAME = 'ActionPlanId')
BEGIN
	ALTER TABLE	[dbo].[SafeCheckChecklist] 
	DROP [ActionPlanId] 		
END
GO

DELETE FROM changelog WHERE change_number = 520 AND delta_set = 'Main'
GO

--------------- Fragment ends: #520: 520_ALTER SafeCheck Checklist.sql ---------------

--------------- Fragment begins: #519: 519_ALTER SafeCheckImmediateRiskNotification.sql ---------------

-- Change script: #519: 519_ALTER SafeCheckImmediateRiskNotification.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckImmediateRiskNotification' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE	[dbo].[SafeCheckImmediateRiskNotification]
	DROP COLUMN [Deleted]
END
GO


DELETE FROM changelog WHERE change_number = 519 AND delta_set = 'Main'
GO

--------------- Fragment ends: #519: 519_ALTER SafeCheckImmediateRiskNotification.sql ---------------

--------------- Fragment begins: #518: 518_UPDATE ActionPlan.sql ---------------

-- Change script: #518: 518_UPDATE ActionPlan.sql

DELETE FROM changelog WHERE change_number = 518 AND delta_set = 'Main'
GO

--------------- Fragment ends: #518: 518_UPDATE ActionPlan.sql ---------------

--------------- Fragment begins: #517: 517_INSERT INTO TaskCategory.sql ---------------

-- Change script: #517: 517_INSERT INTO TaskCategory.sql
USE [BusinessSafe]
GO
IF EXISTS(SELECT * FROM [dbo].[TaskCategory] WHERE [Id] = 8)
BEGIN
	DELETE FROM TaskCategory WHERE Id = 8
END
	
GO

DELETE FROM changelog WHERE change_number = 517 AND delta_set = 'Main'
GO

--------------- Fragment ends: #517: 517_INSERT INTO TaskCategory.sql ---------------

--------------- Fragment begins: #516: 516_Update_SafeCheckIndustries.sql ---------------

-- Change script: #516: 516_Update_SafeCheckIndustries.sql

DELETE FROM changelog WHERE change_number = 516 AND delta_set = 'Main'
GO

--------------- Fragment ends: #516: 516_Update_SafeCheckIndustries.sql ---------------

--------------- Fragment begins: #515: 515_ALTER SafeCheckQuestionResponse rename NonConformance.sql ---------------

-- Change script: #515: 515_ALTER SafeCheckQuestionResponse rename NonConformance.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'ReportLetterStatement')
BEGIN
	EXEC sp_RENAME 'SafeCheckQuestionResponse.[ReportLetterStatement]', 'NonCompliance'
END
GO

DELETE FROM changelog WHERE change_number = 515 AND delta_set = 'Main'
GO

--------------- Fragment ends: #515: 515_ALTER SafeCheckQuestionResponse rename NonConformance.sql ---------------

--------------- Fragment begins: #514: 514_Update_SafeCheck_Questions.sql ---------------

-- Change script: #514: 514_Update_SafeCheck_Questions.sql

DELETE FROM changelog WHERE change_number = 514 AND delta_set = 'Main'
GO

--------------- Fragment ends: #514: 514_Update_SafeCheck_Questions.sql ---------------

--------------- Fragment begins: #513: 513_Update_Real_SafeCheck_Questions.sql ---------------

-- Change script: #513: 513_Update_Real_SafeCheck_Questions.sql

DELETE FROM changelog WHERE change_number = 513 AND delta_set = 'Main'
GO

--------------- Fragment ends: #513: 513_Update_Real_SafeCheck_Questions.sql ---------------

--------------- Fragment begins: #512: 512_Add_Real_SafeCheck_Questions.sql ---------------

-- Change script: #512: 512_Add_Real_SafeCheck_Questions.sql

DELETE FROM changelog WHERE change_number = 512 AND delta_set = 'Main'
GO

--------------- Fragment ends: #512: 512_Add_Real_SafeCheck_Questions.sql ---------------

--------------- Fragment begins: #511: 511_ALTER Task.sql ---------------

-- Change script: #511: 511_ALTER Task.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'ActionId')
BEGIN
	ALTER TABLE [dbo].Task 
	DROP CONSTRAINT [DF_Task_ActionId]
	
	ALTER TABLE Task
	DROP COLUMN [ActionId] 
	
END

DELETE FROM changelog WHERE change_number = 511 AND delta_set = 'Main'
GO

--------------- Fragment ends: #511: 511_ALTER Task.sql ---------------

--------------- Fragment begins: #510: 510_INSERT INTO TaskCategory.sql ---------------

-- Change script: #510: 510_INSERT INTO TaskCategory.sql
USE [BusinessSafe]
GO
IF EXISTS(SELECT * FROM [dbo].[TaskCategory] WHERE [Id] = 8)
BEGIN
	DELETE FROM TaskCategory WHERE Id = 8
END
	
GO

DELETE FROM changelog WHERE change_number = 510 AND delta_set = 'Main'
GO

--------------- Fragment ends: #510: 510_INSERT INTO TaskCategory.sql ---------------

--------------- Fragment begins: #509: 509_RECREATE ImmediateRiskNotification.sql ---------------

-- Change script: #509: 509_RECREATE ImmediateRiskNotification.sql

IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckImmediateRiskNotification')
BEGIN
	DROP TABLE [dbo].[SafeCheckImmediateRiskNotification]
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckImmediateRiskNotification')
BEGIN
	CREATE TABLE [dbo].[SafeCheckImmediateRiskNotification](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[Reference] [nvarchar](100) NULL,
        [Title] [nvarchar](250) NULL,
        [SignificantHazardIdentified] [nvarchar](MAX) NULL,
        [RecommendedImmediateAction] [nvarchar](MAX) NULL,
		[ChecklistId] [uniqueidentifier] NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL
	CONSTRAINT [PK_SafeCheckImmediateRiskNotification] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, UPDATE, DELETE ON [SafeCheckImmediateRiskNotification] TO AllowAll
	GRANT SELECT, INSERT, UPDATE ON [SafeCheckImmediateRiskNotification] TO AllowSelectInsertUpdate
END
GO

DELETE FROM changelog WHERE change_number = 509 AND delta_set = 'Main'
GO

--------------- Fragment ends: #509: 509_RECREATE ImmediateRiskNotification.sql ---------------

--------------- Fragment begins: #508: 508_INSERT ActionPlan.sql ---------------

-- Change script: #508: 508_INSERT ActionPlan.sql

DELETE FROM changelog WHERE change_number = 508 AND delta_set = 'Main'
GO

--------------- Fragment ends: #508: 508_INSERT ActionPlan.sql ---------------

--------------- Fragment begins: #507: 507_ALTER ActionPlan Add Areas Visited.sql ---------------

-- Change script: #507: 507_ALTER ActionPlan Add Areas Visited.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ActionPlan' AND COLUMN_NAME = 'AreasVisited')
BEGIN
	ALTER TABLE	ActionPlan		
		ADD AreasVisited nvarchar(200) null
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ActionPlan' AND COLUMN_NAME = 'AreasNotVisited')
BEGIN
	ALTER TABLE	ActionPlan		
		ADD AreasNotVisited nvarchar(200) null	
END
GO

DELETE FROM changelog WHERE change_number = 507 AND delta_set = 'Main'
GO

--------------- Fragment ends: #507: 507_ALTER ActionPlan Add Areas Visited.sql ---------------

--------------- Fragment begins: #506: 506_INSERT Action data.sql ---------------

-- Change script: #506: 506_INSERT Action data.sql

DELETE FROM changelog WHERE change_number = 506 AND delta_set = 'Main'
GO

--------------- Fragment ends: #506: 506_INSERT Action data.sql ---------------

--------------- Fragment begins: #505: 505_ALTER Action.sql ---------------

-- Change script: #505: 505_ALTER Action.sql

DELETE FROM changelog WHERE change_number = 505 AND delta_set = 'Main'
GO

--------------- Fragment ends: #505: 505_ALTER Action.sql ---------------

--------------- Fragment begins: #504: 504_ALTER SafeCheckQuestionResponse add NonConformance.sql ---------------

-- Change script: #504: 504_ALTER SafeCheckQuestionResponse add NonConformance.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'NonCompliance')
BEGIN
	ALTER TABLE [SafeCheckQuestionResponse]
	DROP COLUMN [NonCompliance]
END
GO

DELETE FROM changelog WHERE change_number = 504 AND delta_set = 'Main'
GO

--------------- Fragment ends: #504: 504_ALTER SafeCheckQuestionResponse add NonConformance.sql ---------------

--------------- Fragment begins: #503: 503_ALTER Action.sql ---------------

-- Change script: #503: 503_ALTER Action.sql

DELETE FROM changelog WHERE change_number = 503 AND delta_set = 'Main'
GO

--------------- Fragment ends: #503: 503_ALTER Action.sql ---------------

--------------- Fragment begins: #502: 502_INSERT ActionPlan.sql ---------------

-- Change script: #502: 502_INSERT ActionPlan.sql

DELETE FROM changelog WHERE change_number = 502 AND delta_set = 'Main'
GO

--------------- Fragment ends: #502: 502_INSERT ActionPlan.sql ---------------

--------------- Fragment begins: #501: 501_Create indexes for Task table.sql ---------------

-- Change script: #501: 501_Create indexes for Task table.sql

DELETE FROM changelog WHERE change_number = 501 AND delta_set = 'Main'
GO

--------------- Fragment ends: #501: 501_Create indexes for Task table.sql ---------------

--------------- Fragment begins: #500: 500_ALTER Action.sql ---------------

-- Change script: #500: 500_ALTER Action.sql

DELETE FROM changelog WHERE change_number = 500 AND delta_set = 'Main'
GO

--------------- Fragment ends: #500: 500_ALTER Action.sql ---------------

--------------- Fragment begins: #499: 499_CREATE ImmediateRiskNotification.sql ---------------

-- Change script: #499: 499_CREATE ImmediateRiskNotification.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckImmediateRiskNotification')
BEGIN
	DROP TABLE [dbo].[SafeCheckImmediateRiskNotification]
END
GO

DELETE FROM changelog WHERE change_number = 499 AND delta_set = 'Main'
GO

--------------- Fragment ends: #499: 499_CREATE ImmediateRiskNotification.sql ---------------

--------------- Fragment begins: #498: 498_INSERT Action data.sql ---------------

-- Change script: #498: 498_INSERT Action data.sql

DELETE FROM changelog WHERE change_number = 498 AND delta_set = 'Main'
GO

--------------- Fragment ends: #498: 498_INSERT Action data.sql ---------------

--------------- Fragment begins: #497: 497_ALTER Action.sql ---------------

-- Change script: #497: 497_ALTER Action.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[ActionPlanId]')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN [ActionPlanId]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[Reference]')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN [Reference]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[SignificantHazardIdentified]')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [SignificantHazardIdentified] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[RecommendedImmediateAction]')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [RecommendedImmediateAction] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[AreaOfNonCompliance]')
BEGIN

	ALTER TABLE	[dbo].[Action] DROP COLUMN  [AreaOfNonCompliance] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[ActionRequired]')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [ActionRequired] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[GuidanceNotes]')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [GuidanceNotes] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[TargetTimescale] ')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [TargetTimescale] 
END
GO
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[AssignedTo]')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [AssignedTo] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[DueDate] ')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [DueDate]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = '[Status] ')
BEGIN
	ALTER TABLE	[dbo].[Action] DROP COLUMN  [Status] 
END
GO
	

DELETE FROM changelog WHERE change_number = 497 AND delta_set = 'Main'
GO

--------------- Fragment ends: #497: 497_ALTER Action.sql ---------------

--------------- Fragment begins: #496: 496_INSERT ActionPlan.sql ---------------

-- Change script: #496: 496_INSERT ActionPlan.sql

DELETE FROM changelog WHERE change_number = 496 AND delta_set = 'Main'
GO

--------------- Fragment ends: #496: 496_INSERT ActionPlan.sql ---------------

--------------- Fragment begins: #495: 495_ALTER ActionPlan.sql ---------------

-- Change script: #495: 495_ALTER ActionPlan.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ActionPlan' AND COLUMN_NAME = 'CompanyId')
BEGIN
	ALTER TABLE	ActionPlan
		DROP COLUMN CompanyId
END

DELETE FROM changelog WHERE change_number = 495 AND delta_set = 'Main'
GO

--------------- Fragment ends: #495: 495_ALTER ActionPlan.sql ---------------

--------------- Fragment begins: #494: 494_CREATE Action.sql ---------------

-- Change script: #494: 494_CREATE Action.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Action')
BEGIN
	DROP TABLE [dbo].[Action]
END
GO

DELETE FROM changelog WHERE change_number = 494 AND delta_set = 'Main'
GO

--------------- Fragment ends: #494: 494_CREATE Action.sql ---------------

--------------- Fragment begins: #493: 493_CREATE ActionPlan.sql ---------------

-- Change script: #493: 493_CREATE ActionPlan.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ActionPlan')
BEGIN
	DROP TABLE [dbo].[ActionPlan]
END
GO

DELETE FROM changelog WHERE change_number = 493 AND delta_set = 'Main'
GO

--------------- Fragment ends: #493: 493_CREATE ActionPlan.sql ---------------

--------------- Fragment begins: #492: 492_INSERT_RolesPermission.sql ---------------

-- Change script: #492: 492_INSERT_RolesPermission.sql

--User Admin
IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 63)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 63 
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 64)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 64 
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 65)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 65 
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 66)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = @UserAdminRole and [PermissionId] = 66 
END

--General User
IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 63)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 63 
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 64)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 64 
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 65)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 65 
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 66)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = @GeneralUserRoleRole and [PermissionId] = 66 
END

DELETE FROM changelog WHERE change_number = 492 AND delta_set = 'Main'
GO

--------------- Fragment ends: #492: 492_INSERT_RolesPermission.sql ---------------

--------------- Fragment begins: #491: 491_INSERT_Permission.sql ---------------

-- Change script: #491: 491_INSERT_Permission.sql
DELETE FROM [BusinessSafe].[dbo].[Permission]
WHERE ID IN (63, 64, 65, 66)

DELETE FROM changelog WHERE change_number = 491 AND delta_set = 'Main'
GO

--------------- Fragment ends: #491: 491_INSERT_Permission.sql ---------------

--------------- Fragment begins: #490: 490_INSERT_PermissionTarget.sql ---------------

-- Change script: #490: 490_INSERT_PermissionTarget.sql
DELETE FROM [PermissionTarget]
WHERE ID = 19

DELETE FROM changelog WHERE change_number = 490 AND delta_set = 'Main'
GO

--------------- Fragment ends: #490: 490_INSERT_PermissionTarget.sql ---------------

--------------- Fragment begins: #489: 489_Add NextReviewDate to risk assessment.sql ---------------

-- Change script: #489: 489_Add NextReviewDate to risk assessment.sql

DELETE FROM changelog WHERE change_number = 489 AND delta_set = 'Main'
GO

--------------- Fragment ends: #489: 489_Add NextReviewDate to risk assessment.sql ---------------

--------------- Fragment begins: #488: 488_UPDATE SafeCheckImpressionType Table DATA.sql ---------------

-- Change script: #488: 488_UPDATE SafeCheckImpressionType Table DATA.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckImpressionType' AND COLUMN_NAME = 'Comments')
BEGIN
	UPDATE [SafeCheckImpressionType]
	SET Comments = 'The overall standard of health and safety management at this site was below an acceptable level with some health and safety matters requiring urgent management corrective action. Judged against the Enforcing Authorities? Enforcement Policy Statement this standard of compliance would leave you open to formal enforcement action should you receive an inspection visit. By implementing the recommendations contained within the Action Plan, your present standards of health and safety will be improved and the likelihood of formal enforcement action being taken against you personally or your company will be reduced.'
	WHERE Title = 'Unsatisfactory'
END

DELETE FROM changelog WHERE change_number = 488 AND delta_set = 'Main'
GO

--------------- Fragment ends: #488: 488_UPDATE SafeCheckImpressionType Table DATA.sql ---------------

--------------- Fragment begins: #487: 487_ALTER SafeCheckQuestionResponse alter QuestionId.sql ---------------

-- Change script: #487: 487_ALTER SafeCheckQuestionResponse alter QuestionId.sql

DELETE FROM changelog WHERE change_number = 487 AND delta_set = 'Main'
GO

--------------- Fragment ends: #487: 487_ALTER SafeCheckQuestionResponse alter QuestionId.sql ---------------

--------------- Fragment begins: #486: 486_ALTER SafeCheckChecklistAnswer alter QuestionId.sql ---------------

-- Change script: #486: 486_ALTER SafeCheckChecklistAnswer alter QuestionId.sql

DELETE FROM changelog WHERE change_number = 486 AND delta_set = 'Main'
GO

--------------- Fragment ends: #486: 486_ALTER SafeCheckChecklistAnswer alter QuestionId.sql ---------------

--------------- Fragment begins: #485: 485_ALTER SafeCheckChecklistQuestion alter QuestionId.sql ---------------

-- Change script: #485: 485_ALTER SafeCheckChecklistQuestion alter QuestionId.sql

DELETE FROM changelog WHERE change_number = 485 AND delta_set = 'Main'
GO

--------------- Fragment ends: #485: 485_ALTER SafeCheckChecklistQuestion alter QuestionId.sql ---------------

--------------- Fragment begins: #484: 484_ALTER SafeCheckCheckListAnswer Table.sql ---------------

-- Change script: #484: 484_ALTER SafeCheckCheckListAnswer Table.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListAnswer' AND COLUMN_NAME = 'EmployeeNotListed')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer
	DROP COLUMN EmployeeNotListed
END

DELETE FROM changelog WHERE change_number = 484 AND delta_set = 'Main'
GO

--------------- Fragment ends: #484: 484_ALTER SafeCheckCheckListAnswer Table.sql ---------------

--------------- Fragment begins: #483: 483_ALTER SafeCheckQuestion add SpecificToClientId.sql ---------------

-- Change script: #483: 483_ALTER SafeCheckQuestion add SpecificToClientId.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckQuestion' AND COLUMN_NAME = 'SpecificToClientId')
BEGIN
	ALTER TABLE [SafeCheckQuestion]
	DROP COLUMN [SpecificToClientId]
END
GO

DELETE FROM changelog WHERE change_number = 483 AND delta_set = 'Main'
GO

--------------- Fragment ends: #483: 483_ALTER SafeCheckQuestion add SpecificToClientId.sql ---------------

--------------- Fragment begins: #482: 482_ALTER SafeCheckCheckList.sql ---------------

-- Change script: #482: 482_ALTER SafeCheckCheckList.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE SafeCheckCheckList DROP COLUMN CreatedBy 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE SafeCheckCheckList DROP COLUMN CreatedOn 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE SafeCheckCheckList DROP COLUMN LastModifiedBy 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE SafeCheckCheckList DROP COLUMN LastModifiedOn
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckList' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE SafeCheckCheckList DROP COLUMN Deleted
END

DELETE FROM changelog WHERE change_number = 482 AND delta_set = 'Main'
GO

--------------- Fragment ends: #482: 482_ALTER SafeCheckCheckList.sql ---------------

--------------- Fragment begins: #481: 481_ALTER SafeCheckCheckList.sql ---------------

-- Change script: #481: 481_ALTER SafeCheckCheckList.sql
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'ImpressionTypeId')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	DROP COLUMN ImpressionTypeId 	
END

GO 


DELETE FROM changelog WHERE change_number = 481 AND delta_set = 'Main'
GO

--------------- Fragment ends: #481: 481_ALTER SafeCheckCheckList.sql ---------------

--------------- Fragment begins: #480: 480_INSERT_SafeCheckImpressionType.sql ---------------

-- Change script: #480: 480_INSERT_SafeCheckImpressionType.sql

DELETE FROM changelog WHERE change_number = 480 AND delta_set = 'Main'
GO

--------------- Fragment ends: #480: 480_INSERT_SafeCheckImpressionType.sql ---------------

--------------- Fragment begins: #479: 479_CREATE SafeCheckImpressionType Table.sql ---------------

-- Change script: #479: 479_CREATE SafeCheckImpressionType Table.sql
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckImpressionType')
BEGIN
	DROP TABLE [dbo].[SafeCheckImpressionType]
END
GO


DELETE FROM changelog WHERE change_number = 479 AND delta_set = 'Main'
GO

--------------- Fragment ends: #479: 479_CREATE SafeCheckImpressionType Table.sql ---------------

--------------- Fragment begins: #478: 478_INSERT SafeCheckCheckList data.sql ---------------

-- Change script: #478: 478_INSERT SafeCheckCheckList data.sql

DELETE FROM changelog WHERE change_number = 478 AND delta_set = 'Main'
GO

--------------- Fragment ends: #478: 478_INSERT SafeCheckCheckList data.sql ---------------

--------------- Fragment begins: #477: 477_UPDATE_SafeCheckQuestion Add Mandatory.sql ---------------

-- Change script: #477: 477_UPDATE_SafeCheckQuestion Add Mandatory.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckQuestion' AND COLUMN_NAME = 'Mandatory')
BEGIN
	ALTER TABLE [SafeCheckQuestion]
	DROP COLUMN [Mandatory]
END


DELETE FROM changelog WHERE change_number = 477 AND delta_set = 'Main'
GO

--------------- Fragment ends: #477: 477_UPDATE_SafeCheckQuestion Add Mandatory.sql ---------------

--------------- Fragment begins: #476: 476_ALTER SafeCheckQuestionResponse Table.sql ---------------

-- Change script: #476: 476_ALTER SafeCheckQuestionResponse Table.sql

DELETE FROM changelog WHERE change_number = 476 AND delta_set = 'Main'
GO

--------------- Fragment ends: #476: 476_ALTER SafeCheckQuestionResponse Table.sql ---------------

--------------- Fragment begins: #475: 475_UPDATE_SafeCheckQestionResponse_Table.sql ---------------

-- Change script: #475: 475_UPDATE_SafeCheckQestionResponse_Table.sql

DELETE FROM changelog WHERE change_number = 475 AND delta_set = 'Main'
GO

--------------- Fragment ends: #475: 475_UPDATE_SafeCheckQestionResponse_Table.sql ---------------

--------------- Fragment begins: #474: 474_ALTER SafeCheckQuestionResponse Table.sql ---------------

-- Change script: #474: 474_ALTER SafeCheckQuestionResponse Table.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckQuestionResponse]' AND COLUMN_NAME = 'SupportingEvidence')
BEGIN
	ALTER TABLE	SafeCheckQuestionResponse
	DROP COLUMN SupportingEvidence
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckQuestionResponse]' AND COLUMN_NAME = 'ActionRequired')
BEGIN
	ALTER TABLE	SafeCheckQuestionResponse
	DROP COLUMN ActionRequired
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckQuestionResponse]' AND COLUMN_NAME = 'Comment')
BEGIN
	ALTER TABLE	SafeCheckQuestionResponse		
	ADD Comment nvarchar(250) NULL
END



DELETE FROM changelog WHERE change_number = 474 AND delta_set = 'Main'
GO

--------------- Fragment ends: #474: 474_ALTER SafeCheckQuestionResponse Table.sql ---------------

--------------- Fragment begins: #473: 473_ALTER SafeCheckCheckListAnswer Table.sql ---------------

-- Change script: #473: 473_ALTER SafeCheckCheckListAnswer Table.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckCheckListAnswer]' AND COLUMN_NAME = 'SupportingEvidence')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer
	DROP COLUMN SupportingEvidence
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckCheckListAnswer]' AND COLUMN_NAME = 'ActionRequired')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer
	DROP COLUMN ActionRequired
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckCheckListAnswer]' AND COLUMN_NAME = 'AssignedTo')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer
	DROP COLUMN AssignedTo
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SafeCheckCheckListAnswer]' AND COLUMN_NAME = 'Comment')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer		
	ADD Comment nvarchar(250) NULL
END



DELETE FROM changelog WHERE change_number = 473 AND delta_set = 'Main'
GO

--------------- Fragment ends: #473: 473_ALTER SafeCheckCheckListAnswer Table.sql ---------------

--------------- Fragment begins: #472: 472_ALTER SafeCheckCheckListQuestion.sql ---------------

-- Change script: #472: 472_ALTER SafeCheckCheckListQuestion.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE	[SafeCheckCheckListQuestion]			
	DROP COLUMN [Deleted] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE	[SafeCheckCheckListQuestion]			
	DROP COLUMN [CreatedBy]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE	[SafeCheckCheckListQuestion]			
	DROP COLUMN [CreatedOn] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE	[SafeCheckCheckListQuestion]			
	DROP COLUMN [LastModifiedBy]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListQuestion' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE	[SafeCheckCheckListQuestion]			
	DROP COLUMN [LastModifiedOn] 
END

DELETE FROM changelog WHERE change_number = 472 AND delta_set = 'Main'
GO

--------------- Fragment ends: #472: 472_ALTER SafeCheckCheckListQuestion.sql ---------------

--------------- Fragment begins: #471: 471_ALTER SafeCheckCheckList.sql ---------------

-- Change script: #471: 471_ALTER SafeCheckCheckList.sql
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'Status')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	DROP COLUMN Status 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'PersonSeenJobTitle')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	DROP COLUMN PersonSeenJobTitle
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'PersonSeenName')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	DROP COLUMN PersonSeenName
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'PersonSeenSalutation')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	DROP COLUMN PersonSeenSalutation 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'AreasVisited')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	DROP COLUMN AreasVisited
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'AreasNotVisited')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	DROP COLUMN AreasNotVisited
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[[SafeCheckCheckList]]' AND COLUMN_NAME = 'EmailAddress')
BEGIN
	ALTER TABLE	[SafeCheckCheckList]			
	DROP COLUMN EmailAddress
END

DELETE FROM changelog WHERE change_number = 471 AND delta_set = 'Main'
GO

--------------- Fragment ends: #471: 471_ALTER SafeCheckCheckList.sql ---------------

--------------- Fragment begins: #470: 470_DROP SafeCheckCategoryQuestion.sql ---------------

-- Change script: #470: 470_DROP SafeCheckCategoryQuestion.sql
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCategoryQuestion')
BEGIN
	CREATE TABLE [dbo].[SafeCheckCategoryQuestion](
		[Id] [uniqueidentifier] NOT NULL,
		[CategoryId] [uniqueidentifier] NOT NULL,
		[QuestionId] [uniqueidentifier] NOT NULL,
	 CONSTRAINT [PK_SafeCheckCategoryQuestion] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[SafeCheckCategoryQuestion] ADD  DEFAULT (newid()) FOR [Id]
END


DELETE FROM changelog WHERE change_number = 470 AND delta_set = 'Main'
GO

--------------- Fragment ends: #470: 470_DROP SafeCheckCategoryQuestion.sql ---------------

--------------- Fragment begins: #469: 469_Update_Responsibility_Table_Site_And_Owner.sql ---------------

-- Change script: #469: 469_Update_Responsibility_Table_Site_And_Owner.sql

DELETE FROM changelog WHERE change_number = 469 AND delta_set = 'Main'
GO

--------------- Fragment ends: #469: 469_Update_Responsibility_Table_Site_And_Owner.sql ---------------

--------------- Fragment begins: #468: 468_INSERT_SafeCheckIndustries.sql ---------------

-- Change script: #468: 468_INSERT_SafeCheckIndustries.sql

DELETE FROM changelog WHERE change_number = 468 AND delta_set = 'Main'
GO

--------------- Fragment ends: #468: 468_INSERT_SafeCheckIndustries.sql ---------------

--------------- Fragment begins: #467: 467_INSERT_SafeCheckClientQuestions.sql ---------------

-- Change script: #467: 467_INSERT_SafeCheckClientQuestions.sql

DELETE FROM changelog WHERE change_number = 467 AND delta_set = 'Main'
GO

--------------- Fragment ends: #467: 467_INSERT_SafeCheckClientQuestions.sql ---------------

--------------- Fragment begins: #466: 466_CREATE SafeCheckClientQuestion Table.sql ---------------

-- Change script: #466: 466_CREATE SafeCheckClientQuestion Table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckClientQuestion')
BEGIN
	DROP TABLE [dbo].[SafeCheckClientQuestion]
END
GO

DELETE FROM changelog WHERE change_number = 466 AND delta_set = 'Main'
GO

--------------- Fragment ends: #466: 466_CREATE SafeCheckClientQuestion Table.sql ---------------

--------------- Fragment begins: #465: 465_INSERT_SafeCheckIndustries.sql ---------------

-- Change script: #465: 465_INSERT_SafeCheckIndustries.sql

DELETE FROM changelog WHERE change_number = 465 AND delta_set = 'Main'
GO

--------------- Fragment ends: #465: 465_INSERT_SafeCheckIndustries.sql ---------------

--------------- Fragment begins: #464: 464_CREATE SafeCheckIndustryQuestion Table.sql ---------------

-- Change script: #464: 464_CREATE SafeCheckIndustryQuestion Table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckIndustryQuestion')
BEGIN
	DROP TABLE [dbo].[SafeCheckIndustryQuestion]
END
GO

DELETE FROM changelog WHERE change_number = 464 AND delta_set = 'Main'
GO

--------------- Fragment ends: #464: 464_CREATE SafeCheckIndustryQuestion Table.sql ---------------

--------------- Fragment begins: #463: 463_CREATE SafeCheckIndustry Table.sql ---------------

-- Change script: #463: 463_CREATE SafeCheckIndustry Table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckIndustry')
BEGIN
	DROP TABLE [dbo].[SafeCheckIndustry]
END
GO

DELETE FROM changelog WHERE change_number = 463 AND delta_set = 'Main'
GO

--------------- Fragment ends: #463: 463_CREATE SafeCheckIndustry Table.sql ---------------

--------------- Fragment begins: #462: 462_Add visit columns to safecheck.sql ---------------

-- Change script: #462: 462_Add visit columns to safecheck.sql

DELETE FROM changelog WHERE change_number = 462 AND delta_set = 'Main'
GO

--------------- Fragment ends: #462: 462_Add visit columns to safecheck.sql ---------------

--------------- Fragment begins: #461: 461_Change safecheck text columns to varchar.sql ---------------

-- Change script: #461: 461_Change safecheck text columns to varchar.sql

DELETE FROM changelog WHERE change_number = 461 AND delta_set = 'Main'
GO

--------------- Fragment ends: #461: 461_Change safecheck text columns to varchar.sql ---------------

--------------- Fragment begins: #460: 460_Add questions to category question table.sql ---------------

-- Change script: #460: 460_Add questions to category question table.sql

DELETE FROM changelog WHERE change_number = 460 AND delta_set = 'Main'
GO

--------------- Fragment ends: #460: 460_Add questions to category question table.sql ---------------

--------------- Fragment begins: #459: 459_Add covering letter content column.sql ---------------

-- Change script: #459: 459_Add covering letter content column.sql

DELETE FROM changelog WHERE change_number = 459 AND delta_set = 'Main'
GO

--------------- Fragment ends: #459: 459_Add covering letter content column.sql ---------------

--------------- Fragment begins: #458: 458_ALTER Accident Record Status.sql ---------------

-- Change script: #458: 458_ALTER Accident Record Status.sql
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[AccidentRecord]' AND COLUMN_NAME = 'IsOpen')
BEGIN
	ALTER TABLE	AccidentRecord
	DROP COLUMN IsOpen
END

DELETE FROM changelog WHERE change_number = 458 AND delta_set = 'Main'
GO

--------------- Fragment ends: #458: 458_ALTER Accident Record Status.sql ---------------

--------------- Fragment begins: #457: 457_add_responseid_to_answers.sql ---------------

-- Change script: #457: 457_add_responseid_to_answers.sql

DELETE FROM changelog WHERE change_number = 457 AND delta_set = 'Main'
GO

--------------- Fragment ends: #457: 457_add_responseid_to_answers.sql ---------------

--------------- Fragment begins: #456: 456_Update Injury.sql ---------------

-- Change script: #456: 456_Update Injury.sql

use businesssafe
go
if exists(select top 1 id from injury where description = 'Other known injuries' and deleted = 1)
begin
	update injury
	set deleted = 0
	where description = 'Other known injuries'
	and deleted = 1
end

if exists(select top 1 id from injury where description = 'Multiple injuries' and deleted = 1)
begin
	update injury
	set deleted = 0
	where description = 'Multiple injuries'
	and deleted = 1 
end
go


DELETE FROM changelog WHERE change_number = 456 AND delta_set = 'Main'
GO

--------------- Fragment ends: #456: 456_Update Injury.sql ---------------

--------------- Fragment begins: #455: 455_Add_clientid_siteid_to_SafecheckChecklist.sql ---------------

-- Change script: #455: 455_Add_clientid_siteid_to_SafecheckChecklist.sql

DELETE FROM changelog WHERE change_number = 455 AND delta_set = 'Main'
GO

--------------- Fragment ends: #455: 455_Add_clientid_siteid_to_SafecheckChecklist.sql ---------------

--------------- Fragment begins: #454: 454_Link questions to categories.sql ---------------

-- Change script: #454: 454_Link questions to categories.sql

DELETE FROM changelog WHERE change_number = 454 AND delta_set = 'Main'
GO

--------------- Fragment ends: #454: 454_Link questions to categories.sql ---------------

--------------- Fragment begins: #453: 453_CREATE SafeCheckSectorQuestion Table.sql ---------------

-- Change script: #453: 453_CREATE SafeCheckSectorQuestion Table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckSectorQuestion')
BEGIN
	DROP TABLE [dbo].[SafeCheckSectorQuestion]
END
GO

DELETE FROM changelog WHERE change_number = 453 AND delta_set = 'Main'
GO

--------------- Fragment ends: #453: 453_CREATE SafeCheckSectorQuestion Table.sql ---------------

--------------- Fragment begins: #452: 452_CREATE SafeCheckClientTypeQuestion Table.sql ---------------

-- Change script: #452: 452_CREATE SafeCheckClientTypeQuestion Table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckClientTypeQuestion')
BEGIN
	DROP TABLE [dbo].[SafeCheckClientTypeQuestion]
END
GO

DELETE FROM changelog WHERE change_number = 452 AND delta_set = 'Main'
GO

--------------- Fragment ends: #452: 452_CREATE SafeCheckClientTypeQuestion Table.sql ---------------

--------------- Fragment begins: #451: 451_CREATE SafeCheckSector Table.sql ---------------

-- Change script: #451: 451_CREATE SafeCheckSector Table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckSector')
BEGIN
	DROP TABLE [dbo].[SafeCheckSector]
END
GO

DELETE FROM changelog WHERE change_number = 451 AND delta_set = 'Main'
GO

--------------- Fragment ends: #451: 451_CREATE SafeCheckSector Table.sql ---------------

--------------- Fragment begins: #450: 450_CREATE SafeCheckClientType Table.sql ---------------

-- Change script: #450: 450_CREATE SafeCheckClientType Table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckClientType')
BEGIN
	DROP TABLE [dbo].[SafeCheckClientType]
END
GO

DELETE FROM changelog WHERE change_number = 450 AND delta_set = 'Main'
GO

--------------- Fragment ends: #450: 450_CREATE SafeCheckClientType Table.sql ---------------

--------------- Fragment begins: #449: 449_ALTER Site_table.sql ---------------

-- Change script: #449: 449_ALTER Site_table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '[SiteStructureElement]' AND COLUMN_NAME = 'SiteContact')
BEGIN
	ALTER TABLE	SiteStructureElement
		DROP COLUMN SiteContact
END

DELETE FROM changelog WHERE change_number = 449 AND delta_set = 'Main'
GO

--------------- Fragment ends: #449: 449_ALTER Site_table.sql ---------------

--------------- Fragment begins: #448: 448_INSERT_SafeCheck Data.sql ---------------

-- Change script: #448: 448_INSERT_SafeCheck Data.sql

DELETE FROM changelog WHERE change_number = 448 AND delta_set = 'Main'
GO

--------------- Fragment ends: #448: 448_INSERT_SafeCheck Data.sql ---------------

--------------- Fragment begins: #447: 447_CREATE SafeCheckCheckListAnswer Table.sql ---------------

-- Change script: #447: 447_CREATE SafeCheckCheckListAnswer Table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCheckListAnswer')
BEGIN
	DROP TABLE [dbo].[SafeCheckCheckListAnswer]
END
GO

DELETE FROM changelog WHERE change_number = 447 AND delta_set = 'Main'
GO

--------------- Fragment ends: #447: 447_CREATE SafeCheckCheckListAnswer Table.sql ---------------

--------------- Fragment begins: #446: 446_CREATE SafeCheckCheckListQuestion Table.sql ---------------

-- Change script: #446: 446_CREATE SafeCheckCheckListQuestion Table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCheckListQuestion')
BEGIN
	DROP TABLE [dbo].[SafeCheckCheckListQuestion]
END
GO

DELETE FROM changelog WHERE change_number = 446 AND delta_set = 'Main'
GO

--------------- Fragment ends: #446: 446_CREATE SafeCheckCheckListQuestion Table.sql ---------------

--------------- Fragment begins: #445: 445_CREATE SafeCheckCheckList Table.sql ---------------

-- Change script: #445: 445_CREATE SafeCheckCheckList Table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCheckList')
BEGIN
	DROP TABLE [dbo].[SafeCheckCheckList]
END
GO

DELETE FROM changelog WHERE change_number = 445 AND delta_set = 'Main'
GO

--------------- Fragment ends: #445: 445_CREATE SafeCheckCheckList Table.sql ---------------

--------------- Fragment begins: #444: 444_CREATE SafeCheckQuestionResponse Table.sql ---------------

-- Change script: #444: 444_CREATE SafeCheckQuestionResponse Table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckQuestionResponse')
BEGIN
	DROP TABLE [dbo].[SafeCheckQuestionResponse]
END
GO

DELETE FROM changelog WHERE change_number = 444 AND delta_set = 'Main'
GO

--------------- Fragment ends: #444: 444_CREATE SafeCheckQuestionResponse Table.sql ---------------

--------------- Fragment begins: #443: 443_CREATE SafeCheckCategoryQuestion Table.sql ---------------

-- Change script: #443: 443_CREATE SafeCheckCategoryQuestion Table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCategoryQuestion')
BEGIN
	DROP TABLE [dbo].[SafeCheckCategoryQuestion]
END
GO

DELETE FROM changelog WHERE change_number = 443 AND delta_set = 'Main'
GO

--------------- Fragment ends: #443: 443_CREATE SafeCheckCategoryQuestion Table.sql ---------------

--------------- Fragment begins: #442: 442_CREATE SafeCheckQuestion Table.sql ---------------

-- Change script: #442: 442_CREATE SafeCheckQuestion Table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckQuestion')
BEGIN
	DROP TABLE [dbo].[SafeCheckQuestion]
END
GO

DELETE FROM changelog WHERE change_number = 442 AND delta_set = 'Main'
GO

--------------- Fragment ends: #442: 442_CREATE SafeCheckQuestion Table.sql ---------------

--------------- Fragment begins: #441: 441_CREATE SafeCheckCategory Table.sql ---------------

-- Change script: #441: 441_CREATE SafeCheckCategory Table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckCategory')
BEGIN
	DROP TABLE [dbo].[SafeCheckCategory]
END
GO

DELETE FROM changelog WHERE change_number = 441 AND delta_set = 'Main'
GO

--------------- Fragment ends: #441: 441_CREATE SafeCheckCategory Table.sql ---------------

--------------- Fragment begins: #440: 440_Alter_length_of_hazard_description.sql ---------------

-- Change script: #440: 440_Alter_length_of_hazard_description.sql

DELETE FROM changelog WHERE change_number = 440 AND delta_set = 'Main'
GO

--------------- Fragment ends: #440: 440_Alter_length_of_hazard_description.sql ---------------

--------------- Fragment begins: #439: 439_Update_StatutoryResponsibilityTaskTemplate.sql ---------------

-- Change script: #439: 439_Update_StatutoryResponsibilityTaskTemplate.sql

DELETE FROM changelog WHERE change_number = 439 AND delta_set = 'Main'
GO

--------------- Fragment ends: #439: 439_Update_StatutoryResponsibilityTaskTemplate.sql ---------------

--------------- Fragment begins: #438: 438_Grant CRU to EscalationOffWorkReminder.sql ---------------

-- Change script: #438: 438_Grant CRU to EscalationOffWorkReminder.sql

DELETE FROM changelog WHERE change_number = 438 AND delta_set = 'Main'
GO

--------------- Fragment ends: #438: 438_Grant CRU to EscalationOffWorkReminder.sql ---------------

--------------- Fragment begins: #437: 437_Update_StatutoryResponsibilityTemplate.sql ---------------

-- Change script: #437: 437_Update_StatutoryResponsibilityTemplate.sql

DELETE FROM changelog WHERE change_number = 437 AND delta_set = 'Main'
GO

--------------- Fragment ends: #437: 437_Update_StatutoryResponsibilityTemplate.sql ---------------

--------------- Fragment begins: #436: 436_INSERT_StatutoryResponsibilityTaskTemplate.sql ---------------

-- Change script: #436: 436_INSERT_StatutoryResponsibilityTaskTemplate.sql

DELETE FROM changelog WHERE change_number = 436 AND delta_set = 'Main'
GO

--------------- Fragment ends: #436: 436_INSERT_StatutoryResponsibilityTaskTemplate.sql ---------------

--------------- Fragment begins: #435: 435_INSERT_StatutoryResponsibilityTemplate.sql ---------------

-- Change script: #435: 435_INSERT_StatutoryResponsibilityTemplate.sql

DELETE FROM changelog WHERE change_number = 435 AND delta_set = 'Main'
GO

--------------- Fragment ends: #435: 435_INSERT_StatutoryResponsibilityTemplate.sql ---------------

--------------- Fragment begins: #434: 434_CREATE EscalationOffWorkReminder table.sql ---------------

-- Change script: #434: 434_CREATE EscalationOffWorkReminder table.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EscalationOffWorkReminder')
BEGIN
	DROP TABLE [EscalationOffWorkReminder];
END

DELETE FROM changelog WHERE change_number = 434 AND delta_set = 'Main'
GO

--------------- Fragment ends: #434: 434_CREATE EscalationOffWorkReminder table.sql ---------------

--------------- Fragment begins: #433: 433_ALTER AccidentRecord Add IsReportable.sql ---------------

-- Change script: #433: 433_ALTER AccidentRecord Add IsReportable.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccidentRecord' AND COLUMN_NAME = 'IsReportable')
begin
	alter table AccidentRecord drop column IsReportable
end
go

DELETE FROM changelog WHERE change_number = 433 AND delta_set = 'Main'
GO

--------------- Fragment ends: #433: 433_ALTER AccidentRecord Add IsReportable.sql ---------------

--------------- Fragment begins: #432: 432_ALTER Injury.sql ---------------

-- Change script: #432: 432_ALTER Injury.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Injury' AND COLUMN_NAME = 'CompanyId')
begin
	alter table Injury drop column CompanyId
end

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Injury' AND COLUMN_NAME = 'AccidentRecordId')
begin
	alter table Injury drop column AccidentRecordId
end
go

DELETE FROM changelog WHERE change_number = 432 AND delta_set = 'Main'
GO

--------------- Fragment ends: #432: 432_ALTER Injury.sql ---------------

--------------- Fragment begins: #431: 431_ALTER AccidentRecordInjury.sql ---------------

-- Change script: #431: 431_ALTER AccidentRecordInjury.sql

DELETE FROM changelog WHERE change_number = 431 AND delta_set = 'Main'
GO

--------------- Fragment ends: #431: 431_ALTER AccidentRecordInjury.sql ---------------

--------------- Fragment begins: #430: 430_ALTER Injury.sql ---------------

-- Change script: #430: 430_ALTER Injury.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccidentRecordInjury' AND COLUMN_NAME = 'CompanyId')
begin
	alter table AccidentRecordInjury drop column CompanyId
end

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccidentRecordInjury' AND COLUMN_NAME = 'AccidentRecordId')
begin
	alter table AccidentRecordInjury drop column AccidentRecordInjuryId
end
go

DELETE FROM changelog WHERE change_number = 430 AND delta_set = 'Main'
GO

--------------- Fragment ends: #430: 430_ALTER Injury.sql ---------------

--------------- Fragment begins: #429: 429_CREATE AccidentRecordNextStepsSection.sql ---------------

-- Change script: #429: 429_CREATE AccidentRecordNextStepsSection.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AccidentRecordNextStepSection')
BEGIN
	DROP TABLE [dbo].[AccidentRecordNextStepSection]
END
GO

DELETE FROM changelog WHERE change_number = 429 AND delta_set = 'Main'
GO

--------------- Fragment ends: #429: 429_CREATE AccidentRecordNextStepsSection.sql ---------------

--------------- Fragment begins: #428: 428_INSERT INTO DocumentType Table.sql ---------------

-- Change script: #428: 428_INSERT INTO DocumentType Table.sql

DELETE FROM changelog WHERE change_number = 428 AND delta_set = 'Main'
GO

--------------- Fragment ends: #428: 428_INSERT INTO DocumentType Table.sql ---------------

--------------- Fragment begins: #427: 427_ALTER AccidentRecord Grant Alter.sql ---------------

-- Change script: #427: 427_ALTER AccidentRecord Grant Alter.sql

REVOKE ALTER ON [AccidentRecord] TO [AllowAll]

DELETE FROM changelog WHERE change_number = 427 AND delta_set = 'Main'
GO

--------------- Fragment ends: #427: 427_ALTER AccidentRecord Grant Alter.sql ---------------

--------------- Fragment begins: #426: 426_ALTER AccidentRecord.sql ---------------

-- Change script: #426: 426_ALTER AccidentRecord.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccidentRecord' AND COLUMN_NAME = 'CompanyId')
begin
	alter table AccidentRecord drop column CompanyId
end
go

DELETE FROM changelog WHERE change_number = 426 AND delta_set = 'Main'
GO

--------------- Fragment ends: #426: 426_ALTER AccidentRecord.sql ---------------

--------------- Fragment begins: #425: 425_INSERT BodyPart.sql ---------------

-- Change script: #425: 425_INSERT BodyPart.sql

DELETE FROM [BodyPart]

DELETE FROM changelog WHERE change_number = 425 AND delta_set = 'Main'
GO

--------------- Fragment ends: #425: 425_INSERT BodyPart.sql ---------------

--------------- Fragment begins: #424: 424_INSERT Injury.sql ---------------

-- Change script: #424: 424_INSERT Injury.sql

DELETE FROM [Injury]

DELETE FROM changelog WHERE change_number = 424 AND delta_set = 'Main'
GO

--------------- Fragment ends: #424: 424_INSERT Injury.sql ---------------

--------------- Fragment begins: #423: 423_INSERT CauseOfAccident.sql ---------------

-- Change script: #423: 423_INSERT CauseOfAccident.sql

DELETE FROM [CauseOfAccident]

DELETE FROM changelog WHERE change_number = 423 AND delta_set = 'Main'
GO

--------------- Fragment ends: #423: 423_INSERT CauseOfAccident.sql ---------------

--------------- Fragment begins: #422: 422_INSERT AccidentType.sql ---------------

-- Change script: #422: 422_INSERT AccidentType.sql

DELETE FROM [AccidentType]

DELETE FROM changelog WHERE change_number = 422 AND delta_set = 'Main'
GO

--------------- Fragment ends: #422: 422_INSERT AccidentType.sql ---------------

--------------- Fragment begins: #421: 421_ALTER AccidentRecord.sql ---------------

-- Change script: #421: 421_ALTER AccidentRecord.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AccidentRecord' AND COLUMN_NAME = 'JurisdictionId' AND DATA_TYPE = 'bigint')
begin
	alter table AccidentRecord drop column JurisdictionId
	alter table AccidentRecord add JusridictionId bigint null
end
go

DELETE FROM changelog WHERE change_number = 421 AND delta_set = 'Main'
GO

--------------- Fragment ends: #421: 421_ALTER AccidentRecord.sql ---------------

--------------- Fragment begins: #420: 420_CREATE AccidentRecordDocument.sql ---------------

-- Change script: #420: 420_CREATE AccidentRecordDocument.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AccidentRecordDocument')
BEGIN
	DROP TABLE [dbo].[AccidentRecordDocument]
END
GO

DELETE FROM changelog WHERE change_number = 420 AND delta_set = 'Main'
GO

--------------- Fragment ends: #420: 420_CREATE AccidentRecordDocument.sql ---------------

--------------- Fragment begins: #419: 419_CREATE AccidentRecordBodyPart.sql ---------------

-- Change script: #419: 419_CREATE AccidentRecordBodyPart.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AccidentRecordBodyPart')
BEGIN
	DROP TABLE [dbo].[AccidentRecordBodyPart]
END
GO

DELETE FROM changelog WHERE change_number = 419 AND delta_set = 'Main'
GO

--------------- Fragment ends: #419: 419_CREATE AccidentRecordBodyPart.sql ---------------

--------------- Fragment begins: #418: 418_INSERT BodyPart.sql ---------------

-- Change script: #418: 418_INSERT BodyPart.sql

DELETE FROM [BodyPart]

DELETE FROM changelog WHERE change_number = 418 AND delta_set = 'Main'
GO

--------------- Fragment ends: #418: 418_INSERT BodyPart.sql ---------------

--------------- Fragment begins: #417: 417_CREATE BodyPart.sql ---------------

-- Change script: #417: 417_CREATE BodyPart.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'BodyPart')
BEGIN
	DROP TABLE [dbo].[BodyPart]
END
GO

DELETE FROM changelog WHERE change_number = 417 AND delta_set = 'Main'
GO

--------------- Fragment ends: #417: 417_CREATE BodyPart.sql ---------------

--------------- Fragment begins: #416: 416_CREATE AccidentRecordInjury.sql ---------------

-- Change script: #416: 416_CREATE AccidentRecordInjury.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AccidentRecordInjury')
BEGIN
	DROP TABLE [dbo].[AccidentRecordInjury]
END
GO

DELETE FROM changelog WHERE change_number = 416 AND delta_set = 'Main'
GO

--------------- Fragment ends: #416: 416_CREATE AccidentRecordInjury.sql ---------------

--------------- Fragment begins: #415: 415_INSERT Injury.sql ---------------

-- Change script: #415: 415_INSERT Injury.sql

DELETE FROM [Injury]

DELETE FROM changelog WHERE change_number = 415 AND delta_set = 'Main'
GO

--------------- Fragment ends: #415: 415_INSERT Injury.sql ---------------

--------------- Fragment begins: #414: 414_CREATE Injury.sql ---------------

-- Change script: #414: 414_CREATE Injury.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Injury')
BEGIN
	DROP TABLE [dbo].[Injury]
END
GO

DELETE FROM changelog WHERE change_number = 414 AND delta_set = 'Main'
GO

--------------- Fragment ends: #414: 414_CREATE Injury.sql ---------------

--------------- Fragment begins: #413: 413_INSERT CauseOfAccident.sql ---------------

-- Change script: #413: 413_INSERT CauseOfAccident.sql

DELETE FROM [CauseOfAccident]

DELETE FROM changelog WHERE change_number = 413 AND delta_set = 'Main'
GO

--------------- Fragment ends: #413: 413_INSERT CauseOfAccident.sql ---------------

--------------- Fragment begins: #412: 412_CREATE CauseOfAccident.sql ---------------

-- Change script: #412: 412_CREATE CauseOfAccident.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CauseOfAccident')
BEGIN
	DROP TABLE [dbo].[CauseOfAccident]
END
GO

DELETE FROM changelog WHERE change_number = 412 AND delta_set = 'Main'
GO

--------------- Fragment ends: #412: 412_CREATE CauseOfAccident.sql ---------------

--------------- Fragment begins: #411: 411_INSERT AccidentType.sql ---------------

-- Change script: #411: 411_INSERT AccidentType.sql

DELETE FROM [AccidentType]

DELETE FROM changelog WHERE change_number = 411 AND delta_set = 'Main'
GO

--------------- Fragment ends: #411: 411_INSERT AccidentType.sql ---------------

--------------- Fragment begins: #410: 410_CREATE AccidentType.sql ---------------

-- Change script: #410: 410_CREATE AccidentType.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AccidentType')
BEGIN
	DROP TABLE [dbo].[AccidentType]
END
GO

DELETE FROM changelog WHERE change_number = 410 AND delta_set = 'Main'
GO

--------------- Fragment ends: #410: 410_CREATE AccidentType.sql ---------------

--------------- Fragment begins: #409: 409_INSERT Jurisdiction.sql ---------------

-- Change script: #409: 409_INSERT Jurisdiction.sql

DELETE FROM [Jurisdiction]

DELETE FROM changelog WHERE change_number = 409 AND delta_set = 'Main'
GO

--------------- Fragment ends: #409: 409_INSERT Jurisdiction.sql ---------------

--------------- Fragment begins: #408: 408_CREATE Jurisdiction.sql ---------------

-- Change script: #408: 408_CREATE Jurisdiction.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Jurisdiction')
BEGIN
	DROP TABLE [dbo].[Jurisdiction]
END
GO

DELETE FROM changelog WHERE change_number = 408 AND delta_set = 'Main'
GO

--------------- Fragment ends: #408: 408_CREATE Jurisdiction.sql ---------------

--------------- Fragment begins: #407: 407_CREATE AccidentRecord.sql ---------------

-- Change script: #407: 407_CREATE AccidentRecord.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AccidentRecord')
BEGIN
	DROP TABLE [dbo].[AccidentRecord]
END
GO

DELETE FROM changelog WHERE change_number = 407 AND delta_set = 'Main'
GO

--------------- Fragment ends: #407: 407_CREATE AccidentRecord.sql ---------------

--------------- Fragment begins: #406: 406_INSERT_RolePermissions.sql ---------------

-- Change script: #406: 406_INSERT_RolePermissions.sql

--User Admin
IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 59)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 59 
END

IF  EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 60)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 60 
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 61)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 61
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 62)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = 'BACF7C01-D210-4DBC-942F-15D8456D3B92' and [PermissionId] = 62
END

--Health and Safety Manager
IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 59)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 59
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 60)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 60
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 61)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 61
END

IF EXISTS (SELECT ID FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 62)
BEGIN
	DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = '1E382767-93DD-47E2-88F2-B3E7F7648642' and [PermissionId] = 62
END
GO


DELETE FROM changelog WHERE change_number = 406 AND delta_set = 'Main'
GO

--------------- Fragment ends: #406: 406_INSERT_RolePermissions.sql ---------------

--------------- Fragment begins: #405: 405_INSERT_Permission.sql ---------------

-- Change script: #405: 405_INSERT_Permission.sql

IF  EXISTS (SELECT ID FROM [dbo].[Permission] WHERE PermissionTargetId = 18 and PermissionActivityId = 1)
BEGIN
	delete from Permission WHERE PermissionTargetId = 18 and PermissionActivityId = 1)
END

IF  EXISTS (SELECT ID FROM [dbo].[Permission] WHERE PermissionTargetId = 18 and PermissionActivityId = 2)
BEGIN
	delete from Permission WHERE PermissionTargetId = 18 and PermissionActivityId = 2)
END

IF  EXISTS (SELECT ID FROM [dbo].[Permission] WHERE PermissionTargetId = 18 and PermissionActivityId = 3)
BEGIN
	delete from Permission WHERE PermissionTargetId = 18 and PermissionActivityId = 3)
END

IF  EXISTS (SELECT ID FROM [dbo].[Permission] WHERE PermissionTargetId = 18 and PermissionActivityId = 4)
BEGIN
	delete from Permission WHERE PermissionTargetId = 18 and PermissionActivityId = 4)
END



DELETE FROM changelog WHERE change_number = 405 AND delta_set = 'Main'
GO

--------------- Fragment ends: #405: 405_INSERT_Permission.sql ---------------

--------------- Fragment begins: #404: 404_INSERT_PermissionTarget.sql ---------------

-- Change script: #404: 404_INSERT_PermissionTarget.sql

IF EXISTS (SELECT ID FROM [dbo].[PermissionTarget] WHERE ID = 18)
BEGIN
	DELETE FROM [PermissionTarget]
	WHERE ID = 18
END

DELETE FROM changelog WHERE change_number = 404 AND delta_set = 'Main'
GO

--------------- Fragment ends: #404: 404_INSERT_PermissionTarget.sql ---------------

--------------- Fragment begins: #403: 403_DROP PersonalRiskAssessmentEmployee.sql ---------------

-- Change script: #403: 403_DROP PersonalRiskAssessmentEmployee.sql

DELETE FROM changelog WHERE change_number = 403 AND delta_set = 'Main'
GO

--------------- Fragment ends: #403: 403_DROP PersonalRiskAssessmentEmployee.sql ---------------

--------------- Fragment begins: #402: 402_ALTER PersonalRiskAssessmentEmployeet.sql ---------------

-- Change script: #402: 402_ALTER PersonalRiskAssessmentEmployeet.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessmentEmployee' AND COLUMN_NAME = 'EmployeeId' AND DATA_TYPE = 'uniqueidentifier')
BEGIN
	ALTER TABLE [dbo].[PersonalRiskAssessmentEmployee]
	DROP COLUMN [EmployeeId] 

	ALTER TABLE [dbo].[PersonalRiskAssessmentEmployee]
	ADD [EmployeeId] [bigint] NOT NULL
END
GO

DELETE FROM changelog WHERE change_number = 402 AND delta_set = 'Main'
GO

--------------- Fragment ends: #402: 402_ALTER PersonalRiskAssessmentEmployeet.sql ---------------

--------------- Fragment begins: #401: 401_ALTER EmployeeChecklist add assessmentdate and assessedbyemployeeid.sql ---------------

-- Change script: #401: 401_ALTER EmployeeChecklist add assessmentdate and assessedbyemployeeid.sql


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'AssessedByEmployeeId')
BEGIN
	ALTER TABLE [EmployeeChecklist]
	DROP COLUMN [AssessedByEmployeeId] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'AssessmentDate')
BEGIN
	ALTER TABLE [EmployeeChecklist]
	DROP COLUMN [AssessmentDate] 
END

DELETE FROM changelog WHERE change_number = 401 AND delta_set = 'Main'
GO

--------------- Fragment ends: #401: 401_ALTER EmployeeChecklist add assessmentdate and assessedbyemployeeid.sql ---------------

--------------- Fragment begins: #400: 400_GANT PersonalRiskAssessmentEmployeet Table.sql ---------------

-- Change script: #400: 400_GANT PersonalRiskAssessmentEmployeet Table.sql



DELETE FROM changelog WHERE change_number = 400 AND delta_set = 'Main'
GO

--------------- Fragment ends: #400: 400_GANT PersonalRiskAssessmentEmployeet Table.sql ---------------

--------------- Fragment begins: #399: 399_ALTER EmployeeChecklist add IsFurtherActionRequired.sql ---------------

-- Change script: #399: 399_ALTER EmployeeChecklist add IsFurtherActionRequired.sql


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'IsFurtherActionRequired')
BEGIN
	ALTER TABLE [EmployeeChecklist]
	DROP COLUMN [IsFurtherActionRequired] 
END

DELETE FROM changelog WHERE change_number = 399 AND delta_set = 'Main'
GO

--------------- Fragment ends: #399: 399_ALTER EmployeeChecklist add IsFurtherActionRequired.sql ---------------

--------------- Fragment begins: #398: 398_CREATE PersonalRiskAssessmentEmployeet Table.sql ---------------

-- Change script: #398: 398_CREATE PersonalRiskAssessmentEmployeet Table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PersonalRiskAssessmentEmployee')
BEGIN
	DROP TABLE [dbo].[PersonalRiskAssessmentEmployee]
END
GO

DELETE FROM changelog WHERE change_number = 398 AND delta_set = 'Main'
GO

--------------- Fragment ends: #398: 398_CREATE PersonalRiskAssessmentEmployeet Table.sql ---------------

--------------- Fragment begins: #397: 397_Update_StatutoryResponsibilityTemplate.sql ---------------

-- Change script: #397: 397_Update_StatutoryResponsibilityTemplate.sql

DELETE FROM changelog WHERE change_number = 397 AND delta_set = 'Main'
GO

--------------- Fragment ends: #397: 397_Update_StatutoryResponsibilityTemplate.sql ---------------

--------------- Fragment begins: #396: 396_ALTER ResponsibilityTask add TemplateCreatedFrom.sql ---------------

-- Change script: #396: 396_ALTER ResponsibilityTask add TemplateCreatedFrom.sql

USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'StatutoryResponsibilityTaskTemplateCreatedFromId')
BEGIN
	ALTER TABLE [Task]
	DROP [StatutoryResponsibilityTaskTemplateCreatedFromId] 
END

DELETE FROM changelog WHERE change_number = 396 AND delta_set = 'Main'
GO

--------------- Fragment ends: #396: 396_ALTER ResponsibilityTask add TemplateCreatedFrom.sql ---------------

--------------- Fragment begins: #395: 395_ALTER Responsibility add TemplateCreatedFrom.sql ---------------

-- Change script: #395: 395_ALTER Responsibility add TemplateCreatedFrom.sql

USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Responsibility' AND COLUMN_NAME = 'StatutoryResponsibilityTemplateCreatedFromId')
BEGIN
	ALTER TABLE [Responsibility]
	DROP [StatutoryResponsibilityTemplateCreatedFromId] 
END

DELETE FROM changelog WHERE change_number = 395 AND delta_set = 'Main'
GO

--------------- Fragment ends: #395: 395_ALTER Responsibility add TemplateCreatedFrom.sql ---------------

--------------- Fragment begins: #394: 394_ALTER Responsibility drop created from wizard.sql ---------------

-- Change script: #394: 394_ALTER Responsibility drop created from wizard.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Responsibility' AND COLUMN_NAME = 'IsCreatedByWizard')
BEGIN
	ALTER TABLE [Responsibility]
	ADD [IsCreatedByWizard] [bit] NOT NULL CONSTRAINT [DF_Responsibility_IsCreatedByWizard]  DEFAULT ((0))
END

DELETE FROM changelog WHERE change_number = 394 AND delta_set = 'Main'
GO

--------------- Fragment ends: #394: 394_ALTER Responsibility drop created from wizard.sql ---------------

--------------- Fragment begins: #393: 393_ALTER Responsibility add created from wizard column correctly.sql ---------------

-- Change script: #393: 393_ALTER Responsibility add created from wizard column correctly.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Responsibility' AND COLUMN_NAME = 'IsCreatedByWizard')
BEGIN
	ALTER TABLE [dbo].[Responsibility] 
	DROP CONSTRAINT [DF_Responsibility_IsCreatedByWizard]
	
	ALTER TABLE [Responsibility]
	DROP COLUMN [IsCreatedByWizard] 
	
END

DELETE FROM changelog WHERE change_number = 393 AND delta_set = 'Main'
GO

--------------- Fragment ends: #393: 393_ALTER Responsibility add created from wizard column correctly.sql ---------------

--------------- Fragment begins: #392: 392_ALTER Responsibility add created from wizard column.sql ---------------

-- Change script: #392: 392_ALTER Responsibility add created from wizard column.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Responsibility' AND COLUMN_NAME = 'IsCreatedByWizard')
BEGIN
	ALTER TABLE [dbo].[Responsibility] 
	DROP CONSTRAINT [DF_Responsibility_IsCreatedByWizard]
	
	ALTER TABLE [Responsibility]
	DROP COLUMN [IsCreatedByWizard] 
	
END

DELETE FROM changelog WHERE change_number = 392 AND delta_set = 'Main'
GO

--------------- Fragment ends: #392: 392_ALTER Responsibility add created from wizard column.sql ---------------

--------------- Fragment begins: #391: 391_INSERT_StatutoryResponsibilityTaskTemplate.sql ---------------

-- Change script: #391: 391_INSERT_StatutoryResponsibilityTaskTemplate.sql

DELETE FROM changelog WHERE change_number = 391 AND delta_set = 'Main'
GO

--------------- Fragment ends: #391: 391_INSERT_StatutoryResponsibilityTaskTemplate.sql ---------------

--------------- Fragment begins: #390: 390_INSERT_INTO ResponsibilityCategory.sql ---------------

-- Change script: #390: 390_INSERT_INTO ResponsibilityCategory.sql

DELETE FROM changelog WHERE change_number = 390 AND delta_set = 'Main'
GO

--------------- Fragment ends: #390: 390_INSERT_INTO ResponsibilityCategory.sql ---------------

--------------- Fragment begins: #389: 389_INSERT_StatutoryResponsibilityTemplate.sql ---------------

-- Change script: #389: 389_INSERT_StatutoryResponsibilityTemplate.sql

DELETE FROM changelog WHERE change_number = 389 AND delta_set = 'Main'
GO

--------------- Fragment ends: #389: 389_INSERT_StatutoryResponsibilityTemplate.sql ---------------

--------------- Fragment begins: #388: 388_ALTER_TABLE StatutoryResponsibilityTemplate .sql ---------------

-- Change script: #388: 388_ALTER_TABLE StatutoryResponsibilityTemplate .sql

IF  NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'StatutoryResponsibilityTemplate' AND COLUMN_NAME = 'SectorId')
BEGIN
    ALTER TABLE StatutoryResponsibilityTemplate
    ADD SectorId [bigint] NULL
END
GO

DELETE FROM changelog WHERE change_number = 388 AND delta_set = 'Main'
GO

--------------- Fragment ends: #388: 388_ALTER_TABLE StatutoryResponsibilityTemplate .sql ---------------

--------------- Fragment begins: #387: 387_GRANT_StatutoryResponsibilityPermissions_for_allowSelectInsertUpdate.sql ---------------

-- Change script: #387: 387_GRANT_StatutoryResponsibilityPermissions_for_allowSelectInsertUpdate.sql

DELETE FROM changelog WHERE change_number = 387 AND delta_set = 'Main'
GO

--------------- Fragment ends: #387: 387_GRANT_StatutoryResponsibilityPermissions_for_allowSelectInsertUpdate.sql ---------------

--------------- Fragment begins: #386: 386_Update_StatutoryResponsibilityTemplate.sql ---------------

-- Change script: #386: 386_Update_StatutoryResponsibilityTemplate.sql

DELETE FROM changelog WHERE change_number = 386 AND delta_set = 'Main'
GO

--------------- Fragment ends: #386: 386_Update_StatutoryResponsibilityTemplate.sql ---------------

--------------- Fragment begins: #385: 385_GRANT_StatutoryResponsibilityPermissions.sql ---------------

-- Change script: #385: 385_GRANT_StatutoryResponsibilityPermissions.sql

DELETE FROM changelog WHERE change_number = 385 AND delta_set = 'Main'
GO

--------------- Fragment ends: #385: 385_GRANT_StatutoryResponsibilityPermissions.sql ---------------

--------------- Fragment begins: #384: 384_INSERT_StatutoryResponsibilityTaskTemplate.sql ---------------

-- Change script: #384: 384_INSERT_StatutoryResponsibilityTaskTemplate.sql

DELETE FROM changelog WHERE change_number = 384 AND delta_set = 'Main'
GO

--------------- Fragment ends: #384: 384_INSERT_StatutoryResponsibilityTaskTemplate.sql ---------------

--------------- Fragment begins: #383: 383_INSERT_StatutoryResponsibilityTemplate.sql ---------------

-- Change script: #383: 383_INSERT_StatutoryResponsibilityTemplate.sql

DELETE FROM changelog WHERE change_number = 383 AND delta_set = 'Main'
GO

--------------- Fragment ends: #383: 383_INSERT_StatutoryResponsibilityTemplate.sql ---------------

--------------- Fragment begins: #382: 382_CREATE_StatutoryResponsibilityTaskTemplate.sql ---------------

-- Change script: #382: 382_CREATE_StatutoryResponsibilityTaskTemplate.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'StatutoryResponsibilityTaskTemplate')
BEGIN
	DROP TABLE [dbo].[StatutoryResponsibilityTaskTemplate]
END
GO

DELETE FROM changelog WHERE change_number = 382 AND delta_set = 'Main'
GO

--------------- Fragment ends: #382: 382_CREATE_StatutoryResponsibilityTaskTemplate.sql ---------------

--------------- Fragment begins: #381: 381_CREATE_StatutoryResponsibilityTemplate.sql ---------------

-- Change script: #381: 381_CREATE_StatutoryResponsibilityTemplate.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'StatutoryResponsibilityTemplate')
BEGIN
	DROP TABLE [dbo].[StatutoryResponsibilityTemplate]
END
GO

DELETE FROM changelog WHERE change_number = 381 AND delta_set = 'Main'
GO

--------------- Fragment ends: #381: 381_CREATE_StatutoryResponsibilityTemplate.sql ---------------

--------------- Fragment begins: #380: 380_INSERT_INTO ResponsibilityCategory.sql ---------------

-- Change script: #380: 380_INSERT_INTO ResponsibilityCategory.sql

DELETE FROM changelog WHERE change_number = 380 AND delta_set = 'Main'
GO

--------------- Fragment ends: #380: 380_INSERT_INTO ResponsibilityCategory.sql ---------------

--------------- Fragment begins: #379: 379_ALTER Responsibility.sql ---------------

-- Change script: #379: 379_ALTER Responsibility.sql

DELETE FROM changelog WHERE change_number = 379 AND delta_set = 'Main'
GO

--------------- Fragment ends: #379: 379_ALTER Responsibility.sql ---------------

--------------- Fragment begins: #378: 378_INSERT_INTO ResponsibilityCategory.sql ---------------

-- Change script: #378: 378_INSERT_INTO ResponsibilityCategory.sql

DELETE FROM changelog WHERE change_number = 378 AND delta_set = 'Main'
GO

--------------- Fragment ends: #378: 378_INSERT_INTO ResponsibilityCategory.sql ---------------

--------------- Fragment begins: #377: 377_INSERT HazardTypes .sql ---------------

-- Change script: #377: 377_INSERT HazardTypes .sql
IF EXISTS (SELECT top  1 * FROM [BusinessSafe].[dbo].[HazardType] WHERE ID = 3)
BEGIN
	DELETE
	FROM [BusinessSafe].[dbo].[HazardType]
	WHERE id = 3
END




DELETE FROM changelog WHERE change_number = 377 AND delta_set = 'Main'
GO

--------------- Fragment ends: #377: 377_INSERT HazardTypes .sql ---------------

--------------- Fragment begins: #376: 376_ALTER COLUMN Task Title.sql ---------------

-- Change script: #376: 376_ALTER COLUMN Task Title.sql



DELETE FROM changelog WHERE change_number = 376 AND delta_set = 'Main'
GO

--------------- Fragment ends: #376: 376_ALTER COLUMN Task Title.sql ---------------

--------------- Fragment begins: #375: 375_ALTER TABLE User.sql ---------------

-- Change script: #375: 375_ALTER TABLE User.sql

alter table [user] drop constraint UC_EmployeeId

DELETE FROM changelog WHERE change_number = 375 AND delta_set = 'Main'
GO

--------------- Fragment ends: #375: 375_ALTER TABLE User.sql ---------------

--------------- Fragment begins: #374: 374_ALTER TABLE RolePermission.sql ---------------

-- Change script: #374: 374_ALTER TABLE RolePermission.sql
USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'PK_RolesPermissions2' )
BEGIN
	ALTER TABLE [RolesPermissions] 
	DROP CONSTRAINT [PK_RolesPermissions2] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RolesPermissions' AND COLUMN_NAME = 'Id')
BEGIN
	ALTER TABLE [RolesPermissions]
	DROP COLUMN [Id]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RolesPermissions' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [RolesPermissions]
	DROP COLUMN [CreatedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RolesPermissions' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [RolesPermissions]
	DROP COLUMN [CreatedBy] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RolesPermissions' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [RolesPermissions]
	DROP COLUMN [LastModifiedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RolesPermissions' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [RolesPermissions]
	DROP COLUMN [LastModifiedBy]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RolesPermissions' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [RolesPermissions]
	DROP COLUMN [Deleted]
END
GO

DELETE FROM changelog WHERE change_number = 374 AND delta_set = 'Main'
GO

--------------- Fragment ends: #374: 374_ALTER TABLE RolePermission.sql ---------------

--------------- Fragment begins: #373: 373_UPDATE Fire Question Information.sql ---------------

-- Change script: #373: 373_UPDATE Fire Question Information.sql


DELETE FROM changelog WHERE change_number = 373 AND delta_set = 'Main'
GO

--------------- Fragment ends: #373: 373_UPDATE Fire Question Information.sql ---------------

--------------- Fragment begins: #372: 372_ALTER FireRiskAssessmentFireSafetlyControlMeasures add entity columns.sql ---------------

-- Change script: #372: 372_ALTER FireRiskAssessmentFireSafetlyControlMeasures add entity columns.sql


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'PK_RiskAssessmentsNonEmployees2' )
BEGIN
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures] 
	DROP CONSTRAINT [PK_FireRiskAssessmentFireSafetlyControlMeasures2] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'Id')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessmentFireSafetlyControlMeasures] 
	DROP CONSTRAINT [DF_FireRiskAssessmentFireSafetlyControlMeasures_Deleted]
	
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	DROP COLUMN [Id] 
	
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessmentFireSafetlyControlMeasures] 
	DROP CONSTRAINT [DF_FireRiskAssessmentFireSafetlyControlMeasures_Deleted]
	
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	DROP COLUMN [Deleted] 
	
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessmentFireSafetlyControlMeasures] 
	DROP CONSTRAINT [DF_FireRiskAssessmentFireSafetlyControlMeasures_CreatedOn]
	
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	DROP COLUMN [CreatedOn] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessmentFireSafetlyControlMeasures] 
	DROP CONSTRAINT [DF_FireRiskAssessmentFireSafetlyControlMeasures_CreatedBy]
	
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	DROP COLUMN [CreatedBy] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	DROP COLUMN [LastModifiedOn] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
	DROP COLUMN [LastModifiedBy] 
END
	

DELETE FROM changelog WHERE change_number = 372 AND delta_set = 'Main'
GO

--------------- Fragment ends: #372: 372_ALTER FireRiskAssessmentFireSafetlyControlMeasures add entity columns.sql ---------------

--------------- Fragment begins: #371: 371_ALTER TABLE FireRiskAssessmentSourceOfFuel.sql ---------------

-- Change script: #371: 371_ALTER TABLE FireRiskAssessmentSourceOfFuel.sql
USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'PK_FireRiskAssessmentSourceOfFuels2' )
BEGIN
	ALTER TABLE [FireRiskAssessmentSourceOfFuels] 
	DROP CONSTRAINT [PK_FireRiskAssessmentSourceOfFuels2] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentSourceOfFuels' AND COLUMN_NAME = 'Id')
BEGIN
	ALTER TABLE [FireRiskAssessmentSourceOfFuels]
	DROP COLUMN [Id]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentSourceOfFuels' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [FireRiskAssessmentSourceOfFuels]
	DROP COLUMN [CreatedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentSourceOfFuels' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [FireRiskAssessmentSourceOfFuels]
	DROP COLUMN [CreatedBy] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentSourceOfFuels' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [FireRiskAssessmentSourceOfFuels]
	DROP COLUMN [LastModifiedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentSourceOfFuels' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [FireRiskAssessmentSourceOfFuels]
	DROP COLUMN [LastModifiedBy]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentSourceOfFuels' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [FireRiskAssessmentSourceOfFuels]
	DROP COLUMN [Deleted]
END
GO

DELETE FROM changelog WHERE change_number = 371 AND delta_set = 'Main'
GO

--------------- Fragment ends: #371: 371_ALTER TABLE FireRiskAssessmentSourceOfFuel.sql ---------------

--------------- Fragment begins: #370: 370_ALTER TABLE FireRiskAssessmentSourceOfIgnition.sql ---------------

-- Change script: #370: 370_ALTER TABLE FireRiskAssessmentSourceOfIgnition.sql
USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'PK_FireRiskAssessmentSourceOfIgnitions2' )
BEGIN
	ALTER TABLE [FireRiskAssessmentSourceOfIgnitions] 
	DROP CONSTRAINT [PK_FireRiskAssessmentSourceOfIgnitions2] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentSourceOfIgnitions' AND COLUMN_NAME = 'Id')
BEGIN
	ALTER TABLE [FireRiskAssessmentSourceOfIgnitions]
	DROP COLUMN [Id]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentSourceOfIgnitions' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [FireRiskAssessmentSourceOfIgnitions]
	DROP COLUMN [CreatedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentSourceOfIgnitions' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [FireRiskAssessmentSourceOfIgnitions]
	DROP COLUMN [CreatedBy] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentSourceOfIgnitions' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [FireRiskAssessmentSourceOfIgnitions]
	DROP COLUMN [LastModifiedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentSourceOfIgnitions' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [FireRiskAssessmentSourceOfIgnitions]
	DROP COLUMN [LastModifiedBy]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentSourceOfIgnitions' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [FireRiskAssessmentSourceOfIgnitions]
	DROP COLUMN [Deleted]
END
GO

DELETE FROM changelog WHERE change_number = 370 AND delta_set = 'Main'
GO

--------------- Fragment ends: #370: 370_ALTER TABLE FireRiskAssessmentSourceOfIgnition.sql ---------------

--------------- Fragment begins: #369: 369_INSERT_Document_Type.sql ---------------

-- Change script: #369: 369_INSERT_Document_Type.sql
DELETE
FROM [BusinessSafe].[dbo].[DocumentType]
WHERE id IN (16)
GO



DELETE FROM changelog WHERE change_number = 369 AND delta_set = 'Main'
GO

--------------- Fragment ends: #369: 369_INSERT_Document_Type.sql ---------------

--------------- Fragment begins: #368: 368_BSO_Create_Indexes_and_stats.sql ---------------

-- Change script: #368: 368_BSO_Create_Indexes_and_stats.sql

DELETE FROM changelog WHERE change_number = 368 AND delta_set = 'Main'
GO

--------------- Fragment ends: #368: 368_BSO_Create_Indexes_and_stats.sql ---------------

--------------- Fragment begins: #367: 367_Create_indexes_for_task_search.sql ---------------

-- Change script: #367: 367_Create_indexes_for_task_search.sql

DELETE FROM changelog WHERE change_number = 367 AND delta_set = 'Main'
GO

--------------- Fragment ends: #367: 367_Create_indexes_for_task_search.sql ---------------

--------------- Fragment begins: #366: 366_ALTER Task.sql ---------------

-- Change script: #366: 366_ALTER Task.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'SiteId')
BEGIN
	ALTER TABLE [dbo].Task 
	DROP CONSTRAINT [DF_Task_SiteId]
	
	ALTER TABLE Task
	DROP COLUMN [SiteId] 
	
END

DELETE FROM changelog WHERE change_number = 366 AND delta_set = 'Main'
GO

--------------- Fragment ends: #366: 366_ALTER Task.sql ---------------

--------------- Fragment begins: #365: 365_ALTER Task add TaskCompletedBy.sql ---------------

-- Change script: #365: 365_ALTER Task add TaskCompletedBy.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'TaskCompletedBy')
BEGIN
    ALTER TABLE [Task]
    DROP COLUMN [TaskCompletedBy] 
END
GO

DELETE FROM changelog WHERE change_number = 365 AND delta_set = 'Main'
GO

--------------- Fragment ends: #365: 365_ALTER Task add TaskCompletedBy.sql ---------------

--------------- Fragment begins: #364: 364_Alter_taskcompleteddate.sql ---------------

-- Change script: #364: 364_Alter_taskcompleteddate.sql

DELETE FROM changelog WHERE change_number = 364 AND delta_set = 'Main'
GO

--------------- Fragment ends: #364: 364_Alter_taskcompleteddate.sql ---------------

--------------- Fragment begins: #363: 363_INSERT INTO TaskCategory.sql ---------------

-- Change script: #363: 363_INSERT INTO TaskCategory.sql
USE [BusinessSafe]
GO
IF EXISTS(SELECT * FROM [dbo].[TaskCategory] WHERE [Id] = 7)
BEGIN
	DELETE FROM TaskCategory WHERE Id = 7
END
	
GO

DELETE FROM changelog WHERE change_number = 363 AND delta_set = 'Main'
GO

--------------- Fragment ends: #363: 363_INSERT INTO TaskCategory.sql ---------------

--------------- Fragment begins: #362: 362_ALTER Task.sql ---------------

-- Change script: #362: 362_ALTER Task.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'ResponsibilityId')
BEGIN
	ALTER TABLE [dbo].Task 
	DROP CONSTRAINT [DF_Task_ResponsibilityId]
	
	ALTER TABLE Task
	DROP COLUMN [ResponsibilityId] 
	
END

DELETE FROM changelog WHERE change_number = 362 AND delta_set = 'Main'
GO

--------------- Fragment ends: #362: 362_ALTER Task.sql ---------------

--------------- Fragment begins: #361: 361_Update ResponsibilityCategory.sql ---------------

-- Change script: #361: 361_Update ResponsibilityCategory.sql
use businesssafe
go

if exists (select top 1 id from Responsibilitycategory where id <> 11 and sequence = 0)
begin
	update Responsibilitycategory 
		set sequence = null
		where id <> 11
end		

if exists (select top 1 id from Responsibilitycategory where id = 11 and sequence = 99)
begin
	update Responsibilitycategory 
		set sequence = null 
		where id = 11
end	

DELETE FROM changelog WHERE change_number = 361 AND delta_set = 'Main'
GO

--------------- Fragment ends: #361: 361_Update ResponsibilityCategory.sql ---------------

--------------- Fragment begins: #360: 360_ALTER ResponsibilityCategory.sql ---------------

-- Change script: #360: 360_ALTER ResponsibilityCategory.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ResponsibilityCategory' AND COLUMN_NAME = 'Sequence')
BEGIN
	ALTER TABLE [dbo].[ResponsibilityCategory] 
	DROP CONSTRAINT [DF_ResponsibilityCategory_Sequence]
	
	ALTER TABLE [ResponsibilityCategory]
	DROP COLUMN [Sequence] 
	
END

DELETE FROM changelog WHERE change_number = 360 AND delta_set = 'Main'
GO

--------------- Fragment ends: #360: 360_ALTER ResponsibilityCategory.sql ---------------

--------------- Fragment begins: #359: 359_Add_FK_constraint_to_task_to_prevent_deletion_of_users.sql ---------------

-- Change script: #359: 359_Add_FK_constraint_to_task_to_prevent_deletion_of_users.sql

DELETE FROM changelog WHERE change_number = 359 AND delta_set = 'Main'
GO

--------------- Fragment ends: #359: 359_Add_FK_constraint_to_task_to_prevent_deletion_of_users.sql ---------------

--------------- Fragment begins: #358: 358_Update ResponsibilityCategory.sql ---------------

-- Change script: #358: 358_Update ResponsibilityCategory.sql
use businesssafe
go

if exists (select top 1 id from Responsibilitycategory where id = 4)
begin
	update Responsibilitycategory
	set Category = 'Management of Health and Safety'
	where id =  4
end

DELETE FROM changelog WHERE change_number = 358 AND delta_set = 'Main'
GO

--------------- Fragment ends: #358: 358_Update ResponsibilityCategory.sql ---------------

--------------- Fragment begins: #357: 357_ALTER HazardousSubstanceSafetyPhrase add entity columns.sql ---------------

-- Change script: #357: 357_ALTER HazardousSubstanceSafetyPhrase add entity columns.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstanceSafetyPhrase] 
	DROP CONSTRAINT [DF_HazardousSubstanceSafetyPhrase_Deleted]
	
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	DROP COLUMN [Deleted] 
	
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstanceSafetyPhrase] 
	DROP CONSTRAINT [DF_HazardousSubstanceSafetyPhrase_CreatedOn]
	
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	DROP COLUMN [CreatedOn] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstanceSafetyPhrase] 
	DROP CONSTRAINT [DF_HazardousSubstanceSafetyPhrase_CreatedBy]
	
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	DROP COLUMN [CreatedBy] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	DROP COLUMN [LastModifiedOn] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	DROP COLUMN [LastModifiedBy] 
END
	

DELETE FROM changelog WHERE change_number = 357 AND delta_set = 'Main'
GO

--------------- Fragment ends: #357: 357_ALTER HazardousSubstanceSafetyPhrase add entity columns.sql ---------------

--------------- Fragment begins: #356: 356_ALTER HazardousSubstanceRiskPhrase add entity columns.sql ---------------

-- Change script: #356: 356_ALTER HazardousSubstanceRiskPhrase add entity columns.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstanceRiskPhrase] 
	DROP CONSTRAINT [DF_HazardousSubstanceRiskPhrase_Deleted]
	
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	DROP COLUMN [Deleted] 
	
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstanceRiskPhrase] 
	DROP CONSTRAINT [DF_HazardousSubstanceRiskPhrase_CreatedOn]
	
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	DROP COLUMN [CreatedOn] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstanceRiskPhrase] 
	DROP CONSTRAINT [DF_HazardousSubstanceRiskPhrase_CreatedBy]
	
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	DROP COLUMN [CreatedBy] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	DROP COLUMN [LastModifiedOn] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	DROP COLUMN [LastModifiedBy] 
END
	

DELETE FROM changelog WHERE change_number = 356 AND delta_set = 'Main'
GO

--------------- Fragment ends: #356: 356_ALTER HazardousSubstanceRiskPhrase add entity columns.sql ---------------

--------------- Fragment begins: #355: 355_GRANT CRU to AllowSelectInsertUpdateRole.sql ---------------

-- Change script: #355: 355_GRANT CRU to AllowSelectInsertUpdateRole.sql

DELETE FROM changelog WHERE change_number = 355 AND delta_set = 'Main'
GO

--------------- Fragment ends: #355: 355_GRANT CRU to AllowSelectInsertUpdateRole.sql ---------------

--------------- Fragment begins: #354: 354_GRANT CRU to AllowSelectInsertUpdateRole.sql ---------------

-- Change script: #354: 354_GRANT CRU to AllowSelectInsertUpdateRole.sql

DELETE FROM changelog WHERE change_number = 354 AND delta_set = 'Main'
GO

--------------- Fragment ends: #354: 354_GRANT CRU to AllowSelectInsertUpdateRole.sql ---------------

--------------- Fragment begins: #353: 353_ALTER HazardousSubstancePictogram add entity columns.sql ---------------

-- Change script: #353: 353_ALTER HazardousSubstancePictogram add entity columns.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstancePictogram' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstancePictogram] 
	DROP CONSTRAINT [DF_HazardousSubstancePictogram_Deleted]
	
	ALTER TABLE [HazardousSubstancePictogram]
	DROP COLUMN [Deleted] 
	
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstancePictogram' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstancePictogram] 
	DROP CONSTRAINT [DF_HazardousSubstancePictogram_CreatedOn]
	
	ALTER TABLE [HazardousSubstancePictogram]
	DROP COLUMN [CreatedOn] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstancePictogram' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [dbo].[HazardousSubstancePictogram] 
	DROP CONSTRAINT [DF_HazardousSubstancePictogram_CreatedBy]
	
	ALTER TABLE [HazardousSubstancePictogram]
	DROP COLUMN [CreatedBy] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstancePictogram' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [HazardousSubstancePictogram]
	DROP COLUMN [LastModifiedOn] 
END
	
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstancePictogram' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [HazardousSubstancePictogram]
	DROP COLUMN [LastModifiedBy] 
END
	

DELETE FROM changelog WHERE change_number = 353 AND delta_set = 'Main'
GO

--------------- Fragment ends: #353: 353_ALTER HazardousSubstancePictogram add entity columns.sql ---------------

--------------- Fragment begins: #352: 352_ALTER TABLE RiskAssessmentPeopleAtRisk.sql ---------------

-- Change script: #352: 352_ALTER TABLE RiskAssessmentPeopleAtRisk.sql
USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'PK_RiskAssessmentPeopleAtRisk2' )
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk] 
	DROP CONSTRAINT [PK_RiskAssessmentPeopleAtRisk2] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'Id')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	DROP COLUMN [Id]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	DROP COLUMN [CreatedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	DROP COLUMN [CreatedBy] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	DROP COLUMN [LastModifiedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	DROP COLUMN [LastModifiedBy]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [RiskAssessmentPeopleAtRisk]
	DROP COLUMN [Deleted]
END


DELETE FROM changelog WHERE change_number = 352 AND delta_set = 'Main'
GO

--------------- Fragment ends: #352: 352_ALTER TABLE RiskAssessmentPeopleAtRisk.sql ---------------

--------------- Fragment begins: #351: 351_ALTER Responsibility add CompanyId column.sql ---------------

-- Change script: #351: 351_ALTER Responsibility add CompanyId column.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Responsibility' AND COLUMN_NAME = 'CompanyId')
BEGIN
	ALTER TABLE [Responsibility]
	DROP COLUMN [CompanyId] 
END
GO

DELETE FROM changelog WHERE change_number = 351 AND delta_set = 'Main'
GO

--------------- Fragment ends: #351: 351_ALTER Responsibility add CompanyId column.sql ---------------

--------------- Fragment begins: #350: 350_ALTER TABLE PersonalRiskAssessmentChecklist.sql ---------------

-- Change script: #350: 350_ALTER TABLE PersonalRiskAssessmentChecklist.sql
USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessmentChecklist' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [PersonalRiskAssessmentChecklist]
	DROP COLUMN [CreatedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessmentChecklist' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [PersonalRiskAssessmentChecklist]
	DROP COLUMN [CreatedBy] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessmentChecklist' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [PersonalRiskAssessmentChecklist]
	DROP COLUMN [LastModifiedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessmentChecklist' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [PersonalRiskAssessmentChecklist]
	DROP COLUMN [LastModifiedBy]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessmentChecklist' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [PersonalRiskAssessmentChecklist]
	DROP COLUMN [Deleted]
END

DELETE FROM changelog WHERE change_number = 350 AND delta_set = 'Main'
GO

--------------- Fragment ends: #350: 350_ALTER TABLE PersonalRiskAssessmentChecklist.sql ---------------

--------------- Fragment begins: #349: 349_ALTER TABLE ChecklistGeneratorEmployee.sql ---------------

-- Change script: #349: 349_ALTER TABLE ChecklistGeneratorEmployee.sql
USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ChecklistGeneratorEmployee' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [ChecklistGeneratorEmployee]
	DROP COLUMN [CreatedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ChecklistGeneratorEmployee' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [ChecklistGeneratorEmployee]
	DROP COLUMN [CreatedBy] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ChecklistGeneratorEmployee' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [ChecklistGeneratorEmployee]
	DROP COLUMN [LastModifiedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ChecklistGeneratorEmployee' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [ChecklistGeneratorEmployee]
	DROP COLUMN [LastModifiedBy]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ChecklistGeneratorEmployee' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [ChecklistGeneratorEmployee]
	DROP COLUMN [Deleted]
END

DELETE FROM changelog WHERE change_number = 349 AND delta_set = 'Main'
GO

--------------- Fragment ends: #349: 349_ALTER TABLE ChecklistGeneratorEmployee.sql ---------------

--------------- Fragment begins: #348: 348_ALTER TABLE RiskAssessmentEmployee RiskAssessmentNonEmployee.sql ---------------

-- Change script: #348: 348_ALTER TABLE RiskAssessmentEmployee RiskAssessmentNonEmployee.sql
USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'PK_RiskAssessmentEmployee2' )
BEGIN
	ALTER TABLE [RiskAssessmentEmployee] 
	DROP CONSTRAINT [PK_RiskAssessmentEmployee2] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'PK_RiskAssessmentsNonEmployees2' )
BEGIN
	ALTER TABLE [RiskAssessmentsNonEmployees] 
	DROP CONSTRAINT [PK_RiskAssessmentsNonEmployees2] 
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentEmployee' AND COLUMN_NAME = 'Id')
BEGIN
	ALTER TABLE [RiskAssessmentEmployee]
	DROP COLUMN [Id]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentsNonEmployees' AND COLUMN_NAME = 'Id')
BEGIN
	ALTER TABLE [RiskAssessmentsNonEmployees]
	DROP COLUMN [Id] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentEmployee' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [RiskAssessmentEmployee]
	DROP COLUMN [CreatedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentsNonEmployees' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [RiskAssessmentsNonEmployees]
	DROP COLUMN  [CreatedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentEmployee' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [RiskAssessmentEmployee]
	DROP COLUMN [CreatedBy] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentsNonEmployees' AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE [RiskAssessmentsNonEmployees]
	DROP COLUMN [CreatedBy]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentEmployee' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [RiskAssessmentEmployee]
	DROP COLUMN [LastModifiedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentsNonEmployees' AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [RiskAssessmentsNonEmployees]
	DROP COLUMN [LastModifiedOn]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentEmployee' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [RiskAssessmentEmployee]
	DROP COLUMN [LastModifiedBy]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentsNonEmployees' AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE [RiskAssessmentsNonEmployees]
	DROP COLUMN [LastModifiedBy]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentEmployee' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [RiskAssessmentEmployee]
	DROP COLUMN [Deleted]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentsNonEmployees' AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [RiskAssessmentsNonEmployees]
	DROP COLUMN [Deleted]
END
GO

DELETE FROM changelog WHERE change_number = 348 AND delta_set = 'Main'
GO

--------------- Fragment ends: #348: 348_ALTER TABLE RiskAssessmentEmployee RiskAssessmentNonEmployee.sql ---------------

--------------- Fragment begins: #347: 347_CREATE Responsibility table.sql ---------------

-- Change script: #347: 347_CREATE Responsibility table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Responsibility' AND TYPE = 'U')
BEGIN
	DROP TABLE [Responsibility]
END

DELETE FROM changelog WHERE change_number = 347 AND delta_set = 'Main'
GO

--------------- Fragment ends: #347: 347_CREATE Responsibility table.sql ---------------

--------------- Fragment begins: #346: 346_INSERT INTO ResponsibilityReason.sql ---------------

-- Change script: #346: 346_INSERT INTO ResponsibilityReason.sql
USE [BusinessSafe]
GO
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 1)
BEGIN
	DELETE FROM ResponsibilityCategory WHERE Id = 1
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 2)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 2
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 3)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 3
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 4)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 4
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 5)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 5
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 6)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 6
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityReason] WHERE [Id] = 7)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 7
END
	
GO

DELETE FROM changelog WHERE change_number = 346 AND delta_set = 'Main'
GO

--------------- Fragment ends: #346: 346_INSERT INTO ResponsibilityReason.sql ---------------

--------------- Fragment begins: #345: 345_CREATE ResponsibilityReason_table.sql ---------------

-- Change script: #345: 345_CREATE ResponsibilityReason_table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'ResponsibilityReason')
BEGIN
	DROP TABLE [ResponsibilityCategory]
END

DELETE FROM changelog WHERE change_number = 345 AND delta_set = 'Main'
GO

--------------- Fragment ends: #345: 345_CREATE ResponsibilityReason_table.sql ---------------

--------------- Fragment begins: #344: 344_User Changes.sql ---------------

-- Change script: #344: 344_User Changes.sql

IF EXISTS (SELECT * FROM sys.database_principals WHERE [name] = 'BusinessSafeDB' and [type] = 'S')
BEGIN
	IF EXISTS
	(
		SELECT *
		FROM sys.database_role_members
		INNER JOIN sys.database_principals [role]
		ON sys.database_role_members.role_principal_id = [role].principal_id
		INNER JOIN sys.database_principals [member]
		ON sys.database_role_members.member_principal_id = [member].principal_id
		WHERE [role].[name] = 'AllowSelectInsertUpdate'
		AND [member].[name] = 'BusinessSafeDB'
	)
	BEGIN
		EXEC sp_droprolemember 'AllowSelectInsertUpdate', 'BusinessSafeDB'
	END
	
	IF NOT EXISTS
	(
		SELECT *
		FROM sys.database_role_members
		INNER JOIN sys.database_principals [role]
		ON sys.database_role_members.role_principal_id = [role].principal_id
		INNER JOIN sys.database_principals [member]
		ON sys.database_role_members.member_principal_id = [member].principal_id
		WHERE [role].[name] = 'AllowAll'
		AND [member].[name] = 'BusinessSafeDB'
	)
	BEGIN
		EXEC sp_addrolemember 'AllowAll', 'BusinessSafeDB'
	END
END
ELSE
BEGIN
	IF  EXISTS
	(
		SELECT *
		FROM sys.database_role_members
		INNER JOIN sys.database_principals [role]
		ON sys.database_role_members.role_principal_id = [role].principal_id
		INNER JOIN sys.database_principals [member]
		ON sys.database_role_members.member_principal_id = [member].principal_id
		WHERE [role].[name] = 'AllowSelectInsertUpdate'
		AND [member].[name] = 'intranetuser'
	)
	BEGIN
		EXEC sp_droprolemember 'AllowSelectInsertUpdate', 'intranetuser'
	END

	IF EXISTS (SELECT * FROM sys.database_principals WHERE [name] = 'intranetuser' and [type] = 'S')
	BEGIN
		DROP USER [intranetuser]
	END

	IF EXISTS (SELECT * FROM sys.sql_logins WHERE [name] = 'intranetuser')
	BEGIN
		DROP LOGIN [intranetuser] 
	END
END

IF EXISTS (SELECT * FROM sys.database_principals WHERE [name] = 'AllowSelectInsertUpdate' and [type] = 'R')
BEGIN
	DROP ROLE AllowSelectInsertUpdate
END

DELETE FROM changelog WHERE change_number = 344 AND delta_set = 'Main'
GO

--------------- Fragment ends: #344: 344_User Changes.sql ---------------

--------------- Fragment begins: #343: 343_GRANT Delete for AllowAll.sql ---------------

-- Change script: #343: 343_GRANT Delete for AllowAll.sql

EXEC sp_MSforeachtable 'REVOKE DELETE ON ? TO [AllowAll]'
GO

DELETE FROM changelog WHERE change_number = 343 AND delta_set = 'Main'
GO

--------------- Fragment ends: #343: 343_GRANT Delete for AllowAll.sql ---------------

--------------- Fragment begins: #342: 342_REVOKE Delete for AllowAll.sql ---------------

-- Change script: #342: 342_REVOKE Delete for AllowAll.sql

EXEC sp_MSforeachtable 'GRANT DELETE ON ? TO [AllowAll]'
GO 

DELETE FROM changelog WHERE change_number = 342 AND delta_set = 'Main'
GO

--------------- Fragment ends: #342: 342_REVOKE Delete for AllowAll.sql ---------------

--------------- Fragment begins: #341: 341_INSERT INTO ResponsibilityCategory.sql ---------------

-- Change script: #341: 341_INSERT INTO ResponsibilityCategory.sql
USE [BusinessSafe]
GO
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 1)
BEGIN
	DELETE FROM ResponsibilityCategory WHERE Id = 1
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 2)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 2
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 3)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 3
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 4)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 4
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 5)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 5
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 6)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 6
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 7)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 7
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 8)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 8
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 9)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 9
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 10)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 10
END
IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 11)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 11
END
	
GO

DELETE FROM changelog WHERE change_number = 341 AND delta_set = 'Main'
GO

--------------- Fragment ends: #341: 341_INSERT INTO ResponsibilityCategory.sql ---------------

--------------- Fragment begins: #340: 340_CREATE ResponsibilityCategory_table.sql ---------------

-- Change script: #340: 340_CREATE ResponsibilityCategory_table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'ResponsibilityCategory')
BEGIN
	DROP TABLE [ResponsibilityCategory]
END

DELETE FROM changelog WHERE change_number = 340 AND delta_set = 'Main'
GO

--------------- Fragment ends: #340: 340_CREATE ResponsibilityCategory_table.sql ---------------

--------------- Fragment begins: #339: 339_INSERT INTO SourcesOfFuel.sql ---------------

-- Change script: #339: 339_INSERT INTO SourcesOfFuel.sql
USE [BusinessSafe]
GO

IF EXISTS(SELECT * FROM [dbo].[SourceOfFuel] WHERE [Id] = 100)
BEGIN
	DELETE FROM [SourceOfFuel] WHERE [Id] = 100
END

IF EXISTS(SELECT * FROM [dbo].[SourceOfFuel] WHERE [Id] = 101)
BEGIN
	DELETE FROM [SourceOfFuel] WHERE [Id] = 101
END

DELETE FROM changelog WHERE change_number = 339 AND delta_set = 'Main'
GO

--------------- Fragment ends: #339: 339_INSERT INTO SourcesOfFuel.sql ---------------

--------------- Fragment begins: #338: 338_Update PersonalRiskAssessment_Set_ChecklistStatus.sql ---------------

-- Change script: #338: 338_Update PersonalRiskAssessment_Set_ChecklistStatus.sql
use businesssafe
go

if exists (select top 1 id from personalriskassessment where id = 1262 and PersonalRiskAssessementEmployeeChecklistStatusId = 2)
begin
	update personalriskassessment
	set PersonalRiskAssessementEmployeeChecklistStatusId = 1
	where id = 1262
end

DELETE FROM changelog WHERE change_number = 338 AND delta_set = 'Main'
GO

--------------- Fragment ends: #338: 338_Update PersonalRiskAssessment_Set_ChecklistStatus.sql ---------------

--------------- Fragment begins: #337: 337_DELETE FROM RolesPermissions.sql ---------------

-- Change script: #337: 337_DELETE FROM RolesPermissions.sql
USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM [dbo].[RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' and [PermissionId] = 19)
BEGIN
	INSERT [RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'952eecb7-2b96-4399-82ae-7e2341d25e51', 19)
END

IF NOT EXISTS(SELECT * FROM [dbo].[RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' and [PermissionId] = 49)
BEGIN
	INSERT [RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'952eecb7-2b96-4399-82ae-7e2341d25e51', 49)
END

IF NOT EXISTS(SELECT * FROM [dbo].[RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' and [PermissionId] = 53)
BEGIN
	INSERT [RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'952eecb7-2b96-4399-82ae-7e2341d25e51', 53)
END





DELETE FROM changelog WHERE change_number = 337 AND delta_set = 'Main'
GO

--------------- Fragment ends: #337: 337_DELETE FROM RolesPermissions.sql ---------------

--------------- Fragment begins: #336: 336_CREATE MIGRATION_HroBsoEmployeeMapping.sql ---------------

-- Change script: #336: 336_CREATE MIGRATION_HroBsoEmployeeMapping.sql

DELETE FROM changelog WHERE change_number = 336 AND delta_set = 'Main'
GO

--------------- Fragment ends: #336: 336_CREATE MIGRATION_HroBsoEmployeeMapping.sql ---------------

--------------- Fragment begins: #335: 335_ALTER TABLE BusinessSafeCompanyDetail.sql.sql ---------------

-- Change script: #335: 335_ALTER TABLE BusinessSafeCompanyDetail.sql.sql
USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'BusinessSafeCompanyDetail' AND COLUMN_NAME = 'CompanyId')
BEGIN
	alter table BusinessSafeCompanyDetail
	drop column CompanyId 
END

DELETE FROM changelog WHERE change_number = 335 AND delta_set = 'Main'
GO

--------------- Fragment ends: #335: 335_ALTER TABLE BusinessSafeCompanyDetail.sql.sql ---------------

--------------- Fragment begins: #334: 334_INSERT INTO RolesPermissions.sql ---------------

-- Change script: #334: 334_INSERT INTO RolesPermissions.sql
USE [BusinessSafe]
GO

IF EXISTS(SELECT * FROM [dbo].[RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' and [PermissionId] = 19)
BEGIN
	DELETE FROM [RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' AND [PermissionId] = 19
END

IF EXISTS(SELECT * FROM [dbo].[RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' and [PermissionId] = 49)
BEGIN
	DELETE FROM [RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' AND [PermissionId] = 49
END

IF EXISTS(SELECT * FROM [dbo].[RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' and [PermissionId] = 53)
BEGIN
	DELETE FROM [RolesPermissions] WHERE RoleId = N'952eecb7-2b96-4399-82ae-7e2341d25e51' AND [PermissionId] = 53
END




DELETE FROM changelog WHERE change_number = 334 AND delta_set = 'Main'
GO

--------------- Fragment ends: #334: 334_INSERT INTO RolesPermissions.sql ---------------

--------------- Fragment begins: #333: 333_Create BusinessSafeCompanyDetail.sql ---------------

-- Change script: #333: 333_Create BusinessSafeCompanyDetail.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'BusinessSafeCompanyDetail')
BEGIN
	DROP TABLE [BusinessSafeCompanyDetail];
END

DELETE FROM changelog WHERE change_number = 333 AND delta_set = 'Main'
GO

--------------- Fragment ends: #333: 333_Create BusinessSafeCompanyDetail.sql ---------------

--------------- Fragment begins: #332: 332_UPDATE Safety Phrase.sql ---------------

-- Change script: #332: 332_UPDATE Safety Phrase.sql
USE [BusinessSafe]
GO  


UPDATE [dbo].[SafetyPhrase] 
SET [RequiresAdditionalInformation] = 0
WHERE [Id] IN (11, 12, 20, 28, 33, 42, 44, 45, 48, 49, 51, 66, 67, 69, 72, 76, 83)

DELETE FROM changelog WHERE change_number = 332 AND delta_set = 'Main'
GO

--------------- Fragment ends: #332: 332_UPDATE Safety Phrase.sql ---------------

--------------- Fragment begins: #331: 331_INSERT INTO DocHandlerDocumentType.sql ---------------

-- Change script: #331: 331_INSERT INTO DocHandlerDocumentType.sql
USE [BusinessSafe]
GO  

IF EXISTS(SELECT * FROM [dbo].[DocHandlerDocumentType] WHERE [Id] = 127 AND [DocumentGroupId] = 1)
BEGIN
	DELETE FROM [dbo].[DocHandlerDocumentType] WHERE [Id] = 127 AND [DocumentGroupId] = 1
END

IF EXISTS(SELECT * FROM [dbo].[DocHandlerDocumentType] WHERE [Id] = 128 AND [DocumentGroupId] = 1)
BEGIN
	DELETE FROM [dbo].[DocHandlerDocumentType] WHERE [Id] = 128 AND [DocumentGroupId] = 1
END

DELETE FROM changelog WHERE change_number = 331 AND delta_set = 'Main'
GO

--------------- Fragment ends: #331: 331_INSERT INTO DocHandlerDocumentType.sql ---------------

--------------- Fragment begins: #330: 330_UPDATE User Permissions.sql ---------------

-- Change script: #330: 330_UPDATE User Permissions.sql

DELETE FROM changelog WHERE change_number = 330 AND delta_set = 'Main'
GO

--------------- Fragment ends: #330: 330_UPDATE User Permissions.sql ---------------

--------------- Fragment begins: #329: 329_INSERT INTO DocHandlerDocumentType.sql ---------------

-- Change script: #329: 329_INSERT INTO DocHandlerDocumentType.sql
USE [BusinessSafe]
GO  

DELETE FROM [dbo].[DocHandlerDocumentType] WHERE [Id] = 135 AND [DocumentGroupId] = 1

DELETE FROM changelog WHERE change_number = 329 AND delta_set = 'Main'
GO

--------------- Fragment ends: #329: 329_INSERT INTO DocHandlerDocumentType.sql ---------------

--------------- Fragment begins: #328: 328 CREATE Escalation Review tables.sql ---------------

-- Change script: #328: 328 CREATE Escalation Review tables.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EscalationOverdueReview')
BEGIN
	DROP TABLE [EscalationOverdueReview];
END

DELETE FROM changelog WHERE change_number = 328 AND delta_set = 'Main'
GO

--------------- Fragment ends: #328: 328 CREATE Escalation Review tables.sql ---------------

--------------- Fragment begins: #327: 327_update_fire_checklist_questions_16_and_17.sql ---------------

-- Change script: #327: 327_update_fire_checklist_questions_16_and_17.sql

DELETE FROM changelog WHERE change_number = 327 AND delta_set = 'Main'
GO

--------------- Fragment ends: #327: 327_update_fire_checklist_questions_16_and_17.sql ---------------

--------------- Fragment begins: #326: 326_Populate New Risk Assessors Table And Switch Risk Assessors Ids.sql ---------------

-- Change script: #326: 326_Populate New Risk Assessors Table And Switch Risk Assessors Ids.sql

DELETE FROM changelog WHERE change_number = 326 AND delta_set = 'Main'
GO

--------------- Fragment ends: #326: 326_Populate New Risk Assessors Table And Switch Risk Assessors Ids.sql ---------------

--------------- Fragment begins: #325: 325_RECREATE RiskAssessor.sql ---------------

-- Change script: #325: 325_RECREATE RiskAssessor.sql


DELETE FROM changelog WHERE change_number = 325 AND delta_set = 'Main'
GO

--------------- Fragment ends: #325: 325_RECREATE RiskAssessor.sql ---------------

--------------- Fragment begins: #324: 324_CREATE RiskAssessor.sql ---------------

-- Change script: #324: 324_CREATE RiskAssessor.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessor')
BEGIN
	DROP TABLE [RiskAssessor];
END

DELETE FROM changelog WHERE change_number = 324 AND delta_set = 'Main'
GO

--------------- Fragment ends: #324: 324_CREATE RiskAssessor.sql ---------------

--------------- Fragment begins: #323: 323_ALTER User Table.sql ---------------

-- Change script: #323: 323_ALTER User Table.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'User' AND COLUMN_NAME = 'DateDeleted')
BEGIN
	ALTER TABLE [User]
	DROP COLUMN DateDeleted
END
GO

DELETE FROM changelog WHERE change_number = 323 AND delta_set = 'Main'
GO

--------------- Fragment ends: #323: 323_ALTER User Table.sql ---------------

--------------- Fragment begins: #322: 322_ALTER EmployeeEmergencyContactDetails Add SameAddressAsEmployee.sql ---------------

-- Change script: #322: 322_ALTER EmployeeEmergencyContactDetails Add SameAddressAsEmployee.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeEmergencyContactDetails' AND COLUMN_NAME = 'SameAddressAsEmployee')
BEGIN
	ALTER TABLE [EmployeeEmergencyContactDetails]
	DROP COLUMN [SameAddressAsEmployee] 
END
GO

DELETE FROM changelog WHERE change_number = 322 AND delta_set = 'Main'
GO

--------------- Fragment ends: #322: 322_ALTER EmployeeEmergencyContactDetails Add SameAddressAsEmployee.sql ---------------

--------------- Fragment begins: #321: 321_ALTER Task Add DoNotSendStartedTaskNotification.sql ---------------

-- Change script: #321: 321_ALTER Task Add DoNotSendStartedTaskNotification.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'LastRecommendedControlSystem')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [LastRecommendedControlSystem] 
END
GO

DELETE FROM changelog WHERE change_number = 321 AND delta_set = 'Main'
GO

--------------- Fragment ends: #321: 321_ALTER Task Add DoNotSendStartedTaskNotification.sql ---------------

--------------- Fragment begins: #320: 320_ALTER Task Add DoNotSendStartedTaskNotification.sql ---------------

-- Change script: #320: 320_ALTER Task Add DoNotSendStartedTaskNotification.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'DoNotSendStartedTaskNotification')
BEGIN
	ALTER TABLE [Task]
	DROP COLUMN [DoNotSendStartedTaskNotification] 
END
GO

DELETE FROM changelog WHERE change_number = 320 AND delta_set = 'Main'
GO

--------------- Fragment ends: #320: 320_ALTER Task Add DoNotSendStartedTaskNotification.sql ---------------

--------------- Fragment begins: #319: 319_INSERT AuthenticationToken for apitest user.sql ---------------

-- Change script: #319: 319_INSERT AuthenticationToken for apitest user.sql

DELETE FROM changelog WHERE change_number = 319 AND delta_set = 'Main'
GO

--------------- Fragment ends: #319: 319_INSERT AuthenticationToken for apitest user.sql ---------------

--------------- Fragment begins: #318: 318_UPDATE Tasks with notification fields.sql ---------------

-- Change script: #318: 318_UPDATE Tasks with notification fields.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'SendTaskOverdueNotification')
BEGIN
	ALTER TABLE [Task]
	DROP COLUMN [SendTaskOverdueNotification] 
END
GO



DELETE FROM changelog WHERE change_number = 318 AND delta_set = 'Main'
GO

--------------- Fragment ends: #318: 318_UPDATE Tasks with notification fields.sql ---------------

--------------- Fragment begins: #317: 317_UPDATE Task when completed notification not sent.sql ---------------

-- Change script: #317: 317_UPDATE Task when completed notification not sent.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'SendCompletionNotificationEmail')
BEGIN
	ALTER TABLE [Task]
	DROP COLUMN [SendCompletionNotificationEmail] 
END
GO

DELETE FROM changelog WHERE change_number = 317 AND delta_set = 'Main'
GO

--------------- Fragment ends: #317: 317_UPDATE Task when completed notification not sent.sql ---------------

--------------- Fragment begins: #316: 316 CREATE EscalationNextReoccurringLiveTask table.sql ---------------

-- Change script: #316: 316 CREATE EscalationNextReoccurringLiveTask table.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EscalationNextReoccurringLiveTask')
BEGIN
	DROP TABLE [EscalationNextReoccurringLiveTask];
END

DELETE FROM changelog WHERE change_number = 316 AND delta_set = 'Main'
GO

--------------- Fragment ends: #316: 316 CREATE EscalationNextReoccurringLiveTask table.sql ---------------

--------------- Fragment begins: #315: 315 CREATE AuthenticationToken table.sql ---------------

-- Change script: #315: 315 CREATE AuthenticationToken table.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AuthenticationToken')
BEGIN
	DROP TABLE [AuthenticationToken];
END

DELETE FROM changelog WHERE change_number = 315 AND delta_set = 'Main'
GO

--------------- Fragment ends: #315: 315 CREATE AuthenticationToken table.sql ---------------

--------------- Fragment begins: #314: 314_UPDATE ApplicationToken for BSO Mobile.sql ---------------

-- Change script: #314: 314_UPDATE ApplicationToken for BSO Mobile.sql

DELETE FROM changelog WHERE change_number = 314 AND delta_set = 'Main'
GO

--------------- Fragment ends: #314: 314_UPDATE ApplicationToken for BSO Mobile.sql ---------------

--------------- Fragment begins: #313: 313 CREATE EscalationOverDueTask table.sql ---------------

-- Change script: #313: 313 CREATE EscalationOverDueTask table.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EscalationOverdueTask')
BEGIN
	DROP TABLE [EscalationOverdueTask];
END

DELETE FROM changelog WHERE change_number = 313 AND delta_set = 'Main'
GO

--------------- Fragment ends: #313: 313 CREATE EscalationOverDueTask table.sql ---------------

--------------- Fragment begins: #312: 312_ALTER AuthorisationToken Add CreatedOn column.sql ---------------

-- Change script: #312: 312_ALTER AuthorisationToken Add CreatedOn column.sql

DELETE FROM changelog WHERE change_number = 312 AND delta_set = 'Main'
GO

--------------- Fragment ends: #312: 312_ALTER AuthorisationToken Add CreatedOn column.sql ---------------

--------------- Fragment begins: #311: 311 CREATE AuthorisationToken table.sql ---------------

-- Change script: #311: 311 CREATE AuthorisationToken table.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AuthorisationToken')
BEGIN
	DROP TABLE [AuthorisationToken];
END

DELETE FROM changelog WHERE change_number = 311 AND delta_set = 'Main'
GO

--------------- Fragment ends: #311: 311 CREATE AuthorisationToken table.sql ---------------

--------------- Fragment begins: #310: 310 CREATE ApplicationToken table.sql ---------------

-- Change script: #310: 310 CREATE ApplicationToken table.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ApplicationToken')
BEGIN
	DROP TABLE [ApplicationToken];
END

DELETE FROM changelog WHERE change_number = 310 AND delta_set = 'Main'
GO

--------------- Fragment ends: #310: 310 CREATE ApplicationToken table.sql ---------------

--------------- Fragment begins: #308: 308_ADD Permissions On Answer.sql ---------------

-- Change script: #308: 308_ADD Permissions On Answer.sql


DELETE FROM changelog WHERE change_number = 308 AND delta_set = 'Main'
GO

--------------- Fragment ends: #308: 308_ADD Permissions On Answer.sql ---------------

--------------- Fragment begins: #307: 307 Delete Duplicated Asbestos On Hazards Table.sql ---------------

-- Change script: #307: 307 Delete Duplicated Asbestos On Hazards Table.sql

DELETE FROM changelog WHERE change_number = 307 AND delta_set = 'Main'
GO

--------------- Fragment ends: #307: 307 Delete Duplicated Asbestos On Hazards Table.sql ---------------

--------------- Fragment begins: #306: 306_update_IsActivated_on_user_table.sql ---------------

-- Change script: #306: 306_update_IsActivated_on_user_table.sql

DELETE FROM changelog WHERE change_number = 306 AND delta_set = 'Main'
GO

--------------- Fragment ends: #306: 306_update_IsActivated_on_user_table.sql ---------------

--------------- Fragment begins: #305: 305_INSERT associations of existing Hazards to PRAs ---------------

-- Change script: #305: 305_INSERT associations of existing Hazards to PRAs

DELETE FROM changelog WHERE change_number = 305 AND delta_set = 'Main'
GO

--------------- Fragment ends: #305: 305_INSERT associations of existing Hazards to PRAs ---------------

--------------- Fragment begins: #304: 304_Create_vwGroupOfHazardSubstance.sql ---------------

-- Change script: #304: 304_Create_vwGroupOfHazardSubstance.sql

DELETE FROM changelog WHERE change_number = 304 AND delta_set = 'Main'
GO

--------------- Fragment ends: #304: 304_Create_vwGroupOfHazardSubstance.sql ---------------

--------------- Fragment begins: #303: 303_ALTER FireRiskAssessmentChecklist Add HasCompleteFailureAttempt.sql ---------------

-- Change script: #303: 303_ALTER FireRiskAssessmentChecklist Add HasCompleteFailureAttempt.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessmentChecklist' AND COLUMN_NAME = 'HasCompleteFailureAttempt')
BEGIN
	ALTER TABLE [FireRiskAssessmentChecklist]
	DROP COLUMN [HasCompleteFailureAttempt] 
END
GO

DELETE FROM changelog WHERE change_number = 303 AND delta_set = 'Main'
GO

--------------- Fragment ends: #303: 303_ALTER FireRiskAssessmentChecklist Add HasCompleteFailureAttempt.sql ---------------

--------------- Fragment begins: #302: 302_Update questions to match changes for UAT.sql ---------------

-- Change script: #302: 302_Update questions to match changes for UAT.sql


DELETE FROM changelog WHERE change_number = 302 AND delta_set = 'Main'
GO

--------------- Fragment ends: #302: 302_Update questions to match changes for UAT.sql ---------------

--------------- Fragment begins: #301: 301_Update invalid characters in information in questions.sql ---------------

-- Change script: #301: 301_Update invalid characters in information in questions.sql


DELETE FROM changelog WHERE change_number = 301 AND delta_set = 'Main'
GO

--------------- Fragment ends: #301: 301_Update invalid characters in information in questions.sql ---------------

--------------- Fragment begins: #300: 300_DELETE wrong question from checklists dse personal ---------------

-- Change script: #300: 300_DELETE wrong question from checklists dse personal

DELETE FROM changelog WHERE change_number = 300 AND delta_set = 'Main'
GO

--------------- Fragment ends: #300: 300_DELETE wrong question from checklists dse personal ---------------

--------------- Fragment begins: #299: 299_ALTER Task Add SendTaskNotification.sql ---------------

-- Change script: #299: 299_ALTER Task Add SendTaskNotification.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'SendTaskNotification')
BEGIN
	ALTER TABLE [Task]
	DROP COLUMN [SendTaskNotification] 
END
GO

DELETE FROM changelog WHERE change_number = 299 AND delta_set = 'Main'
GO

--------------- Fragment ends: #299: 299_ALTER Task Add SendTaskNotification.sql ---------------

--------------- Fragment begins: #298: 298_Sort Out Permissions Issue On Significant Findings.sql ---------------

-- Change script: #298: 298_Sort Out Permissions Issue On Significant Findings.sql

DELETE FROM changelog WHERE change_number = 298 AND delta_set = 'Main'
GO

--------------- Fragment ends: #298: 298_Sort Out Permissions Issue On Significant Findings.sql ---------------

--------------- Fragment begins: #297: 297_Update DSE personal question text.sql ---------------

-- Change script: #297: 297_Update DSE personal question text.sql

DELETE FROM changelog WHERE change_number = 297 AND delta_set = 'Main'
GO

--------------- Fragment ends: #297: 297_Update DSE personal question text.sql ---------------

--------------- Fragment begins: #296: 296_Replace --select option-- text with empty strings.sql ---------------

-- Change script: #296: 296_Replace --select option-- text with empty strings.sql

DELETE FROM changelog WHERE change_number = 296 AND delta_set = 'Main'
GO

--------------- Fragment ends: #296: 296_Replace --select option-- text with empty strings.sql ---------------

--------------- Fragment begins: #295: 295_ALTER Task Add TaskGuid.sql ---------------

-- Change script: #295: 295_ALTER Task Add TaskGuid.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'TaskGuid')
BEGIN
	ALTER TABLE [Task]
	DROP CONSTRAINT [DF_Task_TaskGuid]
	
	--ALTER TABLE [Task]
	--DROP COLUMN [TaskGuid] 
END
GO

DELETE FROM changelog WHERE change_number = 295 AND delta_set = 'Main'
GO

--------------- Fragment ends: #295: 295_ALTER Task Add TaskGuid.sql ---------------

--------------- Fragment begins: #294: 294_UPDATE Checklist.sql ---------------

-- Change script: #294: 294_UPDATE Checklist.sql

DELETE FROM changelog WHERE change_number = 294 AND delta_set = 'Main'
GO

--------------- Fragment ends: #294: 294_UPDATE Checklist.sql ---------------

--------------- Fragment begins: #293: 293_ALTER Task table add SignificantFindingId.sql ---------------

-- Change script: #293: 293_ALTER Task table add SignificantFindingId.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'SignificantFindingId')
BEGIN
	ALTER TABLE [Task]
	DROP COLUMN [SignificantFindingId] 
END
GO

DELETE FROM changelog WHERE change_number = 293 AND delta_set = 'Main'
GO

--------------- Fragment ends: #293: 293_ALTER Task table add SignificantFindingId.sql ---------------

--------------- Fragment begins: #292: 292_ALTER Question Remove Invalid characters.sql ---------------

-- Change script: #292: 292_ALTER Question Remove Invalid characters.sql


DELETE FROM changelog WHERE change_number = 292 AND delta_set = 'Main'
GO

--------------- Fragment ends: #292: 292_ALTER Question Remove Invalid characters.sql ---------------

--------------- Fragment begins: #291: 291_ALTER SignificantFinding Add Fields.sql ---------------

-- Change script: #291: 291_ALTER SignificantFinding Add Fields.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SignificantFinding')
BEGIN
	DROP TABLE [SignificantFinding]
	
	CREATE TABLE [dbo].[SignificantFinding](
		[Id] [bigint] NOT NULL,
		[FireAnswerId] [bigint] NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
	CONSTRAINT [PK_SignificantFinding] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, DELETE, UPDATE ON [SignificantFinding] TO AllowAll
END
GO


DELETE FROM changelog WHERE change_number = 291 AND delta_set = 'Main'
GO

--------------- Fragment ends: #291: 291_ALTER SignificantFinding Add Fields.sql ---------------

--------------- Fragment begins: #290: 290_CREATE SignificantFinding.sql ---------------

-- Change script: #290: 290_CREATE SignificantFinding.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SignificantFinding')
BEGIN
	DROP TABLE [SignificantFinding];
END

DELETE FROM changelog WHERE change_number = 290 AND delta_set = 'Main'
GO

--------------- Fragment ends: #290: 290_CREATE SignificantFinding.sql ---------------

--------------- Fragment begins: #289: 289_ALTER Question Add Information.sql ---------------

-- Change script: #289: 289_ALTER Question Add Information.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Question' AND COLUMN_NAME = 'Information')
BEGIN
ALTER TABLE [Question]
DROP COLUMN [Information]
END
GO


DELETE FROM changelog WHERE change_number = 289 AND delta_set = 'Main'
GO

--------------- Fragment ends: #289: 289_ALTER Question Add Information.sql ---------------

--------------- Fragment begins: #288: 288_UPDATE Checklist.sql ---------------

-- Change script: #288: 288_UPDATE Checklist.sql

DELETE FROM changelog WHERE change_number = 288 AND delta_set = 'Main'
GO

--------------- Fragment ends: #288: 288_UPDATE Checklist.sql ---------------

--------------- Fragment begins: #287: 287_ALTER Section Add ShortTitle.sql ---------------

-- Change script: #287: 287_ALTER Section Add ShortTitle.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Section' AND COLUMN_NAME = 'ShortTitle')
BEGIN
	ALTER TABLE [Section]
	DROP COLUMN [ShortTitle] 
END
GO	

DELETE FROM changelog WHERE change_number = 287 AND delta_set = 'Main'
GO

--------------- Fragment ends: #287: 287_ALTER Section Add ShortTitle.sql ---------------

--------------- Fragment begins: #286: 286_UPDATE Section.sql ---------------

-- Change script: #286: 286_UPDATE Section.sql

UPDATE [Section] SET [ChecklistId] = 4 WHERE [Id] BETWEEN 35 AND 42

DELETE FROM changelog WHERE change_number = 286 AND delta_set = 'Main'
GO

--------------- Fragment ends: #286: 286_UPDATE Section.sql ---------------

--------------- Fragment begins: #285: 285_ALTER Answer add YesNoNotApplicableResponse.sql ---------------

-- Change script: #285: 285_ALTER Answer add YesNoNotApplicableResponse.sql

IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Answer' AND COLUMN_NAME = 'YesNoNotApplicableResponse')
BEGIN
	ALTER TABLE [Answer]
	DROP COLUMN [YesNoNotApplicableResponse]
END
GO	

DELETE FROM changelog WHERE change_number = 285 AND delta_set = 'Main'
GO

--------------- Fragment ends: #285: 285_ALTER Answer add YesNoNotApplicableResponse.sql ---------------

--------------- Fragment begins: #284: 284_ALTER Answer Rename FireAnswerSetId.sql ---------------

-- Change script: #284: 284_ALTER Answer Rename FireAnswerSetId.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Answer' AND COLUMN_NAME = 'FireAnswerSetId')
BEGIN
	EXEC sp_RENAME 'Answer.[FireRiskAssessmentChecklistId]' , 'FireAnswerSetId', 'COLUMN'
END
GO

DELETE FROM changelog WHERE change_number = 284 AND delta_set = 'Main'
GO

--------------- Fragment ends: #284: 284_ALTER Answer Rename FireAnswerSetId.sql ---------------

--------------- Fragment begins: #283: 283_CREATE FireRiskAssessmentChecklist.sql ---------------

-- Change script: #283: 283_CREATE FireRiskAssessmentChecklist.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FireRiskAssessmentChecklist')
BEGIN
	DROP TABLE [dbo].[FireRiskAssessmentChecklist]
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FireAnswerSet')
BEGIN
	CREATE TABLE [dbo].[FireAnswerSet](
		[Id] bigint IDENTITY(1,1) NOT NULL,
		[FireRiskAssessmentId] [bigint] NOT NULL,
		[SubmittedOn] [datetime] NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
	CONSTRAINT [PK_FireAnswerSet] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, DELETE, UPDATE ON [FireAnswerSet] TO AllowAll
END
GO

DELETE FROM changelog WHERE change_number = 283 AND delta_set = 'Main'
GO

--------------- Fragment ends: #283: 283_CREATE FireRiskAssessmentChecklist.sql ---------------

--------------- Fragment begins: #282: 282_INSERT FRA Checklist.sql ---------------

-- Change script: #282: 282_INSERT FRA Checklist.sql

DELETE FROM [Question] WHERE [SectionId] > 34
DELETE FROM [Section] WHERE [Id] > 34
DELETE FROM [Checklist] WHERE [Id] = 5
GO

DELETE FROM changelog WHERE change_number = 282 AND delta_set = 'Main'
GO

--------------- Fragment ends: #282: 282_INSERT FRA Checklist.sql ---------------

--------------- Fragment begins: #281: 281_ALTER AnswerSet Add ReviewGeneratedFromId.sql ---------------

-- Change script: #281: 281_ALTER AnswerSet Add ReviewGeneratedFromId.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireAnswerSet' AND COLUMN_NAME = 'ReviewGeneratedFromId')
BEGIN
	ALTER TABLE [FireAnswerSet]
	DROP COLUMN [ReviewGeneratedFromId]
END
GO

DELETE FROM changelog WHERE change_number = 281 AND delta_set = 'Main'
GO

--------------- Fragment ends: #281: 281_ALTER AnswerSet Add ReviewGeneratedFromId.sql ---------------

--------------- Fragment begins: #280: 280_ADD Permissions On FireRiskAssessment.sql ---------------

-- Change script: #280: 280_ADD Permissions On FireRiskAssessment.sql


DELETE FROM changelog WHERE change_number = 280 AND delta_set = 'Main'
GO

--------------- Fragment ends: #280: 280_ADD Permissions On FireRiskAssessment.sql ---------------

--------------- Fragment begins: #279: 279_ALTER Answer Add AnswerSetId.sql ---------------

-- Change script: #279: 279_ALTER Answer Add AnswerSetId.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Answer' AND COLUMN_NAME = 'FireAnswerSetId')
BEGIN
	ALTER TABLE [Answer]
	DROP COLUMN [FireAnswerSetId]
END
GO

DELETE FROM changelog WHERE change_number = 279 AND delta_set = 'Main'
GO

--------------- Fragment ends: #279: 279_ALTER Answer Add AnswerSetId.sql ---------------

--------------- Fragment begins: #278: 278_CREATE AnswerSet.sql ---------------

-- Change script: #278: 278_CREATE AnswerSet.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FireAnswerSet')
BEGIN
	DROP TABLE [dbo].[FireAnswerSet]
END
GO

DELETE FROM changelog WHERE change_number = 278 AND delta_set = 'Main'
GO

--------------- Fragment ends: #278: 278_CREATE AnswerSet.sql ---------------

--------------- Fragment begins: #277: 277_ALTER Checklist Add ChecklistRiskAssessmentType.sql ---------------

-- Change script: #277: 277_ALTER Checklist Add ChecklistRiskAssessmentType.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Checklist' AND COLUMN_NAME = 'ChecklistRiskAssessmentType')
BEGIN
	ALTER TABLE [Answer]
	DROP COLUMN [ChecklistRiskAssessmentType]
END
GO

DELETE FROM changelog WHERE change_number = 277 AND delta_set = 'Main'
GO

--------------- Fragment ends: #277: 277_ALTER Checklist Add ChecklistRiskAssessmentType.sql ---------------

--------------- Fragment begins: #276: 276_ALTER Answer Add Discriminator.sql ---------------

-- Change script: #276: 276_ALTER Answer Add Discriminator.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Answer' AND COLUMN_NAME = 'Discriminator')
BEGIN
	ALTER TABLE [Answer]
	DROP COLUMN [Discriminator]
END
GO

DELETE FROM changelog WHERE change_number = 276 AND delta_set = 'Main'
GO

--------------- Fragment ends: #276: 276_ALTER Answer Add Discriminator.sql ---------------

--------------- Fragment begins: #275: 275_CREATE Sources Of Fuel.sql ---------------

-- Change script: #275: 275_CREATE Sources Of Fuel.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SourceOfFuel')
BEGIN
	DROP TABLE [dbo].[SourceOfFuel]
END
GO


DELETE FROM [BusinessSafe].[dbo].[SourceOfFuel] WHERE ID >= 1 AND ID <=15
GO


IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'FireRiskAssessmentSourceOfFuels' AND TYPE = 'U')
BEGIN
	DROP TABLE [FireRiskAssessmentSourceOfFuels]
END
GO

DELETE FROM changelog WHERE change_number = 275 AND delta_set = 'Main'
GO

--------------- Fragment ends: #275: 275_CREATE Sources Of Fuel.sql ---------------

--------------- Fragment begins: #274: 274_CREATE Sources Of Ignition.sql ---------------

-- Change script: #274: 274_CREATE Sources Of Ignition.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SourceOfIgnition')
BEGIN
	DROP TABLE [dbo].[SourceOfIgnition]
END
GO


DELETE FROM [BusinessSafe].[dbo].[SourceOfIgnition] WHERE ID >= 1 AND ID <=13
GO


IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'FireRiskAssessmentSourceOfIgnitions' AND TYPE = 'U')
BEGIN
	DROP TABLE [FireRiskAssessmentSourceOfIgnitions]
END
GO

DELETE FROM changelog WHERE change_number = 274 AND delta_set = 'Main'
GO

--------------- Fragment ends: #274: 274_CREATE Sources Of Ignition.sql ---------------

--------------- Fragment begins: #273: 273_ALTER EmployeeChecklist Add CompletedOnEmployeesBehalfBy.sql ---------------

-- Change script: #273: 273_ALTER EmployeeChecklist Add CompletedOnEmployeesBehalfBy.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'CompletedOnEmployeesBehalfById')
BEGIN
	ALTER TABLE [dbo].[EmployeeChecklist]
	DROP [CompletedOnEmployeesBehalfById] 
END
GO

DELETE FROM changelog WHERE change_number = 273 AND delta_set = 'Main'
GO

--------------- Fragment ends: #273: 273_ALTER EmployeeChecklist Add CompletedOnEmployeesBehalfBy.sql ---------------

--------------- Fragment begins: #272: 272_CREATE Fire Safety and Control Measures.sql ---------------

-- Change script: #272: 272_CREATE Fire Safety and Control Measures.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FireSafetyControlMeasure')
BEGIN
	DROP TABLE [dbo].[FireSafetyControlMeasure]
END
GO


DELETE FROM [BusinessSafe].[dbo].[FireSafetyControlMeasure] WHERE ID >= 1 AND ID <=12
GO


IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND TYPE = 'U')
BEGIN
	DROP TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
END
GO

DELETE FROM changelog WHERE change_number = 272 AND delta_set = 'Main'
GO

--------------- Fragment ends: #272: 272_CREATE Fire Safety and Control Measures.sql ---------------

--------------- Fragment begins: #271: 271_ALTER FireRiskAssessment Emergency Shut Off Field Lengths.sql ---------------

-- Change script: #271: 271_ALTER FireRiskAssessment Emergency Shut Off Field Lengths.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'ElectricityEmergencyShutOff' AND CHARACTER_MAXIMUM_LENGTH = 500)
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	ALTER COLUMN [ElectricityEmergencyShutOff] [nvarchar](200)
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'GasEmergencyShutOff' AND CHARACTER_MAXIMUM_LENGTH = 500)
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	ALTER COLUMN [GasEmergencyShutOff] [nvarchar](200)
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'WaterEmergencyShutOff' AND CHARACTER_MAXIMUM_LENGTH = 500)
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	ALTER COLUMN [WaterEmergencyShutOff] [nvarchar](200)
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'OtherEmergencyShutOff' AND CHARACTER_MAXIMUM_LENGTH = 500)
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	ALTER COLUMN [OtherEmergencyShutOff] [nvarchar](200)
END
GO

DELETE FROM changelog WHERE change_number = 271 AND delta_set = 'Main'
GO

--------------- Fragment ends: #271: 271_ALTER FireRiskAssessment Emergency Shut Off Field Lengths.sql ---------------

--------------- Fragment begins: #270: 270_ALTER FireRiskAssessment Building Use.sql ---------------

-- Change script: #270: 270_ALTER FireRiskAssessment Building Use.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'BuildingUse' AND CHARACTER_MAXIMUM_LENGTH = 500)
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	ALTER COLUMN [BuildingUse] [nvarchar](200)
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'NumberOfFloors')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [NumberOfFloors]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'NumberOfPeople')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [NumberOfPeople]
END
GO

DELETE FROM changelog WHERE change_number = 270 AND delta_set = 'Main'
GO

--------------- Fragment ends: #270: 270_ALTER FireRiskAssessment Building Use.sql ---------------

--------------- Fragment begins: #269: 269_ALTER FireRiskAssessment Location Length.sql ---------------

-- Change script: #269: 269_ALTER FireRiskAssessment Location Length.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'Title' AND CHARACTER_MAXIMUM_LENGTH = 250)
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	ALTER COLUMN [Location] [nvarchar](500)
END
GO

DELETE FROM changelog WHERE change_number = 269 AND delta_set = 'Main'
GO

--------------- Fragment ends: #269: 269_ALTER FireRiskAssessment Location Length.sql ---------------

--------------- Fragment begins: #268: 268_ALTER FireRiskAssessment Add BuildingUse And Emergency Shut Off.sql ---------------

-- Change script: #268: 268_ALTER FireRiskAssessment Add BuildingUse And Emergency Shut Off.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'Location')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [Location]
END
GO


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'BuildingUse')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [BuildingUse]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'ElectricityEmergencyShutOff')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [ElectricityEmergencyShutOff]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'GasEmergencyShutOff')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [GasEmergencyShutOff]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'WaterEmergencyShutOff')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [WaterEmergencyShutOff]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'OtherEmergencyShutOff')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [OtherEmergencyShutOff]
END
GO

DELETE FROM changelog WHERE change_number = 268 AND delta_set = 'Main'
GO

--------------- Fragment ends: #268: 268_ALTER FireRiskAssessment Add BuildingUse And Emergency Shut Off.sql ---------------

--------------- Fragment begins: #267: 267_GRANT Delete Permissions EmployeeChecklistEmpoloyeeChecklistEmail.sql ---------------

-- Change script: #267: 267_GRANT Delete Permissions EmployeeChecklistEmpoloyeeChecklistEmail.sql

REVOKE DELETE ON [EmployeeChecklistEmployeeChecklistEmail] TO AllowAll

DELETE FROM changelog WHERE change_number = 267 AND delta_set = 'Main'
GO

--------------- Fragment ends: #267: 267_GRANT Delete Permissions EmployeeChecklistEmpoloyeeChecklistEmail.sql ---------------

--------------- Fragment begins: #266: 266_ALTER FireRiskAssessment Add PremisesProvidesSleepingAccommodation.sql ---------------

-- Change script: #266: 266_ALTER FireRiskAssessment Add PremisesProvidesSleepingAccommodation.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'PremisesProvidesSleepingAccommodation')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [PremisesProvidesSleepingAccommodation]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'PremisesProvidesSleepingAccommodationConfirmed')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [PremisesProvidesSleepingAccommodationConfirmed]
END
GO

DELETE FROM changelog WHERE change_number = 266 AND delta_set = 'Main'
GO

--------------- Fragment ends: #266: 266_ALTER FireRiskAssessment Add PremisesProvidesSleepingAccommodation.sql ---------------

--------------- Fragment begins: #265: 265_INSERT INTO Document Type.sql ---------------

-- Change script: #265: 265_INSERT INTO Document Type.sql
USE [BusinessSafe]
GO

DELETE FROM [BusinessSafe].[dbo].[DocumentType]
WHERE [Id] >= 13 AND [Id] <=15

DELETE FROM changelog WHERE change_number = 265 AND delta_set = 'Main'
GO

--------------- Fragment ends: #265: 265_INSERT INTO Document Type.sql ---------------

--------------- Fragment begins: #264: 264_GRANT Permissions FireRiskAssessment.sql ---------------

-- Change script: #264: 264_GRANT Permissions FireRiskAssessment.sql

REVOKE SELECT, INSERT, UPDATE ON [FireRiskAssessment] TO AllowAll

DELETE FROM changelog WHERE change_number = 264 AND delta_set = 'Main'
GO

--------------- Fragment ends: #264: 264_GRANT Permissions FireRiskAssessment.sql ---------------

--------------- Fragment begins: #262: 262_ALTER RiskAssessment Title.sql ---------------

-- Change script: #262: 262_ALTER RiskAssessment Title.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessment' AND COLUMN_NAME = 'Title' AND CHARACTER_MAXIMUM_LENGTH = 250)
BEGIN
	ALTER TABLE [dbo].[RiskAssessment]
	ALTER COLUMN [Title] [nvarchar](200)
END
GO

DELETE FROM changelog WHERE change_number = 262 AND delta_set = 'Main'
GO

--------------- Fragment ends: #262: 262_ALTER RiskAssessment Title.sql ---------------

--------------- Fragment begins: #261: 261_ALTER RiskAssessment Title.sql ---------------

-- Change script: #261: 261_ALTER RiskAssessment Title.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FireRiskAssessment' AND COLUMN_NAME = 'PersonAppointed')
BEGIN
	ALTER TABLE [dbo].[FireRiskAssessment]
	DROP [PersonAppointed]
END
GO

DELETE FROM changelog WHERE change_number = 261 AND delta_set = 'Main'
GO

--------------- Fragment ends: #261: 261_ALTER RiskAssessment Title.sql ---------------

--------------- Fragment begins: #260: 260_CREATE FireRiskAssessment Table.sql ---------------

-- Change script: #260: 260_CREATE FireRiskAssessment Table.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FireRiskAssessment')
BEGIN
	DROP TABLE [FireRiskAssessment];
END

DELETE FROM changelog WHERE change_number = 260 AND delta_set = 'Main'
GO

--------------- Fragment ends: #260: 260_CREATE FireRiskAssessment Table.sql ---------------

--------------- Fragment begins: #259: 259_ALTER EmployeeChecklistEmail add new email column.sql ---------------

-- Change script: #259: 259_ALTER EmployeeChecklistEmail add new email column.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklistEmail' AND COLUMN_NAME = 'RecipientEmail')
BEGIN
	ALTER TABLE [dbo].[EmployeeChecklistEmail] 
	DROP COLUMN [RecipientEmail]
END
GO

DELETE FROM changelog WHERE change_number = 259 AND delta_set = 'Main'
GO

--------------- Fragment ends: #259: 259_ALTER EmployeeChecklistEmail add new email column.sql ---------------

--------------- Fragment begins: #258: 258_ALTER EmployeeChecklist add new reference columns.sql ---------------

-- Change script: #258: 258_ALTER EmployeeChecklist add new reference columns.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'ReferencePrefix')
BEGIN
	DROP INDEX [IX_EmployeeChecklistReference] ON [EmployeeChecklist]
	
	ALTER TABLE [dbo].[EmployeeChecklist] 
	DROP COLUMN [ReferencePrefix]
	
	ALTER TABLE [dbo].[EmployeeChecklist] 
	DROP COLUMN [ReferenceIncremental]
END
GO

DELETE FROM changelog WHERE change_number = 258 AND delta_set = 'Main'
GO

--------------- Fragment ends: #258: 258_ALTER EmployeeChecklist add new reference columns.sql ---------------

--------------- Fragment begins: #257: 257_UPDATE Checklist Section Question - Fix misinterpreted characters in NEM checklist.sql ---------------

-- Change script: #257: 257_UPDATE Checklist Section Question - Fix misinterpreted characters in NEM checklist.sql

DELETE FROM changelog WHERE change_number = 257 AND delta_set = 'Main'
GO

--------------- Fragment ends: #257: 257_UPDATE Checklist Section Question - Fix misinterpreted characters in NEM checklist.sql ---------------

--------------- Fragment begins: #256: 256_UPDATE DSE Question Texts.sql ---------------

-- Change script: #256: 256_UPDATE DSE Question Texts.sql

DELETE FROM changelog WHERE change_number = 256 AND delta_set = 'Main'
GO

--------------- Fragment ends: #256: 256_UPDATE DSE Question Texts.sql ---------------

--------------- Fragment begins: #254: 254_UPDATE NEM Section and Question Texts.sql ---------------

-- Change script: #254: 254_UPDATE NEM Section and Question Texts.sql

DELETE FROM changelog WHERE change_number = 254 AND delta_set = 'Main'
GO

--------------- Fragment ends: #254: 254_UPDATE NEM Section and Question Texts.sql ---------------

--------------- Fragment begins: #253: 253_UPDATE Checklist Description NEM.sql ---------------

-- Change script: #253: 253_UPDATE Checklist Description NEM.sql

DELETE FROM changelog WHERE change_number = 253 AND delta_set = 'Main'
GO

--------------- Fragment ends: #253: 253_UPDATE Checklist Description NEM.sql ---------------

--------------- Fragment begins: #252: 252_Create HazardousSubstanceRiskAssessment StoredProcedure.sql ---------------

-- Change script: #252: 252_Create HazardousSubstanceRiskAssessment StoredProcedure.sql

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPRPT_BSO_GRA]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[SPRPT_BSO_GRA]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPRPT_BSO006_HRAHeader]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[SPRPT_BSO006_HRAHeader]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPRPT_BSO006_HRAPictogram]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[SPRPT_BSO006_HRAPictogram]
GO


DELETE FROM changelog WHERE change_number = 252 AND delta_set = 'Main'
GO

--------------- Fragment ends: #252: 252_Create HazardousSubstanceRiskAssessment StoredProcedure.sql ---------------

--------------- Fragment begins: #251: 251_Add_PersonalRiskAssessementEmployeeChecklistStatus_rows.sql ---------------

-- Change script: #251: 251_Add_PersonalRiskAssessementEmployeeChecklistStatus_rows.sql
--Nothing to undo. Would cause more issues attempting to rollback

DELETE FROM changelog WHERE change_number = 251 AND delta_set = 'Main'
GO

--------------- Fragment ends: #251: 251_Add_PersonalRiskAssessementEmployeeChecklistStatus_rows.sql ---------------

--------------- Fragment begins: #250: 250_Create PersonalRiskAssessementEmployeeChecklistStatus table.sql ---------------

-- Change script: #250: 250_Create PersonalRiskAssessementEmployeeChecklistStatus table.sql
IF EXISTS(
SELECT * FROM sys.objects AS o
	INNER JOIN sys.columns AS c ON o.object_id = c.object_id
WHERE o.name = 'PersonalRiskAssessment'
AND c.name='PersonalRiskAssessementEmployeeChecklistStatusId')
BEGIN
	ALTER TABLE dbo.PersonalRiskAssessment DROP CONSTRAINT DF_PersonalRiskAssessment_PersonalRiskAssessementEmployeeChecklistStatusId
	ALTER TABLE PersonalRiskAssessment DROP COLUMN PersonalRiskAssessementEmployeeChecklistStatusId
END

GO
IF EXISTS(SELECT * FROM sys.objects AS o
WHERE o.name = 'PersonalRiskAssessementEmployeeChecklistStatus')
BEGIN
	DROP TABLE PersonalRiskAssessementEmployeeChecklistStatus
END
GO


DELETE FROM changelog WHERE change_number = 250 AND delta_set = 'Main'
GO

--------------- Fragment ends: #250: 250_Create PersonalRiskAssessementEmployeeChecklistStatus table.sql ---------------

--------------- Fragment begins: #249: 249_ALTER EmployeeChecklist Table Add SendCompletedChecklistNotificationEmail.sql ---------------

-- Change script: #249: 249_ALTER EmployeeChecklist Table Add SendCompletedChecklistNotificationEmail.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'SendCompletedChecklistNotificationEmail')
BEGIN
	ALTER TABLE [EmployeeChecklist]
	DROP  COLUMN [SendCompletedChecklistNotificationEmail] 
END

DELETE FROM changelog WHERE change_number = 249 AND delta_set = 'Main'
GO

--------------- Fragment ends: #249: 249_ALTER EmployeeChecklist Table Add SendCompletedChecklistNotificationEmail.sql ---------------

--------------- Fragment begins: #248: 248_ALTER Personal Risk Assessment Add Checklist Notification Details.sql ---------------

-- Change script: #248: 248_ALTER Personal Risk Assessment Add Checklist Notification Details.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'SendCompletedChecklistNotificationEmail')
BEGIN
	ALTER TABLE [PersonalRiskAssessment]
	DROP  COLUMN [SendCompletedChecklistNotificationEmail] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'CompletionDueDateForChecklists')
BEGIN
	ALTER TABLE [PersonalRiskAssessment]
	DROP  COLUMN [CompletionDueDateForChecklists] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'CompletionNotificationEmailAddress')
BEGIN
	ALTER TABLE [PersonalRiskAssessment]
	DROP  COLUMN [CompletionNotificationEmailAddress] 
END


DELETE FROM changelog WHERE change_number = 248 AND delta_set = 'Main'
GO

--------------- Fragment ends: #248: 248_ALTER Personal Risk Assessment Add Checklist Notification Details.sql ---------------

--------------- Fragment begins: #247: 247_ALTER EmployeeChecklist Table Add DueDateForCompletion.sql ---------------

-- Change script: #247: 247_ALTER EmployeeChecklist Table Add DueDateForCompletion.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'DueDateForCompletion')
BEGIN
	ALTER TABLE [EmployeeChecklist]
	DROP  COLUMN [DueDateForCompletion] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'CompletionNotificationEmailAddress')
BEGIN
	ALTER TABLE [EmployeeChecklist]
	DROP  COLUMN [CompletionNotificationEmailAddress] 
END

DELETE FROM changelog WHERE change_number = 247 AND delta_set = 'Main'
GO

--------------- Fragment ends: #247: 247_ALTER EmployeeChecklist Table Add DueDateForCompletion.sql ---------------

--------------- Fragment begins: #246: 246_children_and_young_persons_questions.sql ---------------

-- Change script: #246: 246_children_and_young_persons_questions.sql

DELETE FROM changelog WHERE change_number = 246 AND delta_set = 'Main'
GO

--------------- Fragment ends: #246: 246_children_and_young_persons_questions.sql ---------------

--------------- Fragment begins: #245: 245_Update_manual_handling_questions.sql ---------------

-- Change script: #245: 245_Update_manual_handling_questions.sql

DELETE FROM changelog WHERE change_number = 245 AND delta_set = 'Main'
GO

--------------- Fragment ends: #245: 245_Update_manual_handling_questions.sql ---------------

--------------- Fragment begins: #244: 244_INSERT New Mothers Checklist.sql ---------------

-- Change script: #244: 244_INSERT New Mothers Checklist.sql
DELETE from question where sectionid in (
  select id from section where checklistid = 4
)
DELETE from section where checklistid = 4
DELETE from checklist where id = 4

DELETE FROM changelog WHERE change_number = 244 AND delta_set = 'Main'
GO

--------------- Fragment ends: #244: 244_INSERT New Mothers Checklist.sql ---------------

--------------- Fragment begins: #243: 243_ALTER EmployeeChecklist add PersonalRiskAssessmentId.sql ---------------

-- Change script: #243: 243_ALTER EmployeeChecklist add PersonalRiskAssessmentId.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'PersonalRiskAssessmentId')
BEGIN
	ALTER TABLE EmployeeChecklist
	DROP COLUMN PersonalRiskAssessmentId
END
GO

DELETE FROM changelog WHERE change_number = 243 AND delta_set = 'Main'
GO

--------------- Fragment ends: #243: 243_ALTER EmployeeChecklist add PersonalRiskAssessmentId.sql ---------------

--------------- Fragment begins: #242: 242_ALTER Checklist Table - Children and Young Persons.sql ---------------

-- Change script: #242: 242_ALTER Checklist Table - Children and Young Persons.sql

UPDATE [BusinessSafe].[dbo].[Checklist] SET [Title] = 'Children and Young Persons Information Checklist' WHERE Id = 3

DELETE FROM changelog WHERE change_number = 242 AND delta_set = 'Main'
GO

--------------- Fragment ends: #242: 242_ALTER Checklist Table - Children and Young Persons.sql ---------------

--------------- Fragment begins: #241: 241_ALTER EmployeeChecklist.sql ---------------

-- Change script: #241: 241_ALTER EmployeeChecklist.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'FriendlyReference')
BEGIN
	ALTER TABLE EmployeeChecklist
	DROP COLUMN FriendlyReference
END
GO

DELETE FROM changelog WHERE change_number = 241 AND delta_set = 'Main'
GO

--------------- Fragment ends: #241: 241_ALTER EmployeeChecklist.sql ---------------

--------------- Fragment begins: #240: 240_GRANT Delete ChecklistGeneratorEmployee PersonalRiskAssessmentChecklist.sql ---------------

-- Change script: #240: 240_GRANT Delete ChecklistGeneratorEmployee PersonalRiskAssessmentChecklist.sql

REVOKE DELETE ON [ChecklistGeneratorEmployee] TO AllowAll
REVOKE DELETE ON [PersonalRiskAssessmentChecklist] TO AllowAll

DELETE FROM changelog WHERE change_number = 240 AND delta_set = 'Main'
GO

--------------- Fragment ends: #240: 240_GRANT Delete ChecklistGeneratorEmployee PersonalRiskAssessmentChecklist.sql ---------------

--------------- Fragment begins: #239: 239_ INSERT INTO CompanyVehicleTypes.sql ---------------

-- Change script: #239: 239_ INSERT INTO CompanyVehicleTypes.sql
USE [BusinessSafe]
GO

DELETE FROM [CompanyVehicleType] WHERE Id = 1

Go

DELETE FROM changelog WHERE change_number = 239 AND delta_set = 'Main'
GO

--------------- Fragment ends: #239: 239_ INSERT INTO CompanyVehicleTypes.sql ---------------

--------------- Fragment begins: #238: 238_INSERT CYP Checklist.sql ---------------

-- Change script: #238: 238_INSERT CYP Checklist.sql
DELETE from question where sectionid in (
  select id from section where checklistid = 3
)
DELETE from section where checklistid = 3
DELETE from checklist where id = 3

DELETE FROM changelog WHERE change_number = 238 AND delta_set = 'Main'
GO

--------------- Fragment ends: #238: 238_INSERT CYP Checklist.sql ---------------

--------------- Fragment begins: #237: 237_CREATE ChecklistGeneratorEmployee Table.sql ---------------

-- Change script: #237: 237_CREATE ChecklistGeneratorEmployee Table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ChecklistGeneratorEmployee')
BEGIN
	DROP TABLE [dbo].[ChecklistGeneratorEmployee]
END
GO

DELETE FROM changelog WHERE change_number = 237 AND delta_set = 'Main'
GO

--------------- Fragment ends: #237: 237_CREATE ChecklistGeneratorEmployee Table.sql ---------------

--------------- Fragment begins: #236: 236_CREATE PersonalRiskAssessmentChecklist Table.sql ---------------

-- Change script: #236: 236_CREATE PersonalRiskAssessmentChecklist Table.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PersonalRiskAssessmentChecklist')
BEGIN
	DROP TABLE [dbo].[PersonalRiskAssessmentChecklist]
END
GO

DELETE FROM changelog WHERE change_number = 236 AND delta_set = 'Main'
GO

--------------- Fragment ends: #236: 236_CREATE PersonalRiskAssessmentChecklist Table.sql ---------------

--------------- Fragment begins: #235: 235_INSERT New Mothers Checklist.sql ---------------

-- Change script: #235: 235_INSERT New Mothers Checklist.sql
DELETE from question where sectionid in (
  select id from section where checklistid = 3
)
DELETE from section where checklistid = 3
DELETE from checklist where id = 3

DELETE FROM changelog WHERE change_number = 235 AND delta_set = 'Main'
GO

--------------- Fragment ends: #235: 235_INSERT New Mothers Checklist.sql ---------------

--------------- Fragment begins: #234: 234_ALTER PersonalRiskAssessment Table.sql ---------------

-- Change script: #234: 234_ALTER PersonalRiskAssessment Table.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'HasMultipleChecklistRecipients')
BEGIN
	ALTER TABLE [PersonalRiskAssessment]
	DROP COLUMN [HasMultipleChecklistRecipients]
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'ChecklistGeneratorMessage')
BEGIN
	ALTER TABLE [PersonalRiskAssessment]
	DROP COLUMN [ChecklistGeneratorMessage]
END

DELETE FROM changelog WHERE change_number = 234 AND delta_set = 'Main'
GO

--------------- Fragment ends: #234: 234_ALTER PersonalRiskAssessment Table.sql ---------------

--------------- Fragment begins: #233: 233_UPDATE Checklist Title.sql ---------------

-- Change script: #233: 233_UPDATE Checklist Title.sql

UPDATE [BusinessSafe].[dbo].[Checklist] SET [Title] = 'Display Screen Equipment Self Assessment Questionnaire' WHERE Id = 1
UPDATE [BusinessSafe].[dbo].[Checklist] SET [Title] = 'Manual Handling Self Assessment Questionnaire' WHERE Id = 2

DELETE FROM changelog WHERE change_number = 233 AND delta_set = 'Main'
GO

--------------- Fragment ends: #233: 233_UPDATE Checklist Title.sql ---------------

--------------- Fragment begins: #232: 232_GRANT Permissions EmployeeChecklistEmpoloyeeChecklistEmail.sql ---------------

-- Change script: #232: 232_GRANT Permissions EmployeeChecklistEmpoloyeeChecklistEmail.sql

REVOKE SELECT, INSERT, UPDATE ON [EmployeeChecklistEmployeeChecklistEmail] TO AllowAll

DELETE FROM changelog WHERE change_number = 232 AND delta_set = 'Main'
GO

--------------- Fragment ends: #232: 232_GRANT Permissions EmployeeChecklistEmpoloyeeChecklistEmail.sql ---------------

--------------- Fragment begins: #231: 231_ALTER EmployeeChecklist Table.sql ---------------

-- Change script: #231: 231_ALTER EmployeeChecklist Table.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EmployeeChecklist' AND COLUMN_NAME = 'Password' AND IS_NULLABLE = 'YES')
BEGIN
	ALTER TABLE [EmployeeChecklist]
	ALTER COLUMN [password] [nvarchar](50) NOT NULL
END

DELETE FROM changelog WHERE change_number = 231 AND delta_set = 'Main'
GO

--------------- Fragment ends: #231: 231_ALTER EmployeeChecklist Table.sql ---------------

--------------- Fragment begins: #230: 230_INSERT MH Checklist.sql ---------------

-- Change script: #230: 230_INSERT MH Checklist.sql
DELETE from question where sectionid in (
  select id from section where checklistid = 2
)
DELETE from section where checklistid = 2
DELETE from checklist where id = 2

DELETE FROM changelog WHERE change_number = 230 AND delta_set = 'Main'
GO

--------------- Fragment ends: #230: 230_INSERT MH Checklist.sql ---------------

--------------- Fragment begins: #229: 229_CREATE EmployeeChecklistEmail Table.sql ---------------

-- Change script: #229: 229_CREATE EmployeeChecklistEmail Table.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EmployeeChecklistEmployeeChecklistEmail')
BEGIN
	DROP TABLE [dbo].[EmployeeChecklistEmployeeChecklistEmail]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EmployeeChecklistEmail')
BEGIN
	DROP TABLE [dbo].[EmployeeChecklistEmail]
END
GO

DELETE FROM changelog WHERE change_number = 229 AND delta_set = 'Main'
GO

--------------- Fragment ends: #229: 229_CREATE EmployeeChecklistEmail Table.sql ---------------

--------------- Fragment begins: #228: 228_INSERT DSE Checklist.sql ---------------

-- Change script: #228: 228_INSERT DSE Checklist.sql
DELETE FROM [BusinessSafe].[dbo].[Checklist]
DELETE FROM [BusinessSafe].[dbo].[Section]
DELETE FROM [BusinessSafe].[dbo].[Question]

DELETE FROM changelog WHERE change_number = 228 AND delta_set = 'Main'
GO

--------------- Fragment ends: #228: 228_INSERT DSE Checklist.sql ---------------

--------------- Fragment begins: #225: 225_RENAME and UPDATE various tables.sql ---------------

-- Change script: #225: 225_RENAME and UPDATE various tables.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'MultiHazardRiskAssessmentControlMeasure' AND COLUMN_NAME = 'MultiHazardRiskAssessmentHazardId')
BEGIN
	EXEC SP_RENAME 'MultiHazardRiskAssessmentControlMeasure.MultiHazardRiskAssessmentHazardId' , 'RiskAssessmentHazardId', 'COLUMN'
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'MultiHazardRiskAssessmentHazardId')
BEGIN
	EXEC SP_RENAME 'Task.MultiHazardRiskAssessmentHazardId' , 'RiskAssessmentHazardId', 'COLUMN'
END
GO

IF EXISTS (SELECT * FROM [Task] WHERE [Discriminator] = 'MultiHazardRiskAssessmentFurtherControlMeasureTask')
BEGIN
	UPDATE [Task] 
	SET [Discriminator] = 'GeneralRiskAssessmentFurtherControlMeasureTask'
	WHERE [Discriminator] = 'MultiHazardRiskAssessmentFurtherControlMeasureTask'
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TaskCategory')
BEGIN
	EXEC SP_RENAME 'TaskCategory', 'ResponsibilityTaskCategory'
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'MultiHazardRiskAssessmentControlMeasure')
BEGIN
	EXEC SP_RENAME 'MultiHazardRiskAssessmentControlMeasure', 'GeneralRiskAssessmentControlMeasure'
END
GO

DELETE FROM changelog WHERE change_number = 225 AND delta_set = 'Main'
GO

--------------- Fragment ends: #225: 225_RENAME and UPDATE various tables.sql ---------------

--------------- Fragment begins: #224: 224_ALTER Checklist Tables.sql ---------------

-- Change script: #224: 224_ALTER Checklist Tables.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Answer' AND COLUMN_NAME = 'EmployeeChecklistId' AND DATA_TYPE = 'uniqueidentifier')
BEGIN
	ALTER TABLE [Answer]
	DROP COLUMN [EmployeeChecklistId] 
	
	ALTER TABLE [Answer]
	ADD [EmployeeChecklistId] [bigint] NOT NULL
END

DELETE FROM changelog WHERE change_number = 224 AND delta_set = 'Main'
GO

--------------- Fragment ends: #224: 224_ALTER Checklist Tables.sql ---------------

--------------- Fragment begins: #223: 223_CREATE Checklist Tables.sql ---------------

-- Change script: #223: 223_CREATE Checklist Tables.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EmployeeChecklist')
BEGIN
	DROP TABLE [dbo].[EmployeeChecklist]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Checklist')
BEGIN
	DROP TABLE [dbo].[Checklist]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Section')
BEGIN
	DROP TABLE [dbo].[Section]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Question')
BEGIN
	DROP TABLE [dbo].[Question]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Answer')
BEGIN
	DROP TABLE [dbo].[Answer]
END
GO

DELETE FROM changelog WHERE change_number = 223 AND delta_set = 'Main'
GO

--------------- Fragment ends: #223: 223_CREATE Checklist Tables.sql ---------------

--------------- Fragment begins: #222: 222_ALTER PersonalRiskAssessment Add Sensitive.sql ---------------

-- Change script: #222: 222_ALTER PersonalRiskAssessment Add Sensitive.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'Sensitive')
BEGIN
	ALTER TABLE [PersonalRiskAssessment]
	DROP COLUMN [Sensitive] 
END

DELETE FROM changelog WHERE change_number = 222 AND delta_set = 'Main'
GO

--------------- Fragment ends: #222: 222_ALTER PersonalRiskAssessment Add Sensitive.sql ---------------

--------------- Fragment begins: #221: 221_Insert DocumentType.sql ---------------

-- Change script: #221: 221_Insert DocumentType.sql
DELETE
FROM [BusinessSafe].[dbo].[DocumentType]
WHERE id IN (12)
GO



DELETE FROM changelog WHERE change_number = 221 AND delta_set = 'Main'
GO

--------------- Fragment ends: #221: 221_Insert DocumentType.sql ---------------

--------------- Fragment begins: #220: 220_INSERT blank site.sql ---------------

-- Change script: #220: 220_INSERT blank site.sql

IF EXISTS (SELECT * FROM [SiteStructureElement] WHERE [Id] = 0)
BEGIN
	DELETE FROM [SiteStructureElement] WHERE [Id] = 0
END

DELETE FROM changelog WHERE change_number = 220 AND delta_set = 'Main'
GO

--------------- Fragment ends: #220: 220_INSERT blank site.sql ---------------

--------------- Fragment begins: #219: 219_INSERT PRA hazards.sql ---------------

-- Change script: #219: 219_INSERT PRA hazards.sql

DELETE FROM changelog WHERE change_number = 219 AND delta_set = 'Main'
GO

--------------- Fragment ends: #219: 219_INSERT PRA hazards.sql ---------------

--------------- Fragment begins: #218: 218_DROPPING Old Tables.sql ---------------

-- Change script: #218: 218_DROPPING Old Tables.sql
USE [BusinessSafe]
GO

-- These tables need to go so let's not go back hey code does not reference them!




DELETE FROM changelog WHERE change_number = 218 AND delta_set = 'Main'
GO

--------------- Fragment ends: #218: 218_DROPPING Old Tables.sql ---------------

--------------- Fragment begins: #217: 217_ALTER Audit Table.sql ---------------

-- Change script: #217: 217_ALTER Audit Table.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'UpdateId')
BEGIN
	ALTER TABLE [Audit]
	DROP COLUMN [UpdateId] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'UpdateDate' AND [DATA_TYPE] = 'datetime')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [UpdateDate] [date]
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'NewValue' AND [CHARACTER_MAXIMUM_LENGTH] = '128')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [NewValue] [nvarchar](128)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'OldValue' AND [DATA_TYPE] = 'nvarchar')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [OldValue] [varchar](128)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'FieldName' AND [DATA_TYPE] = 'nvarchar')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [FieldName] [varchar](200)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'EntityId' AND [DATA_TYPE] = 'nvarchar')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [EntityId] [varchar](200)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'EntityName' AND [DATA_TYPE] = 'nvarchar')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [EntityName] [varchar](200)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'Type' AND [DATA_TYPE] = 'nvarchar')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [Type] [varchar](1)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'Audit' AND [COLUMN_NAME] = 'Id' AND [DATA_TYPE] = 'bigint')
BEGIN
	ALTER TABLE [Audit]
	ALTER COLUMN [Id] [int]
END


DELETE FROM changelog WHERE change_number = 217 AND delta_set = 'Main'
GO

--------------- Fragment ends: #217: 217_ALTER Audit Table.sql ---------------

--------------- Fragment begins: #216: 216_Insert DocumentType.sql ---------------

-- Change script: #216: 216_Insert DocumentType.sql
DELETE
FROM [BusinessSafe].[dbo].[DocumentType]
WHERE id IN (10, 11)




DELETE FROM changelog WHERE change_number = 216 AND delta_set = 'Main'
GO

--------------- Fragment ends: #216: 216_Insert DocumentType.sql ---------------

--------------- Fragment begins: #215: 215_Add Default To Task TaskReoccurringTypeId.sql ---------------

-- Change script: #215: 215_Add Default To Task TaskReoccurringTypeId.sql
IF EXISTS (SELECT * FROM sys.default_constraints 
    				WHERE NAME ='DF_Task_TaskReoccurringTypeId')
BEGIN

	ALTER TABLE dbo.Task 
  	DROP CONSTRAINT DF_Task_TaskReoccurringTypeId
END
GO

IF EXISTS (SELECT * FROM sys.default_constraints 
    				WHERE NAME ='DF_SafetyPhrase_RequiresAdditionalInformation')
BEGIN

	ALTER TABLE dbo.SafetyPhrase 
  	DROP CONSTRAINT DF_SafetyPhrase_RequiresAdditionalInformation
END
GO

DELETE FROM changelog WHERE change_number = 215 AND delta_set = 'Main'
GO

--------------- Fragment ends: #215: 215_Add Default To Task TaskReoccurringTypeId.sql ---------------

--------------- Fragment begins: #214: 214_ALTER various tables add primary key.sql ---------------

-- Change script: #214: 214_ALTER various tables add primary key.sql

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_HazardHazardType')
BEGIN
	ALTER TABLE [HazardHazardType]
	DROP CONSTRAINT PK_HazardHazardType
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_SiteStructureElement')
BEGIN
	ALTER TABLE [SiteStructureElement]
	DROP CONSTRAINT PK_SiteStructureElement
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardHazardType' AND COLUMN_NAME = 'Id')
BEGIN
	ALTER TABLE [HazardHazardType]
	ADD [Id] bigint NULL
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_PersonalRiskAssessment')
BEGIN
	ALTER TABLE [PersonalRiskAssessment]
	DROP CONSTRAINT PK_PersonalRiskAssessment
END
GO

IF((SELECT IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonalRiskAssessment' AND COLUMN_NAME = 'Id') = 'NO')
BEGIN 
	ALTER TABLE [PersonalRiskAssessment]
	ALTER COLUMN [Id] bigint NULL
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_PeopleAtRisk')
BEGIN
	ALTER TABLE [PeopleAtRisk]
	DROP CONSTRAINT PK_PeopleAtRisk
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_NonEmployee')
BEGIN
	ALTER TABLE [NonEmployee]
	DROP CONSTRAINT PK_NonEmployee 
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_Hazard')
BEGIN
	ALTER TABLE [Hazard]
	DROP CONSTRAINT PK_Hazard 
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_GeneralRiskAssessmentControlMeasure')
BEGIN
	ALTER TABLE [GeneralRiskAssessmentControlMeasure]
	DROP CONSTRAINT PK_GeneralRiskAssessmentControlMeasure
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_GeneralRiskAssessment')
BEGIN
	ALTER TABLE [GeneralRiskAssessment]
	DROP CONSTRAINT PK_GeneralRiskAssessment
END
GO

IF((SELECT IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'GeneralRiskAssessment' AND COLUMN_NAME = 'Id') = 'NO')
BEGIN 
	ALTER TABLE [GeneralRiskAssessment]
	ALTER COLUMN [Id] bigint NULL
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_DocumentType')
BEGIN
	ALTER TABLE [DocumentType]
	DROP CONSTRAINT PK_DocumentType
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'PK_AddedDocument')
BEGIN
	ALTER TABLE [AddedDocument]
	DROP CONSTRAINT PK_AddedDocument
END
GO

DELETE FROM changelog WHERE change_number = 214 AND delta_set = 'Main'
GO

--------------- Fragment ends: #214: 214_ALTER various tables add primary key.sql ---------------

--------------- Fragment begins: #213: 213_CREATE Audit Table.sql ---------------

-- Change script: #213: 213_CREATE Audit Table.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Audit')
BEGIN
	DROP TABLE [Audit];
END

DELETE FROM changelog WHERE change_number = 213 AND delta_set = 'Main'
GO

--------------- Fragment ends: #213: 213_CREATE Audit Table.sql ---------------

--------------- Fragment begins: #212: 212_Create function fn_GetRADocuments.sql ---------------

-- Change script: #212: 212_Create function fn_GetRADocuments.sql

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetRADocuments]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[fn_GetRADocuments]
GO




DELETE FROM changelog WHERE change_number = 212 AND delta_set = 'Main'
GO

--------------- Fragment ends: #212: 212_Create function fn_GetRADocuments.sql ---------------

--------------- Fragment begins: #211: 211_Create function fn_GetPeopleAtRisk.sql ---------------

-- Change script: #211: 211_Create function fn_GetPeopleAtRisk.sql

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetPeopleAtRiskString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[fn_GetPeopleAtRiskString]
GO




DELETE FROM changelog WHERE change_number = 211 AND delta_set = 'Main'
GO

--------------- Fragment ends: #211: 211_Create function fn_GetPeopleAtRisk.sql ---------------

--------------- Fragment begins: #210: 210_Create function fn_GetNonEmployeesInvolvedString.sql ---------------

-- Change script: #210: 210_Create function fn_GetNonEmployeesInvolvedString.sql

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetNonEmployeesInvolvedString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[fn_GetNonEmployeesInvolvedString]
GO




DELETE FROM changelog WHERE change_number = 210 AND delta_set = 'Main'
GO

--------------- Fragment ends: #210: 210_Create function fn_GetNonEmployeesInvolvedString.sql ---------------

--------------- Fragment begins: #209: 209_Create function fn_GetEmployeesInvolvedString.sql ---------------

-- Change script: #209: 209_Create function fn_GetEmployeesInvolvedString.sql

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetEmployeesInvolvedString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[fn_GetEmployeesInvolvedString]
GO

DELETE FROM changelog WHERE change_number = 209 AND delta_set = 'Main'
GO

--------------- Fragment ends: #209: 209_Create function fn_GetEmployeesInvolvedString.sql ---------------

--------------- Fragment begins: #205: 205_UPDATE Risk Assessment.sql ---------------

-- Change script: #205: 205_UPDATE Risk Assessment.sql

ALTER TABLE dbo.RiskAssessment ALTER COLUMN Title NVARCHAR(200) NOT NULL
GO

DELETE FROM changelog WHERE change_number = 205 AND delta_set = 'Main'
GO

--------------- Fragment ends: #205: 205_UPDATE Risk Assessment.sql ---------------

--------------- Fragment begins: #204: 204_UPDATE HazardHazardType.sql ---------------

-- Change script: #204: 204_UPDATE HazardHazardType.sql

DELETE FROM [dbo].[HazardHazardType]

DELETE FROM changelog WHERE change_number = 204 AND delta_set = 'Main'
GO

--------------- Fragment ends: #204: 204_UPDATE HazardHazardType.sql ---------------

--------------- Fragment begins: #203: 203_UPDATE GeneralRiskAssessment.sql ---------------

-- Change script: #203: 203_UPDATE GeneralRiskAssessment.sql

DELETE FROM [dbo].[GeneralRiskAssessment]

DELETE FROM changelog WHERE change_number = 203 AND delta_set = 'Main'
GO

--------------- Fragment ends: #203: 203_UPDATE GeneralRiskAssessment.sql ---------------

--------------- Fragment begins: #202: 202_CREATE HazardType.sql ---------------

-- Change script: #202: 202_CREATE HazardType.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'HazardType')
BEGIN
	DROP TABLE [HazardType];
END

DELETE FROM changelog WHERE change_number = 202 AND delta_set = 'Main'
GO

--------------- Fragment ends: #202: 202_CREATE HazardType.sql ---------------

--------------- Fragment begins: #201: 201_CREATE PersonalRiskAssessment.sql ---------------

-- Change script: #201: 201_CREATE PersonalRiskAssessment.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PersonalRiskAssessment')
BEGIN
	DROP TABLE [PersonalRiskAssessment];
END

DELETE FROM changelog WHERE change_number = 201 AND delta_set = 'Main'
GO

--------------- Fragment ends: #201: 201_CREATE PersonalRiskAssessment.sql ---------------

--------------- Fragment begins: #200: 200_ALTER GeneralRiskAssessmentHazard Rename MultiHazardRiskAssessmentHazard.sql ---------------

-- Change script: #200: 200_ALTER GeneralRiskAssessmentHazard Rename MultiHazardRiskAssessmentHazard.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'MultiHazardRiskAssessmentHazard')
BEGIN
	EXEC sp_rename 'MultiHazardRiskAssessmentHazard', 'GeneralRiskAssessmentHazard'
END

DELETE FROM changelog WHERE change_number = 200 AND delta_set = 'Main'
GO

--------------- Fragment ends: #200: 200_ALTER GeneralRiskAssessmentHazard Rename MultiHazardRiskAssessmentHazard.sql ---------------

--------------- Fragment begins: #199: 199_ALTER GeneralRiskAssessment Table Grant All Permissions.sql ---------------

-- Change script: #199: 199_ALTER GeneralRiskAssessment Table Grant All Permissions.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessment')
BEGIN
	REVOKE SELECT, INSERT, DELETE, UPDATE ON [GeneralRiskAssessment] FROM [AllowAll]
END
GO

DELETE FROM changelog WHERE change_number = 199 AND delta_set = 'Main'
GO

--------------- Fragment ends: #199: 199_ALTER GeneralRiskAssessment Table Grant All Permissions.sql ---------------

--------------- Fragment begins: #198: 198_CREATE GeneralRiskAssessment Table.sql ---------------

-- Change script: #198: 198_CREATE GeneralRiskAssessment Table.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessment')
BEGIN
	DROP TABLE [GeneralRiskAssessment];
END

DELETE FROM changelog WHERE change_number = 198 AND delta_set = 'Main'
GO

--------------- Fragment ends: #198: 198_CREATE GeneralRiskAssessment Table.sql ---------------

--------------- Fragment begins: #197: 197_ALTER GeneralRiskAssessment Rename MultiHazardRiskAssessment.sql ---------------

-- Change script: #197: 197_ALTER GeneralRiskAssessment Rename MultiHazardRiskAssessment.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'MultiHazardRiskAssessment')
BEGIN
	EXEC sp_rename 'MultiHazardRiskAssessment', 'GeneralRiskAssessment'
END

DELETE FROM changelog WHERE change_number = 197 AND delta_set = 'Main'
GO

--------------- Fragment ends: #197: 197_ALTER GeneralRiskAssessment Rename MultiHazardRiskAssessment.sql ---------------

--------------- Fragment begins: #196: 196_ALTER Rename GeneralRiskAssessmentPeopleAtRisk.sql ---------------

-- Change script: #196: 196_ALTER Rename GeneralRiskAssessmentPeopleAtRisk.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentPeopleAtRisk')
BEGIN
	EXEC sp_rename 'RiskAssessmentPeopleAtRisk', 'GeneralRiskAssessmentPeopleAtRisk'
END

DELETE FROM changelog WHERE change_number = 196 AND delta_set = 'Main'
GO

--------------- Fragment ends: #196: 196_ALTER Rename GeneralRiskAssessmentPeopleAtRisk.sql ---------------

--------------- Fragment begins: #195: 195_DROP HazardType.sql ---------------

-- Change script: #195: 195_DROP HazardType.sql
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'HazardType' AND TYPE = 'U')
BEGIN
	CREATE TABLE [dbo].[HazardType](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](250) NULL,
		[Deleted] [bit] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[LastModifiedBy] [uniqueidentifier] NULL
	) ON [PRIMARY]
END
GO

GRANT SELECT, INSERT, DELETE, UPDATE ON [HazardType] TO [AllowAll]
GO

SET IDENTITY_INSERT [BusinessSafe].[dbo].[HazardType] ON

INSERT INTO [BusinessSafe].[dbo].[HazardType] ([Id], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
VALUES (1, 'General', 0, GETDATE(), NULL, NULL, NULL)

INSERT INTO [BusinessSafe].[dbo].[HazardType] ([Id], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
VALUES (2, 'Personal', 0, GETDATE(), NULL, NULL, NULL)

INSERT INTO [BusinessSafe].[dbo].[HazardType] ([Id], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
VALUES (3, 'Hazardous Substance', 0, GETDATE(), NULL, NULL, NULL)

SET IDENTITY_INSERT [BusinessSafe].[dbo].[HazardType] OFF

DELETE FROM changelog WHERE change_number = 195 AND delta_set = 'Main'
GO

--------------- Fragment ends: #195: 195_DROP HazardType.sql ---------------

--------------- Fragment begins: #194: 194_INSERT HazardTypes.sql ---------------

-- Change script: #194: 194_INSERT HazardTypes.sql
print 'deleting HazardTypes'
DELETE
FROM [BusinessSafe].[dbo].[HazardType]
WHERE id IN ( 1, 2, 3)

DELETE FROM changelog WHERE change_number = 194 AND delta_set = 'Main'
GO

--------------- Fragment ends: #194: 194_INSERT HazardTypes.sql ---------------

--------------- Fragment begins: #193: 193_CREATE HazardHazardType Table.sql ---------------

-- Change script: #193: 193_CREATE HazardHazardType Table.sql
IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'HazardHazardType' AND TYPE = 'U')
BEGIN
	DROP TABLE [HazardHazardType]
END

DELETE FROM changelog WHERE change_number = 193 AND delta_set = 'Main'
GO

--------------- Fragment ends: #193: 193_CREATE HazardHazardType Table.sql ---------------

--------------- Fragment begins: #192: 192_CREATE HazardType Table.sql ---------------

-- Change script: #192: 192_CREATE HazardType Table.sql
IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'HazardType' AND TYPE = 'U')
BEGIN
	DROP TABLE [HazardType]
END

DELETE FROM changelog WHERE change_number = 192 AND delta_set = 'Main'
GO

--------------- Fragment ends: #192: 192_CREATE HazardType Table.sql ---------------

--------------- Fragment begins: #191: 191_ALTER HazardousSubstanceSafetyPhrase Alter Id Fields From nvarchar to bigint.sql ---------------

-- Change script: #191: 191_ALTER HazardousSubstanceSafetyPhrase Alter Id Fields From nvarchar to bigint.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'HazardousSubstanceId')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	ALTER COLUMN [HazardousSubstanceId] nvarchar(200) not null
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'SafetyPhraseId')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	ALTER COLUMN [SafetyPhraseId] nvarchar(50) not null
END
GO

DELETE FROM changelog WHERE change_number = 191 AND delta_set = 'Main'
GO

--------------- Fragment ends: #191: 191_ALTER HazardousSubstanceSafetyPhrase Alter Id Fields From nvarchar to bigint.sql ---------------

--------------- Fragment begins: #190: 190_ALTER HazardousSubstanceRiskPhrase Alter Id Fields From nvarchar to bigint.sql ---------------

-- Change script: #190: 190_ALTER HazardousSubstanceRiskPhrase Alter Id Fields From nvarchar to bigint.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'HazardousSubstanceId')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	ALTER COLUMN [HazardousSubstanceId] nvarchar(200) not null
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' AND COLUMN_NAME = 'RiskPhraseId')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
	ALTER COLUMN [RiskPhraseId] nvarchar(50) not null
END
GO

DELETE FROM changelog WHERE change_number = 190 AND delta_set = 'Main'
GO

--------------- Fragment ends: #190: 190_ALTER HazardousSubstanceRiskPhrase Alter Id Fields From nvarchar to bigint.sql ---------------

--------------- Fragment begins: #189: 189_ALTER HazardousSubstanceControlMeasure Alter Field Length ControlMeasure.sql ---------------

-- Change script: #189: 189_ALTER HazardousSubstanceControlMeasure Alter Field Length ControlMeasure.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessmentControlMeasure' AND COLUMN_NAME = 'ControlMeasure' AND CHARACTER_MAXIMUM_LENGTH = '300')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessmentControlMeasure]
	ALTER COLUMN [ControlMeasure] nvarchar(150)
END
GO

DELETE FROM changelog WHERE change_number = 189 AND delta_set = 'Main'
GO

--------------- Fragment ends: #189: 189_ALTER HazardousSubstanceControlMeasure Alter Field Length ControlMeasure.sql ---------------

--------------- Fragment begins: #188: 188_ALTER GeneralRiskAssessmentControlMeasure Alter Field Length ControlMeasure.sql ---------------

-- Change script: #188: 188_ALTER GeneralRiskAssessmentControlMeasure Alter Field Length ControlMeasure.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'GeneralRiskAssessmentControlMeasure' AND COLUMN_NAME = 'ControlMeasure' AND CHARACTER_MAXIMUM_LENGTH = '300')
BEGIN
	ALTER TABLE [GeneralRiskAssessmentControlMeasure]
	ALTER COLUMN [ControlMeasure] nvarchar(150)
END
GO

DELETE FROM changelog WHERE change_number = 188 AND delta_set = 'Main'
GO

--------------- Fragment ends: #188: 188_ALTER GeneralRiskAssessmentControlMeasure Alter Field Length ControlMeasure.sql ---------------

--------------- Fragment begins: #187: 187_INSERT into RolePermissions Table Personal Risk Assessments.sql ---------------

-- Change script: #187: 187_INSERT into RolePermissions Table Personal Risk Assessments.sql
USE [BusinessSafe]
GO

DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = N'bacf7c01-d210-4dbc-942f-15d8456d3b92' AND [PermissionId] IN (47,48,49,50)


DELETE FROM changelog WHERE change_number = 187 AND delta_set = 'Main'
GO

--------------- Fragment ends: #187: 187_INSERT into RolePermissions Table Personal Risk Assessments.sql ---------------

--------------- Fragment begins: #186: 186_INSERT Into Permissions Table Personal Risk Assessments.sql ---------------

-- Change script: #186: 186_INSERT Into Permissions Table Personal Risk Assessments.sql
DELETE FROM [BusinessSafe].[dbo].[Permission]
WHERE ID IN (47, 48, 49, 50)

DELETE FROM changelog WHERE change_number = 186 AND delta_set = 'Main'
GO

--------------- Fragment ends: #186: 186_INSERT Into Permissions Table Personal Risk Assessments.sql ---------------

--------------- Fragment begins: #185: 185_INSERT into PermissionTarget Personal Risk Assessments.sql ---------------

-- Change script: #185: 185_INSERT into PermissionTarget Personal Risk Assessments.sql
DELETE FROM [PermissionTarget]
WHERE ID = 15

DELETE FROM changelog WHERE change_number = 185 AND delta_set = 'Main'
GO

--------------- Fragment ends: #185: 185_INSERT into PermissionTarget Personal Risk Assessments.sql ---------------

--------------- Fragment begins: #184: 184_DROPPING Old Tables.sql ---------------

-- Change script: #184: 184_DROPPING Old Tables.sql
USE [BusinessSafe]
GO

-- These tables need to go so let's not go back hey code does not reference them!




DELETE FROM changelog WHERE change_number = 184 AND delta_set = 'Main'
GO

--------------- Fragment ends: #184: 184_DROPPING Old Tables.sql ---------------

--------------- Fragment begins: #183: 183_CHANGE FK Constraint from using a previous table.sql ---------------

-- Change script: #183: 183_CHANGE FK Constraint from using a previous table.sql
USE [BusinessSafe]
GO

IF (OBJECT_ID('FK_RolesPermissions_Permission', 'F') IS NULL)
BEGIN
	ALTER TABLE [dbo].[RolesPermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolesPermissions_Permission] FOREIGN KEY([PermissionId])
	REFERENCES [dbo].[PREVIOUS_Permission] ([PermissionId])	
END
GO


DELETE FROM changelog WHERE change_number = 183 AND delta_set = 'Main'
GO

--------------- Fragment ends: #183: 183_CHANGE FK Constraint from using a previous table.sql ---------------

--------------- Fragment begins: #182: 182_INSERT into RolePermissions Table.sql ---------------

-- Change script: #182: 182_INSERT into RolePermissions Table.sql
USE [BusinessSafe]
GO

DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = N'bacf7c01-d210-4dbc-942f-15d8456d3b92' AND [PermissionId] IN (43,44,45,46)
DELETE FROM [dbo].[RolesPermissions] WHERE [RoleId] = N'1E382767-93DD-47E2-88F2-B3E7F7648642' AND [PermissionId] IN (43,44,45,46)



DELETE FROM changelog WHERE change_number = 182 AND delta_set = 'Main'
GO

--------------- Fragment ends: #182: 182_INSERT into RolePermissions Table.sql ---------------

--------------- Fragment begins: #181: 181_INSERT Into Permissions Table.sql ---------------

-- Change script: #181: 181_INSERT Into Permissions Table.sql
DELETE FROM [BusinessSafe].[dbo].[Permission]
WHERE ID IN (43, 44, 45, 46)

DELETE FROM changelog WHERE change_number = 181 AND delta_set = 'Main'
GO

--------------- Fragment ends: #181: 181_INSERT Into Permissions Table.sql ---------------

--------------- Fragment begins: #180: 180_INSERT into PermissionTarget.sql ---------------

-- Change script: #180: 180_INSERT into PermissionTarget.sql
DELETE FROM [PermissionTarget]
WHERE ID = 14

DELETE FROM changelog WHERE change_number = 180 AND delta_set = 'Main'
GO

--------------- Fragment ends: #180: 180_INSERT into PermissionTarget.sql ---------------

--------------- Fragment begins: #179: 179_ALTER User Table Drop PermissionsApplyToAllSites Field.sql ---------------

-- Change script: #179: 179_ALTER User Table Drop PermissionsApplyToAllSites Field.sql
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'User' AND COLUMN_NAME = 'PermissionsApplyToAllSites')
BEGIN
	ALTER TABLE [User]
	ADD [PermissionsApplyToAllSites] BIT NULL
END
GO

DELETE FROM changelog WHERE change_number = 179 AND delta_set = 'Main'
GO

--------------- Fragment ends: #179: 179_ALTER User Table Drop PermissionsApplyToAllSites Field.sql ---------------

--------------- Fragment begins: #178: 178_INSERT INTO DocumentType.sql ---------------

-- Change script: #178: 178_INSERT INTO DocumentType.sql
USE [BusinessSafe]
GO

DELETE FROM [DocumentType] WHERE Id = 9

DELETE FROM changelog WHERE change_number = 178 AND delta_set = 'Main'
GO

--------------- Fragment ends: #178: 178_INSERT INTO DocumentType.sql ---------------

--------------- Fragment begins: #177: 177_UPDATE Safety Phrases.sql ---------------

-- Change script: #177: 177_UPDATE Safety Phrases.sql

UPDATE [SafetyPhrase] SET [RequiresAdditionalInformation] = 0 WHERE Id = 91
UPDATE [SafetyPhrase] SET [RequiresAdditionalInformation] = 0 WHERE Id = 102
UPDATE [SafetyPhrase] SET [RequiresAdditionalInformation] = 0 WHERE Id = 106
UPDATE [SafetyPhrase] SET [RequiresAdditionalInformation] = 0 WHERE Id = 112
UPDATE [SafetyPhrase] SET [RequiresAdditionalInformation] = 0 WHERE Id = 140
UPDATE [SafetyPhrase] SET [RequiresAdditionalInformation] = 0 WHERE Id = 141
UPDATE [SafetyPhrase] SET [RequiresAdditionalInformation] = 0 WHERE Id = 142
UPDATE [SafetyPhrase] SET [RequiresAdditionalInformation] = 0 WHERE Id = 171
UPDATE [SafetyPhrase] SET [RequiresAdditionalInformation] = 0 WHERE Id = 196
UPDATE [SafetyPhrase] SET [RequiresAdditionalInformation] = 0 WHERE Id = 200
UPDATE [SafetyPhrase] SET [RequiresAdditionalInformation] = 0 WHERE Id = 205
UPDATE [SafetyPhrase] SET [RequiresAdditionalInformation] = 0 WHERE Id = 208
UPDATE [SafetyPhrase] SET [RequiresAdditionalInformation] = 0 WHERE Id = 211
UPDATE [SafetyPhrase] SET [RequiresAdditionalInformation] = 0 WHERE Id = 217
UPDATE [SafetyPhrase] SET [RequiresAdditionalInformation] = 0 WHERE Id = 218

DELETE FROM changelog WHERE change_number = 177 AND delta_set = 'Main'
GO

--------------- Fragment ends: #177: 177_UPDATE Safety Phrases.sql ---------------

--------------- Fragment begins: #176: 176_ALTER ControlSystem.sql ---------------

-- Change script: #176: 176_ALTER ControlSystem.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ControlSystem' AND COLUMN_NAME = 'Url')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	ADD [Url] varchar(150) NULL
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ControlSystem' AND COLUMN_NAME = 'DocumentLibraryId')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [DocumentLibraryId]
END
GO

DELETE FROM changelog WHERE change_number = 176 AND delta_set = 'Main'
GO

--------------- Fragment ends: #176: 176_ALTER ControlSystem.sql ---------------

--------------- Fragment begins: #175: 175_Add Delete Peremission DocumentKeyword.sql ---------------

-- Change script: #175: 175_Add Delete Peremission DocumentKeyword.sql

DENY DELETE ON [DocumentKeyword] TO [AllowAll]

DELETE FROM changelog WHERE change_number = 175 AND delta_set = 'Main'
GO

--------------- Fragment ends: #175: 175_Add Delete Peremission DocumentKeyword.sql ---------------

--------------- Fragment begins: #174: 174_ALTER HazardousSubstanceSafetyPhrase Add AdditionalInformation.sql ---------------

-- Change script: #174: 174_ALTER HazardousSubstanceSafetyPhrase Add AdditionalInformation.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' AND COLUMN_NAME = 'AdditionalInformation')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
	DROP COLUMN [AdditionalInformation]
END
GO

DELETE FROM changelog WHERE change_number = 174 AND delta_set = 'Main'
GO

--------------- Fragment ends: #174: 174_ALTER HazardousSubstanceSafetyPhrase Add AdditionalInformation.sql ---------------

--------------- Fragment begins: #173: 173_ALTER SafetyPhrase Add RequiresAdditionalInformation.sql ---------------

-- Change script: #173: 173_ALTER SafetyPhrase Add RequiresAdditionalInformation.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafetyPhrase' AND COLUMN_NAME = 'RequiresAdditionalInformation')
BEGIN
	ALTER TABLE [SafetyPhrase]
	DROP COLUMN [RequiresAdditionalInformation]
END
GO

DELETE FROM changelog WHERE change_number = 173 AND delta_set = 'Main'
GO

--------------- Fragment ends: #173: 173_ALTER SafetyPhrase Add RequiresAdditionalInformation.sql ---------------

--------------- Fragment begins: #172: 172_ALTER RiskAssessment Table Make Reference Field Nullable.sql ---------------

-- Change script: #172: 172_ALTER RiskAssessment Table Make Reference Field Nullable.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessment' AND COLUMN_NAME = 'Reference')
BEGIN
	ALTER TABLE [RiskAssessment]
	ALTER COLUMN [Reference] VARCHAR(50) NOT NULL
END
GO

DELETE FROM changelog WHERE change_number = 172 AND delta_set = 'Main'
GO

--------------- Fragment ends: #172: 172_ALTER RiskAssessment Table Make Reference Field Nullable.sql ---------------

--------------- Fragment begins: #171: 171_INSERT INTO TaskCategory - DLL data.sql ---------------

-- Change script: #171: 171_INSERT INTO TaskCategory - DLL data.sql
USE [BusinessSafe]
GO

DELETE FROM ResponsibilityTaskCategory WHERE Id = 6

DELETE FROM changelog WHERE change_number = 171 AND delta_set = 'Main'
GO

--------------- Fragment ends: #171: 171_INSERT INTO TaskCategory - DLL data.sql ---------------

--------------- Fragment begins: #170: 170_ALTER HazardousSubstance Table Make Reference Field Nullable.sql ---------------

-- Change script: #170: 170_ALTER HazardousSubstance Table Make Reference Field Nullable.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstance' AND COLUMN_NAME = 'Reference')
BEGIN
	ALTER TABLE [HazardousSubstance]
	ALTER COLUMN [Reference] VARCHAR(50) NOT NULL
END
GO

DELETE FROM changelog WHERE change_number = 170 AND delta_set = 'Main'
GO

--------------- Fragment ends: #170: 170_ALTER HazardousSubstance Table Make Reference Field Nullable.sql ---------------

--------------- Fragment begins: #169: 169_UPDATE ControlSystem with link to documents.sql ---------------

-- Change script: #169: 169_UPDATE ControlSystem with link to documents.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ControlSystem' AND COLUMN_NAME = 'Url')
BEGIN
	UPDATE [ControlSystem]
	SET [Url] = ''
	
	ALTER TABLE [ControlSystem]
	ALTER COLUMN [Url] VARCHAR(100) NOT NULL
END

DELETE FROM changelog WHERE change_number = 169 AND delta_set = 'Main'
GO

--------------- Fragment ends: #169: 169_UPDATE ControlSystem with link to documents.sql ---------------

--------------- Fragment begins: #168: 168_INSERT into RolePermissions Table.sql ---------------

-- Change script: #168: 168_INSERT into RolePermissions Table.sql
USE [BusinessSafe]
GO

TRUNCATE TABLE [dbo].[RolesPermissions]

INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 1)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 2)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 3)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 4)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 5)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 6)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 7)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 8)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 9)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 10)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 11)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 12)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 13)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 14)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 15)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 16)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 17)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 18)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 19)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 20)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 21)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 22)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 23)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 24)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 25)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 26)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 27)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 28)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 29)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 30)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 31)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 32)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 33)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 34)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 35)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 36)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 37)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 38)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', 39)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'952eecb7-2b96-4399-82ae-7e2341d25e51', 1)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'952eecb7-2b96-4399-82ae-7e2341d25e51', 37)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'952eecb7-2b96-4399-82ae-7e2341d25e51', 38)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'952eecb7-2b96-4399-82ae-7e2341d25e51', 39)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 1)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 2)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 3)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 4)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 8)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 12)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 13)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 14)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 15)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 16)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 17)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 18)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 19)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 20)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 21)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 22)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 23)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 24)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 25)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 26)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 27)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 37)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 38)
INSERT [dbo].[RolesPermissions] ([RoleId], [PermissionId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', 39)

DELETE FROM changelog WHERE change_number = 168 AND delta_set = 'Main'
GO

--------------- Fragment ends: #168: 168_INSERT into RolePermissions Table.sql ---------------

--------------- Fragment begins: #167: 167_ALTER Rename Site.sql ---------------

-- Change script: #167: 167_ALTER Rename Site.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SiteStructureElement')
BEGIN
	EXEC sp_rename 'SiteStructureElement', 'Site'
END

DELETE FROM changelog WHERE change_number = 167 AND delta_set = 'Main'
GO

--------------- Fragment ends: #167: 167_ALTER Rename Site.sql ---------------

--------------- Fragment begins: #166: 166_INSERT Into Permissions Table.sql ---------------

-- Change script: #166: 166_INSERT Into Permissions Table.sql
DELETE FROM [BusinessSafe].[dbo].[Permission]

DELETE FROM changelog WHERE change_number = 166 AND delta_set = 'Main'
GO

--------------- Fragment ends: #166: 166_INSERT Into Permissions Table.sql ---------------

--------------- Fragment begins: #165: 165_RENAME PermissionGroup and PermissionGroupPermissions Tables.sql ---------------

-- Change script: #165: 165_RENAME PermissionGroup and PermissionGroupPermissions Tables.sql
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_Permission')
BEGIN
	EXEC SP_RENAME 'PREVIOUS_PermissionGroup', 'PermissionGroup'
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_PermissionGroupsPermissions')
BEGIN
	EXEC SP_RENAME 'PREVIOUS_PermissionGroupsPermissions', 'PermissionGroupsPermissions'
END
GO

DELETE FROM changelog WHERE change_number = 165 AND delta_set = 'Main'
GO

--------------- Fragment ends: #165: 165_RENAME PermissionGroup and PermissionGroupPermissions Tables.sql ---------------

--------------- Fragment begins: #164: 164_RENAME Permission Table.sql ---------------

-- Change script: #164: 164_RENAME Permission Table.sql
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_Permission')
BEGIN
	DROP TABLE [dbo].Permission
	EXEC SP_RENAME 'PREVIOUS_Permission', 'Permission'
	EXEC SP_RENAME 'PK_PREVIOUS_Permission', 'PK_Permission'
END
GO

DELETE FROM changelog WHERE change_number = 164 AND delta_set = 'Main'
GO

--------------- Fragment ends: #164: 164_RENAME Permission Table.sql ---------------

--------------- Fragment begins: #163: 163_INSERT into PermissionTarget.sql ---------------

-- Change script: #163: 163_INSERT into PermissionTarget.sql
DELETE FROM [PermissionTarget]
WHERE id BETWEEN 1 AND 12

DELETE FROM changelog WHERE change_number = 163 AND delta_set = 'Main'
GO

--------------- Fragment ends: #163: 163_INSERT into PermissionTarget.sql ---------------

--------------- Fragment begins: #162: 162_CREATE PermissionTarget table.sql ---------------

-- Change script: #162: 162_CREATE PermissionTarget table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'PermissionTarget' AND TYPE = 'U')
BEGIN
	DROP TABLE [PermissionTarget]
END

DELETE FROM changelog WHERE change_number = 162 AND delta_set = 'Main'
GO

--------------- Fragment ends: #162: 162_CREATE PermissionTarget table.sql ---------------

--------------- Fragment begins: #161: 161_ALTER Permission add target and activity id columns.sql ---------------

-- Change script: #161: 161_ALTER Permission add target and activity id columns.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Permission' AND COLUMN_NAME = 'PermissionTargetId')
BEGIN
	ALTER TABLE [Permission]
	DROP COLUMN [PermissionTargetId]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Permission' AND COLUMN_NAME = 'PermissionActivityId')
BEGIN
	ALTER TABLE [Permission]
	DROP COLUMN [PermissionActivityId]
END
GO


DELETE FROM changelog WHERE change_number = 161 AND delta_set = 'Main'
GO

--------------- Fragment ends: #161: 161_ALTER Permission add target and activity id columns.sql ---------------

--------------- Fragment begins: #160: 160_ALTER User add column PermissionsApplyToAllSites.sql ---------------

-- Change script: #160: 160_ALTER User add column PermissionsApplyToAllSites.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'User' AND COLUMN_NAME = 'PermissionsApplyToAllSites')
BEGIN
	ALTER TABLE [User]
	DROP COLUMN [PermissionsApplyToAllSites] 
END
GO	


DELETE FROM changelog WHERE change_number = 160 AND delta_set = 'Main'
GO

--------------- Fragment ends: #160: 160_ALTER User add column PermissionsApplyToAllSites.sql ---------------

--------------- Fragment begins: #159: 159_Create GeneralRiskAssessment HazardousSubstanceRiskAssessment.sql ---------------

-- Change script: #159: 159_Create GeneralRiskAssessment HazardousSubstanceRiskAssessment.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'HazardousSubstanceRiskAssessment' AND TYPE = 'U')
BEGIN
	DROP TABLE [HazardousSubstanceRiskAssessment]
END

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'GeneralRiskAssessment' AND TYPE = 'U')
BEGIN
	DROP TABLE [GeneralRiskAssessment]
END

DELETE FROM changelog WHERE change_number = 159 AND delta_set = 'Main'
GO

--------------- Fragment ends: #159: 159_Create GeneralRiskAssessment HazardousSubstanceRiskAssessment.sql ---------------

--------------- Fragment begins: #158: 158_ALTER Various tables.sql ---------------

-- Change script: #158: 158_ALTER Various tables.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentDocument')
BEGIN
	EXEC sp_rename 'RiskAssessmentDocument', 'GeneralRiskAssessmentDocument'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentReview')
BEGIN
	EXEC sp_rename 'RiskAssessmentReview', 'GeneralRiskAssessmentReview'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentEmployee')
BEGIN
	EXEC sp_rename 'RiskAssessmentEmployee', 'GeneralRiskAssessmentEmployee'
END

DELETE FROM changelog WHERE change_number = 158 AND delta_set = 'Main'
GO

--------------- Fragment ends: #158: 158_ALTER Various tables.sql ---------------

--------------- Fragment begins: #157: 157_ALTER Rename GeneralRiskAssessment HazardousSubstance.sql ---------------

-- Change script: #157: 157_ALTER Rename GeneralRiskAssessment HazardousSubstance.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_HazardousSubstanceRiskAssessment')
BEGIN
	EXEC sp_rename 'PREVIOUS_HazardousSubstanceRiskAssessment', 'HazardousSubstanceRiskAssessment'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_GeneralRiskAssessment')
BEGIN
	EXEC sp_rename 'PREVIOUS_GeneralRiskAssessment', 'GeneralRiskAssessment'
END

DELETE FROM changelog WHERE change_number = 157 AND delta_set = 'Main'
GO

--------------- Fragment ends: #157: 157_ALTER Rename GeneralRiskAssessment HazardousSubstance.sql ---------------

--------------- Fragment begins: #156: 156_Create RiskAssessment_table.sql ---------------

-- Change script: #156: 156_Create RiskAssessment_table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RiskAssessment' AND TYPE = 'U')
BEGIN
	DROP TABLE [RiskAssessment]
END

DELETE FROM changelog WHERE change_number = 156 AND delta_set = 'Main'
GO

--------------- Fragment ends: #156: 156_Create RiskAssessment_table.sql ---------------

--------------- Fragment begins: #154: 154_ALTER Task to lengthen discriminator column.sql ---------------

-- Change script: #154: 154_ALTER Task to lengthen discriminator column.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'Discrimnator')
BEGIN
	ALTER TABLE [Task] ALTER COLUMN [Discriminator] VARCHAR(50) NOT NULL
END
GO

DELETE FROM changelog WHERE change_number = 154 AND delta_set = 'Main'
GO

--------------- Fragment ends: #154: 154_ALTER Task to lengthen discriminator column.sql ---------------

--------------- Fragment begins: #153: 153_CREATE ControlSystem.sql ---------------

-- Change script: #153: 153_CREATE ControlSystem.sql
IF EXISTS (SELECT * FROM [ControlSystem])
BEGIN
	DELETE FROM [ControlSystem]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ControlSystem')
BEGIN
	DROP TABLE [ControlSystem]
END
GO

DELETE FROM changelog WHERE change_number = 153 AND delta_set = 'Main'
GO

--------------- Fragment ends: #153: 153_CREATE ControlSystem.sql ---------------

--------------- Fragment begins: #151: 151_GRANT permissions on HazardousSubstancePictogram.sql ---------------

-- Change script: #151: 151_GRANT permissions on HazardousSubstancePictogram.sql

	DENY DELETE ON [HazardousSubstancePictogram] TO AllowAll
	DENY DELETE ON [HazardousSubstanceRiskPhrase] TO AllowAll
	DENY DELETE ON [HazardousSubstanceSafetyPhrase] TO AllowAll

DELETE FROM changelog WHERE change_number = 151 AND delta_set = 'Main'
GO

--------------- Fragment ends: #151: 151_GRANT permissions on HazardousSubstancePictogram.sql ---------------

--------------- Fragment begins: #150: 150_GRANT permissions on Pictogram.sql ---------------

-- Change script: #150: 150_GRANT permissions on Pictogram.sql


	DENY DELETE ON [Pictogram] TO AllowAll
	

DELETE FROM changelog WHERE change_number = 150 AND delta_set = 'Main'
GO

--------------- Fragment ends: #150: 150_GRANT permissions on Pictogram.sql ---------------

--------------- Fragment begins: #149: 149_UPDATE RiskPhrase Grups.sql ---------------

-- Change script: #149: 149_UPDATE RiskPhrase Grups.sql

UPDATE [RiskPhrase] 
SET [HazardousSubstanceGroupId] = NULL

DELETE FROM changelog WHERE change_number = 149 AND delta_set = 'Main'
GO

--------------- Fragment ends: #149: 149_UPDATE RiskPhrase Grups.sql ---------------

--------------- Fragment begins: #148: 148_UPDATE Task.sql ---------------

-- Change script: #148: 148_UPDATE Task.sql

UPDATE Task SET Discriminator = 'FurtherControlMeasureTask' WHERE Discriminator = 'GeneralRiskAssessmentFurtherControlMeasureTask'

DELETE FROM changelog WHERE change_number = 148 AND delta_set = 'Main'
GO

--------------- Fragment ends: #148: 148_UPDATE Task.sql ---------------

--------------- Fragment begins: #147: 147_ALTER HazardousSubstanceRiskAssessment.sql ---------------

-- Change script: #147: 147_ALTER HazardousSubstanceRiskAssessment.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'HealthSurveillanceRequired')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [HealthSurveillanceRequired] 
END
GO	


DELETE FROM changelog WHERE change_number = 147 AND delta_set = 'Main'
GO

--------------- Fragment ends: #147: 147_ALTER HazardousSubstanceRiskAssessment.sql ---------------

--------------- Fragment begins: #146: 146_ALTER Task to add HazardousSubstanceRiskAssessmentId.sql ---------------

-- Change script: #146: 146_ALTER Task to add HazardousSubstanceRiskAssessmentId.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'HazardousSubstanceRiskAssessmentId')
BEGIN
	ALTER TABLE [Task]
	DROP COLUMN [HazardousSubstanceRiskAssessmentId]
END
GO

DELETE FROM changelog WHERE change_number = 146 AND delta_set = 'Main'
GO

--------------- Fragment ends: #146: 146_ALTER Task to add HazardousSubstanceRiskAssessmentId.sql ---------------

--------------- Fragment begins: #145: 145_CREATE table HazardousSubstanceRiskAssessmentControlMeasures.sql ---------------

-- Change script: #145: 145_CREATE table HazardousSubstanceRiskAssessmentControlMeasures.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessmentControlMeasure')
BEGIN
	DROP TABLE [HazardousSubstanceRiskAssessmentControlMeasure]
END
GO

DELETE FROM changelog WHERE change_number = 145 AND delta_set = 'Main'
GO

--------------- Fragment ends: #145: 145_CREATE table HazardousSubstanceRiskAssessmentControlMeasures.sql ---------------

--------------- Fragment begins: #144: 144_ALTER HazardousSubstanceRiskAssessment.sql ---------------

-- Change script: #144: 144_ALTER HazardousSubstanceRiskAssessment.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'Quantity')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [Quantity]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'MatterState')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN  [MatterState]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'DustinessOrVolatility')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [DustinessOrVolatility]
END
GO


DELETE FROM changelog WHERE change_number = 144 AND delta_set = 'Main'
GO

--------------- Fragment ends: #144: 144_ALTER HazardousSubstanceRiskAssessment.sql ---------------

--------------- Fragment begins: #143: 143 RECREATE HazardousSubstance.sql ---------------

-- Change script: #143: 143 RECREATE HazardousSubstance.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'HazardousSubstance' AND TYPE = 'U')
BEGIN
	DROP TABLE [HazardousSubstance]
	
	CREATE TABLE [HazardousSubstance]
	(
		Id bigint IDENTITY(1,1) NOT NULL
		,[Name] varchar(200) NOT NULL 		
		,Reference varchar(50) NOT NULL
		,SupplierId bigint NULL
		,HazardousSubstanceStandardId bigint NOT NULL
		,SDSDate datetime NOT NULL
		,DetailsOfUse varchar(500) NOT NULL
		,AssessmentRequired bit NOT NULL
		,CompanyId bigint NOT NULL
		,Deleted bit NOT NULL DEFAULT 0
		,CreatedOn datetime NOT NULL DEFAULT GetDate()	
		,CreatedBy uniqueidentifier NULL
		,LastModifiedOn datetime NULL DEFAULT GetDate()
		,LastModifiedBy uniqueidentifier NULL
	)
END


GRANT SELECT, INSERT,DELETE, UPDATE ON [HazardousSubstance] TO [AllowAll]

GO

DELETE FROM changelog WHERE change_number = 143 AND delta_set = 'Main'
GO

--------------- Fragment ends: #143: 143 RECREATE HazardousSubstance.sql ---------------

--------------- Fragment begins: #142: 142_ALTER HazardousSubstanceRiskPhrase HazardousSubstanceSafetyPhrase add column HazardousSubstanceGroupId.sql ---------------

-- Change script: #142: 142_ALTER HazardousSubstanceRiskPhrase HazardousSubstanceSafetyPhrase add column HazardousSubstanceGroupId.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskPhrase' AND COLUMN_NAME = 'HazardousSubstanceGroupId')
BEGIN
	ALTER TABLE [RiskPhrase]
	DROP COLUMN [HazardousSubstanceGroupId]
END
GO


DELETE FROM changelog WHERE change_number = 142 AND delta_set = 'Main'
GO

--------------- Fragment ends: #142: 142_ALTER HazardousSubstanceRiskPhrase HazardousSubstanceSafetyPhrase add column HazardousSubstanceGroupId.sql ---------------

--------------- Fragment begins: #141: 141_INSERT INTO HazardousSubstanceGroup.sql ---------------

-- Change script: #141: 141_INSERT INTO HazardousSubstanceGroup.sql

DELETE FROM [dbo].[HazardousSubstanceGroup] WHERE [Id] < 6

DELETE FROM changelog WHERE change_number = 141 AND delta_set = 'Main'
GO

--------------- Fragment ends: #141: 141_INSERT INTO HazardousSubstanceGroup.sql ---------------

--------------- Fragment begins: #140: 140_CREATE table HazardousSubstanceGroup.sql ---------------

-- Change script: #140: 140_CREATE table HazardousSubstanceGroup.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'HazardousSubstanceGroup')
BEGIN
	DROP TABLE [HazardousSubstanceGroup]
END
GO

DELETE FROM changelog WHERE change_number = 140 AND delta_set = 'Main'
GO

--------------- Fragment ends: #140: 140_CREATE table HazardousSubstanceGroup.sql ---------------

--------------- Fragment begins: #139: 139_ALTER HSRA add Site.sql ---------------

-- Change script: #139: 139_ALTER HSRA add Site.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'SiteId')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [SiteId]
END
GO


DELETE FROM changelog WHERE change_number = 139 AND delta_set = 'Main'
GO

--------------- Fragment ends: #139: 139_ALTER HSRA add Site.sql ---------------

--------------- Fragment begins: #138: 138_ALTER rename RiskAssessment GeneralRiskAssessment.sql ---------------

-- Change script: #138: 138_ALTER rename RiskAssessment GeneralRiskAssessment.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessmentReview')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessmentReview', 'RiskAssessmentReview'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessmentPeopleAtRisk')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessmentPeopleAtRisk', 'RiskAssessmentPeopleAtRisk'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessmentEmployee')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessmentEmployee', 'RiskAssessmentEmployees'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessmentDocument')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessmentDocument', 'RiskAssessmentDocument'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessmentControlMeasure')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessmentControlMeasure', 'RiskAssessmentControlMeasures'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessmentHazard')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessmentHazard', 'RiskAssessmentHazards'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeneralRiskAssessment')
BEGIN
	EXEC sp_rename 'GeneralRiskAssessment', 'RiskAssessment'
END

DELETE FROM changelog WHERE change_number = 138 AND delta_set = 'Main'
GO

--------------- Fragment ends: #138: 138_ALTER rename RiskAssessment GeneralRiskAssessment.sql ---------------

--------------- Fragment begins: #137: 137_GRANT permissions on HazardousSubstancePictogram.sql ---------------

-- Change script: #137: 137_GRANT permissions on HazardousSubstancePictogram.sql

	DENY SELECT ON [HazardousSubstancePictogram] TO AllowAll
	DENY INSERT ON [HazardousSubstancePictogram] TO AllowAll
	DENY UPDATE ON [HazardousSubstancePictogram] TO AllowAll

DELETE FROM changelog WHERE change_number = 137 AND delta_set = 'Main'
GO

--------------- Fragment ends: #137: 137_GRANT permissions on HazardousSubstancePictogram.sql ---------------

--------------- Fragment begins: #136: 136_GRANT permissions on Pictogram.sql ---------------

-- Change script: #136: 136_GRANT permissions on Pictogram.sql


	DENY SELECT ON [Pictogram] TO AllowAll
	DENY INSERT ON [Pictogram] TO AllowAll
	DENY UPDATE ON [Pictogram] TO AllowAll

DELETE FROM changelog WHERE change_number = 136 AND delta_set = 'Main'
GO

--------------- Fragment ends: #136: 136_GRANT permissions on Pictogram.sql ---------------

--------------- Fragment begins: #135: 135_INSERT Pictogram.sql ---------------

-- Change script: #135: 135_INSERT Pictogram.sql

DELETE FROM [dbo].[Pictogram]

DELETE FROM changelog WHERE change_number = 135 AND delta_set = 'Main'
GO

--------------- Fragment ends: #135: 135_INSERT Pictogram.sql ---------------

--------------- Fragment begins: #134: 134_HazardousSubstancePictogram.sql ---------------

-- Change script: #134: 134_HazardousSubstancePictogram.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'HazardousSubstancePictogram' AND TYPE = 'U')
BEGIN
	DROP TABLE [HazardousSubstancePictogram]
END

DELETE FROM changelog WHERE change_number = 134 AND delta_set = 'Main'
GO

--------------- Fragment ends: #134: 134_HazardousSubstancePictogram.sql ---------------

--------------- Fragment begins: #133: 133_CREATE Pictogram.sql ---------------

-- Change script: #133: 133_CREATE Pictogram.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Pictogram' AND TYPE = 'U')
BEGIN
	DROP TABLE [Pictogram]
END

DELETE FROM changelog WHERE change_number = 133 AND delta_set = 'Main'
GO

--------------- Fragment ends: #133: 133_CREATE Pictogram.sql ---------------

--------------- Fragment begins: #132: 132_INSERT correct RiskPhrase and SafePhrase.sql ---------------

-- Change script: #132: 132_INSERT correct RiskPhrase and SafePhrase.sql

DELETE FROM [dbo].[RiskPhrase]
DELETE FROM [dbo].[SafetyPhrase]

DELETE FROM changelog WHERE change_number = 132 AND delta_set = 'Main'
GO

--------------- Fragment ends: #132: 132_INSERT correct RiskPhrase and SafePhrase.sql ---------------

--------------- Fragment begins: #131: 131_ALTER HazarousSubstanceSafetyPhrase HazardousSubstanceRiskPhrase Drop unneeded fields.sql ---------------

-- Change script: #131: 131_ALTER HazarousSubstanceSafetyPhrase HazardousSubstanceRiskPhrase Drop unneeded fields.sql
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' 
	AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE HazardousSubstanceSafetyPhrase
	ADD [CreatedBy] [uniqueidentifier] NOT NULL		
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' 
	AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
    ADD [CreatedOn] [datetime] NOT NULL	
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' 
	AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
    ADD [Deleted] [bit] NOT NULL	
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' 
	AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE HazardousSubstanceSafetyPhrase
	ADD [LastModifiedBy] [uniqueidentifier] NULL		
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase' 
	AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceSafetyPhrase]
    ADD [LastModifiedOn] [datetime] NULL	
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' 
	AND COLUMN_NAME = 'CreatedBy')
BEGIN
	ALTER TABLE HazardousSubstanceRiskPhrase
	ADD [CreatedBy] [uniqueidentifier] NOT NULL		
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' 
	AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
    ADD [CreatedOn] [datetime] NOT NULL	
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' 
	AND COLUMN_NAME = 'Deleted')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
    ADD [Deleted] [bit] NOT NULL	
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' 
	AND COLUMN_NAME = 'LastModifiedBy')
BEGIN
	ALTER TABLE HazardousSubstanceRiskPhrase
	ADD [LastModifiedBy] [uniqueidentifier] NULL		
END
GO	

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase' 
	AND COLUMN_NAME = 'LastModifiedOn')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskPhrase]
    ADD [LastModifiedOn] [datetime] NULL	
END
GO	

DELETE FROM changelog WHERE change_number = 131 AND delta_set = 'Main'
GO

--------------- Fragment ends: #131: 131_ALTER HazarousSubstanceSafetyPhrase HazardousSubstanceRiskPhrase Drop unneeded fields.sql ---------------

--------------- Fragment begins: #130: 130_ALTER SafetyPhrase to add HazardousSubstanceStandardId.sql ---------------

-- Change script: #130: 130_ALTER SafetyPhrase to add HazardousSubstanceStandardId.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafetyPhrase' AND COLUMN_NAME = 'HazardousSubstanceStandardId')
BEGIN
	ALTER TABLE [SafetyPhrase]
	DROP COLUMN [HazardousSubstanceStandardId]
END
GO


DELETE FROM changelog WHERE change_number = 130 AND delta_set = 'Main'
GO

--------------- Fragment ends: #130: 130_ALTER SafetyPhrase to add HazardousSubstanceStandardId.sql ---------------

--------------- Fragment begins: #129: 129_ALTER RiskPhrase to add HazardousSubstanceStandardId.sql ---------------

-- Change script: #129: 129_ALTER RiskPhrase to add HazardousSubstanceStandardId.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskPhrase' AND COLUMN_NAME = 'HazardousSubstanceStandardId')
BEGIN
	ALTER TABLE [RiskPhrase]
	DROP COLUMN [HazardousSubstanceStandardId]
END
GO


DELETE FROM changelog WHERE change_number = 129 AND delta_set = 'Main'
GO

--------------- Fragment ends: #129: 129_ALTER RiskPhrase to add HazardousSubstanceStandardId.sql ---------------

--------------- Fragment begins: #128: 128_ALTER HazardousSubstanceRiskAssessment.sql ---------------

-- Change script: #128: 128_ALTER HazardousSubstanceRiskAssessment.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'HazardousSubstanceId')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [HazardousSubstanceId]
END
GO


DELETE FROM changelog WHERE change_number = 128 AND delta_set = 'Main'
GO

--------------- Fragment ends: #128: 128_ALTER HazardousSubstanceRiskAssessment.sql ---------------

--------------- Fragment begins: #127: 127_CREATE Supplier.sql ---------------

-- Change script: #127: 127_CREATE Supplier.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Supplier' )
BEGIN
	DROP TABLE [Supplier]
END

DELETE FROM changelog WHERE change_number = 127 AND delta_set = 'Main'
GO

--------------- Fragment ends: #127: 127_CREATE Supplier.sql ---------------

--------------- Fragment begins: #126: 126_Create Hazardous Substance Risk Assessments Non Employees.sql ---------------

-- Change script: #126: 126_Create Hazardous Substance Risk Assessments Non Employees.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'HazardousSubstanceRiskAssessmentsNonEmployees' AND TYPE = 'U')
BEGIN
	DROP TABLE [HazardousSubstanceRiskAssessmentsNonEmployees]
END

DELETE FROM changelog WHERE change_number = 126 AND delta_set = 'Main'
GO

--------------- Fragment ends: #126: 126_Create Hazardous Substance Risk Assessments Non Employees.sql ---------------

--------------- Fragment begins: #125: 125_CREATE HazardousSubstanceRiskAssessmentEmployee Table.sql ---------------

-- Change script: #125: 125_CREATE HazardousSubstanceRiskAssessmentEmployee Table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'HazardousSubstanceRiskAssessmentEmployee' AND TYPE = 'U')
BEGIN
	DROP TABLE [HazardousSubstanceRiskAssessmentEmployee]
END

DELETE FROM changelog WHERE change_number = 125 AND delta_set = 'Main'
GO

--------------- Fragment ends: #125: 125_CREATE HazardousSubstanceRiskAssessmentEmployee Table.sql ---------------

--------------- Fragment begins: #124: 124_CREATE table HazardousSubstanceRiskPhrase.sql ---------------

-- Change script: #124: 124_CREATE table HazardousSubstanceRiskPhrase.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'HazardousSubstanceRiskPhrase')
BEGIN
	DROP TABLE [HazardousSubstanceRiskPhrase]
END
GO

DELETE FROM changelog WHERE change_number = 124 AND delta_set = 'Main'
GO

--------------- Fragment ends: #124: 124_CREATE table HazardousSubstanceRiskPhrase.sql ---------------

--------------- Fragment begins: #123: 123_CREATE table HazardousSubstanceSafetyPhrase.sql ---------------

-- Change script: #123: 123_CREATE table HazardousSubstanceSafetyPhrase.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'HazardousSubstanceSafetyPhrase')
BEGIN
	DROP TABLE [HazardousSubstanceSafetyPhrase]
END
GO

DELETE FROM changelog WHERE change_number = 123 AND delta_set = 'Main'
GO

--------------- Fragment ends: #123: 123_CREATE table HazardousSubstanceSafetyPhrase.sql ---------------

--------------- Fragment begins: #122: 122_CREATE table RiskPhrase.sql ---------------

-- Change script: #122: 122_CREATE table RiskPhrase.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskPhrase')
BEGIN
	DROP TABLE [RiskPhrase]
END
GO

DELETE FROM changelog WHERE change_number = 122 AND delta_set = 'Main'
GO

--------------- Fragment ends: #122: 122_CREATE table RiskPhrase.sql ---------------

--------------- Fragment begins: #121: 121_CREATE table SafetyPhrase.sql ---------------

-- Change script: #121: 121_CREATE table SafetyPhrase.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafetyPhrase')
BEGIN
	DROP TABLE [SafetyPhrase]
END
GO

DELETE FROM changelog WHERE change_number = 121 AND delta_set = 'Main'
GO

--------------- Fragment ends: #121: 121_CREATE table SafetyPhrase.sql ---------------

--------------- Fragment begins: #120: 120_ALTER HazardousSubstanceRiskAssessment.sql ---------------

-- Change script: #120: 120_ALTER HazardousSubstanceRiskAssessment.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'WorkspaceExposureLimits')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [WorkspaceExposureLimits]
END
GO


DELETE FROM changelog WHERE change_number = 120 AND delta_set = 'Main'
GO

--------------- Fragment ends: #120: 120_ALTER HazardousSubstanceRiskAssessment.sql ---------------

--------------- Fragment begins: #119: 119_ALTER HazardousSubstanceRiskAssessment.sql ---------------

-- Change script: #119: 119_ALTER HazardousSubstanceRiskAssessment.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'IsInhalationRouteOfEntry')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [IsInhalationRouteOfEntry]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'IsIngestionRouteOfEntry')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [IsIngestionRouteOfEntry]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'IsAbsorptionRouteOfEntry')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment]
	DROP COLUMN [IsAbsorptionRouteOfEntry]
END
GO

DELETE FROM changelog WHERE change_number = 119 AND delta_set = 'Main'
GO

--------------- Fragment ends: #119: 119_ALTER HazardousSubstanceRiskAssessment.sql ---------------

--------------- Fragment begins: #118: 118_ALTER HazardousSubstanceRiskAssessment Add RiskAssessmentStatusId.sql ---------------

-- Change script: #118: 118_ALTER HazardousSubstanceRiskAssessment Add RiskAssessmentStatusId.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HazardousSubstanceRiskAssessment' AND COLUMN_NAME = 'RiskAssessmentStatusId')
BEGIN
	ALTER TABLE [HazardousSubstanceRiskAssessment] DROP [RiskAssessmentStatusId] 
END
GO

DELETE FROM changelog WHERE change_number = 118 AND delta_set = 'Main'
GO

--------------- Fragment ends: #118: 118_ALTER HazardousSubstanceRiskAssessment Add RiskAssessmentStatusId.sql ---------------

--------------- Fragment begins: #117: 117_CREATE HazardousSubstance.sql ---------------

-- Change script: #117: 117_CREATE HazardousSubstance.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'HazardousSubstance' AND TYPE = 'U')
BEGIN
	DROP TABLE [HazardousSubstance]
END

DELETE FROM changelog WHERE change_number = 117 AND delta_set = 'Main'
GO

--------------- Fragment ends: #117: 117_CREATE HazardousSubstance.sql ---------------

--------------- Fragment begins: #116: 116_Create_HazardousSubstancesRiskAssessmentTable.sql ---------------

-- Change script: #116: 116_Create_HazardousSubstancesRiskAssessmentTable.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'HazardousSubstanceRiskAssessment' AND TYPE = 'U')
BEGIN
	DROP TABLE [HazardousSubstanceRiskAssessment]
END

DELETE FROM changelog WHERE change_number = 116 AND delta_set = 'Main'
GO

--------------- Fragment ends: #116: 116_Create_HazardousSubstancesRiskAssessmentTable.sql ---------------

--------------- Fragment begins: #115: 115_ INSERT INTO Permission.sql ---------------

-- Change script: #115: 115_ INSERT INTO Permission.sql
USE [BusinessSafe]
GO

--UPDATE [Permission] SET [Name] = 'ViewGeneralRiskAssessments' WHERE [PermissionId] = 16 
--UPDATE [Permission] SET [Name] = 'AddGeneralRiskAssessments' WHERE [PermissionId] = 17
--UPDATE [Permission] SET [Name] = 'EditGeneralRiskAssessments' WHERE [PermissionId] = 18 
--UPDATE [Permission] SET [Name] = 'DeleteGeneralRiskAssessments' WHERE [PermissionId] = 19
--UPDATE [Permission] SET [Name] = 'ViewGRAActions' WHERE [PermissionId] = 20
--UPDATE [Permission] SET [Name] = 'AddGRAActions' WHERE [PermissionId] = 21
--UPDATE [Permission] SET [Name] = 'EditGRAActions' WHERE [PermissionId] = 22
--UPDATE [Permission] SET [Name] = 'DeleteGRAActions' WHERE [PermissionId] = 23
--UPDATE [Permission] SET [Name] = 'ViewOwnGRAActions' WHERE [PermissionId] = 24
--UPDATE [Permission] SET [Name] = 'AddOwnGRAActions' WHERE [PermissionId] = 25
--UPDATE [Permission] SET [Name] = 'EditOwnGRAActions' WHERE [PermissionId] = 26
--UPDATE [Permission] SET [Name] = 'DeleteOwnGRAActions' WHERE [PermissionId] = 27
--UPDATE [Permission] SET [Name] = 'UploadDocuments' WHERE [PermissionId] = 37
--UPDATE [Permission] SET [Name] = 'DeleteDocuments' WHERE [PermissionId] = 38
--UPDATE [Permission] SET [Name] = 'DownloadDocuments' WHERE [PermissionId] = 39

IF EXISTS(SELECT * FROM [Permission] WHERE [PermissionId] > 39)
BEGIN
	DELETE FROM [Permission] WHERE [PermissionId] > 39
END

IF EXISTS(SELECT * FROM [PermissionGroup] WHERE [PermissionGroupId] > 8)
BEGIN
	DELETE FROM [PermissionGroup] WHERE [PermissionGroupId] > 8
END

IF EXISTS(SELECT * FROM [PermissionGroupsPermissions] WHERE [PermissionId] > 36)
BEGIN
	DELETE FROM [PermissionGroupsPermissions] WHERE [PermissionId] > 36
END

DELETE FROM changelog WHERE change_number = 115 AND delta_set = 'Main'
GO

--------------- Fragment ends: #115: 115_ INSERT INTO Permission.sql ---------------

--------------- Fragment begins: #114: 114_ALTER remove mapping of ignored client documentation doc types.sql ---------------

-- Change script: #114: 114_ALTER remove mapping of ignored client documentation doc types.sql
USE [BusinessSafe]
GO

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType] ([Id], [DocumentGroupId])
SELECT	143, 1
UNION ALL
SELECT	144, 1
GO

DELETE FROM changelog WHERE change_number = 114 AND delta_set = 'Main'
GO

--------------- Fragment ends: #114: 114_ALTER remove mapping of ignored client documentation doc types.sql ---------------

--------------- Fragment begins: #113: 113_ INSERT INTO DocHandlerDocumentType.sql ---------------

-- Change script: #113: 113_ INSERT INTO DocHandlerDocumentType.sql
USE [BusinessSafe]
GO

DELETE FROM [DocHandlerDocumentType]
GO

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 125)

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 131)

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 132)

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 127)

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 128)

-- Reference Library
INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 124)

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 126)

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 129)

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 130)

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 133)
GO

DELETE FROM changelog WHERE change_number = 113 AND delta_set = 'Main'
GO

--------------- Fragment ends: #113: 113_ INSERT INTO DocHandlerDocumentType.sql ---------------

--------------- Fragment begins: #112: 112_ALTER rename clientdocumenttype and clientdocumenttype to use prefix dochandler.sql ---------------

-- Change script: #112: 112_ALTER rename clientdocumenttype and clientdocumenttype to use prefix dochandler.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DocHandlerDocumentType')
BEGIN
	EXEC sp_rename 'DocHandlerDocumentType', 'ClientDocumentType'
END

DELETE FROM changelog WHERE change_number = 112 AND delta_set = 'Main'
GO

--------------- Fragment ends: #112: 112_ALTER rename clientdocumenttype and clientdocumenttype to use prefix dochandler.sql ---------------

--------------- Fragment begins: #111: 111_INSERT correct dochandler document type to group associations.sql ---------------

-- Change script: #111: 111_INSERT correct dochandler document type to group associations.sql
USE [BusinessSafe]
GO

DELETE FROM [ClientDocumentType]
GO

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 125)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 131)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 132)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 127)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 128)

-- Reference Library
INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 124)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 126)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 129)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 130)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 133)
GO

DELETE FROM changelog WHERE change_number = 111 AND delta_set = 'Main'
GO

--------------- Fragment ends: #111: 111_INSERT correct dochandler document type to group associations.sql ---------------

--------------- Fragment begins: #110: 110_ INSERT INTO ClientDocumentType.sql ---------------

-- Change script: #110: 110_ INSERT INTO ClientDocumentType.sql
USE [BusinessSafe]
GO

DELETE FROM [ClientDocumentType]
GO

DELETE FROM changelog WHERE change_number = 110 AND delta_set = 'Main'
GO

--------------- Fragment ends: #110: 110_ INSERT INTO ClientDocumentType.sql ---------------

--------------- Fragment begins: #109: 109_CREATE ClientDocumentType table.sql ---------------

-- Change script: #109: 109_CREATE ClientDocumentType table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'ClientDocumentType' AND TYPE = 'U')
BEGIN
	DROP TABLE [ClientDocumentType]
END

DELETE FROM changelog WHERE change_number = 109 AND delta_set = 'Main'
GO

--------------- Fragment ends: #109: 109_CREATE ClientDocumentType table.sql ---------------

--------------- Fragment begins: #108: 108_ALTER AddedDocument to remove ClientId.sql ---------------

-- Change script: #108: 108_ALTER AddedDocument to remove ClientId.sql

-- add client id null
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AddedDocument' AND COLUMN_NAME = 'ClientId')
BEGIN
	ALTER TABLE [AddedDocument] ADD [ClientId] bigint null
END
GO

-- copy table data back
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AddedDocument' AND COLUMN_NAME = 'ClientId')
BEGIN
	DECLARE @Id as bigint,
			@ClientId as bigint

	DECLARE curDoc CURSOR FOR
		SELECT [Id],
			   [ClientId]
		FROM [PREVIOUS_AddedDocument_With_ClientId]
		
	OPEN curDoc
	FETCH NEXT FROM curDoc INTO @Id, @ClientId

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		UPDATE [AddedDocument] 
		SET ClientId = @ClientId
		WHERE Id = @Id
			    
		FETCH NEXT FROM curDoc INTO @Id, @ClientId

	END

	CLOSE curDoc
	DEALLOCATE curDoc
END
GO

-- set client id not null
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AddedDocument' AND COLUMN_NAME = 'ClientId')
BEGIN
	ALTER TABLE [AddedDocument] ALTER COLUMN [ClientId] bigint not null
END
GO

-- remove backup table
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_AddedDocument_With_ClientId')
BEGIN
	DROP TABLE [dbo].[PREVIOUS_AddedDocument_With_ClientId]
END
GO

DELETE FROM changelog WHERE change_number = 108 AND delta_set = 'Main'
GO

--------------- Fragment ends: #108: 108_ALTER AddedDocument to remove ClientId.sql ---------------

--------------- Fragment begins: #107: 107_ALTER Document to add ClientId.sql ---------------

-- Change script: #107: 107_ALTER Document to add ClientId.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Document' AND COLUMN_NAME = 'ClientId')
BEGIN
	ALTER TABLE [Document] DROP COLUMN [ClientId]
END

DELETE FROM changelog WHERE change_number = 107 AND delta_set = 'Main'
GO

--------------- Fragment ends: #107: 107_ALTER Document to add ClientId.sql ---------------

--------------- Fragment begins: #106: 106_ALTER AddedDocument Table Add Field ClientId.sql ---------------

-- Change script: #106: 106_ALTER AddedDocument Table Add Field ClientId.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AddedDocument' AND COLUMN_NAME = 'ClientId')
BEGIN
	ALTER TABLE [AddedDocument]
	DROP COLUMN [ClientId]
END
GO

DELETE FROM changelog WHERE change_number = 106 AND delta_set = 'Main'
GO

--------------- Fragment ends: #106: 106_ALTER AddedDocument Table Add Field ClientId.sql ---------------

--------------- Fragment begins: #105: 105_ALTER AddedDocument to inherit fields.sql ---------------

-- Change script: #105: 105_ALTER AddedDocument to inherit fields.sql
-------------------------------------------------------------------------------------------------------------

DELETE FROM Document WHERE Id IN (
	SELECT Id FROM AddedDocument
)

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AddedDocument')
BEGIN
	DROP TABLE [AddedDocument] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_AddedDocument')
BEGIN
	EXEC sp_rename 'PREVIOUS_AddedDocument', 'AddedDocument'
END

DELETE FROM changelog WHERE change_number = 105 AND delta_set = 'Main'
GO

--------------- Fragment ends: #105: 105_ALTER AddedDocument to inherit fields.sql ---------------

--------------- Fragment begins: #104: 104_CREATE tables Keyword DocumentKeyword.sql ---------------

-- Change script: #104: 104_CREATE tables Keyword DocumentKeyword.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Keyword')
BEGIN
	DROP TABLE [Keyword]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DocumentKeyword')
BEGIN
	DROP TABLE [DocumentKeyword]
END
GO

DELETE FROM changelog WHERE change_number = 104 AND delta_set = 'Main'
GO

--------------- Fragment ends: #104: 104_CREATE tables Keyword DocumentKeyword.sql ---------------

--------------- Fragment begins: #103: 103_ALTER AddedDocument to remove inherited fields.sql ---------------

-- Change script: #103: 103_ALTER AddedDocument to remove inherited fields.sql

DELETE FROM changelog WHERE change_number = 103 AND delta_set = 'Main'
GO

--------------- Fragment ends: #103: 103_ALTER AddedDocument to remove inherited fields.sql ---------------

--------------- Fragment begins: #102: 102_ALTER Document add column Title.sql ---------------

-- Change script: #102: 102_ALTER Document add column Title.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Document' AND COLUMN_NAME = 'Title')
BEGIN
	ALTER TABLE [Document]
	DROP COLUMN [Title]
END
GO

DELETE FROM changelog WHERE change_number = 102 AND delta_set = 'Main'
GO

--------------- Fragment ends: #102: 102_ALTER Document add column Title.sql ---------------

--------------- Fragment begins: #101: 101_ALTER TaskDocument Rename FurtherControlMeasureId TaskIs.sql ---------------

-- Change script: #101: 101_ALTER TaskDocument Rename FurtherControlMeasureId TaskIs.sql
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TaskDocument' AND COLUMN_NAME = 'FurtherControlMeasureId')
BEGIN
	EXEC sp_rename
    @objname = 'Task.TaskId',
    @newname = 'FurtherControlMeasureId',
    @objtype = 'COLUMN'
END
GO

DELETE FROM changelog WHERE change_number = 101 AND delta_set = 'Main'
GO

--------------- Fragment ends: #101: 101_ALTER TaskDocument Rename FurtherControlMeasureId TaskIs.sql ---------------

--------------- Fragment begins: #100: 100_ALTER Rename FurtherControlMeasureDocument TaskDocument.sql ---------------

-- Change script: #100: 100_ALTER Rename FurtherControlMeasureDocument TaskDocument.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TaskDocument')
BEGIN
	EXEC sp_rename 'TaskDocument', 'FurtherControlMeasureDocument'
END

DELETE FROM changelog WHERE change_number = 100 AND delta_set = 'Main'
GO

--------------- Fragment ends: #100: 100_ALTER Rename FurtherControlMeasureDocument TaskDocument.sql ---------------

--------------- Fragment begins: #99: 99_INSERT INTO Document FurtherControlMeasureDocument and RiskAssessment from PREVIOUS_RiskAssessmentDocument.sql ---------------

-- Change script: #99: 99_INSERT INTO Document FurtherControlMeasureDocument and RiskAssessment from PREVIOUS_RiskAssessmentDocument.sql

DELETE FROM FurtherControlMeasureDocument
DELETE FROM RiskAssessmentDocument
DELETE FROM Document

DELETE FROM changelog WHERE change_number = 99 AND delta_set = 'Main'
GO

--------------- Fragment ends: #99: 99_INSERT INTO Document FurtherControlMeasureDocument and RiskAssessment from PREVIOUS_RiskAssessmentDocument.sql ---------------

--------------- Fragment begins: #98: 98_CREATE tables RiskAssessmentDocument FurtherControlMeasureTaskDocument.sql ---------------

-- Change script: #98: 98_CREATE tables RiskAssessmentDocument FurtherControlMeasureTaskDocument.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RiskAssessmentDocument')
BEGIN
	DROP TABLE [RiskAssessmentDocument]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FurtherControlMeasureDocument')
BEGIN
	DROP TABLE [FurtherControlMeasureDocument]
END
GO

DELETE FROM changelog WHERE change_number = 98 AND delta_set = 'Main'
GO

--------------- Fragment ends: #98: 98_CREATE tables RiskAssessmentDocument FurtherControlMeasureTaskDocument.sql ---------------

--------------- Fragment begins: #97: 97_ALTER Rename RiskAssessmentDocument FurtherControlMeasureTaskDocument.sql ---------------

-- Change script: #97: 97_ALTER Rename RiskAssessmentDocument FurtherControlMeasureTaskDocument.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_RiskAssessmentDocument')
BEGIN
	EXEC sp_rename 'PREVIOUS_RiskAssessmentDocument', 'RiskAssessmentDocument'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_FurtherControlMeasureDocument')
BEGIN
	EXEC sp_rename 'PREVIOUS_FurtherControlMeasureDocument', 'FurtherControlMeasureDocument'
END

DELETE FROM changelog WHERE change_number = 97 AND delta_set = 'Main'
GO

--------------- Fragment ends: #97: 97_ALTER Rename RiskAssessmentDocument FurtherControlMeasureTaskDocument.sql ---------------

--------------- Fragment begins: #96: 96_CREATE Document Table.sql ---------------

-- Change script: #96: 96_CREATE Document Table.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Document')
BEGIN
	DROP TABLE [Document]
END

DELETE FROM changelog WHERE change_number = 96 AND delta_set = 'Main'
GO

--------------- Fragment ends: #96: 96_CREATE Document Table.sql ---------------

--------------- Fragment begins: #95: 95_Alter Document Type Add More Document Types.sql ---------------

-- Change script: #95: 95_Alter Document Type Add More Document Types.sql
USE [BusinessSafe]
GO

DELETE FROM [DocumentType] WHERE Id in (3, 4, 5, 6, 7, 8)
Go

DELETE FROM changelog WHERE change_number = 95 AND delta_set = 'Main'
GO

--------------- Fragment ends: #95: 95_Alter Document Type Add More Document Types.sql ---------------

--------------- Fragment begins: #94: 94_CREATE AddedDocument Table.sql ---------------

-- Change script: #94: 94_CREATE AddedDocument Table.sql
IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'AddedDocument' AND TYPE = 'U')
BEGIN
	DROP TABLE [AddedDocument]
END

DELETE FROM changelog WHERE change_number = 94 AND delta_set = 'Main'
GO

--------------- Fragment ends: #94: 94_CREATE AddedDocument Table.sql ---------------

--------------- Fragment begins: #93: 93_ALTER Task Rename FurtherControlMeasureTaskCategoryId Column.sql ---------------

-- Change script: #93: 93_ALTER Task Rename FurtherControlMeasureTaskCategoryId Column.sql
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'FurtherControlMeasureTaskCategoryId')
BEGIN
	EXEC sp_rename
    @objname = 'Task.TaskCategoryId',
    @newname = 'FurtherControlMeasureTaskCategoryId',
    @objtype = 'COLUMN'
END
GO

DELETE FROM changelog WHERE change_number = 93 AND delta_set = 'Main'
GO

--------------- Fragment ends: #93: 93_ALTER Task Rename FurtherControlMeasureTaskCategoryId Column.sql ---------------

--------------- Fragment begins: #92: 92_ALTER RiskAssessment Table Task Rename Columns.sql ---------------

-- Change script: #92: 92_ALTER RiskAssessment Table Task Rename Columns.sql
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'FollowingFurtherControlMeasureTaskId')
BEGIN
	EXEC sp_rename
    @objname = 'Task.FollowingTaskId',
    @newname = 'FollowingFurtherControlMeasureTaskId',
    @objtype = 'COLUMN'
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'OriginalFurtherControlMeasureTaskId')
BEGIN
	EXEC sp_rename
    @objname = 'Task.OriginalTaskId',
    @newname = 'OriginalFurtherControlMeasureTaskId',
    @objtype = 'COLUMN'
END
GO

DELETE FROM changelog WHERE change_number = 92 AND delta_set = 'Main'
GO

--------------- Fragment ends: #92: 92_ALTER RiskAssessment Table Task Rename Columns.sql ---------------

--------------- Fragment begins: #91: 91_ALTER RiskAssessment Table Add Field RiskAssessmentStatusId.sql ---------------

-- Change script: #91: 91_ALTER RiskAssessment Table Add Field RiskAssessmentStatusId.sql
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessment' AND COLUMN_NAME = 'IsDraft')
BEGIN
	ALTER TABLE [RiskAssessment]
	ADD [IsDraft] BIT NULL
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessment' AND COLUMN_NAME = 'RiskAssessmentStatusId')
BEGIN
	UPDATE
		[RiskAssessment]
	SET
		[IsDraft] = CASE [RiskAssessmentStatusId] WHEN 0 THEN 1 ELSE 0 END

	ALTER TABLE [RiskAssessment]
	DROP COLUMN [RiskAssessmentStatusId]
END
GO

DELETE FROM changelog WHERE change_number = 91 AND delta_set = 'Main'
GO

--------------- Fragment ends: #91: 91_ALTER RiskAssessment Table Add Field RiskAssessmentStatusId.sql ---------------

--------------- Fragment begins: #90: 90_Delete_redundant_data_from_DocumentType.sql ---------------

-- Change script: #90: 90_Delete_redundant_data_from_DocumentType.sql
IF NOT EXISTS (SELECT * FROM [DocumentType] WHERE [Name] = 'Document Type3')
BEGIN
	SET IDENTITY_INSERT [DocumentType] ON;
	Go

	INSERT INTO [BusinessSafe].[dbo].[DocumentType]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (3
           ,'Document Type3'
           ,0           
           ,getdate()
           ,null)
     GO

	SET IDENTITY_INSERT [DocumentType] OFF;
	GO
END
GO

DELETE FROM changelog WHERE change_number = 90 AND delta_set = 'Main'
GO

--------------- Fragment ends: #90: 90_Delete_redundant_data_from_DocumentType.sql ---------------

--------------- Fragment begins: #89: 89_ALTER Task set RiskAssessmaentHazardId nullable.sql ---------------

-- Change script: #89: 89_ALTER Task set RiskAssessmaentHazardId nullable.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'RiskAssessmentHazardId' AND IS_NULLABLE = 'NO')
BEGIN
	ALTER TABLE [Task]
	ALTER COLUMN [RiskAssessmentHazardId] [bigint] NOT NULL
END
GO

DELETE FROM changelog WHERE change_number = 89 AND delta_set = 'Main'
GO

--------------- Fragment ends: #89: 89_ALTER Task set RiskAssessmaentHazardId nullable.sql ---------------

--------------- Fragment begins: #88: 88_ALTER Task add column RiskAssessmentReviewId.sql ---------------

-- Change script: #88: 88_ALTER Task add column RiskAssessmentReviewId.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Task' AND COLUMN_NAME = 'RiskAssessmentReviewId')
BEGIN
	ALTER TABLE [Task]
	DROP COLUMN [RiskAssessmentReviewId]
END
GO

DELETE FROM changelog WHERE change_number = 88 AND delta_set = 'Main'
GO

--------------- Fragment ends: #88: 88_ALTER Task add column RiskAssessmentReviewId.sql ---------------

--------------- Fragment begins: #87: 87_ALTER Rename FurtherControlMeasureTask to Task.sql ---------------

-- Change script: #87: 87_ALTER Rename FurtherControlMeasureTask to Task.sql

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FurtherControlMeasureTask')
BEGIN
	EXEC sp_rename 'Task', 'FurtherControlMeasureTask'
END

DELETE FROM changelog WHERE change_number = 87 AND delta_set = 'Main'
GO

--------------- Fragment ends: #87: 87_ALTER Rename FurtherControlMeasureTask to Task.sql ---------------

--------------- Fragment begins: #86: 86_ALTER RiskAssessmentReview add completion columns and rename description.sql ---------------

-- Change script: #86: 86_ALTER RiskAssessmentReview add completion columns and rename description.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentReview' AND COLUMN_NAME = 'CompletedById')
BEGIN
	ALTER TABLE [RiskAssessmentReview]
	DROP COLUMN [CompletedById]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentReview' AND COLUMN_NAME = 'CompletedDate')
BEGIN
	ALTER TABLE [RiskAssessmentReview]
	DROP COLUMN [CompletedDate]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentReview' AND COLUMN_NAME = 'Comments')
BEGIN
	EXEC SP_RENAME 'RiskAssessmentReview.Comments', 'Description', 'COLUMN'
END
GO	

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentReview' AND COLUMN_NAME = 'Description')
BEGIN
	ALTER TABLE RiskAssessmentReview
	ALTER COLUMN [Description] NVARCHAR(150) NULL

END
GO	

DELETE FROM changelog WHERE change_number = 86 AND delta_set = 'Main'
GO

--------------- Fragment ends: #86: 86_ALTER RiskAssessmentReview add completion columns and rename description.sql ---------------

--------------- Fragment begins: #85: 85_CREATE RiskAssessmentReview.sql ---------------

-- Change script: #85: 85_CREATE RiskAssessmentReview.sql

DELETE FROM changelog WHERE change_number = 85 AND delta_set = 'Main'
GO

--------------- Fragment ends: #85: 85_CREATE RiskAssessmentReview.sql ---------------

--------------- Fragment begins: #84: 84_ALTER FurtherControlMeasureTask Add New Fields.sql ---------------

-- Change script: #84: 84_ALTER FurtherControlMeasureTask Add New Fields.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FurtherControlMeasureTask' AND COLUMN_NAME = 'Discriminator')
BEGIN
	ALTER TABLE [FurtherControlMeasureTask]
	DROP COLUMN [Discriminator]
END
GO

DELETE FROM changelog WHERE change_number = 84 AND delta_set = 'Main'
GO

--------------- Fragment ends: #84: 84_ALTER FurtherControlMeasureTask Add New Fields.sql ---------------

--------------- Fragment begins: #83: 83_ALTER Rename ArchiveRiskAssesmentFurtherControlMeasureTask to ArchiveFurtherControlMeasureTask.sql ---------------

-- Change script: #83: 83_ALTER Rename ArchiveRiskAssesmentFurtherControlMeasureTask to ArchiveFurtherControlMeasureTask.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ArchivedFurtherControlMeasureTask')
BEGIN
	EXEC sp_rename 'ArchivedFurtherControlMeasureTask', 'ArchiveRiskAssessmentFurtherControlMeasureTask'
END

DELETE FROM changelog WHERE change_number = 83 AND delta_set = 'Main'
GO

--------------- Fragment ends: #83: 83_ALTER Rename ArchiveRiskAssesmentFurtherControlMeasureTask to ArchiveFurtherControlMeasureTask.sql ---------------

--------------- Fragment begins: #82: 82_ALTER Rename RiskAssesmentFurtherControlMeasureTask to FurtherControlMeasureTask.sql ---------------

-- Change script: #82: 82_ALTER Rename RiskAssesmentFurtherControlMeasureTask to FurtherControlMeasureTask.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FurtherControlMeasureTask')
BEGIN
	EXEC sp_rename 'FurtherControlMeasureTask', 'RiskAssessmentFurtherControlMeasureTask'
END

DELETE FROM changelog WHERE change_number = 82 AND delta_set = 'Main'
GO

--------------- Fragment ends: #82: 82_ALTER Rename RiskAssesmentFurtherControlMeasureTask to FurtherControlMeasureTask.sql ---------------

--------------- Fragment begins: #81: 81_ALTER FurtherControlMeasureDocument Table Add Field DocumentOriginTypeId.sql ---------------

-- Change script: #81: 81_ALTER FurtherControlMeasureDocument Table Add Field DocumentOriginTypeId.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FurtherControlMeasureDocument' AND COLUMN_NAME = 'DocumentOriginTypeId')
BEGIN
	ALTER TABLE [FurtherControlMeasureDocument]
	DROP COLUMN [DocumentOriginTypeId]
END
GO

DELETE FROM changelog WHERE change_number = 81 AND delta_set = 'Main'
GO

--------------- Fragment ends: #81: 81_ALTER FurtherControlMeasureDocument Table Add Field DocumentOriginTypeId.sql ---------------

--------------- Fragment begins: #80: 80_ALTER RiskAssessmentFurtherActionTasks add column UniqueReference.sql ---------------

-- Change script: #80: 80_ALTER RiskAssessmentFurtherActionTasks add column UniqueReference.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherControlMeasureTask' AND COLUMN_NAME = 'OriginalFurtherControlMeasureTaskId')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherControlMeasureTask]
	DROP COLUMN [OriginalFurtherControlMeasureTaskId]
END
GO

DELETE FROM changelog WHERE change_number = 80 AND delta_set = 'Main'
GO

--------------- Fragment ends: #80: 80_ALTER RiskAssessmentFurtherActionTasks add column UniqueReference.sql ---------------

--------------- Fragment begins: #79: 79_ALTER RiskAssessmentFurtherActionTasks add column FollowingFurtherControlMeasureTaskId.sql ---------------

-- Change script: #79: 79_ALTER RiskAssessmentFurtherActionTasks add column FollowingFurtherControlMeasureTaskId.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherControlMeasureTask' AND COLUMN_NAME = 'FollowingFurtherControlMeasureTaskId')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherControlMeasureTask]
	DROP COLUMN [FollowingFurtherControlMeasureTaskId]
END
GO

DELETE FROM changelog WHERE change_number = 79 AND delta_set = 'Main'
GO

--------------- Fragment ends: #79: 79_ALTER RiskAssessmentFurtherActionTasks add column FollowingFurtherControlMeasureTaskId.sql ---------------

--------------- Fragment begins: #78: 78_Alter Document Type DataEntries.sql ---------------

-- Change script: #78: 78_Alter Document Type DataEntries.sql

UPDATE [BusinessSafe].[dbo].[DocumentType] SET [Name] ='Document Type 2' WHERE ID = 2
GO

DELETE FROM changelog WHERE change_number = 78 AND delta_set = 'Main'
GO

--------------- Fragment ends: #78: 78_Alter Document Type DataEntries.sql ---------------

--------------- Fragment begins: #77: 77_Create Risk Assessments Document.sql ---------------

-- Change script: #77: 77_Create Risk Assessments Document.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RiskAssessmentDocument' AND TYPE = 'U')
BEGIN
	DROP TABLE [RiskAssessmentDocument]
END

DELETE FROM changelog WHERE change_number = 77 AND delta_set = 'Main'
GO

--------------- Fragment ends: #77: 77_Create Risk Assessments Document.sql ---------------

--------------- Fragment begins: #76: 76_INSERT INTO RolesPermissions.sql ---------------

-- Change script: #76: 76_INSERT INTO RolesPermissions.sql
USE [BusinessSafe]
GO

DELETE FROM [RolesPermissions] WHERE [RoleId] = N'952eecb7-2b96-4399-82ae-7e2341d25e51' AND [PermissionId] = 37
DELETE FROM [RolesPermissions] WHERE [RoleId] = N'1e382767-93dd-47e2-88f2-b3e7f7648642' AND [PermissionId] = 37
DELETE FROM [RolesPermissions] WHERE [RoleId] = N'bacf7c01-d210-4dbc-942f-15d8456d3b92' AND [PermissionId] = 37
DELETE FROM [RolesPermissions] WHERE [RoleId] = N'952eecb7-2b96-4399-82ae-7e2341d25e51' AND [PermissionId] = 38
DELETE FROM [RolesPermissions] WHERE [RoleId] = N'1e382767-93dd-47e2-88f2-b3e7f7648642' AND [PermissionId] = 38
DELETE FROM [RolesPermissions] WHERE [RoleId] = N'bacf7c01-d210-4dbc-942f-15d8456d3b92' AND [PermissionId] = 38
DELETE FROM [RolesPermissions] WHERE [RoleId] = N'952eecb7-2b96-4399-82ae-7e2341d25e51' AND [PermissionId] = 39
DELETE FROM [RolesPermissions] WHERE [RoleId] = N'1e382767-93dd-47e2-88f2-b3e7f7648642' AND [PermissionId] = 39
DELETE FROM [RolesPermissions] WHERE [RoleId] = N'bacf7c01-d210-4dbc-942f-15d8456d3b92' AND [PermissionId] = 39

DELETE FROM changelog WHERE change_number = 76 AND delta_set = 'Main'
GO

--------------- Fragment ends: #76: 76_INSERT INTO RolesPermissions.sql ---------------

--------------- Fragment begins: #75: 75_INSERT INTO Permission.sql ---------------

-- Change script: #75: 75_INSERT INTO Permission.sql
USE [BusinessSafe]
GO

DELETE FROM [BusinessSafe].[dbo].[Permission]
WHERE [PermissionId] >= 37 AND [PermissionId] <= 39

DELETE FROM changelog WHERE change_number = 75 AND delta_set = 'Main'
GO

--------------- Fragment ends: #75: 75_INSERT INTO Permission.sql ---------------

--------------- Fragment begins: #74: 74_ALTER ArchivedRiskAssessmentFurtherControlMeasure Table Add New Fields.sql ---------------

-- Change script: #74: 74_ALTER ArchivedRiskAssessmentFurtherControlMeasure Table Add New Fields.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ArchiveRiskAssessmentFurtherControlMeasureTask' AND COLUMN_NAME = 'FurtherControlMeasureTaskCategoryId')
BEGIN
	ALTER TABLE [ArchiveRiskAssessmentFurtherControlMeasureTask]
	DROP COLUMN [FurtherControlMeasureTaskCategoryId]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ArchiveRiskAssessmentFurtherControlMeasureTask' AND COLUMN_NAME = 'TaskReoccurringTypeId')
BEGIN
	ALTER TABLE [ArchiveRiskAssessmentFurtherControlMeasureTask]
	DROP COLUMN [TaskReoccurringTypeId]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ArchiveRiskAssessmentFurtherControlMeasureTask' AND COLUMN_NAME = 'TaskReoccurringEndDate')
BEGIN
	ALTER TABLE [ArchiveRiskAssessmentFurtherControlMeasureTask]
	DROP COLUMN [TaskReoccurringEndDate]
END
GO

DELETE FROM changelog WHERE change_number = 74 AND delta_set = 'Main'
GO

--------------- Fragment ends: #74: 74_ALTER ArchivedRiskAssessmentFurtherControlMeasure Table Add New Fields.sql ---------------

--------------- Fragment begins: #73: 73_ALTER RiskAssessmentFurtherActionTasks Table Rename Table RiskAssessmentFurtherControlMeasures.sql ---------------

-- Change script: #73: 73_ALTER RiskAssessmentFurtherActionTasks Table Rename Table RiskAssessmentFurtherControlMeasures.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherControlMeasureTask')
BEGIN
	EXEC SP_RENAME 'RiskAssessmentFurtherControlMeasureTask', 'RiskAssessmentFurtherActionTasks'
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ArchiveRiskAssessmentFurtherControlMeasureTask')
BEGIN
	EXEC SP_RENAME 'ArchiveRiskAssessmentFurtherControlMeasureTask', 'ArchiveRiskAssessmentFurtherActionTasks'
END
GO

DELETE FROM changelog WHERE change_number = 73 AND delta_set = 'Main'
GO

--------------- Fragment ends: #73: 73_ALTER RiskAssessmentFurtherActionTasks Table Rename Table RiskAssessmentFurtherControlMeasures.sql ---------------

--------------- Fragment begins: #72: 72_ALTER RiskAssessmentFurtherActionTasks Table Add Field TaskReoccurringEndDate.sql ---------------

-- Change script: #72: 72_ALTER RiskAssessmentFurtherActionTasks Table Add Field TaskReoccurringEndDate.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'TaskReoccurringEndDate')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	DROP COLUMN [TaskReoccurringEndDate]
END
GO

DELETE FROM changelog WHERE change_number = 72 AND delta_set = 'Main'
GO

--------------- Fragment ends: #72: 72_ALTER RiskAssessmentFurtherActionTasks Table Add Field TaskReoccurringEndDate.sql ---------------

--------------- Fragment begins: #71: 71_ALTER RiskAssessmentFurtherActionTasks Table Add Field TaskReoccurringTypeId.sql ---------------

-- Change script: #71: 71_ALTER RiskAssessmentFurtherActionTasks Table Add Field TaskReoccurringTypeId.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'TaskReoccurringTypeId')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	DROP COLUMN [TaskReoccurringTypeId]
END
GO

DELETE FROM changelog WHERE change_number = 71 AND delta_set = 'Main'
GO

--------------- Fragment ends: #71: 71_ALTER RiskAssessmentFurtherActionTasks Table Add Field TaskReoccurringTypeId.sql ---------------

--------------- Fragment begins: #70: 70_ALTER RiskAssessment Table Add IsDraft field.sql ---------------

-- Change script: #70: 70_ALTER RiskAssessment Table Add IsDraft field.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessment' AND COLUMN_NAME = 'IsDraft')
BEGIN
	ALTER TABLE [RiskAssessment]
	DROP COLUMN [IsDraft]
END
GO

DELETE FROM changelog WHERE change_number = 70 AND delta_set = 'Main'
GO

--------------- Fragment ends: #70: 70_ALTER RiskAssessment Table Add IsDraft field.sql ---------------

--------------- Fragment begins: #69: 69_DROP Responsibility Tasks table.sql ---------------

-- Change script: #69: 69_DROP Responsibility Tasks table.sql

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'ResponsibilityTask' AND TYPE = 'U')
BEGIN
	print 'Need to add responsibilty task'
END

DELETE FROM changelog WHERE change_number = 69 AND delta_set = 'Main'
GO

--------------- Fragment ends: #69: 69_DROP Responsibility Tasks table.sql ---------------

--------------- Fragment begins: #68: 68_ INSERT INTO DocumentType.sql ---------------

-- Change script: #68: 68_ INSERT INTO DocumentType.sql
USE [BusinessSafe]
GO

DELETE FROM [Hazard] WHERE Id = 1
DELETE FROM [Hazard] WHERE Id = 2
DELETE FROM [Hazard] WHERE Id = 3

Go

DELETE FROM changelog WHERE change_number = 68 AND delta_set = 'Main'
GO

--------------- Fragment ends: #68: 68_ INSERT INTO DocumentType.sql ---------------

--------------- Fragment begins: #67: 67_CREATE DocumentType_table.sql ---------------

-- Change script: #67: 67_CREATE DocumentType_table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'DocumentType' AND TYPE = 'U')
BEGIN
	DROP TABLE [DocumentType]
END

DELETE FROM changelog WHERE change_number = 67 AND delta_set = 'Main'
GO

--------------- Fragment ends: #67: 67_CREATE DocumentType_table.sql ---------------

--------------- Fragment begins: #65: 65_ALTER RiskAssessmentFurtherActionTask Add Column FurtherControlMeasureTaskCategoryId.sql ---------------

-- Change script: #65: 65_ALTER RiskAssessmentFurtherActionTask Add Column FurtherControlMeasureTaskCategoryId.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'FurtherControlMeasureTaskCategoryId')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	DROP COLUMN [FurtherControlMeasureTaskCategoryId]
END
GO

DELETE FROM changelog WHERE change_number = 65 AND delta_set = 'Main'
GO

--------------- Fragment ends: #65: 65_ALTER RiskAssessmentFurtherActionTask Add Column FurtherControlMeasureTaskCategoryId.sql ---------------

--------------- Fragment begins: #64: 64_ALTER ArchivedEmployee alter column EmployeeReference.sql ---------------

-- Change script: #64: 64_ALTER ArchivedEmployee alter column EmployeeReference.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ArchivedEmployee' AND COLUMN_NAME = 'EmployeeReference' AND IS_NULLABLE = 'YES')
BEGIN
	ALTER TABLE [ArchivedEmployee]
	ALTER COLUMN [EmployeeReference] nvarchar(100) NOT NULL
END
GO

DELETE FROM changelog WHERE change_number = 64 AND delta_set = 'Main'
GO

--------------- Fragment ends: #64: 64_ALTER ArchivedEmployee alter column EmployeeReference.sql ---------------

--------------- Fragment begins: #62: 62_ArchiveCreateRiskAssessmentFurtherActions Tabke.sql ---------------

-- Change script: #62: 62_ArchiveCreateRiskAssessmentFurtherActions Tabke.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'ArchiveRiskAssessmentFurtherActionTasks' AND TYPE = 'U')
BEGIN
	DROP TABLE [ArchiveRiskAssessmentFurtherActionTasks]
END

DELETE FROM changelog WHERE change_number = 62 AND delta_set = 'Main'
GO

--------------- Fragment ends: #62: 62_ArchiveCreateRiskAssessmentFurtherActions Tabke.sql ---------------

--------------- Fragment begins: #61: 61_CREATE FurtherControlMeasureDocument_table.sql ---------------

-- Change script: #61: 61_CREATE FurtherControlMeasureDocument_table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'FurtherControlMeasureDocument' AND TYPE = 'U')
BEGIN
	DROP TABLE [FurtherControlMeasureDocument]
END


DELETE FROM changelog WHERE change_number = 61 AND delta_set = 'Main'
GO

--------------- Fragment ends: #61: 61_CREATE FurtherControlMeasureDocument_table.sql ---------------

--------------- Fragment begins: #60: 60_ALTER_Employee_MODIFY_EmployeeReference.sql ---------------

-- Change script: #60: 60_ALTER_Employee_MODIFY_EmployeeReference.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Employee' AND COLUMN_NAME = 'EmployeeReference' AND DATA_TYPE = 'nvarchar' AND IS_NULLABLE = 'YES')
BEGIN
	ALTER TABLE Employee
	ALTER COLUMN EmployeeReference NVARCHAR(100)
END
GO

DELETE FROM changelog WHERE change_number = 60 AND delta_set = 'Main'
GO

--------------- Fragment ends: #60: 60_ALTER_Employee_MODIFY_EmployeeReference.sql ---------------

--------------- Fragment begins: #59: 59_ALTER RiskAssessmentFurtherActionTasks alter length  Task Description.sql ---------------

-- Change script: #59: 59_ALTER RiskAssessmentFurtherActionTasks alter length  Task Description.sql
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'Description' AND 'CHARACTER_MAXIMUM_LENGTH' = '500')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	ALTER COLUMN [Description] nvarchar(50)
END
GO

DELETE FROM changelog WHERE change_number = 59 AND delta_set = 'Main'
GO

--------------- Fragment ends: #59: 59_ALTER RiskAssessmentFurtherActionTasks alter length  Task Description.sql ---------------

--------------- Fragment begins: #58: 58_ALTER RiskAssessmentFurtherActionTasks add column TaskCompletedDate And Task Completed Comments.sql ---------------

-- Change script: #58: 58_ALTER RiskAssessmentFurtherActionTasks add column TaskCompletedDate And Task Completed Comments.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'TaskCompletedDate')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	DROP COLUMN [TaskCompletedDate]
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'TaskCompletedComments')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	DROP COLUMN [TaskCompletedComments]
END
GO

DELETE FROM changelog WHERE change_number = 58 AND delta_set = 'Main'
GO

--------------- Fragment ends: #58: 58_ALTER RiskAssessmentFurtherActionTasks add column TaskCompletedDate And Task Completed Comments.sql ---------------

--------------- Fragment begins: #57: 57_ALTER RiskAssessmentFurtherActionTasks add column TaskStatusId.sql ---------------

-- Change script: #57: 57_ALTER RiskAssessmentFurtherActionTasks add column TaskStatusId.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'TaskStatusId')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	DROP COLUMN [TaskStatusId]
END
GO

DELETE FROM changelog WHERE change_number = 57 AND delta_set = 'Main'
GO

--------------- Fragment ends: #57: 57_ALTER RiskAssessmentFurtherActionTasks add column TaskStatusId.sql ---------------

--------------- Fragment begins: #56: 56_ALTER RiskAssessmentFurtherActionTasks add column TaskCompletionDueDate.sql ---------------

-- Change script: #56: 56_ALTER RiskAssessmentFurtherActionTasks add column TaskCompletionDueDate.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'TaskCompletionDueDate')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	DROP COLUMN [TaskCompletionDueDate]
END
GO

DELETE FROM changelog WHERE change_number = 56 AND delta_set = 'Main'
GO

--------------- Fragment ends: #56: 56_ALTER RiskAssessmentFurtherActionTasks add column TaskCompletionDueDate.sql ---------------

--------------- Fragment begins: #55: 55_ALTER RiskAssessmentFurtherActionTasks add column TaskAssignedToId.sql ---------------

-- Change script: #55: 55_ALTER RiskAssessmentFurtherActionTasks add column TaskAssignedToId.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessmentFurtherActionTasks' AND COLUMN_NAME = 'TaskAssignedToId')
BEGIN
	ALTER TABLE [RiskAssessmentFurtherActionTasks]
	DROP COLUMN [TaskAssignedToId]
END
GO

DELETE FROM changelog WHERE change_number = 55 AND delta_set = 'Main'
GO

--------------- Fragment ends: #55: 55_ALTER RiskAssessmentFurtherActionTasks add column TaskAssignedToId.sql ---------------

--------------- Fragment begins: #54: 54_INSERT INTO Employee - unregistered Employee.sql ---------------

-- Change script: #54: 54_INSERT INTO Employee - unregistered Employee.sql
USE [BusinessSafe]
GO  

DELETE FROM [Employee] WHERE [Id] = '4d91b7e6-5e25-4620-bfab-d5d4b598cbf7'
GO

DELETE FROM changelog WHERE change_number = 54 AND delta_set = 'Main'
GO

--------------- Fragment ends: #54: 54_INSERT INTO Employee - unregistered Employee.sql ---------------

--------------- Fragment begins: #53: 53_CreateRiskAssessmentFurtherActions Tabke.sql ---------------

-- Change script: #53: 53_CreateRiskAssessmentFurtherActions Tabke.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RiskAssessmentFurtherActionTasks' AND TYPE = 'U')
BEGIN
	DROP TABLE [RiskAssessmentFurtherActionTasks]
END

DELETE FROM changelog WHERE change_number = 53 AND delta_set = 'Main'
GO

--------------- Fragment ends: #53: 53_CreateRiskAssessmentFurtherActions Tabke.sql ---------------

--------------- Fragment begins: #52: 52_ALTER User add column IsActivated.sql ---------------

-- Change script: #52: 52_ALTER User add column IsActivated.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'User' AND COLUMN_NAME = 'IsActivated')
BEGIN
	ALTER TABLE [User]
	DROP COLUMN [IsActivated]
END
GO

DELETE FROM changelog WHERE change_number = 52 AND delta_set = 'Main'
GO

--------------- Fragment ends: #52: 52_ALTER User add column IsActivated.sql ---------------

--------------- Fragment begins: #51: 51_CREATE RiskAssessmentPeopleAtRisk Table.sql ---------------

-- Change script: #51: 51_CREATE RiskAssessmentPeopleAtRisk Table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RiskAssessmentPeopleAtRisk' AND TYPE = 'U')
BEGIN
	DROP TABLE [RiskAssessmentPeopleAtRisk]
END

DELETE FROM changelog WHERE change_number = 51 AND delta_set = 'Main'
GO

--------------- Fragment ends: #51: 51_CREATE RiskAssessmentPeopleAtRisk Table.sql ---------------

--------------- Fragment begins: #50: 50_Create RiskAssessment Control Measures.sql ---------------

-- Change script: #50: 50_Create RiskAssessment Control Measures.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RiskAssessmentControlMeasures' AND TYPE = 'U')
BEGIN
	DROP TABLE [RiskAssessmentControlMeasures]
END

DELETE FROM changelog WHERE change_number = 50 AND delta_set = 'Main'
GO

--------------- Fragment ends: #50: 50_Create RiskAssessment Control Measures.sql ---------------

--------------- Fragment begins: #49: 49_Create Risk Assessment Hazards.sql ---------------

-- Change script: #49: 49_Create Risk Assessment Hazards.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RiskAssessmentHazards' AND TYPE = 'U')
BEGIN
	DROP TABLE [RiskAssessmentHazards]
END

DELETE FROM changelog WHERE change_number = 49 AND delta_set = 'Main'
GO

--------------- Fragment ends: #49: 49_Create Risk Assessment Hazards.sql ---------------

--------------- Fragment begins: #48: 48_ALTER_RiskAssessment_MODIFY_RiskAssessorEmployeeId.sql ---------------

-- Change script: #48: 48_ALTER_RiskAssessment_MODIFY_RiskAssessorEmployeeId.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessment' AND COLUMN_NAME = 'RiskAssessorEmployeeId' AND DATA_TYPE = 'uniqueidentifier')
BEGIN
	ALTER TABLE RiskAssessment
	DROP COLUMN RiskAssessorEmployeeId
	
	ALTER TABLE RiskAssessment
	ADD RiskAssessorEmployeeId [bigint] NULL
END
GO

DELETE FROM changelog WHERE change_number = 48 AND delta_set = 'Main'
GO

--------------- Fragment ends: #48: 48_ALTER_RiskAssessment_MODIFY_RiskAssessorEmployeeId.sql ---------------

--------------- Fragment begins: #47: 47_INSERT INTO User - System User.sql ---------------

-- Change script: #47: 47_INSERT INTO User - System User.sql
USE [BusinessSafe]
GO  

DELETE FROM [User] WHERE [UserId] = N'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99'

DELETE FROM changelog WHERE change_number = 47 AND delta_set = 'Main'
GO

--------------- Fragment ends: #47: 47_INSERT INTO User - System User.sql ---------------

--------------- Fragment begins: #46: 46_CREATE ArchivedEmployee.sql ---------------

-- Change script: #46: 46_CREATE ArchivedEmployee.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'ArchivedEmployee' AND TYPE = 'U')
BEGIN
	DROP TABLE [ArchivedEmployee]
END

DELETE FROM changelog WHERE change_number = 46 AND delta_set = 'Main'
GO

--------------- Fragment ends: #46: 46_CREATE ArchivedEmployee.sql ---------------

--------------- Fragment begins: #45: 45_CREATE CompanyVehicleType.sql ---------------

-- Change script: #45: 45_CREATE CompanyVehicleType.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'CompanyVehicleType' AND TYPE = 'U')
BEGIN
	DROP TABLE [CompanyVehicleType]
END

DELETE FROM changelog WHERE change_number = 45 AND delta_set = 'Main'
GO

--------------- Fragment ends: #45: 45_CREATE CompanyVehicleType.sql ---------------

--------------- Fragment begins: #44: 44_INSERT INTO EmploymentStatus.sql ---------------

-- Change script: #44: 44_INSERT INTO EmploymentStatus.sql
USE [BusinessSafe]
GO  

DELETE FROM [EmploymentStatus] 

DELETE FROM changelog WHERE change_number = 44 AND delta_set = 'Main'
GO

--------------- Fragment ends: #44: 44_INSERT INTO EmploymentStatus.sql ---------------

--------------- Fragment begins: #43: 43_INSERT INTO Country.sql ---------------

-- Change script: #43: 43_INSERT INTO Country.sql
USE [BusinessSafe]
GO  

DELETE FROM [Country] 

DELETE FROM changelog WHERE change_number = 43 AND delta_set = 'Main'
GO

--------------- Fragment ends: #43: 43_INSERT INTO Country.sql ---------------

--------------- Fragment begins: #42: 42_INSERT INTO Nationality.sql ---------------

-- Change script: #42: 42_INSERT INTO Nationality.sql
USE [PeninsulaOnline]
GO  

DELETE FROM [Nationality] 

DELETE FROM changelog WHERE change_number = 42 AND delta_set = 'Main'
GO

--------------- Fragment ends: #42: 42_INSERT INTO Nationality.sql ---------------

--------------- Fragment begins: #41: 41_CREATE EmploymentStatus.sql ---------------

-- Change script: #41: 41_CREATE EmploymentStatus.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'EmploymentStatus' AND TYPE = 'U')
BEGIN
	DROP TABLE [EmploymentStatus]
END

DELETE FROM changelog WHERE change_number = 41 AND delta_set = 'Main'
GO

--------------- Fragment ends: #41: 41_CREATE EmploymentStatus.sql ---------------

--------------- Fragment begins: #40: 40_CREATE Nationality.sql ---------------

-- Change script: #40: 40_CREATE Nationality.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Nationality' AND TYPE = 'U')
BEGIN
	DROP TABLE [Nationality]
END

DELETE FROM changelog WHERE change_number = 40 AND delta_set = 'Main'
GO

--------------- Fragment ends: #40: 40_CREATE Nationality.sql ---------------

--------------- Fragment begins: #39: 39_CREATE Countries.sql ---------------

-- Change script: #39: 39_CREATE Countries.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Country' AND TYPE = 'U')
BEGIN
	DROP TABLE [Country]
END

DELETE FROM changelog WHERE change_number = 39 AND delta_set = 'Main'
GO

--------------- Fragment ends: #39: 39_CREATE Countries.sql ---------------

--------------- Fragment begins: #38: 38_CREATE EmployeeEmergencyContactDetails.sql ---------------

-- Change script: #38: 38_CREATE EmployeeEmergencyContactDetails.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'EmployeeEmergencyContactDetails' AND TYPE = 'U')
BEGIN
	DROP TABLE [EmployeeEmergencyContactDetails]
END

DELETE FROM changelog WHERE change_number = 38 AND delta_set = 'Main'
GO

--------------- Fragment ends: #38: 38_CREATE EmployeeEmergencyContactDetails.sql ---------------

--------------- Fragment begins: #37: 37_CREATE EmployeeContactDetails.sql ---------------

-- Change script: #37: 37_CREATE EmployeeContactDetails.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'EmployeeContactDetails' AND TYPE = 'U')
BEGIN
	DROP TABLE [EmployeeContactDetails]
END

DELETE FROM changelog WHERE change_number = 37 AND delta_set = 'Main'
GO

--------------- Fragment ends: #37: 37_CREATE EmployeeContactDetails.sql ---------------

--------------- Fragment begins: #36: 36_CREATE Employee.sql ---------------

-- Change script: #36: 36_CREATE Employee.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Employee' AND TYPE = 'U')
BEGIN
	DROP TABLE [Employee]
END

DELETE FROM changelog WHERE change_number = 36 AND delta_set = 'Main'
GO

--------------- Fragment ends: #36: 36_CREATE Employee.sql ---------------

--------------- Fragment begins: #35: 35_INSERT INTO RolesPermissions.sql ---------------

-- Change script: #35: 35_INSERT INTO RolesPermissions.sql
USE [BusinessSafe]
GO

DELETE FROM [BusinessSafe].[dbo].[RolesPermissions]

DELETE FROM changelog WHERE change_number = 35 AND delta_set = 'Main'
GO

--------------- Fragment ends: #35: 35_INSERT INTO RolesPermissions.sql ---------------

--------------- Fragment begins: #34: 34_CREATE RolesPermissions Table.sql ---------------

-- Change script: #34: 34_CREATE RolesPermissions Table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RolesPermissions' AND TYPE = 'U')
BEGIN
	DROP TABLE [RolesPermissions]
END

DELETE FROM changelog WHERE change_number = 34 AND delta_set = 'Main'
GO

--------------- Fragment ends: #34: 34_CREATE RolesPermissions Table.sql ---------------

--------------- Fragment begins: #33: 33_INSERT INTO PermissionGroupsPermissions.sql ---------------

-- Change script: #33: 33_INSERT INTO PermissionGroupsPermissions.sql
USE [BusinessSafe]
GO

DELETE FROM [BusinessSafe].[dbo].[PermissionGroupsPermissions]
WHERE ([PermissionGroupId] >= 1 AND [PermissionGroupId] <=7) 
AND   ([PermissionId] >= 2 AND [PermissionId] <= 28)

DELETE FROM changelog WHERE change_number = 33 AND delta_set = 'Main'
GO

--------------- Fragment ends: #33: 33_INSERT INTO PermissionGroupsPermissions.sql ---------------

--------------- Fragment begins: #32: 32_INSERT INTO PermissionGroups.sql ---------------

-- Change script: #32: 32_INSERT INTO PermissionGroups.sql
USE [BusinessSafe]
GO

DELETE FROM [BusinessSafe].[dbo].[PermissionGroup]
WHERE [PermissionGroupId] >= 1 AND [PermissionGroupId] <=7

DELETE FROM changelog WHERE change_number = 32 AND delta_set = 'Main'
GO

--------------- Fragment ends: #32: 32_INSERT INTO PermissionGroups.sql ---------------

--------------- Fragment begins: #31: 31_INSERT INTO Permission.sql ---------------

-- Change script: #31: 31_INSERT INTO Permission.sql
USE [BusinessSafe]
GO

DELETE FROM [BusinessSafe].[dbo].[Permission]
WHERE [PermissionId] >= 2 AND [PermissionId] <=32

DELETE FROM changelog WHERE change_number = 31 AND delta_set = 'Main'
GO

--------------- Fragment ends: #31: 31_INSERT INTO Permission.sql ---------------

--------------- Fragment begins: #30: 30_CREATE PermissionGroupsPermissions Table.sql ---------------

-- Change script: #30: 30_CREATE PermissionGroupsPermissions Table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'PermissionGroupsPermissions' AND TYPE = 'U')
BEGIN
	DROP TABLE [PermissionGroupsPermissions]
END

DELETE FROM changelog WHERE change_number = 30 AND delta_set = 'Main'
GO

--------------- Fragment ends: #30: 30_CREATE PermissionGroupsPermissions Table.sql ---------------

--------------- Fragment begins: #29: 29_CREATE Permission Group Table.sql ---------------

-- Change script: #29: 29_CREATE Permission Group Table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'PermissionGroup' AND TYPE = 'U')
BEGIN
	DROP TABLE [PermissionGroup]
END

DELETE FROM changelog WHERE change_number = 29 AND delta_set = 'Main'
GO

--------------- Fragment ends: #29: 29_CREATE Permission Group Table.sql ---------------

--------------- Fragment begins: #28: 28_CREATE Permission Table.sql ---------------

-- Change script: #28: 28_CREATE Permission Table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Permission' AND TYPE = 'U')
BEGIN
	DROP TABLE [Permission]
END

DELETE FROM changelog WHERE change_number = 28 AND delta_set = 'Main'
GO

--------------- Fragment ends: #28: 28_CREATE Permission Table.sql ---------------

--------------- Fragment begins: #27: 27_INSERT INTO RiskAssessmentEmployee.sql ---------------

-- Change script: #27: 27_INSERT INTO RiskAssessmentEmployee.sql
USE [BusinessSafe]
GO

--DELETE FROM [BusinessSafe].[dbo].[RiskAssessmentEmployee]

DELETE FROM changelog WHERE change_number = 27 AND delta_set = 'Main'
GO

--------------- Fragment ends: #27: 27_INSERT INTO RiskAssessmentEmployee.sql ---------------

--------------- Fragment begins: #26: 26_INSERT INTO RiskAssessments.sql ---------------

-- Change script: #26: 26_INSERT INTO RiskAssessments.sql
USE [BusinessSafe]
GO

DELETE FROM [BusinessSafe].[dbo].[RiskAssessment]

DELETE FROM changelog WHERE change_number = 26 AND delta_set = 'Main'
GO

--------------- Fragment ends: #26: 26_INSERT INTO RiskAssessments.sql ---------------

--------------- Fragment begins: #25: 25_INSERT INTO User.sql ---------------

-- Change script: #25: 25_INSERT INTO User.sql
USE [BusinessSafe]
GO

--DELETE FROM [User] WHERE UserId = '790D8CC9-04F8-4643-90EE-FAED4BA711EC'
--DELETE FROM [User] WHERE UserId = '16ac58fb-4ea4-4482-ac3d-000d607af67c'
--DELETE FROM [User] WHERE UserId = '817927d0-ed72-44f9-bc20-fc9e26909754'
--DELETE FROM [User] WHERE UserId = '91f0e64a-7c04-4d89-a336-56c82d810652'

DELETE FROM changelog WHERE change_number = 25 AND delta_set = 'Main'
GO

--------------- Fragment ends: #25: 25_INSERT INTO User.sql ---------------

--------------- Fragment begins: #24: 24_INSERT INTO Role.sql ---------------

-- Change script: #24: 24_INSERT INTO Role.sql
USE [BusinessSafe]
GO

DELETE FROM [Role] WHERE RoleId = '1e382767-93dd-47e2-88f2-b3e7f7648642'
DELETE FROM [Role] WHERE RoleId = 'bacf7c01-d210-4dbc-942f-15d8456d3b92'
DELETE FROM [Role] WHERE RoleId = '952eecb7-2b96-4399-82ae-7e2341d25e51'


DELETE FROM changelog WHERE change_number = 24 AND delta_set = 'Main'
GO

--------------- Fragment ends: #24: 24_INSERT INTO Role.sql ---------------

--------------- Fragment begins: #23: 23_CREATE Role Table.sql ---------------

-- Change script: #23: 23_CREATE Role Table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Role' AND TYPE = 'U')
BEGIN
	DROP TABLE [Role]
END

DELETE FROM changelog WHERE change_number = 23 AND delta_set = 'Main'
GO

--------------- Fragment ends: #23: 23_CREATE Role Table.sql ---------------

--------------- Fragment begins: #22: 22_CREATE User Table.sql ---------------

-- Change script: #22: 22_CREATE User Table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'User' AND TYPE = 'U')
BEGIN
	DROP TABLE [User]
END

DELETE FROM changelog WHERE change_number = 22 AND delta_set = 'Main'
GO

--------------- Fragment ends: #22: 22_CREATE User Table.sql ---------------

--------------- Fragment begins: #21: 21_CREATE RiskAssessmentEmployee Table.sql ---------------

-- Change script: #21: 21_CREATE RiskAssessmentEmployee Table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RiskAssessmentEmployees' AND TYPE = 'U')
BEGIN
	DROP TABLE [RiskAssessmentEmployees]
END

DELETE FROM changelog WHERE change_number = 21 AND delta_set = 'Main'
GO

--------------- Fragment ends: #21: 21_CREATE RiskAssessmentEmployee Table.sql ---------------

--------------- Fragment begins: #19: 19_ INSERT INTO People At Risk.sql ---------------

-- Change script: #19: 19_ INSERT INTO People At Risk.sql
USE [BusinessSafe]
GO

DELETE FROM [PeopleAtRisk] WHERE Id = 1
DELETE FROM [PeopleAtRisk] WHERE Id = 2
DELETE FROM [PeopleAtRisk] WHERE Id = 3
DELETE FROM [PeopleAtRisk] WHERE Id = 4
DELETE FROM [PeopleAtRisk] WHERE Id = 5
DELETE FROM [PeopleAtRisk] WHERE Id = 6
DELETE FROM [PeopleAtRisk] WHERE Id = 7
DELETE FROM [PeopleAtRisk] WHERE Id = 8
DELETE FROM [PeopleAtRisk] WHERE Id = 9
Go

DELETE FROM changelog WHERE change_number = 19 AND delta_set = 'Main'
GO

--------------- Fragment ends: #19: 19_ INSERT INTO People At Risk.sql ---------------

--------------- Fragment begins: #18: 18_ INSERT INTO Hazards.sql ---------------

-- Change script: #18: 18_ INSERT INTO Hazards.sql
USE [BusinessSafe]
GO

DELETE FROM [Hazard] WHERE Id = 1
DELETE FROM [Hazard] WHERE Id = 2
DELETE FROM [Hazard] WHERE Id = 3
DELETE FROM [Hazard] WHERE Id = 4
DELETE FROM [Hazard] WHERE Id = 5
DELETE FROM [Hazard] WHERE Id = 6
DELETE FROM [Hazard] WHERE Id = 7
DELETE FROM [Hazard] WHERE Id = 8
DELETE FROM [Hazard] WHERE Id = 9
DELETE FROM [Hazard] WHERE Id = 10
DELETE FROM [Hazard] WHERE Id = 11
DELETE FROM [Hazard] WHERE Id = 12
DELETE FROM [Hazard] WHERE Id = 13
DELETE FROM [Hazard] WHERE Id = 14
DELETE FROM [Hazard] WHERE Id = 15
DELETE FROM [Hazard] WHERE Id = 16
DELETE FROM [Hazard] WHERE Id = 17
DELETE FROM [Hazard] WHERE Id = 18
DELETE FROM [Hazard] WHERE Id = 19
DELETE FROM [Hazard] WHERE Id = 20
DELETE FROM [Hazard] WHERE Id = 21
DELETE FROM [Hazard] WHERE Id = 22
DELETE FROM [Hazard] WHERE Id = 23
DELETE FROM [Hazard] WHERE Id = 24

Go

DELETE FROM changelog WHERE change_number = 18 AND delta_set = 'Main'
GO

--------------- Fragment ends: #18: 18_ INSERT INTO Hazards.sql ---------------

--------------- Fragment begins: #17: 17_ALTER_RiskAssessment_ADD_RiskAssessorEmployeeId.sql ---------------

-- Change script: #17: 17_ALTER_RiskAssessment_ADD_RiskAssessorEmployeeId.sql

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RiskAssessment' AND COLUMN_NAME = 'RiskAssessorEmployeeId')
BEGIN
	ALTER TABLE RiskAssessment
	DROP COLUMN RiskAssessorEmployeeId
END
GO

DELETE FROM changelog WHERE change_number = 17 AND delta_set = 'Main'
GO

--------------- Fragment ends: #17: 17_ALTER_RiskAssessment_ADD_RiskAssessorEmployeeId.sql ---------------

--------------- Fragment begins: #16: 16_Create People at Risk Table.sql ---------------

-- Change script: #16: 16_Create People at Risk Table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'PeopleAtRisk' AND TYPE = 'U')
BEGIN
	DROP TABLE [PeopleAtRisk]
END

DELETE FROM changelog WHERE change_number = 16 AND delta_set = 'Main'
GO

--------------- Fragment ends: #16: 16_Create People at Risk Table.sql ---------------

--------------- Fragment begins: #15: 15_Create Hazard Table.sql ---------------

-- Change script: #15: 15_Create Hazard Table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Hazard' AND TYPE = 'U')
BEGIN
	DROP TABLE [Hazard]
END

DELETE FROM changelog WHERE change_number = 15 AND delta_set = 'Main'
GO

--------------- Fragment ends: #15: 15_Create Hazard Table.sql ---------------

--------------- Fragment begins: #14: 14_Create LogEvent table.sql ---------------

-- Change script: #14: 14_Create LogEvent table.sql

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'LogEvent')
BEGIN
	DROP TABLE [LogEvent]
END
GO

DELETE FROM changelog WHERE change_number = 14 AND delta_set = 'Main'
GO

--------------- Fragment ends: #14: 14_Create LogEvent table.sql ---------------

--------------- Fragment begins: #13: 13_Create Risk Assessments Non Employees.sql ---------------

-- Change script: #13: 13_Create Risk Assessments Non Employees.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RiskAssessmentsNonEmployees' AND TYPE = 'U')
BEGIN
	DROP TABLE [RiskAssessmentsNonEmployees]
END

DELETE FROM changelog WHERE change_number = 13 AND delta_set = 'Main'
GO

--------------- Fragment ends: #13: 13_Create Risk Assessments Non Employees.sql ---------------

--------------- Fragment begins: #12: 12_Create NonEmployeesLookup table.sql ---------------

-- Change script: #12: 12_Create NonEmployeesLookup table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'NonEmployee' AND TYPE = 'U')
BEGIN
	DROP TABLE [NonEmployee]
END

DELETE FROM changelog WHERE change_number = 12 AND delta_set = 'Main'
GO

--------------- Fragment ends: #12: 12_Create NonEmployeesLookup table.sql ---------------

--------------- Fragment begins: #11: 11_Create GeneralRiskAssessment_table.sql ---------------

-- Change script: #11: 11_Create GeneralRiskAssessment_table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RiskAssessment' AND TYPE = 'U')
BEGIN
	DROP TABLE [RiskAssessment]
END

DELETE FROM changelog WHERE change_number = 11 AND delta_set = 'Main'
GO

--------------- Fragment ends: #11: 11_Create GeneralRiskAssessment_table.sql ---------------

--------------- Fragment begins: #10: 10_ALTER Site_table.sql ---------------

-- Change script: #10: 10_ALTER Site_table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Site' AND TYPE = 'U')
BEGIN
	ALTER TABLE	[Site]
		ALTER COLUMN SiteId bigint NOT NULL
END

DELETE FROM changelog WHERE change_number = 10 AND delta_set = 'Main'
GO

--------------- Fragment ends: #10: 10_ALTER Site_table.sql ---------------

--------------- Fragment begins: #9: 09_ALTER Site_table.sql ---------------

-- Change script: #9: 09_ALTER Site_table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Site' AND TYPE = 'U')
BEGIN
	ALTER TABLE	[Site]
		DROP COLUMN  SiteType
END

DELETE FROM changelog WHERE change_number = 9 AND delta_set = 'Main'
GO

--------------- Fragment ends: #9: 09_ALTER Site_table.sql ---------------

--------------- Fragment begins: #8: 08_RENAME SiteAddress_table.sql ---------------

-- Change script: #8: 08_RENAME SiteAddress_table.sql
IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'Site' AND TYPE = 'U')
BEGIN
	exec sp_rename 'Site', 'SiteAddress'
END


SELECT * FROM site

DELETE FROM changelog WHERE change_number = 8 AND delta_set = 'Main'
GO

--------------- Fragment ends: #8: 08_RENAME SiteAddress_table.sql ---------------

--------------- Fragment begins: #7: 07_CREATE SiteAddress_table.sql ---------------

-- Change script: #7: 07_CREATE SiteAddress_table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'SiteAddress' AND TYPE = 'U')
BEGIN
	DROP TABLE [SiteAddress]
END

DELETE FROM changelog WHERE change_number = 7 AND delta_set = 'Main'
GO

--------------- Fragment ends: #7: 07_CREATE SiteAddress_table.sql ---------------

--------------- Fragment begins: #6: 06_INSERT INTO EmailTemplate - Templates.sql ---------------

-- Change script: #6: 06_INSERT INTO EmailTemplate - Templates.sql
USE [BusinessSafe]
GO

DELETE FROM [EmailTemplate] WHERE Id = 1


DELETE FROM changelog WHERE change_number = 6 AND delta_set = 'Main'
GO

--------------- Fragment ends: #6: 06_INSERT INTO EmailTemplate - Templates.sql ---------------

--------------- Fragment begins: #5: 05_CREATE EmailTemplate_table.sql ---------------

-- Change script: #5: 05_CREATE EmailTemplate_table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'EmailTemplate' AND TYPE = 'U')
BEGIN
	DROP TABLE [EmailTemplate]
END

DELETE FROM changelog WHERE change_number = 5 AND delta_set = 'Main'
GO

--------------- Fragment ends: #5: 05_CREATE EmailTemplate_table.sql ---------------

--------------- Fragment begins: #4: 04_INSERT INTO ResponsibilityTask - Grid Data.sql ---------------

-- Change script: #4: 04_INSERT INTO ResponsibilityTask - Grid Data.sql
USE [BusinessSafe]
GO

DELETE FROM ResponsibilityTask WHERE Id = 1
DELETE FROM ResponsibilityTask WHERE Id = 2
DELETE FROM ResponsibilityTask WHERE Id = 3
DELETE FROM ResponsibilityTask WHERE Id = 4
DELETE FROM ResponsibilityTask WHERE Id = 5
DELETE FROM ResponsibilityTask WHERE Id = 6

DELETE FROM changelog WHERE change_number = 4 AND delta_set = 'Main'
GO

--------------- Fragment ends: #4: 04_INSERT INTO ResponsibilityTask - Grid Data.sql ---------------

--------------- Fragment begins: #3: 03_CREATE ResponsibilityTask_table.sql ---------------

-- Change script: #3: 03_CREATE ResponsibilityTask_table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'ResponsibilityTask' AND TYPE = 'U')
BEGIN
	DROP TABLE [ResponsibilityTask]
END

DELETE FROM changelog WHERE change_number = 3 AND delta_set = 'Main'
GO

--------------- Fragment ends: #3: 03_CREATE ResponsibilityTask_table.sql ---------------

--------------- Fragment begins: #2: 02_INSERT INTO TaskCategory - DLL data.sql ---------------

-- Change script: #2: 02_INSERT INTO TaskCategory - DLL data.sql
USE [BusinessSafe]
GO

DELETE FROM ResponsibilityTaskCategory WHERE Id = 1
DELETE FROM ResponsibilityTaskCategory WHERE Id = 2
DELETE FROM ResponsibilityTaskCategory WHERE Id = 3
DELETE FROM ResponsibilityTaskCategory WHERE Id = 4

DELETE FROM changelog WHERE change_number = 2 AND delta_set = 'Main'
GO

--------------- Fragment ends: #2: 02_INSERT INTO TaskCategory - DLL data.sql ---------------

--------------- Fragment begins: #1: 01_CREATE TaskCategory_table.sql ---------------

-- Change script: #1: 01_CREATE TaskCategory_table.sql

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'ResponsibilityTaskCategory' AND TYPE = 'U')
BEGIN
	DROP TABLE [ResponsibilityTaskCategory]
END

DELETE FROM changelog WHERE change_number = 1 AND delta_set = 'Main'
GO

--------------- Fragment ends: #1: 01_CREATE TaskCategory_table.sql ---------------
