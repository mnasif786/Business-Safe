Feature: CompleteTask
	Complete a task
	As a BSO User
	I want to be able to complete a task

@NeedsResponsibilityTasksToComplete
@Acceptance
Scenario: CompleteANonReoccuringTask
	Given I have logged in as company with id '55881'
	And Complete task is clicked for 'NonRecResp01'
	And 'TaskComplete' check box is ticked 'true'
	And Complete button is enabled
	And I wait for '100' miliseconds
	When Complete is clicked
	And I wait for '1000' miliseconds
	Then the task 'NonRecResp01' for company '55881' should be completed


@NeedsResponsibilityTasksToComplete
@Acceptance
Scenario: CompleteAReoccuringTask
	Given I have logged in as company with id '55881'
	And Complete task is clicked for 'RecResp01'
	And 'TaskComplete' check box is ticked 'true'
	And I wait for '5000' miliseconds
	And Complete button is enabled
	And I wait for '500' miliseconds
	When Complete is clicked
	And I wait for '1000' miliseconds
	Then the task 'RecResp01' for company '55881' should be completed


