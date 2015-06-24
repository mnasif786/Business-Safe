Feature: SendToSingleEmployee
	In order to ensure PRAs are complete
	As a BSO user with add PRA permissions
	I want to be select a single employee and if necessary add their email

Background:
	Given I have logged in as company with id '55881'

Scenario: Display Selected Employee Email Address
	Given I am on the checklist generator page for risk assessment '52' and companyid '55881'
	And I click the singleEmployee radiobutton
	When I have entered 'Gary Green (test@testing.com)' into the 'Employee' field
	And I have entered '4d91b7e6-5e25-4620-bfab-d5d4b598cbf7' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	Then the element with id 'ExistingEmployeeEmail' should contain 'gary.green@testing.com'
	And the element with id 'NewEmployeeEmail' has visibility of 'false'
	When I have entered '-- Select Option --' into the 'Employee' field
	And I have entered '' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	Then the element with id 'ExistingEmployeeEmail' contains null
	And the element with id 'NewEmployeeEmail' has visibility of 'false'
	When I have entered 'Barry Brown' into the 'Employee' field
	And I have entered '3ece3fd2-db29-4abd-a812-fcc6b8e621a1' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	Then the element with id 'ExistingEmployeeEmail' contains null
	And the element with id 'NewEmployeeEmail' has visibility of 'true'
