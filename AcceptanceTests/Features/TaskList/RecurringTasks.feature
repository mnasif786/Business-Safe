Feature: Recurring Tasks
	As A BSO User
	When I views tasks in my task list
	I want to view the schedule for recurring tasks

@ignore
#can't get double click to work 
Scenario: View Recurring Tasks
	Given I have logged in as company with id '55881'
	And I have entered '-- Select Option --' and '' into the 'Employee' combobox
	And I press 'ResponsibilityTaskCategoryGoToResponsibilityTasks' button
	When I view the recurring task schedule for a task
	Then the element with class 'task-details-summary' has visibility of 'true'