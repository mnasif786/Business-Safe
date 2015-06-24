@Acceptance
Feature: Employee Core Functionality
	In order to organise my company
	As a business safe online user
	I want to be able to organise add and edit my employees

@core_functionality
Background: Setup Employees for scenario
	Given I have setup the employee 'Bob Smith' for scenario

@core_functionality
Scenario: Adding Employee With All Fields
	Given I have logged in as company with id '55881'
	And I am on the create employee page for company '55881'
	And I have entered 'New Employee Reference' into the employee reference field
	And I have entered 'Bob' into the forename field
	And I have entered 'Smith' into the surname field
	And I have entered 'James' into the middle name field
	And I have selected 'Male' in the sex dropdown
	When I have clicked save 
	Given I have entered 'Mr' into the title field
	And I have entered 'Me No' into the previous surname field
	And I have entered '01/01/1970' into the date of birth field
	And I have entered 'Angolan' with id '6' in the nationality dropdown	
	And I have clicked on the 'additional-details' header
	And I have entered 'JN 11 23 22 22' in the ni number field
	And I have entered 'Afghanistan' with id '1' in the country dropdown
	And I have entered 'Wa 11 Er1' into the postcode field
	And I have clicked on the 'organisational-details' header
	And I have entered 'Aberdeen' with id '378' in the site dropdown
	And I have entered 'Employed' with id '1' in the employment status dropdown
	When I have clicked save
	Then the employee should be saved

@core_functionality
Scenario: Updating Employee With Manadatory Fields
	Given I have logged in as company with id '55881'
	And I am on the update employee page for company '55881' for employee 'Bob Smith' 
	And I have entered 'New Employee Reference' into the employee reference field
	And I have entered 'Bob' into the forename field
	And I have entered 'Smith' into the surname field
	And I have selected 'Male' in the sex dropdown
	When I have clicked save 
	Then the employee should be saved

@Acceptance
@core_functionality
Scenario: Add Emergency Contact Details to Employee
	Given I have logged in as company with id '55881'
	And I am on the update employee page for company '55881' for employee 'Bob Smith' 
	And I press 'addAnotherEmergencyContact' link
	And I have entered 'Mr' into the 'Title' field of the 'addEmergencyContactDetails' form
	And I have entered 'William' into the 'Forename' field of the 'addEmergencyContactDetails' form
	And I have entered 'Shakespeare' into the 'Surname' field of the 'addEmergencyContactDetails' form
	And I have entered 'Family friend' into the 'Relationship' field of the 'addEmergencyContactDetails' form
	And I have entered '01942 123456' into the 'WorkTelephone' field of the 'addEmergencyContactDetails' form
	And I have entered '01704 101112' into the 'HomeTelephone' field of the 'addEmergencyContactDetails' form
	And I have entered '07779 951 357' into the 'MobileTelephone' field of the 'addEmergencyContactDetails' form
	And I press 'PreferredContactNumber' radio button with the value of '2'
	And 'SameAddressAsEmployee' check box is ticked 'true'
	When I have clicked emergency contact save
	And I wait for '3000' miliseconds
	Then the emergency contact details table should contain the following data:
	| Title | Forename | Surname     | Relationship  | Preferred Telephone |
	| Mr    | William  | Shakespeare | Family friend | 01704 101112        |


