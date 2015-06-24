@Acceptance
Feature: View Further Control Measure Tasks for Risk Assessment
	In order to check further control measure tasks for a hazardous substance risk assessment
	As a business safe online user
	I want to be able to view completed further control measure tasks

Background:
	Given I have logged in as company with id '55881'

@finetune
Scenario: Viewing completed HSRA FCM Task show Task in View Mode
	Given I am on the hazardous substance risk assessment index page for company '55881'
	And I have clicked on the edit risk assessment link for id '42'
	And I press 'controlmeasures' link
	And I click on the further action task row for id '21'
	When I press View button on the further control measure task row
	Then the element with id 'dialogFurtherControlMeasureTask' has visibility of 'true'
	And the text box with id 'Reference' should contain 'Edit Task Test'
	And the text box with id 'Title' should contain 'Edit Task Test'
	And the text box with id 'TaskDescription' should contain 'Edit Task Test'
	And the text box with id 'TaskAssignedTo' should contain 'Barry Brown'
	And the text box with id 'TaskCompletionDueDate' should contain '31/10/2012'	
	And the element with id 'Reference' has a 'readonly' attribute of 'readonly'
	And the element with id 'Title' has a 'readonly' attribute of 'readonly'
	And the element with id 'TaskDescription' has a 'readonly' attribute of 'readonly'
	And the element with id 'TaskAssignedTo' has a 'readonly' attribute of 'readonly'
	And the element with id 'TaskCompletionDueDate' has a 'readonly' attribute of 'readonly'