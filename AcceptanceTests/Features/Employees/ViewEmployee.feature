Feature: ViewEmployee
	When viewing employees
	As a BSO employee
	I should not be able to edit those values

Scenario: View Employee in View Mode can not edit values
	Given I have logged in as company with id '55881'
	And I am on the view employee page for company '55881' and employee id 'a433e9b2-84f6-4ad7-a89c-050e914dff01'
	Then the element with id 'EmployeeReference' has a 'readonly' attribute of 'readonly'
	Then the element with id 'NameTitle' has a 'readonly' attribute of 'readonly'
	Then the element with id 'Forename' has a 'readonly' attribute of 'readonly'
	Then the element with id 'Surname' has a 'readonly' attribute of 'readonly'
	Then the element with id 'addAnotherEmergencyContact' has a 'disabled' attribute of 'disabled'
	And I have clicked on the 'organisational-details' header
	Then the element with id 'JobTitle' has a 'readonly' attribute of 'readonly'
	And I have clicked on the 'additional-details' header
	Then the element with id 'Address1' has a 'readonly' attribute of 'readonly'
	Then the element with id 'NINumber' has a 'readonly' attribute of 'readonly'
	When I press 'close-employee-record' link
	Then I am redirected to the url 'Employees/EmployeeSearch?'
	Given I am on the view employee page for company '55881' and employee id 'a433e9b2-84f6-4ad7-a89c-050e914dff01'
	When I press 'edit-employee-record' link
	Then I am redirected to the url '/Employees/Employee?employeeId=a433e9b2-84f6-4ad7-a89c-050e914dff01'