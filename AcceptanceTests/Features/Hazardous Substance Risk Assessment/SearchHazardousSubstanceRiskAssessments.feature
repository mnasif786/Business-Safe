Feature: SearchHazardousSubstanceRiskAssessments
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background:
	Given I have logged in as company with id '55881'

Scenario: Search Hazardous Substance Risk Assessments
	Given I am on the Index for Hazardous Substances RiskAssessments for company '55881'
	When I press 'Search' button
	And I wait for '1000' miliseconds
	Then the Hazardous Substance Risk Assessments table should contains data

Scenario: Search Hazardous Substance Risk Assessments by Title
	Given I am on the Index for Hazardous Substances RiskAssessments for company '55881'
	When I have entered 'RA 1' into the 'Title' field
	When I press 'Search' button
	And I wait for '1000' miliseconds
	Then the Hazardous Substance Risk Assessments table should contains data
