Feature: Edit Personal Risk Assessment Further Control Measure Task
	In order to complete my PRAs correctly
	As a BSO user with add PRA permissions
	I want to be able to add further control measure tasks

Background:
	Given I have logged in as company with id '55881'

@Acceptance
@changesTitleOfPRATask43
Scenario: Edit Further Control Measure Task PRA
	Given I am on the hazards page of Personal Risk Assessment with id '53' and companyId '55881'
	And I press 'controlmeasures' link
	When I click on the further action task row for task with title 'PRA RA 2'
	And I press 'Edit' button on the further action task row
	And I wait for '4000' miliseconds
	And I have entered 'PRA UPDATED TITLE' into the 'Title' field
	When I press 'FurtherActionTaskSaveButton' button
	Then the personal risk assessment further control measure task with the description 'PRA RA 2' should have title 'PRA UPDATED TITLE'

