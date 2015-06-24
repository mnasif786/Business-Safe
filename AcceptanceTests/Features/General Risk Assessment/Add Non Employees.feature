@ignore
Feature: Adding Non Employee
	In order to add non employees to general risk assessments
	As a business safe online user
	I want to be able to add non employees

Background:
	Given I have logged in as company with id '55881'
    Given We have the cleared non employees for company '55881'
	Given We have the following non employees:
	| Name       | Company  | Position		| LinkToCompanyId | LinkToRiskAssessment |
	| Paul Cooke | Do Do Do | Director		| 55881           |                      |
	| Paul Smith | Test Co  | IT Director	| 55881           |                      |
	


Scenario: Searching for non employees
	Given I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	When I have entered 'Paul' into the non employee search field
	Then the result should be:
	| Label |
	| Paul Cooke, Do Do Do, Director			|
	| Paul Smith, Test Co, IT Director			|
	

Scenario: Adding Non Employee
	Given I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	When I have entered 'Does Not Exist' into the non employee search field
	Then the create new non employee button should be enabled
	Given I have clicked on the create new non employee button for company '55881'
	And I have entered 'New Name' into the Name field
	And I have entered 'New Company' into the Company field
	And I have entered 'New Position' into the Position field
	When I have clicked create 
	Then the new non employee should be created
	Then the new non employee should be linked to the risk assessment


Scenario: Checking Validation On Adding Non Employee
	Given I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	When I have entered 'Does Not Exist' into the non employee search field
	Then the create new non employee button should be enabled
	Given I have clicked on the create new non employee button for company '55881'
	And I have entered '' into the Name field
	When I have clicked create 
	Then the name is required validation message should be shown
	Given I have entered 'Paul Cooke' into the Name field
	When I have clicked create 
	Then the non employee already exists validation message should be shown
	