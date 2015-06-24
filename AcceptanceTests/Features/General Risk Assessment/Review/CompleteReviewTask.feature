Feature: CompleteReviewTask
	In order to continually check the validity of a general risk assessment
	As a business safe online user
	I must review the GRA and record that I have reviewed it
	
Background:
	Given I have logged in as company with id '55881'

@requiresRiskAssessmentReviewAssignedToRussellWilliamsSettingUp
@Acceptance
Scenario: Complete Review Form Filled Validates Saves And Creates Next Review and Task
	Given I am on the risk assessment review page for risk assessment with id '39' and companyId '55881'
	When I have clicked on the complete review link for a review 
	And I press 'IsComplete' checkbox with client side events
	And I have entered 'hello world' into the 'CompletedComments' field
	And I click the button with id 'ReviewSaveButton'
	Then validation error is displayed warning archive must be checked or next review date must be entered
	When I have entered '01/01/2050' into the 'NextReviewDate' field
	And I click the button with id 'ReviewSaveButton'
	And I wait for '2000' miliseconds
	Then the review I just completed is viewable
	And the next review is created
	When I press link with ID 'my-planner'
	And I press link with ID 'my-task-list'
	And I have entered 'Russell Williams' into the 'Employee' field
	And I have entered 'd2122fff-1dcd-4a3c-83ae-e3503b394eb4' into the 'EmployeeId' field
	When user click on GoToTasks for Assigned To Filter
	Then RiskAssessmentReviewTask 'Acceptance Test Risk Assessment' should exist in task list
	And RiskAssessmentReviewTask 'Acceptance Test Risk Assessment' status should be 'Outstanding'

	#ignore this test because it fails often aand the complete review functionality is covered elsewhere. The assertion that the task created a sql report should be covered in a unit test.  ALPS
@requiresRiskAssessmentReviewAssignedToRussellWilliamsSettingUp
@Acceptance
@ignore
Scenario: Complete Review generates file of sql report and displays it in added documents
	Given I am on the risk assessment review page for risk assessment with id '39' and companyId '55881'
	When I have clicked on the complete review link for a review 
	And I press 'IsComplete' checkbox with client side events
	And I have entered 'hello world' into the 'CompletedComments' field
	And I have entered '01/01/2050' into the 'NextReviewDate' field
	And I click the button with id 'ReviewSaveButton'
	And I navigate to added documents
	And I have waited for the page to reload
	And I wait for '8000' miliseconds
	And I have entered 'GRA Review Document' into the 'DocumentType' field
	And I have entered '10' into the 'DocumentTypeId' field
	When I press 'Search' button
	And I wait for '4000' miliseconds
	Then the added document results table should contain the risk assessment report

	#ignore this test because it fails often aand the complete review functionality is covered elsewhere. The assertion that the task is set to not required should be covered in a unit test.  ALPS
@requiresRiskAssessmentReviewAssignedToKimHowardSettingUp
@Acceptance
@ignore
Scenario: Complete Review Assigned To Someone Else Completes Review and Sets Task To No Longer Required
	Given I am on the risk assessment review page for risk assessment with id '39' and companyId '55881'
	When I have clicked on the complete review link for a review 
	And I press 'IsComplete' checkbox with client side events
	And I have entered 'hello world' into the 'CompletedComments' field
	And I have entered '01/01/2050' into the 'NextReviewDate' field
	And I click the button with id 'ReviewSaveButton'
	And I have waited for the page to reload
	Given I wait for '5000' miliseconds
	Then the review is saved as complete
	When I press link with ID 'my-planner'
	And I press link with ID 'my-task-list'
	And I have entered 'Kim Howard' into the 'Employee' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'EmployeeId' field
	When user click on GoToTasks for Assigned To Filter
	And I have waited for the page to reload
	And I wait for '3000' miliseconds
	Then RiskAssessmentReviewTask 'Acceptance Test Risk Assessment' should exist in task list
	And RiskAssessmentReviewTask 'Acceptance Test Risk Assessment' status should be 'No Longer Required'
	
@requiresRiskAssessmentReviewAssignedToKimHowardSettingUp
@altersTaskStatusOfRiskAssessment39
@Acceptance
@finetune
Scenario: Complete Review and Archive Risk Assessment With Outstanding Tasks Then Tasks Are Set to No Longer Required
	Given I am on the risk assessment index page for company '55881'
	When I have clicked on the edit risk assessment link for id '39'
	And I press 'hazardspeople' link
	And I have added 'Asbestos' to the 'Hazards' risk assessment
	And I press 'SaveButton' button
	And I press 'controlmeasures' link
	When I press 'AddFurtherActionTask1' button
	And I have entered mandatory further action task data
	When I press 'FurtherActionTaskSaveButton' button
	And I press 'review' link
	And I have clicked on the complete review link for a review 
	And I press 'IsComplete' checkbox with client side events
	And I have entered 'hello world' into the 'CompletedComments' field
	And I press 'Archive' checkbox
	And I click the button with id 'ReviewSaveButton'
	And I have waited for the page to reload
	And I wait for '3000' miliseconds
	Then the review is saved as complete
	When I press link with ID 'my-planner'
	And I press link with ID 'my-task-list'
	And I have entered 'Kim Howard' into the 'Employee' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'EmployeeId' field
	When user click on GoToTasks for Assigned To Filter
	Then RiskAssessmentReviewTask 'Acceptance Test Risk Assessment' should exist in task list
	And RiskAssessmentReviewTask 'Acceptance Test Risk Assessment' status should be 'No Longer Required'
	Then RiskAssessmentReviewTask 'Reference' should exist in task list
	And RiskAssessmentReviewTask 'Reference' status should be 'No Longer Required'
	
@finetune
@altersTaskStatusOfRiskAssessment39
@Acceptance
@requiresRiskAssessmentReviewSettingUp
Scenario: Complete Review Form Filled In With Archive checked And Next Review Date empty sets Risk Assessment Review to Archived
	Given I am on the risk assessment review page for risk assessment with id '39' and companyId '55881'
	When I have clicked on the complete review link for a review 
	And I press 'IsComplete' checkbox with client side events
	And I have entered 'hello world' into the 'CompletedComments' field
	And I press 'Archive' checkbox
	And I click the button with id 'ReviewSaveButton'
	And I have waited for the page to reload
	Then risk assessment should have a status of archived
