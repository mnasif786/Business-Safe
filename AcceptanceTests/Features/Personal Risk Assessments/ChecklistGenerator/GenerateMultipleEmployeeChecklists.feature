Feature: GenerateMultipleEmployeeChecklists
	In order to ensure PRAs are complete
	As a BSO user with add PRA permissions
	I want to be send checklists out to multiple employee

Background:
	Given I have logged in as company with id '55881'

@Acceptance
Scenario: Send Checklists to Multiple Employees
	Given I am on the checklist generator page for risk assessment '52' and companyid '55881'
	And I click the multipleEmployees radiobutton
	And I have ticked checkbox with value '3ece3fd2-db29-4abd-a812-fcc6b8e621a1'
	And I have ticked checkbox with value '4d91b7e6-5e25-4620-bfab-d5d4b598cbf7'
	When I press 'AddMultipleEmployees' button
	Then the multiple selected employees table should contain the following data in correct order:
	| Name        | Email                  |
	| Barry Brown |						   |
	| Gary Green  | gary.green@testing.com |
	 
