@Acceptance
Feature: Create
	In order to record Personal Risk Assessments
	As a BSO User
	I want to be create a new Personal Risk Assessment
	
Background:
	Given I have logged in as company with id '55881'
  
@Acceptance
Scenario: Risk assessment validates and can be created successfully
	Given I am on the add new personal risk assessment page
	When I press 'createSummary' button
	Then Error List 'errorCreating' Contains:
	| Error Message        |
	| Title is required |
	Given I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	When I press 'createSummary' button
	Then a new risk assessment should be created with the 'reference' set as 'Test Reference'
	And the DateOfAssessment text box should contain todays date
	Given I am on the add new personal risk assessment page
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	When I press 'createSummary' button
	Then Error List 'errorCreating' Contains:
	| Error Message |
	| Reference Already Exists |
