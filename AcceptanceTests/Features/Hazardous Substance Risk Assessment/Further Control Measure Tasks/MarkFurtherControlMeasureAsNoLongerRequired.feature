Feature: MarkFurtherControlMeasureAsNoLongerRequired
	In order to end a reoccuring further control measure task
	As a BSO user
	I want to be able to mark the task as No Longer Required.

@Acceptance
@finetune
Scenario: MarkFurtherControlMeasureAsNoLongerRequired
	Given A reoccuring further control measure task exisits for hazardous substance risk assessment '42'
	And I have logged in as company with id '55881'
	And I am on the hazardous substances control measures page for company '55881' and risk assessment '42'
	And I click on the further action task row for task with title 'Task To Mark As No Longer Required'
	And I press 'edit-fcm-task' button
	And I press 'FurtherActionTaskNoLongerRequiredButton' button
	And I select confirm mark as no longer required
	Then the further action task with title 'Task To Mark As No Longer Required' should be no longer required