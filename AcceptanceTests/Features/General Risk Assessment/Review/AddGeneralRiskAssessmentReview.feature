Feature: Add General Risk Assessment Review
	In order to continually check the validity of a general risk assessment
	As a business safe online user
	I want to be able to create a pending review record for that general risk assessment 

Background:
	Given I have logged in as company with id '55881'

@Acceptance
Scenario: On first viewing of review page clicking save, saves the review and notifies the user and can be edited.
	Given I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	And I wait for '1000' miliseconds
	And I press 'review' link
	And I press 'AddReview' link
	And I have entered '26/09/2024' into the 'ReviewDate' field
	And I have entered 'Kim Howard' into the 'ReviewingEmployee' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'ReviewingEmployeeId' field
	When I press 'SaveButton' button
	Then the review should be saved
	And a success message should be displayed
	When I press 'EditButton' for the only risk assessment review
	And the 'ReviewDate' field is cleared
	And I have entered '27/09/2024' into the 'ReviewDate' field
	And I have entered 'Glen Ross' into the 'ReviewingEmployee' field
	And I have entered '9D24AE1A-6645-45FC-9D50-8FC70BABEB89' into the 'ReviewingEmployeeId' field
	When I press 'SaveButton' button
	And I have waited for the page to reload
	Then the editied review should be saved

@Acceptance
Scenario: Creating risk assessment creates RiskAssessmentReviewTask in the task list
	Given I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	And I wait for '1000' miliseconds
	And I have entered 'Aberdeen' into the 'Site' field	
	And I have entered '378' into the 'SiteId' field
	When I press 'saveButton' button
	And I have waited for the page to reload
	And I press 'review' link
	And I press 'AddReview' link
	And I have entered '26/09/2024' into the 'ReviewDate' field
	And I have entered 'Kim Howard' into the 'ReviewingEmployee' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'ReviewingEmployeeId' field
	And I press 'SaveButton' button
	And I navigate to task list
	And I have entered 'Kim Howard' into the 'Employee' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'EmployeeId' field
	When user click on GoToTasks for Assigned To Filter
	Then RiskAssessmentReviewTask 'GRA Review' should exist in task list
	When complete review task is clicked for task with description 'GRA Review'
	Then I should be redirected to the general risk assessment page

Scenario: If assign review to employee with General User role then show alert
	Given I am on riskassessment '1' for company '55881'
	And I press 'review' link
	And I have waited for the page to reload
	And I press 'AddReview' link
	And I have entered 'John Connor' into the 'ReviewingEmployee' field
	And I have entered '8929BAC1-5403-4837-A72F-A077AA0C4E81' into the 'ReviewingEmployeeId' field
	And I have triggered after select drop down event for 'ReviewingEmployee'
	Then the element with id 'employee-cannot-complete-review' has visibility of 'true'
	And the element with class 'employee-not-user-alert-message hide' has visibility of 'false'

Scenario: If assign review to employee with no User account then show alert
	Given I am on riskassessment '1' for company '55881'
	And I press 'review' link
	And I have waited for the page to reload
	And I press 'AddReview' link
	And I have entered 'Gary Green' into the 'ReviewingEmployee' field
	And I have entered '4D91B7E6-5E25-4620-BFAB-D5D4B598CBF7' into the 'ReviewingEmployeeId' field
	And I have triggered after select drop down event for 'ReviewingEmployee'
	Then the element with class 'employee-not-user-alert-message hide' has visibility of 'true'
	And the element with id 'employee-cannot-complete-review' has visibility of 'false'