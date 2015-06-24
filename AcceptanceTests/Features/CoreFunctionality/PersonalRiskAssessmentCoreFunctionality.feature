@core_functionality
@PRA_core_functionality
Feature: Personal Risk Assessment Core Functionality
	In order to use PRA
	As a business safe user
	I want the core PRA functionality to work

Background:
	Given I have logged in as company with id '55881'

Scenario: Add Title
	Given I am on create riskassessment page in area 'PersonalRiskAssessments' with company Id '55881'
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	When I press 'createSummary' button
	And I have waited for the page to reload
	Then the text box with id 'Title' should contain 'Test Title'
	Then the text box with id 'Reference' should contain 'Test Reference'

Scenario: Select Site and Enter Location and Enter Task Description
	Given I am on personalriskassessment '56' for company '55881'
	And I have entered a unique value into the 'Title' field
	And I have entered 'Barnsley' into the 'Site' field
	And I have entered '379' into the 'SiteId' ch
	And I have entered 'Barry Scott' into the 'RiskAssessor' field
	And I have entered '1' into the 'RiskAssessorId' field
	When I press 'saveButton' button
	And I have waited for the page to reload
	Then the text box with id 'Site' should contain 'Barnsley'
	Given I press 'premisesinformation' link
	And I have waited for the page to reload
	And I have entered 'Test Location' into the 'LocationAreaDepartment' field
	And I have entered 'Test Task' into the 'TaskProcessDescription' field
	When I press 'saveButton' button
	And I have waited for the page to reload
	Then the text box with id 'LocationAreaDepartment' should contain 'Test Location'
	And the text box with id 'TaskProcessDescription' should contain 'Test Task'

@ignore
Scenario: Checklist Generator
	Given I am on personalriskassessment '56' for company '55881'
	And I press 'checklist-generator' link
	And I have waited for the page to reload
	And I wait for '4000' miliseconds
	And I press 'IsForMultipleEmployees' radio button with the value of 'single'
	And I have entered 'Gary Green' into the 'Employee' field
	And I have entered '4d91b7e6-5e25-4620-bfab-d5d4b598cbf7' into the 'EmployeeId' field
	And I have entered 'false' into the 'NewEmployeeEmailVisible' field
	And I press 'IncludeChecklist_1' checkbox
	And I press 'IncludeChecklist_2' checkbox
	And I press 'IncludeChecklist_3' checkbox
	And I press 'IncludeChecklist_4' checkbox
	And I have entered 'Test Checklist Generator Title' into the 'Message' field
	And I wait for '4000' miliseconds
	When I press link with ID 'sendButton'
	And I select 'yes' on confirmation
	And I have waited for the page to reload
	Then the notice 'Employee Checklists Sent' should be displayed

@ignore
Scenario: Checklist Generator With Completition Date And Send Email Notification Checked
	Given I am on personalriskassessment '56' for company '55881'
	And I press 'checklist-generator' link
	And I have waited for the page to reload
	And I wait for '4000' miliseconds
	And I press 'IsForMultipleEmployees' radio button with the value of 'single'
	And I have entered 'Gary Green' into the 'Employee' field
	And I have entered '4d91b7e6-5e25-4620-bfab-d5d4b598cbf7' into the 'EmployeeId' field
	And I have entered 'false' into the 'NewEmployeeEmailVisible' field
	And I press 'IncludeChecklist_1' checkbox
	And I press 'IncludeChecklist_2' checkbox
	And I press 'IncludeChecklist_3' checkbox
	And I press 'IncludeChecklist_4' checkbox
	And I have entered 'Test Checklist Generator Title' into the 'Message' field
	And I have entered '01/01/2030' into the 'CompletionDueDateForChecklists' field
	And I press 'SendCompletedChecklistNotificationEmail' radio button with the value of 'True'
	And I have entered 'test@hotmail.com' into the 'CompletionNotificationEmailAddress' field
	And I wait for '4000' miliseconds
	When I press link with ID 'sendButton'
	And I select 'yes' on confirmation
	And I have waited for the page to reload
	Then the notice 'Employee Checklists Sent' should be displayed	

Scenario: Upload Document
	Given I am on personalriskassessment '56' for company '55881'
	And I press 'attachdocuments' link
	And It is simulated that a document has been uploaded
	When I press 'saveButton' button
	And I have waited for the page to reload
	Then the document should be saved to the risk assessment

Scenario: Add Review Date 
	Given I am on personalriskassessment '56' for company '55881'
	And I press 'review' link
	And I have waited for the page to reload
	And I press 'AddReview' link
	And I have waited for the page to reload
	And I have entered '26/09/2024' into the 'ReviewDate' field
	And I have entered 'Kim Howard' into the 'ReviewingEmployee' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'ReviewingEmployeeId' field
	When I press 'SaveButton' button
	Then the review should be saved

@ignore
Scenario: Complete a Review 
	Given I am on personalriskassessment '56' for company '55881'
	And I press 'review' link
	And I have waited for the page to reload
	And I press 'AddReview' link
	And I have waited for the page to reload
	And I have entered '26/09/2024' into the 'ReviewDate' field
	And I have entered 'Kim Howard' into the 'ReviewingEmployee' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'ReviewingEmployeeId' field
	When I press 'SaveButton' button
	Then the review should be saved
	Given I click on the complete review icon
	And I press 'IsComplete' checkbox
	And I have entered 'Complete Text' into the 'CompletedComments' field
	And I have entered '01/01/2030' into the 'NextReviewDate' field
	And I press 'ReviewSaveButton' button
	Then the review should be completed

Scenario: Select Default Hazard
	Given I am on personalriskassessment '56' for company '55881'
	And I press 'hazardspeople' link
	And I have waited for the page to reload
	And I have added 'Asbestos' to the hazards list
	When I press 'SaveButton' button
	And I have waited for the page to reload
	Then the selected hazards list contains 'Asbestos'

Scenario: Add Bespoke Hazard
	Given I am on personalriskassessment '56' for company '55881'
	And I press 'hazardspeople' link
	And I wait for '2000' miliseconds
	And I have entered 'Test Hazard' into the 'AddHazard' field
	When I press 'AddNewHazard' button
	And I press 'SaveButton' button
	And I have waited for the page to reload
	Then the selected hazards list contains 'Test Hazard'

Scenario: Add Further Control Measure (single)
	Given I am on personalriskassessment '56' for company '55881'
	And I press 'hazardspeople' link
	And I have waited for the page to reload
	And I have added 'Asbsetos' to the hazards list
	When I press 'SaveButton' button
	And I have waited for the page to reload
	Given I press 'controlmeasures' link
	And I have waited for the page to reload
	When I press 'AddFurtherActionTask1' button
	And I have entered mandatory further action task data
	When I press 'FurtherActionTaskSaveButton' button
	Then the further action task should be saved

Scenario: Add Control Measure
	Given I am on personalriskassessment '56' for company '55881'
	And I press 'hazardspeople' link
	And I have waited for the page to reload
	And I have added 'Asbsetos' to the hazards list
	When I press 'SaveButton' button
	And I have waited for the page to reload
	Given I press 'controlmeasures' link
	And I press button Add Control Measure
	And I have entered 'Test Control Measure' into the 'newControlMeasureText' field
	When I press 'saveNewControlMeasure' button
	Then the control measure table should contain 'Test Control Measure'
