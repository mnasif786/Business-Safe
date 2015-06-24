Feature: BUG 1836 - Remove Button to Remove Employee Not Displaying in IE
	In order to keep system integrity
	As a business safe online user
	I should be able to remove an employee from risk assessment

Background:
	Given I have logged in as company with id '55881'

@Acceptance
Scenario: Bug 1836
	Given I am on the risk assessment index page for company '55881'
	And I have clicked on the edit risk assessment link for id '39'
	And I press 'premisesinformation' link
	And I wait for '2000' miliseconds
	And I have clicked on 'employeesMultiSelect'
	Then the element with id 'removeEmployeesBtn' has visibility of 'true'
