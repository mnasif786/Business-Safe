@Acceptance
Feature: Edit Hazards
	In order to add and remove Hazards to Personal Risk Assessments
	As a business safe online user
	I want to be able to add and remove Hazards to a risk assessment

@ignore
Scenario: Add and/remove Hazards
	Given I have logged in as company with id '55881'
	Given I am on the hazards page of Personal Risk Assessment with id '50' and companyId '55881'
	And I press 'hazardspeople' link
	And I have selected the option label 'Electrical' from multi-select control 'hazards' 
	And I have selected the option label 'Fire' from multi-select control 'hazards' 
	And I have selected the option label 'Gas Appliances and Equipment' from multi-select control 'hazards' 
	Then the 'Hazard' multi-select contains 'Electrical,Fire,Gas Appliances and Equipment' are in the selected column
	When I have deselected option label 'Electrical' from multi-select control 'hazards'
	And I have deselected option label 'Fire' from multi-select control 'hazards'
	And I have deselected option label 'Gas Appliances and Equipment' from multi-select control 'hazards'
	Then the 'Hazard' multi-select contains '' are in the selected column
	Given I have selected the option label 'Electrical' from multi-select control 'hazards' 
	And I have selected the option label 'Fire' from multi-select control 'hazards' 
	And I have selected the option label 'Gas Appliances and Equipment' from multi-select control 'hazards' 
	When I press 'SaveButton' button
	Then the 'Hazard' multi-select contains 'Electrical,Fire,Gas Appliances and Equipment' are in the selected column
	Given I have deselected option label 'Electrical' from multi-select control 'hazards'
	When I press link with ID 'nextBtn'
	And I press 'hazardspeople' link
	Then the 'Hazard' multi-select contains 'Fire,Gas Appliances and Equipment' are in the selected column