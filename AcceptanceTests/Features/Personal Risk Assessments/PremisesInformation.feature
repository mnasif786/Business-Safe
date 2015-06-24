@Acceptance
Feature: Premises Information
	In order to assign assessors to risk assessment
	As a business safe online user
	I want to be able to create a new risk assessment

Background:
	Given I have logged in as company with id '55881'
	
Scenario: Premises Information Validates and Saves
	Given I am on create riskassessment page in area 'PersonalRiskAssessments' with company Id '55881'
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	When I press 'createSummary' button
	And I wait for '1000' miliseconds
	And I press link with ID 'premisesinformation'
	And I press 'saveButton' button
	Then Error List 'errorSaving' Contains:
	| Error Message                          |
	| Location/Area/Department is required   |
	| Task/Process Description is required   |
	When I have entered 'location location' into the 'LocationAreaDepartment' field
	And I have entered 'test test' into the 'TaskProcessDescription' field
	When I press 'saveButton' button
	Then the text box with id 'LocationAreaDepartment' should contain 'location location'
	Then the text box with id 'TaskProcessDescription' should contain 'test test'

	# don't query database
	#Then the new risk assessment has correct values

