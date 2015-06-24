@DeletingEmployee
Feature: Deleting Employee
	In order to delete employees
	As a business safe online user with the correct user access rights
	I want to be able to delete employees

Background: Setup Employees for scenario
	Given I have the following employees for company '55881':
	| Forename | Surname | Job Title | Site      | Employee Reference |
	| Bob      | Smith   | Decorator | Aberdeen  | 1w                 |
	| Tracy    | Jones   | Telephone | Barnsley  | 2w                 |

Scenario: Delete Employee
	Given I have logged in as company with id '55881'
	And I am on the search employee page for company '55881'
	#And I have entered '1w' into the 'EmployeeReference' field
	And I press 'Search' button
	When I press 'Delete' link for 'Bob' 'Smith'
	When I select 'yes' on confirmation
	Then 'Bob' 'Smith' should then be deleted

Scenario: Delete Employee but dont confirm
	Given I have logged in as company with id '55881'
	And I am on the search employee page for company '55881'
	#And I have entered '1w' into the 'EmployeeReference' field
	And I press 'Search' button
	When I press 'Delete' link for 'Bob' 'Smith'
	When I select 'no' on confirmation
	Then 'Bob' 'Smith' should then not be deleted

@finetune
Scenario: Can Not Delete Employee
	Given I have logged in as company with id '55881'
	And I am on the search employee page for company '55881'
	And I have entered 'Brown' into the 'Surname' field
	And I press 'Search' button
	And I have waited for the page to reload
	When I press 'Delete' link for 'Barry' 'Brown'
	And I wait for '4000' miliseconds
	Then the element with id 'ui-dialog-title-dialogCannotRemoveEmployee' has visibility of 'true'
	