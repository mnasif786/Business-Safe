@ReassigningFurtherActionTasks
Feature: Reassigning Further Action Tasks
	In order to reassign further action tasks
	As a business safe online user with the correct user access rights
	I want to be able to reassign further action tasks

Background:
	Given I have logged in as company with id '55881'

@ReassigningFurtherActionTasks
Scenario: Reassign GRA further action task from task list
	Given I am on the responsibility planner page for company '55881'
	And I have entered 'Kim Howard' into the 'Employee' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'EmployeeId' field
	And I press 'ResponsibilityTaskCategoryGoToResponsibilityTasks' button
	Then the relevant employee tasks should be shown
	When I have clicked on the reassign task link for id '11'
	Then the reassign task screen should be shown
	And the TaskDialog popup element is visible
	Given I have entered 'Barry Brown ( Team leader )' into the 'ReassignTaskTo' field
	And I have entered '3ECE3FD2-DB29-4ABD-A812-FCC6B8E621A1' into the 'ReassignTaskToId' field
	When I press 'FurtherActionTaskSaveButton' button
	Then the task '11' should no longer be assigned to 'Kim Howard'
	And the task '11' should be assigned to 'Barry Brown'

@ReassigningFurtherActionTasks
Scenario: Reassign HSRA further action task from task list
	Given I am on the responsibility planner page for company '55881'
	And I have entered 'Barry Brown' into the 'Employee' field
	And I have entered '3ece3fd2-db29-4abd-a812-fcc6b8e621a1' into the 'EmployeeId' field
	And I press 'ResponsibilityTaskCategoryGoToResponsibilityTasks' button
	Then the relevant employee tasks should be shown
	When I have clicked on the reassign task link for id '17'
	Then the reassign task screen should be shown
	And the TaskDialog popup element is visible
	Given I have entered 'Kim Howard' into the 'ReassignTaskTo' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'ReassignTaskToId' field
	When I press 'FurtherActionTaskSaveButton' button
	Then the task '17' should no longer be assigned to 'Barry Brown'
	And the task '17' should be assigned to 'Kim Howard'
	#Then the task '17' retrieved from the db should be assigned to 'Kim Howard'