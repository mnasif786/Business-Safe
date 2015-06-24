Feature: CompleteFurtherControlMeasureTask
	As A BSO User with Recuring Further Control Measure Task assigned
	When I complete a RFCM task
	The next task should be auto generated and displayed on my task list.

Background: Setup tasks for scenario
	Given I have logged in as company with id '55881'
	And I have the following tasks:
		| MultiHazardRiskAssessmentHazardId | Title      | Description   | Reference  | Deleted | CreatedOn               | CreatedBy                            | TaskAssignedToId                     | TaskCompletionDueDate   | TaskStatusId | TaskCompletedDate | TaskCompletedComments | TaskCategoryId | TaskReoccurringTypeId | TaskReoccurringEndDate  | Discriminator                                             | HazardousSubstanceRiskAssessmentId | TaskGuid                             | SendTaskNotification | SendTaskCompletedNotification | SendTaskOverdueNotification |
		| 29                                | Test FCM 1 | Description 1 | RecurFCM01 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | a433e9b2-84f6-4ad7-a89c-050e914dff01 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask        |                                    | f1e25526-07a3-4fe0-8215-d2420f1aea20 | 0                    | 1                             | 1                           |	
		| 29                                | Test FCM 2 | Description 2 | RecurFCM02 | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | a433e9b2-84f6-4ad7-a89c-050e914dff01 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 3              | 2                     | 2012-12-25 00:00:00.000 | MultiHazardRiskAssessmentFurtherControlMeasureTask        |                                    | f29cfd1c-fcaa-45e2-9282-28591ad00511 | 0                    | 1                             | 1                           |	
		|                                   | HST        | Description 3 | HST        | 0       | 2012-09-13 13:25:28.000 | 16ac58fb-4ea4-4482-ac3d-000d607af67c | a433e9b2-84f6-4ad7-a89c-050e914dff01 | 2012-09-19 00:00:00.000 | 0            | NULL              | NULL                  | 6              | 0                     | 2012-12-25 00:00:00.000 | HazardousSubstanceRiskAssessmentFurtherControlMeasureTask | 42                                 | a77f52eb-c53a-4f04-b781-8736801a30f8 | 0                    | 1                             | 1                           |	
	And I am viewing tasks for EmployeeId 'A433E9B2-84F6-4AD7-A89C-050E914DFF01'

@finetune
@usesBackgroundFCMCreation
Scenario: Complete Recurring Further Control Measure Task
	And Complete task is clicked for 'RecurFCM01'
	And 'TaskComplete' check box is ticked 'true'
	And Complete button is enabled
	When Complete is clicked
	And I wait for '2000' miliseconds
	Then FCM Task should have due date of 19/10/2012

@usesBackgroundFCMCreation
Scenario: Add A Document And Complete A Further Control Measure Task
	And Complete task is clicked for 'RecurFCM02'
	And 'TaskComplete' check box is ticked 'true'
	And Complete button is enabled
	And It is simulated that a document has been uploaded when completing fcm task
	When Complete is clicked
	And I have waited for the page to reload
	Then Document should be saved as a completed document against the fcm task

@usesBackgroundFCMCreation
@finetune
Scenario: Complete Hazardous Substance Risk Assessment Further Control Measure Task
	And Complete task is clicked for 'HST'
	And 'TaskComplete' check box is ticked 'true'
	And Complete button is enabled
	When Complete is clicked
	Then the task 'HST' for company '55881' should be completed