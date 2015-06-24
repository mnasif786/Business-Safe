@Acceptance
Feature: Add Further Control Measure Tasks To Risk Assessment
	In order to add further control measure tasks to a hazardous substance risk assessment
	As a business safe online user
	I want to be able to add further control measure tasks

Background:
	Given I have logged in as company with id '55881'

@acceptance
Scenario: Add Further Control Measure Task To Risk Assessment
	Given I am on description tab with company Id '55881'
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	And I have entered 'Toilet Cleaner' into the 'NewHazardousSubstance' field
	And I have entered '4' into the 'NewHazardousSubstanceId' field
	When I press 'createSummary' button
	And I wait for '1000' miliseconds
	And I press 'controlmeasures' link
	When I press 'AddFurtherControlMeasureTask' button
	And I wait for '4000' miliseconds
	Then the element with id 'dialogFurtherControlMeasureTask' has visibility of 'true'
	When I press 'FurtherActionTaskCancelButton' button
	Then the element with id 'dialogFurtherControlMeasureTask' has visibility of 'false'
	When I press 'AddFurtherControlMeasureTask' button
	And I press 'FurtherActionTaskSaveButton' button
	Then the 'Title is required' error message is displayed
	And the 'Description is required' error message is displayed
	And the 'Task Assigned To is required' error message is displayed
	When I have entered 'HSRA FCM 01' into the 'Reference' field
	And I have entered 'HSRA FCM Task 01' into the 'Title' field
	And I have entered 'This is HSRA FCM Task 01' into the 'Description' field
	And I have entered 'Kim Howard' and 'a433e9b2-84f6-4ad7-a89c-050e914dff01' into the 'TaskAssignedTo' combobox
	And I have entered '23/06/2016' into the 'TaskCompletionDueDate' field
	And It is simulated that a document has been created for the fcm task
	And I press 'FurtherActionTaskSaveButton' button
	Then the hazardous substance risk assessment with reference 'HSRA FCM 01' fcm task is saved to the database
	Then the Document should be saved as part of the created fcm task
	And FCM task 'HSRA FCM Task 01' should have visibility of 'true'
	When reload the current page
	Then FCM task 'HSRA FCM Task 01' should have visibility of 'true'

@acceptance
@finetune
@changesTaskTitleAndDescriptionForTask18
Scenario: Edit Further Control Measure Task On Hazardous Substance Risk Assessment
	Given I am on the hazardous substance risk assessment index page for company '55881'
	And I have clicked on the edit risk assessment link for id '42'
	And I wait for '1000' miliseconds
	And I press 'controlmeasures' link
	And I click on the further action task row for id '18'
	When I press 'Edit' button on the further control measure task row
	Then the element with id 'IsRecurring' has a 'readonly' attribute of 'readonly'
	When I have entered 'Title Test Passed' into the 'Title' field
	And I have entered 'Description Test Passed' into the 'Description' field
	And I have entered 'Kim Howard' and 'a433e9b2-84f6-4ad7-a89c-050e914dff01' into the 'TaskAssignedTo' combobox
	And I have entered '23/06/2016' into the 'TaskCompletionDueDate' field
	And I press 'FurtherActionTaskSaveButton' button
	Then the modified further control measure task is saved to the database

@acceptance
Scenario: Add Reoccurring Further Control Measure Task To Risk Assessment
	Given I am on description tab with company Id '55881'
	And I have entered 'Reoccurring Test Title' into the 'Title' field
	And I have entered 'Reoccurring Test Reference' into the 'Reference' field
	And I have entered 'Toilet Cleaner' into the 'NewHazardousSubstance' field
	And I have entered '4' into the 'NewHazardousSubstanceId' field
	When I press 'createSummary' button
	And I wait for '1000' miliseconds
	And I press 'controlmeasures' link
	When I press 'AddFurtherControlMeasureTask' button
	Then the element with id 'dialogFurtherControlMeasureTask' has visibility of 'true'
	And I press IsReoccurring checkbox for the HSRA FCM Task
	When I press 'FurtherActionTaskSaveButton' button
	Then the 'Title is required' error message is displayed
	Then the 'Description is required' error message is displayed
	Then the 'Task Assigned To is required' error message is displayed
	Then the 'Task Recurrence Frequency is required' error message is displayed
	Then the 'First Due Date requires a valid date' error message is displayed
	Given I have entered mandatory further control measure task data for this hsra
	When I press 'FurtherActionTaskSaveButton' button
	Then the reoccurring hazardous substance risk assessment further control measure task is saved to the database
	When I click on the further action task row for the task 'Reoccurring-HSRA-FCM-Task-Title'
	And I press 'Edit' button on the 'Reoccurring-HSRA-FCM-Task-Title' row
	Then the element with id 'IsRecurring' has a 'readonly' attribute of 'readonly'
	Then the input with id 'TaskReoccurringEndDate' has value '01/01/2025'
	When I enter todays date into the field 'FirstDueDate'
	And I press 'FurtherActionTaskSaveButton' button
	Then the 'First Due Date must be in the future' error message is displayed
