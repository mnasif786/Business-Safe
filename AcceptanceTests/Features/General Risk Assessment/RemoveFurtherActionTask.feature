	Feature: Remove Further Action Tasks From Risk Assessment
	In order to remove further action tasks from a general risk assessment
	As a business safe online user
	I want to be able to remove a further action tasks

Background:
	Given I have logged in as company with id '55881'

@Acceptance
@RemoveFurtherActionTask
Scenario: Remove Further Action Task To Risk Assessment
	Given I am on the risk assessment index page for company '55881'
	And I have clicked on the edit risk assessment link for id '39'
	And I press 'controlmeasures' link
	And I have waited for the page to reload
	And I click on the further action task row for id '11'
	When I press 'Remove' button on the further action task row
	And I select 'yes' on confirmation
	Then the further action task with id '11' should be deleted
	
@Acceptance
@RemoveFurtherActionTask
Scenario: Removing a reoccurring further action task 
	Given I am on the risk assessment index page for company '55881'
	And I have clicked on the edit risk assessment link for id '39'
	And I press 'controlmeasures' link
	And I have waited for the page to reload
	And I click on the further action task row for id '12'
	When I press 'Remove' button on the further action task row
	And I select 'yes' on confirmation
	And I wait for '2000' miliseconds
	Then the further action task with id '12' should be deleted

@Acceptance
Scenario: Removing a reoccurring further action task with previous tasks displays error message
	Given there is a gra roccurring task with previous completed task for risk assessment hazard '29'
	And I am on the general risk assessment control measures page for company '55881' and risk assessment '39'
	And I click on the further action task row for task with title 'Reoccurring task with completed tasks'
	When I press 'Remove' button on the further action task row
	And I select 'yes' on confirmation
	Then the element with id 'dialogDeleteFurtherControlMeasureTaskResponse' has visibility of 'true' 