@Acceptance
Feature: Mark General Risk Assessment As Draft
	In order to sort general risk assessments
	As a business safe online user
	I want to be able to mark a general risk assessment as a draft

Scenario: Mark risk assessment as draft
	Given I have logged in as company with id '55881'
	And I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	And I wait for '1000' miliseconds
	And the new risk assessment is not marked as draft
	And I have loaded the risk assessment 
	And I wait for '1000' miliseconds
	When I have clicked 'Draft' 
	And reload the current page
	Then the risk assessment should be marked as draft

Scenario: Mark risk assessment as live
	Given I have logged in as company with id '55881'
	And I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	And I wait for '1000' miliseconds
	When I have clicked 'Draft' 
	Then the risk assessment should be marked as live
