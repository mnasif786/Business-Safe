Feature: Remove Personal Risk Assessment Further Control Measure Task
	In order to complete my PRAs correctly
	As a BSO user with add PRA permissions
	I want to be able to delete further control measure tasks

Background:
	Given I have logged in as company with id '55881'

@Acceptance
@finetune
@deletesPRATask43
Scenario: Remove Further Control Measure Task PRA
	Given I am on the hazards page of Personal Risk Assessment with id '53' and companyId '55881'
	And I press 'controlmeasures' link
	When I click on the further action task row for task with title 'PRA RA 2'
	And I press 'Remove' button on the further action task row
	And I select 'yes' on confirmation
	And I wait for '2000' miliseconds
	Then the personal risk assessment further control measure task should be removed with the title 'PRA RA 2'

