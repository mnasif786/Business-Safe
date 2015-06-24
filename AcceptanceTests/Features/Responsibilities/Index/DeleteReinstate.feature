Feature: Delete Reinstate
	In order to ensure our responsibilities are being kept up
	As a business safe online user
	I want to be able to delete and reinstate responsibilities

Background:
	Given I have logged in as company with id '55881'

Scenario: Deleting Responsibility Hides Tasks From Task List, Reinstating Responsibility Reinstates Tasks
	Given I have logged in as company with id '55881'
	And I am on page '2' of the Responsibility Index page for companyId '55881'
	When I click to delete the responsibility with title 'Responsibility 12'
	Then the element with id 'delete-responsibility-has-tasks-warning-dialog' has visibility of 'true'
	When I click confirm button on delete
	And I navigate to task list
	Then task list has no rows
	Given I am on page '1' of the Responsibility Index page for companyId '55881'
	And I press 'show-deleted' button
	And I click to reinstate the responsibility with title 'Responsibility 12'
	When I click confirm button on delete
	And I navigate to task list
	Then task with title 'Resp Title 3' should be present
	