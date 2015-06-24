Feature: Add Custom Hazard
	In order to complete my PRAs correctly
	As a BSO user with add PRA permissions
	I want to be able to add custom hazards specific to my PRA

Background:
	Given I have logged in as company with id '55881'

@Acceptance
Scenario: Add Custom Hazard to PRA is only available for that PRA
	Given I am on the hazards page of Personal Risk Assessment with id '50' and companyId '55881'
	And I press 'hazardspeople' link
	And I have waited for the page to reload
	When I have entered 'My custom hazard for PRA 50' into the 'AddHazard' field
	And I press 'AddNewHazard' button
	Then the 'Hazard' multi-select contains 'My custom hazard for PRA 50' are in the selected column
	Given I am on the hazards page of Personal Risk Assessment with id '51' and companyId '55881'
	When I press 'hazardspeople' link
	And I wait for '3000' miliseconds
	Then the hazards multi selector should have '23' options