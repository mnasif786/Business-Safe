@BulkReassigningFurtherActionTasks
Feature: Bulk Reassigning Further Action Tasks From GRA
	In order to reassign several further action tasks at a time
	As a business safe online user with the correct user access rights
	I want to be able to reassign several further action tasks to a given user

Background:
	Given I have logged in as company with id '55881'
	And I have the following tasks:
		| MultiHazardRiskAssessmentHazardId | Title      | Description   | Reference           | Deleted | CreatedOn               | CreatedBy                            | TaskAssignedToId                     | TaskCompletionDueDate   | TaskStatusId | TaskCompletedDate | TaskCompletedComments | TaskCategoryId | TaskReoccurringTypeId | TaskReoccurringEndDate  | Discriminator                                      | TaskGuid                             | SendTaskNotification | SendTaskCompletedNotification | SendTaskOverdueNotification |
		| 29                                | Test FCM 1 | Description 1 | Bulk_Reassign_FCM01 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | a433e9b2-84f6-4ad7-a89c-050e914dff01 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | 641cad05-b901-4d72-9904-34513e3ac671 | 0                    | 1                             | 1                           |
		| 29                                | Test FCM 2 | Description 2 | Bulk_Reassign_FCM02 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | a433e9b2-84f6-4ad7-a89c-050e914dff01 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | eb17f6a0-1dda-4e5f-a059-b983ae1f3b9e | 0                    | 1                             | 1                           |
		| 29                                | Test FCM 3 | Description 3 | Bulk_Reassign_FCM03 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | 9D24AE1A-6645-45FC-9D50-8FC70BABEB89 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | a4a26912-a1b2-4013-9be1-39ba8cfbf28a | 0                    | 1                             | 1                           |
		| 29                                | Test FCM 4 | Description 4 | Bulk_Reassign_FCM04 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | 9D24AE1A-6645-45FC-9D50-8FC70BABEB89 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | 92b77d25-d4bc-448b-8c43-4c18b2761945 | 0                    | 1                             | 1                           |
		| 29                                | Test FCM 5 | Description 5 | Bulk_Reassign_FCM05 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | 9D24AE1A-6645-45FC-9D50-8FC70BABEB89 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | 0bd18845-5dea-4707-9986-c537da762ec6 | 0                    | 1                             | 1                           |
		| 29                                | Test FCM 6 | Description 6 | Bulk_Reassign_FCM06 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | 9D24AE1A-6645-45FC-9D50-8FC70BABEB89 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | 3c2d9060-67ae-4adf-b58b-1f07910f9e5b | 0                    | 1                             | 1                           |
		| 29                                | Test FCM 7 | Description 7 | Bulk_Reassign_FCM07 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | 9D24AE1A-6645-45FC-9D50-8FC70BABEB89 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | f9ed4fbd-fcfb-4a10-995b-eff36320906a | 0                    | 1                             | 1                           |
		| 29                                | Test FCM 8 | Description 8 | Bulk_Reassign_FCM08 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | 9D24AE1A-6645-45FC-9D50-8FC70BABEB89 | 2012-09-19 00:00:00.000 | 1            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | db46d056-0cb7-402b-ae92-2494ed59d2d8 | 0                    | 1                             | 1                           |
		| 29                                | Test FCM 9 | Description 9 | Bulk_Reassign_FCM09 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | 9D24AE1A-6645-45FC-9D50-8FC70BABEB89 | 2012-09-19 00:00:00.000 | 2            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | 3734a8b7-8868-4fd3-a031-7f0bff85b723 | 0                    | 1                             | 1                           |

Scenario: Reassign several further action tasks to a user
	Given I am on the risk assessment index page for company '55881'
	And I have clicked on the edit risk assessment link for id '39'
	And I press 'controlmeasures' link
	And I press 'init-bulk-reassign-tasks' button
	And I tick task 'Test FCM 4'
	And I tick task 'Test FCM 5'
	And I tick task 'Test FCM 6'
	And I select 'Kim Howard' with id 'a433e9b2-84f6-4ad7-a89c-050e914dff01' to reassign the tasks to
	When I press 'update-bulk-reassign-tasks' button
	Then task 'Test FCM 4' is assigned to 'Kim Howard'
	Then task 'Test FCM 5' is assigned to 'Kim Howard'
	Then task 'Test FCM 6' is assigned to 'Kim Howard'
	
Scenario: Reassign several further action tasks to user and cancel
	Given I am on the risk assessment index page for company '55881'
	And I have clicked on the edit risk assessment link for id '39'
	And I press 'controlmeasures' link
	And I press 'init-bulk-reassign-tasks' button
	And I tick task 'Test FCM 4'
	And I tick task 'Test FCM 5'
	And I tick task 'Test FCM 6'
	And I select 'Kim Howard' with id 'a433e9b2-84f6-4ad7-a89c-050e914dff01' to reassign the tasks to
	When I press 'cancel-bulk-reassign-tasks' button
	Then task 'Test FCM 4' is assigned to 'Glen Ross'
	Then task 'Test FCM 5' is assigned to 'Glen Ross'
	Then task 'Test FCM 6' is assigned to 'Glen Ross'

Scenario: Can only reassign several further action tasks to a user if that task is outstanding
	Given I am on the risk assessment index page for company '55881'
	And I have clicked on the edit risk assessment link for id '39'
	And I press 'controlmeasures' link
	And I wait for '1000' miliseconds
	When I press 'init-bulk-reassign-tasks' button
	Then task 'Test FCM 1' has a checkbox 'true'
	Then task 'Test FCM 2' has a checkbox 'true'
	Then task 'Test FCM 3' has a checkbox 'true'
	Then task 'Test FCM 4' has a checkbox 'true'
	Then task 'Test FCM 5' has a checkbox 'true'
	Then task 'Test FCM 6' has a checkbox 'true'
	Then task 'Test FCM 7' has a checkbox 'true'
	Then task 'Test FCM 8' has a checkbox 'false'
	Then task 'Test FCM 9' has a checkbox 'false'
	
#can't get to work cos decorated DDL for employee list won't play with WatiN
@ignore
Scenario: Reassign several further action tasks to non-user and continue
	Given I am on the risk assessment index page for company '55881'
	And I have clicked on the edit risk assessment link for id '39'
	And I press 'controlmeasures' link
	And I press 'init-bulk-reassign-tasks' button
	And I tick task 'Test FCM 4'
	And I tick task 'Test FCM 5'
	And I tick task 'Test FCM 6'
	And I select 'Barry Scott' with id '086838FC-76C0-4BF7-AFD7-9B0D53372D7B' to reassign the tasks to
	Then a popup warning me that Barry is not a user
	When I press 'update-bulk-reassign-tasks' button
	Then task 'Test FCM 4' is assigned to 'Barry Scott'
	Then task 'Test FCM 5' is assigned to 'Barry Scott'
	Then task 'Test FCM 6' is assigned to 'Barry Scott'