@ReinstatingDeletedEmployee
Feature: Reinstating Deleted Employee
	In order to reinstate employees
	As a business safe online user with the correct user access rights
	I want to be able to reinstate deleted employees

Background: Setup Employees for scenario
	Given I have the following employees for company '55881':
	| Forename | Surname | Job Title | Site     | Employee Reference | Deleted |
	| Bob      | Smith   | Decorator | Aberdeen | 1w                 | true    |
	| Tracy    | Jones   | Telephone | Barnsley | 2w                 | true    |

Scenario: Reassign a deleted Employee
	Given I have logged in as company with id '55881'
	And I am on the search employee page for company '55881'
	#And I have entered '1w' into the 'EmployeeReference' field
	And I have checked show deleted employees
	When I press 'Reassign' link for 'Bob' 'Smith'
	Then confirmation for reinstate deleted employee should be shown
	When I select 'yes' on confirmation
	Then 'Bob' 'Smith' should then not be deleted


Scenario: Reassign a deleted Employee but dont confirm
	Given I have logged in as company with id '55881'
	And I am on the search employee page for company '55881'
	#And I have entered '1w' into the 'EmployeeReference' field
	And I have checked show deleted employees
	When I press 'Reassign' link for 'Bob' 'Smith'
	Then confirmation for reinstate deleted employee should be shown
	When I select 'no' on confirmation
	Then 'Bob' 'Smith' should then be deleted
