@Acceptance
Feature: Cannot Copy PRAs
	In order to maintain seperation between the potentially sensitive data in a PRA
	As a user
	I should not be able to easily copy risk assessments 

Background:
	Given I have logged in as company with id '55881'
	And I am on the personal risk assessments page for company '55881'

Scenario: Cannot use Copy PRA link
	Then the element with id 'CopyPersonalRiskAssessmentLink' has visibility of 'false'