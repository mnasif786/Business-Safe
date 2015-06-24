@Acceptance
Feature: Adding Hazards To Risk Assessment
	In order to add hazards to general risk assessments
	As a business safe online user
	I want to be able to add hazards

Background:
	Given I have logged in as company with id '55881'

@finetune
Scenario: Adding Hazard
	Given I am on riskassessment '54' for company '55881'
	When I press 'hazardspeople' link
	And I have waited for the page to reload
	And I wait for '4000' miliseconds
	Then the hazards multi selector should have '23' options
	Given I have selected the option label 'Asbestos' from multi-select control 'hazards' 
	When I press 'SaveButton' button
	Then the hazard with id '1' should be saved to the general risk assessment

Scenario: Editing Hazard Description
	Given I have logged in as company with id '55881'
	And I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	And I press 'hazardspeople' link
	And I have waited for the page to reload
	When I have added 'Asbestos' to the 'Hazards' risk assessment
	When I press 'SaveButton' button
	And I press 'controlmeasures' link
	And I have waited for the page to reload
	And I press edit hazard description link
	When I press 'saveHazardDescription' button
	Then the please enter a description error message should be displayed
	Given I have entered 'test hazard name' into the 'newHazardDescription' field
	When I press 'saveHazardDescription' button
	Then the hazard description should be saved

Scenario: Editing Hazard Title
	Given I have logged in as company with id '55881'
	And I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	And I press 'hazardspeople' link
	And I have waited for the page to reload
	And I have entered 'New RA Hazard' into the 'AddHazard' field
	And I press 'AddNewHazard' button
	When I press 'SaveButton' button
	And I press 'controlmeasures' link
	And I have waited for the page to reload
	And I press 'edit-hazard-title' link
	And I have entered 'New RA Hazard Name' into the 'newHazardTitle' field
	And I press 'saveHazardTitle' button
	Then the hazard title should be saved
