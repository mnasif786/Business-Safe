Feature: Archive General Risk Assessment
	In order to close general risk assessments that are no longer needed
	As a business safe online user
	I must be able to archive the GRA

Background:
Given I have logged in as company with id '55881'

@requiresRiskAssessmentReviewAssignedToKimHowardSettingUp
Scenario: Complete Review And Archive GRA While There Are Still outstanding FCM Tasks
	Given I am on the risk assessment index page for company '55881'
	When I press link with ID 'my-planner'
	And I press link with ID 'my-task-list'
	And I have entered 'Kim Howard' into the 'Employee' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'EmployeeId' field
	When user click on GoToTasks for Assigned To Filter
	Then RiskAssessmentReviewTask 'Acceptance Test Risk Assessment' should exist in task list
	And RiskAssessmentReviewTask 'Acceptance Test Risk Assessment' status should be 'Outstanding'
	Given I am on the risk assessment index page for company '55881'
	When I have clicked on the edit risk assessment link for id '39'
	And I press 'review' link
	And I have clicked on the complete review link for a review 
	And I press 'IsComplete' checkbox with client side events
	And I have entered 'hello world' into the 'CompletedComments' field
	And I press 'Archive' checkbox with client side events
	Then the element with id 'outstanding-further-control-measure-tasks-message' has visibility of 'true'