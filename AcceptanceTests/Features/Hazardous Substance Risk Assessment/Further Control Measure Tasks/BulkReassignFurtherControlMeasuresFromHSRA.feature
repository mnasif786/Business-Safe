@BulkReassigningFurtherActionTasks
Feature: Bulk Reassigning Further Action Tasks From HSRA
	In order to reassign several further action tasks at a time
	As a business safe online user with the correct user access rights
	I want to be able to reassign several further action tasks to a given user

Background:
	Given I have logged in as company with id '55881'
	And I have the following tasks:
		| MultiHazardRiskAssessmentHazardId | Title      | Description   | Reference           | Deleted | CreatedOn               | CreatedBy                            | TaskAssignedToId                     | TaskCompletionDueDate   | TaskStatusId | TaskCompletedDate | TaskCompletedComments | TaskCategoryId | TaskReoccurringTypeId | TaskReoccurringEndDate  | Discriminator                                      | HazardousSubstanceRiskAssessmentId | TaskGuid                             | SendTaskNotification | SendTaskCompletedNotification | SendTaskOverdueNotification |
		|                                   | Test FCM 1 | Description 1 | Bulk_Reassign_FCM01 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | 9D24AE1A-6645-45FC-9D50-8FC70BABEB89 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | 42                                 | 850cb58e-5381-4824-9383-560537fd6ea5 | 0                    | 1                             | 1                           |
		|                                   | Test FCM 2 | Description 2 | Bulk_Reassign_FCM02 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | 9D24AE1A-6645-45FC-9D50-8FC70BABEB89 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | 42                                 | 342dc960-4165-427b-bb46-17408a23befd | 0                    | 1                             | 1                           |
		|                                   | Test FCM 3 | Description 3 | Bulk_Reassign_FCM03 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | 9D24AE1A-6645-45FC-9D50-8FC70BABEB89 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | 42                                 | 9730c1a7-c127-43cc-a680-c5d524c18d8b | 0                    | 1                             | 1                           |

Scenario: Reassign several further action tasks to a user
	Given I am on the hazardous substance risk assessment index page for company '55881'
	And I have clicked on the edit risk assessment link for id '42'
	And I press 'controlmeasures' link
	And I have waited for the page to reload
	And I press 'init-bulk-reassign-tasks' button
	And I tick task 'Test FCM 1'
	And I tick task 'Test FCM 2'
	And I select 'Kim Howard' with id 'a433e9b2-84f6-4ad7-a89c-050e914dff01' to reassign the tasks to
	When I press 'update-bulk-reassign-tasks' button
	Then task 'Test FCM 1' is assigned to 'Kim Howard'
	Then task 'Test FCM 2' is assigned to 'Kim Howard'
	Then task 'Test FCM 3' is assigned to 'Glen Ross'