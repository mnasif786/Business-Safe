@UpdatingEmployee
Feature: Updating Employee
	In order to update employees
	As a business safe online user with the correct user access rights
	I want to be able to update employees

Background: Setup Employees for scenario
	Given I have setup the employee 'Bob Smith' for scenario

Scenario: Validation Updating Employee
	Given I have logged in as company with id '55881'
	And I am on the update employee page for company '55881' for employee 'Bob Smith'
	And I have entered 'New Employee Reference' into the employee reference field
	And I have entered '' into the forename field
	And I have entered 'Smith' into the surname field
	And I have selected 'Male' in the sex dropdown
	When I have clicked save 
	Then validation message should be displayed saying 'forename' is required
	Given I am on the update employee page for company '55881' for employee 'Bob Smith' 
	And I have entered 'New Employee Reference' into the employee reference field
	And I have entered 'Bob' into the forename field
	And I have entered '' into the surname field
	And I have selected 'Male' in the sex dropdown
	When I have clicked save 
	Then validation message should be displayed saying 'surname' is required
	Given I am on the update employee page for company '55881' for employee 'Bob Smith' 
	And I have entered 'New Employee Reference' into the employee reference field
	And I have entered 'Bob' into the forename field
	And I have entered 'Smith' into the surname field
	And I have selected '--Select Option--' in the sex dropdown
	When I have clicked save
	Then validation message should be displayed saying 'gender' is required
	Given I am on the update employee page for company '55881' for employee 'Bob Smith' 
	And I have entered 'Not a date' into the date of birth field
	When I have clicked save 
	Then validation message should be displayed saying 'not valid for DateOfBirth.'