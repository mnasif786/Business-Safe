@Acceptance
@HazardousSubstanceRiskAssessments
Feature: Hazardous Substance Risk Assessment Calculator
	In order to find the control system for a given hazardous substance
	As a business safe online user
	I want to be able to specify the parameters of my hazardous substance and be provided guidance notes on how to use it safely

Background:
	Given I have logged in as company with id '55881'

@finetune
Scenario: View correct guidance notes
	Given I am on description tab with company Id '55881'
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	And I have entered 'Toilet Cleaner' into the 'NewHazardousSubstance' field
	And I have entered '4' into the 'NewHazardousSubstanceId' field
	And I press 'createSummary' button
	And I have clicked the 'assessment'
	Then the work approach is 'None'
	And the element with id 'GuidanceNotes' has visibility of 'false'
	When value 'Medium' is selected for radio button 'Quantity'
	And value 'Liquid' is selected for radio button 'MatterState'
	And value 'Medium' is selected for radio button 'DustinessOrVolatility'
	And I wait for '2000' miliseconds
	Then the work approach is 'Engineering Controls'
	And the element with id 'GuidanceNotes' has visibility of 'true'
	And the GuidanceNotes href is '/HazardousSubstanceRiskAssessments/ControlSystem/LoadControlSystem?controlSystem=Engineering%20Controls'
	When value 'Large' is selected for radio button 'Quantity'
	And value 'Solid' is selected for radio button 'MatterState'
	And value 'High' is selected for radio button 'DustinessOrVolatility'
	And I wait for '2000' miliseconds
	Then the work approach is 'Containment'
	And the element with id 'GuidanceNotes' has visibility of 'true'
	And the GuidanceNotes href is '/HazardousSubstanceRiskAssessments/ControlSystem/LoadControlSystem?controlSystem=Containment'