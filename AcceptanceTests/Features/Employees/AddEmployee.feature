Feature: Adding Employee
	In order to add employees
	As a business safe online user with the correct user access rights
	I want to be able to add employees

Scenario: When Creating a New Employee Then Cannot Fill In Other Fields Until Employee Created
	Given I have logged in as company with id '55881'
	And I am on the create employee page for company '55881'
	Then the element with id 'organisational-details-link' has visibility of 'false'
	And the element with id 'additional-details-link' has visibility of 'false'
	And the element with id 'emergency-contact-details-fieldset' has visibility of 'false'
	And the element with id 'SaveEmployeeButton' has visibility of 'false'
	And the element with id 'CreateEmployeeButton' has visibility of 'true'
	Given I have entered 'New Employee Reference' into the employee reference field
	And I have entered 'Bob' into the forename field
	And I have entered 'Smith' into the surname field
	And I have selected 'Male' in the sex dropdown
	When I have clicked save 
	Then the element with id 'organisational-details-link' has visibility of 'true'
	And the element with id 'additional-details-link' has visibility of 'true'
	And the element with id 'emergency-contact-details-fieldset' has visibility of 'true'
	And the element with id 'SaveEmployeeButton' has visibility of 'true'
	And the element with id 'CreateEmployeeButton' has visibility of 'false'

Scenario: Validation Adding Employee
	Given I have logged in as company with id '55881'
	And I am on the create employee page for company '55881'
	And I have entered 'New Employee Reference' into the employee reference field
	And I have entered '' into the forename field
	And I have entered 'Smith' into the surname field
	And I have selected 'Male' in the sex dropdown
	When I have clicked save 
	Then validation message should be displayed saying 'forename' is required
	Given I am on the create employee page for company '55881'
	And I have entered 'New Employee Reference' into the employee reference field
	And I have entered 'Bob' into the forename field
	And I have entered '' into the surname field
	And I have selected 'Male' in the sex dropdown
	When I have clicked save 
	Then validation message should be displayed saying 'surname' is required
	Given I am on the create employee page for company '55881'
	And I have entered 'New Employee Reference' into the employee reference field
	And I have entered 'Bob' into the forename field
	And I have entered 'Smith' into the surname field
	And I have selected '--Select Option--' in the sex dropdown
	When I have clicked save 
	Then validation message should be displayed saying 'gender' is required
	Given I am on the create employee page for company '55881'
	And I have entered 'Not a date' into the date of birth field
	When I have clicked save 
	Then validation message should be displayed saying 'not valid for DateOfBirth.'
	
