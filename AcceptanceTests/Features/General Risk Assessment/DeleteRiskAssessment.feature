Feature: DeleteRiskAssessment
	Delete risk assessment that has also added tasks and reviews

Background:
	Given I have logged in as company with id '55881'
  
@Acceptance
Scenario: Deleting risk assessment deletes associated tasks
	Given I am on the risk assessment page for company '55881'
	And I have created a new risk assessment for deletion
	And I wait for '1000' miliseconds
	And I press 'hazardspeople' link
	And I have added 'Asbestos' to the 'Hazards' risk assessment
	And I press 'SaveButton' button
	And I press 'controlmeasures' link
	And I have waited for the page to reload
	And I press 'AddFurtherActionTask1' button
	And I have entered mandatory further action task data into the risk assessment for deletion
	And I press 'FurtherActionTaskSaveButton' button
	And I press 'review' link
	And I press 'AddReview' link
	And I have entered '26/09/2024' into the 'ReviewDate' field
	And I have entered 'Glen Ross' into the 'ReviewingEmployee' field
	And I have entered '9D24AE1A-6645-45FC-9D50-8FC70BABEB89' into the 'ReviewingEmployeeId' field
	And I press 'SaveButton' button
	And I navigate to risk assessments
	When I click to delete the risk assessment with reference 'DEL001'
	Then the element with id 'dialogDeleteRiskAssessmentWithAssociatedTasks' has visibility of 'true'
	When I click confirm button on delete
	And I navigate to task list
	And I have entered 'Glen Ross' into the 'Employee' field
	And I have entered '9D24AE1A-6645-45FC-9D50-8FC70BABEB89' into the 'EmployeeId' field
	And user click on GoToTasks for Assigned To Filter
	Then task list has no rows