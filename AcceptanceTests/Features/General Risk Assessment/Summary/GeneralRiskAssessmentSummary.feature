@Acceptance
Feature: General Risk Assessment Summary
	In keep my general risk assessments accurate
	As a business safe online user
	I want to be able to edit a GRA's title and reference

Background:
	Given I have logged in as company with id '55881'
  
Scenario: Risk assessment is created successfully
	Given I am on create riskassessment page in area 'GeneralRiskAssessments' with company Id '55881'
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	When I press 'createSummary' button
	Then the input with id 'Title' has value 'Test Title'
	And the input with id 'Reference' has value 'Test Reference'
	And the input with id 'DateOfAssessment' has todays date
	When I have entered '' into the 'Title' field
	When I have entered '' into the 'Reference' field
	When I have entered '' into the 'DateOfAssessment' field
	And I press 'saveButton' button
	Then Error List 'errorCreating' Contains:
	| Error Message					   |
	| Title is required	   |
	| Date of Assessment should be selected |
	| Please select a Risk Assessor |
	| Please select a Site |
	When I have entered 'New Title' into the 'Title' field
	When I have entered 'TRA01' into the 'Reference' field
	When I have entered '378' into the 'SiteId' field
	When I have entered '1' into the 'RiskAssessorId' field
	When I have entered '23/06/2020' into the 'DateOfAssessment' field
	And I press 'saveButton' button
	Then Error List 'errorCreating' Contains:
	| Error Message				   |
	| Reference already exists	   |
	When I press link with ID 'nextBtn'
	Then Error List 'errorCreating' Contains:
	| Error Message				   |
	| Reference already exists	   |
	When I press 'premisesinformation' link
	Then Error List 'errorCreating' Contains:
	| Error Message				   |
	| Reference already exists	   |
	When I have entered 'New Reference' into the 'Reference' field
	And I press 'saveButton' button
	Then the input with id 'Title' has value 'New Title'
	And the input with id 'Reference' has value 'New Reference' 
	And the notice 'Risk Assessment Summary Successfully Updated' should be displayed
