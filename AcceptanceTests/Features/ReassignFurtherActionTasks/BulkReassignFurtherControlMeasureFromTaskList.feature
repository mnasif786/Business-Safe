@BulkReassigningFurtherActionTasks
Feature: Bulk Reassigning Further Action Tasks From Task List
	In order to reassign several further action tasks at a time
	As a business safe online user with the correct user access rights
	I want to be able to reassign several further action tasks to a given user

Background:
	Given I have logged in as company with id '55881'
	And I have the following tasks:
		| MultiHazardRiskAssessmentHazardId | Title                   | Description   | Reference                         | Deleted | CreatedOn               | CreatedBy                            | TaskAssignedToId                     | TaskCompletionDueDate   | TaskStatusId | TaskCompletedDate | TaskCompletedComments | TaskCategoryId | TaskReoccurringTypeId | TaskReoccurringEndDate  | Discriminator                                      | TaskGuid                             | SendTaskNotification | SendTaskCompletedNotification | SendTaskOverdueNotification |
		| 29                                | Bulk RA FROM List FCM 1 | Description 1 | Bulk_Reassign_From_TaskList_FCM01 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | 9D24AE1A-6645-45FC-9D50-8FC70BABEB89 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | ab751aa0-35e5-4306-a530-32c62404f853 | 0                    | 1                             | 1                           |		
		| 29                                | Bulk RA FROM List FCM 2 | Description 2 | Bulk_Reassign_From_TaskList_FCM02 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | 9D24AE1A-6645-45FC-9D50-8FC70BABEB89 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | 0152ba8b-92f3-402c-8505-5ab5601ebcb4 | 0                    | 1                             | 1                           |
		| 29                                | Bulk RA FROM List FCM 3 | Description 3 | Bulk_Reassign_From_TaskList_FCM03 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | 9D24AE1A-6645-45FC-9D50-8FC70BABEB89 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | 5e90f428-76c0-4b6f-8939-0d916c145f5f | 0                    | 1                             | 1                           |
		| 29                                | Bulk RA FROM List FCM 4 | Description 4 | Bulk_Reassign_From_TaskList_FCM04 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | 9D24AE1A-6645-45FC-9D50-8FC70BABEB89 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | d07a4628-c743-4c75-bcaa-b65f4396625a | 0                    | 1                             | 1                           |
		| 29                                | Bulk RA FROM List FCM 5 | Description 5 | Bulk_Reassign_From_TaskList_FCM05 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | 9D24AE1A-6645-45FC-9D50-8FC70BABEB89 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | 2529b4d8-eca3-4eae-a096-00f5301a6d60 | 0                    | 1                             | 1                           |
		| 29                                | Bulk RA FROM List FCM 6 | Description 6 | Bulk_Reassign_From_TaskList_FCM06 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | 9D24AE1A-6645-45FC-9D50-8FC70BABEB89 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask | 5f977193-229f-432a-ae2b-e89f928b8a35 | 0                    | 1                             | 1                           |

Scenario: Reassign several further action tasks to a user from task list
	Given I have entered 'Glen Ross' into the 'Employee' field
	And I have entered '9D24AE1A-6645-45FC-9D50-8FC70BABEB89' into the 'EmployeeId' field
	And user click on GoToTasks for Assigned To Filter
	And I press 'btnBulkAssign' link
	Then the task list should be in bulk reassign mode
	Given I tick task 'Bulk RA FROM List FCM 1' in the task list
	And I tick task 'Bulk RA FROM List FCM 2' in the task list
	And I tick task 'Bulk RA FROM List FCM 3' in the task list
	Given I have entered 'Kim Howard' into the 'BulkReassignTo' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'BulkReassignToId' field
	When I press 'BulkReassignSaveButton' button
	Then task 'Bulk RA FROM List FCM 1' is assigned to 'Kim Howard' in the task list
	Then task 'Bulk RA FROM List FCM 2' is assigned to 'Kim Howard' in the task list
	Then task 'Bulk RA FROM List FCM 3' is assigned to 'Kim Howard' in the task list