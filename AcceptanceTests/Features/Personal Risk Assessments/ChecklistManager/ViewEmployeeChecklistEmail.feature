Feature: ViewEmployeeChecklistEmail
	In order to review what checklists I have sent out
	As a BSO PRA assessor
	I want to be able to view the details of a given employee checklist email

Background: 
	Given I have logged in as company with id '55881'

Scenario: View Employee Checklist Email
	Given I am on the checklist manager page for risk assessment '53' and companyid '55881'
	Then the element with id 'employee-checklist-email-contents' has visibility of 'false'
	When I click on the view employee checklist email for id '8e71888c-f72b-4b20-9dc7-16152a7e90c9'
	Then the element with id 'employee-checklist-email-contents' has visibility of 'true'
	And the element with id 'CompletedOnEmployeesBehalfDiv' has visibility of 'false'
	When I click on the view employee checklist email for id '71733ad3-53e1-4423-95a8-66d004d44ed8'
	Then the element with id 'employee-checklist-email-contents' has visibility of 'true'
	And the element with id 'CompletedOnEmployeesBehalfDiv' has visibility of 'true'
	And the element with id 'CompletedOnEmployeesBehalfNameDiv' should contain 'John Conner'
	When I press 'ChecklistUrl' link
	Then The checklist link has a url of '/Checklists/71733ad3-53e1-4423-95a8-66d004d44ed8?completedOnEmployeesBehalf=true'
