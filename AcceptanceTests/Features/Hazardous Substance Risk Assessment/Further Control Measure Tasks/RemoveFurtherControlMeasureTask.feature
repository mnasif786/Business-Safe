Feature: Remove Further Control Measure Task From Hazardous Substance Risk Assessment
	In order to remove further control measure tasks from a hazardous substance risk assessment
	As a business safe online user
	I want to be able to remove a further control measure task

Background:
	Given I have logged in as company with id '55881'

@Acceptance
@RemoveFurtherActionTask
Scenario: Remove Further Control Measure Task From Risk Assessment
	Given I am on the hazardous substance risk assessment index page for company '55881'
	And I have clicked on the edit risk assessment link for id '45'
	And I press 'controlmeasures' link
	And I have waited for the page to reload
	And I click on the further action task row for id '20'
	When I press 'Remove' button on the further action task row
	And I select 'yes' on confirmation
	Then the further action task with id '20' should be deleted

@Acceptance
@RemoveFurtherActionTask
@finetune
Scenario: Removing a reoccurring further action task with previous tasks displays error message
	Given there is an hsra roccurring task with previous completed task for risk assessment '42'
	And I am on the hazardous substances control measures page for company '55881' and risk assessment '42'
	And I click on the further action task row for task with title 'Reoccurring task with completed tasks'
	When I press 'Remove' button on the further action task row
	And I select 'yes' on confirmation
	Then the element with id 'dialogDeleteFurtherControlMeasureTaskResponse' has visibility of 'true' 
