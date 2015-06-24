Feature: ReassignFurtherControlMeasureTask
	As BSO User with reassign task permissions 
	I want to be able to reassign a task from the GRA

Background:
	Given I have logged in as company with id '55881'

@ReassigningFurtherActionTasks
Scenario: Reassign Further Control Measure Task from GRA page
	Given I am on the risk assessment index page for company '55881'
	And I have clicked on the edit risk assessment link for id '39'
	And I press 'controlmeasures' link
	And I have waited for the page to reload
	And I click on the further action task row for id '11'
	And I press 'Reassign' button on the further action task row
	Then the reassign task screen should be shown
	Given I have entered 'Barry Brown ( Team leader )' into the Reassign field
	And I have entered 'Barry Brown ( Team leader )' into the 'ReassignTaskTo' field
	When I press 'FurtherActionTaskSaveButton' button
	Then the task '11' should be assigned to 'Barry Brown'

@requiresCompletedAndAnOutstandingRecurringFCMTasksToBeCreated
Scenario: When reassigning a recurring task the no longer required button is not visible
	Given I am on the risk assessment index page for company '55881'
	And I have clicked on the edit risk assessment link for id '39'
	And I press 'controlmeasures' link
	And I have waited for the page to reload
	Then FCM task 'recurring_task_6' should have visibility of 'true'
	And I click on the further action task row for the task 'recurring_task_6'
	When I press 'Reassign' button on the 'recurring_task_6' row
	Then the element with id 'completeTaskDialog' has visibility of 'true'
	And the element with id 'FurtherActionTaskNoLongerRequiredButton' has visibility of 'false'