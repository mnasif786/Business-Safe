Feature: Adding and Removing Hazards
	In order to record the various fire hazards
	As a BSO User
	I want to be able to add and remove hazards

Background:
	Given I have logged in as company with id '55881'

Scenario: Add remove various hazards, save and next, then hit back hazards are still selected
	Given I am on the Fire Risk Assessment Hazards page with id '55' and companyId '55881'
	And I have added 'Fire fighting equipment' to the 'ControlMeasures' risk assessment
	And I have added 'Employees' to the 'PeopleAtRisk' risk assessment
	And I have added 'Textiles' to the 'SourcesOfFuel' risk assessment
	And I have added 'Electrical equipment' to the 'SourcesOfIgnition' risk assessment
	When I press 'nextBtn' button
	And I press back
	Then the 'FireSafetyControlMeasure' multi-select contains 'Fire fighting equipment' are in the selected column
	And the 'PeopleAtRisk' multi-select contains 'Employees' are in the selected column
	And the 'SourceOfFuels' multi-select contains 'Textiles' are in the selected column
	And the 'SourceOfIgnition' multi-select contains 'Electrical equipment' are in the selected column
	When I have deselected option label 'Fire fighting equipment' from multi-select control 'fire-safety-control-measures'
	When I have deselected option label 'Employees' from multi-select control 'people-at-risk'
	When I have deselected option label 'Textiles' from multi-select control 'source-of-fuel'
	When I have deselected option label 'Electrical equipment' from multi-select control 'source-of-ignition'
	And I have added 'Fire sprinkler system' to the 'ControlMeasures' risk assessment
	And I have added 'Volunteers' to the 'PeopleAtRisk' risk assessment
	And I have added 'Fuel oil' to the 'SourcesOfFuel' risk assessment
	And I have added 'Pilot lights' to the 'SourcesOfIgnition' risk assessment
	When I press 'nextBtn' button
	And I press back
	Then the 'FireSafetyControlMeasure' multi-select contains 'Fire sprinkler system' are in the selected column
	And the 'PeopleAtRisk' multi-select contains 'Volunteers' are in the selected column
	And the 'SourceOfFuels' multi-select contains 'Fuel oil' are in the selected column
	And the 'SourceOfIgnition' multi-select contains 'Pilot lights' are in the selected column
