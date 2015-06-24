Feature: Add Personal Risk Assessment Further Control Measure Task
	In order to complete my PRAs correctly
	As a BSO user with add PRA permissions
	I want to be able to add further control measure tasks

Background:
	Given I have logged in as company with id '55881'

@Acceptance
Scenario: Add Further Control Measure Task PRA
	Given I am on the hazards page of Personal Risk Assessment with id '52' and companyId '55881'
	And I press 'controlmeasures' link
	When I press 'AddFurtherActionTask1' button
	And I have entered 'Test' into the 'Reference' textfield
	And I have entered 'PRA TEST TITLE' into the 'Title' textfield
	And I have entered 'Test' into the 'Description' textfield
	And I have entered '01/01/2018' into the 'TaskCompletionDueDate' field
	And I have entered 'Kim Howard' into the 'TaskAssignedTo' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'TaskAssignedToId' field
	And I press 'FurtherActionTaskSaveButton' button
	Then the personal risk assessment further control measure task should be created with the title 'PRA TEST TITLE'

