@Acceptance
Feature: Create Risk Assessment
	In order to assign assessors to risk assessment
	As a business safe online user
	I want to be able to create a new risk assessment

Background:
	Given I have logged in as company with id '55881'
  
Scenario: Risk assessment validates and can be created successfully
	Given I am on create riskassessment page in area 'GeneralRiskAssessments' with company Id '55881'
	When I press 'createSummary' button
	Then Error List 'errorCreating' Contains:
	| Error Message        |
	| Title is required |
	Given I have entered 'Title' into the 'Title' field
	And I have entered 'TRA01' into the 'Reference' field
	When I press 'createSummary' button
	Then Error List 'errorCreating' Contains:
	| Error Message        |
	| Reference already exists |
	Given I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	When I press 'createSummary' button
	Then a new risk assessment should be created with the 'reference' set as 'Test Reference'
	
Scenario: Premises Information Validates and Saves
	Given I am on create riskassessment page in area 'GeneralRiskAssessments' with company Id '55881'
	And I have created a new risk assessment
	And I press 'premisesinformation' link
	And I wait for '2000' miliseconds
	And I press 'saveButton' button
	And I wait for '2000' miliseconds
	Then Error List 'errorSaving' Contains:
	| Error Message                          |
	| Location/Area/Department is required   |
	| Task/Process Description is required   |
	When I have entered 'location location' into the 'LocationAreaDepartment' field
	And I have entered 'test test' into the 'TaskProcessDescription' field
	When I press 'saveButton' button
	Then the text box with id 'LocationAreaDepartment' should contain 'location location'
	Then the text box with id 'TaskProcessDescription' should contain 'test test'


