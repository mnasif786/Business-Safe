@Acceptance
Feature: Add Hazards Further Action Tasks
	In order to add further action tasks to hazards on a general risk assessments
	As a business safe online user
	I want to be able to add further action tasks

Background:
	Given I have logged in as company with id '55881'

@finetune
@Acceptance
Scenario: Add Further Action Task To Risk Assessment
	Given I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	And I wait for '1000' miliseconds
	And I press 'hazardspeople' link
	And I have added 'Asbestos' to the 'Hazards' risk assessment
	And I press 'SaveButton' button
	And I press 'controlmeasures' link
	And I have waited for the page to reload
	And I have waited for element 'AddFurtherActionTask1' to exist 
	When I press 'AddFurtherActionTask1' button
	And I have entered mandatory further action task data
	When I press 'FurtherActionTaskSaveButton' button
	Then the further action task should be saved
	And Created Date should be displayed

Scenario: Further Action Task validation
	Given I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	And I wait for '1000' miliseconds
	And I press 'hazardspeople' link
	And I wait for '1000' miliseconds
	And I have added 'Asbestos' to the 'Hazards' risk assessment
	And I press 'SaveButton' button
	And I press 'controlmeasures' link
	And I wait for '1000' miliseconds
	When I press 'AddFurtherActionTask1' button
	And I wait for '3000' miliseconds
	And I press IsReoccurring checkbox
	And I have entered 'Kim Howard ( Business Analyst )' into the 'TaskAssignedTo' field
	And I have entered 'a433e9b2-84f6-4ad7-a89c-050e914dff01' into the 'TaskAssignedToId' field
	And I press 'FurtherActionTaskSaveButton' button
	Then the 'Title is required' error message is displayed
	Then the 'Description is required' error message is displayed
	Then the 'Task Recurrence Frequency is required' error message is displayed
	Then the 'First Due Date requires a valid date' error message is displayed

@Acceptance
Scenario: Add Reoccurring Further Control Measure Task To Risk Assessment And Edit it
	Given I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	And I wait for '1000' miliseconds
	And I press 'hazardspeople' link
	And I have added 'Asbestos' to the 'Hazards' risk assessment
	And I press 'SaveButton' button
	And I press 'controlmeasures' link
	And I wait for '1000' miliseconds
	When I press 'AddFurtherActionTask1' button
	And I wait for '5000' miliseconds
	When I press IsReoccurring checkbox
	And I have entered mandatory further control measure task data
	When I press 'FurtherActionTaskSaveButton' button
	Then the reoccurring further control measure task should be saved
	When I click on the further action task row for the task 'Reoccurring-Title'
	And I press 'Edit' button on the 'Reoccurring-Title' row
	Then the input with id 'TaskReoccurringEndDate' has value '01/01/2025'
	When I enter todays date into the field 'FirstDueDate'
	And I press 'FurtherActionTaskSaveButton' button
	Then the 'First Due Date must be in the future' error message is displayed

@requiresCompletedAndAnOutstandingRecurringFCMTasksToBeCreated
Scenario: Completed Recurring Tasks Do Not Display In GRA Control Measures Tab
	Given I am on the risk assessment index page for company '55881'
	And I have clicked on the edit risk assessment link for id '39'
	And I have waited for the page to reload
	And I press 'controlmeasures' link
	And I have waited for the page to reload
	Then FCM task 'recurring_task_1' should have visibility of 'false'
	Then FCM task 'recurring_task_2' should have visibility of 'false'
	Then FCM task 'recurring_task_3' should have visibility of 'false'
	Then FCM task 'recurring_task_4' should have visibility of 'false'
	Then FCM task 'recurring_task_5' should have visibility of 'false'
	Then FCM task 'recurring_task_6' should have visibility of 'true'
