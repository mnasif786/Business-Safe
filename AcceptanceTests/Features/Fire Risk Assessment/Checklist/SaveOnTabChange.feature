Feature: SaveOnTabChange
	In order to ensure I don't lose my data
	As a BSO user
	I want my checklist to save each time I cange my tab
	
Background:
	Given I have logged in as company with id '55881'

@ignore
#Needs finishing
Scenario: SaveOnTabChange
	Given I am on the Fire Risk Assessment Checklist page with id '55' and companyId '55881'
	And I click 'Yes' for radio button for question with index '1' on section '35'
