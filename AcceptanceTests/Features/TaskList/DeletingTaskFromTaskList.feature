Feature: Deleting Task From Task List
	As A BSO User
	When I delete task from task list
	task should be marked as deleted and no longer shown in task list

Background:
	Given I have logged in as company with id '55881'

@DeleteTaskFromTaskList
@finetune
Scenario: Delete task from task list
	Given I am on the responsibility planner page for company '55881'
	And I have entered 'Barry Brown' into the 'Employee' field
	And I have entered '3ECE3FD2-DB29-4ABD-A812-FCC6B8E621A1' into the 'EmployeeId' field
	And user click on GoToTasks for Assigned To Filter
	When I have clicked on the delete task link for id '16'
	And I select 'yes' on confirmation
	Then the task '16' should be marked as deleted
	When I have clicked on the delete task link for id '20'
	And I select 'yes' on confirmation
	Then the task '20' should be marked as deleted
	
@Acceptance
Scenario: Deleteing reoccuring task with completed tasks displays error message
	Given there is an hsra roccurring task with previous completed task for risk assessment '42'
	And I am on the responsibility planner page for company '55881'
	And I have entered 'Barry Brown' into the 'Employee' field
	And I have entered '3ECE3FD2-DB29-4ABD-A812-FCC6B8E621A1' into the 'EmployeeId' field
	And user click on GoToTasks for Assigned To Filter
	And I have clicked on the delete task link for task with title 'Reoccurring task with completed tasks'
	When I select 'yes' on confirmation
	Then the element with id 'dialogDeleteFurtherControlMeasureTaskResponse' has visibility of 'true' 
