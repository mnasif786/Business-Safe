@Acceptance
Feature: Edit Hazards And People
	In order to add and remove Hazards and People At Risk to Risk Assessments
	As a business safe online user
	I want to be able to add and remove Hazards to a risk assessment

Scenario: Add and/remove Hazards
	Given I have logged in as company with id '55881'
	And I am on create riskassessment page in area 'GeneralRiskAssessments' with company Id '55881'
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	When I press 'createSummary' button
	And Go to the Hazards tabs
	And I have selected the option label 'Electrical' from multi-select control 'hazards' 
	And I have selected the option label 'Fire' from multi-select control 'hazards' 
	And I have selected the option label 'Gas Appliances and Equipment' from multi-select control 'hazards' 
	Then the 'Hazard' multi-select contains 'Electrical,Fire,Gas Appliances and Equipment' are in the selected column
	When I have deselected option label 'Electrical' from multi-select control 'hazards'
	And I have deselected option label 'Fire' from multi-select control 'hazards'
	And I have deselected option label 'Gas Appliances and Equipment' from multi-select control 'hazards'
	Then the 'Hazard' multi-select contains '' are in the selected column
	When I have selected the option label 'Employees' from multi-select control 'people-at-risk' 
	And I have selected the option label 'Contractors' from multi-select control 'people-at-risk' 
	And I have selected the option label 'Residents' from multi-select control 'people-at-risk' 
	Then the 'PeopleAtRisk' multi-select contains 'Employees,Contractors,Residents' are in the selected column
	When I have deselected option label 'Contractors' from multi-select control 'people-at-risk'
	And I have deselected option label 'Residents' from multi-select control 'people-at-risk'
	Then the 'PeopleAtRisk' multi-select contains 'Employees' are in the selected column

	