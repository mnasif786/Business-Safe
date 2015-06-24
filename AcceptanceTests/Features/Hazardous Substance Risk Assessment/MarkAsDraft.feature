@HazardousSubstanceRiskAssessments
Feature: Mark Hazardous Substance Risk Assessment As Draft
	In order to sort hazardous substance risk assessments
	As a business safe online user
	I want to be able to mark a hazardous substance risk assessment as a draft

@Acceptance
@finetune
Scenario: Mark hazardous substance risk assessment as draft then live
	Given I have logged in as company with id '55881'
	And I am on the create hazardous substance risk assessment page for company '55881'
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	And I have entered 'Toilet Cleaner' into the 'NewHazardousSubstance' field
	And I have entered '4' into the 'NewHazardousSubstanceId' field
	And I press 'createSummary' button
	When I have clicked 'Draft' 
	And reload the current page
	Then the hazardous substance risk assessment should be marked as live
	When I have clicked 'Draft' 
	And reload the current page
	Then the hazardous substance risk assessment should be marked as draft
