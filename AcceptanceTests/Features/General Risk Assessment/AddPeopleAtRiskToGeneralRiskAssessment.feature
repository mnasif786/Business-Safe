@Acceptance
Feature: Add People At Risk To Risk Assessment
	In order to add people at risk to general risk assessments
	As a business safe online user
	I want to be able to add people at risk

Background:
	Given I have logged in as company with id '55881'

Scenario: Adding Person At Risk
	Given I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	And I press 'hazardspeople' link
	And I have selected the option label 'Employees' from multi-select control 'people-at-risk' 
	Then the 'PeopleAtRisk' multi-select contains 'Employees' are in the selected column
	Given I press 'SaveButton' button
	Then the person at risk with id '1' should be saved to the general risk assessment

Scenario: Adding Person At Risk With Display Message
	Given I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	And I press 'hazardspeople' link
	And I have selected the option label 'New and Expectant Mothers' from multi-select control 'people-at-risk' 
	And I have selected the option label 'Children & Young Persons' from multi-select control 'people-at-risk' 
	Then the element with id 'people-at-risk-alert' has visibility of 'true'
	When I have deselected option label 'Children & Young Persons' from multi-select control 'people-at-risk'
	Then the element with id 'people-at-risk-alert' has visibility of 'true'
	When I have deselected option label 'New and Expectant Mothers' from multi-select control 'people-at-risk'
	Then the element with id 'people-at-risk-alert' has visibility of 'false'

Scenario: Adding Person At Risk and Saving With Different Buttons
	Given I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	And I press 'hazardspeople' link
	And I have waited for the page to reload
	And I have entered 'Person at Risk 1' into the 'AddPersonAtRisk' field
	When I press 'AddNewPersonAtRisk' button
	And I press 'SaveButton' button
	And I have waited for the page to reload
	Then the 'PeopleAtRisk' multi-select contains 'Person at Risk 1' are in the selected column
	Given I have entered 'Person at Risk 2' into the 'AddPersonAtRisk' field
	When I press 'AddNewPersonAtRisk' button
	And I press 'nextBtn' button
	And I have waited for the page to reload
	And I press 'hazardspeople' link
	Then the 'PeopleAtRisk' multi-select contains 'Person at Risk 1,Person at Risk 2' are in the selected column
	Given I have entered 'Person at Risk 3' into the 'AddPersonAtRisk' field
	When I press 'AddNewPersonAtRisk' button
	And I press 'controlmeasures' link
	And I have waited for the page to reload
	And I press 'hazardspeople' link
	And I have waited for the page to reload
	Then the 'PeopleAtRisk' multi-select contains 'Person at Risk 1,Person at Risk 2,Person at Risk 3' are in the selected column