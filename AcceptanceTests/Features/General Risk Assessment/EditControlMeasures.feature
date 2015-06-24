@Acceptance
Feature: Edit Control Measures
	In order to add and remove Control Measures to Risk Assessments
	As a business safe online user
	I want to be able to add and remove Control Measures to a risk assessment

@finetune
Scenario: Add and remove a control measure
	Given I have logged in as company with id '55881'
	And I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	And I press 'hazardspeople' link
	When I have added 'Asbestos' to the 'Hazards' risk assessment
	When I press 'SaveButton' button
	And I press 'controlmeasures' link
	When I press button Add Control Measure
	Given I have entered 'test control measure' into the 'newControlMeasureText' field
	When I press 'saveNewControlMeasure' button
	Then the control measure 'test control measure' should be saved to the general risk assessment
	And I highlight the newly created control measure
	When I press 'removeControlMeasure' button
	Then the control measure should be removed from the general risk assessment

@finetune
Scenario: Control measure validation
	Given I have logged in as company with id '55881'
	And I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	And I press 'hazardspeople' link
	When I have added 'Asbestos' to the 'Hazards' risk assessment
	When I press 'SaveButton' button
	And I press 'controlmeasures' link
	When I press button Add Control Measure
	When I press 'saveNewControlMeasure' button
	Then the 'Please enter a control measure' error is displayed