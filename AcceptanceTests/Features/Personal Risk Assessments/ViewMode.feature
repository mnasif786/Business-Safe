Feature: ViewMode
	In order to check risk asssessments
	As a BSO user
	I want to be able to view the details of risk assessments

Background:
	Given I have logged in as company with id '55881'
	And I am on the personal risk assessments page for company '55881'

Scenario: View mode and edit
	Given I have clicked on the view risk assessment link for id '50'
	When I press 'summary' link
	And I wait for '1000' miliseconds
	Then the element with id 'Site' has a 'readonly' attribute of 'readonly'
	When I press 'premisesinformation' link
	And I wait for '1000' miliseconds
	Then the element with id 'LocationAreaDepartment' has a 'readonly' attribute of 'readonly'
	And the element with id 'TaskProcessDescription' has a 'readonly' attribute of 'readonly'
	When I press link with ID 'edit'
	And I wait for '1000' miliseconds
	Then the element with id 'LocationAreaDepartment' has a 'readonly' attribute of 'False'
	And the element with id 'TaskProcessDescription' has a 'readonly' attribute of 'False'
	When I press 'summary' link
	And I wait for '1000' miliseconds
	Then the element with id 'Site' has a 'readonly' attribute of 'False'