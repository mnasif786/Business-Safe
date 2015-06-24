@Acceptance
Feature: Hazardous Substance Risk Assessment Summary
	In order to keep my hazsub risk assessments accurate
	As a business safe online user
	I want to be able to edit a HSRA's title and reference

Background:
	Given I have logged in as company with id '55881'
  
Scenario: Risk assessment is created successfully
	Given I am on the create hazardous substance risk assessment page for company '55881'
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	And I have entered 'Toilet Cleaner' into the 'NewHazardousSubstance' field
	And I have entered '4' into the 'NewHazardousSubstanceId' field
	When I press 'createSummary' button
	Then the input with id 'Title' has value 'Test Title'
	And the input with id 'Reference' has value 'Test Reference'
	And the input with id 'HazardousSubstance' has value 'Toilet Cleaner'
	And the input with id 'DateOfAssessment' has todays date
	And I have entered 'Aberdeen' into the 'Site' field
	And I have entered '378' into the 'SiteId' field
	And I have entered 'Russell Willaims' into the 'RiskAssessor' field
	And I have entered '3' into the 'RiskAssessorId' field
	When I have entered '' into the 'Title' field
	When I have entered '' into the 'Reference' field
	When I have entered '' into the 'DateOfAssessment' field
	And I have entered '' into the 'HazardousSubstanceId' field
	And I press 'saveButton' button
	Then Error List 'errorCreating' Contains:
	| Error Message					   |
	| Title is required	   |
	| Date of Assessment should be selected |
	| The Hazardous Substance is required |
	When I have entered 'New Title' into the 'Title' field
	When I have entered 'Acceptance Test HSRA' into the 'Reference' field
	When I have entered '23/06/2020' into the 'DateOfAssessment' field
	And I have entered 'Toilet Cleaner' into the 'HazardousSubstance' field
	And I have entered '4' into the 'HazardousSubstanceId' field
	And I press 'saveButton' button
	Then Error List 'errorCreating' Contains:
	| Error Message				   |
	| Reference Already Exists	   |
	When I press link with ID 'nextBtn'
	Then Error List 'errorCreating' Contains:
	| Error Message				   |
	| Reference Already Exists	   |
	When I press 'description' link
	Then Error List 'errorCreating' Contains:
	| Error Message				   |
	| Reference Already Exists	   |
	When I have entered 'New Reference' into the 'Reference' field
	And I press 'saveButton' button
	Then the input with id 'Title' has value 'New Title'
	And the input with id 'Reference' has value 'New Reference' 
	And the notice 'Risk Assessment Summary Successfully Updated' should be displayed
