@core_functionality
@GRA_core_functionality
Feature: General Risk Assessment Core Functionality
	In order to use GRA
	As a business safe user
	I want the core GRA functionality to work

Background:
	Given I have logged in as company with id '55881'

Scenario: Add Title
	Given I am on create riskassessment page in area 'GeneralRiskAssessments' with company Id '55881'
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	When I press 'createSummary' button
	And I have waited for the page to reload
	Then the text box with id 'Title' should contain 'Test Title'
	Then the text box with id 'Reference' should contain 'Test Reference'

Scenario: Select Site and Enter Location and Enter Task Description
	Given I am on riskassessment '54' for company '55881'
	And I have entered a unique value into the 'Title' field
	And I have entered 'Barnsley' into the 'Site' field
	And I have entered '379' into the 'SiteId' field
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
	

Scenario: Select Employee and Remove Employee
	Given I am on riskassessment '54' for company '55881'
	And I have entered a unique value into the 'Title' field
	And I press 'premisesinformation' link
	And I wait for '3000' miliseconds
	And I have entered 'Test Location' into the 'LocationAreaDepartment' field
	And I have entered 'Test Task' into the 'TaskProcessDescription' field
	And I have entered 'Kim Howard' with id 'a433e9b2-84f6-4ad7-a89c-050e914dff01' into the employee search field
	When I press 'saveButton' button
	And I have waited for the page to reload
	And I wait for '2000' miliseconds
	Then the 'employeesMultiSelect' select list contains 'Kim Howard' 
	Given I select 'Kim Howard' in Employees list
	And I press 'removeEmployeesBtn' button
	When I press 'saveButton' button
	And I have waited for the page to reload
	And I wait for '2000' miliseconds
	Then the 'employeesMultiSelect' select list should not contain 'Kim Howard' 

@requiresNonEmployeeDaveSmith
Scenario: Select NonEmployee and Remove NonEmployee	
	Given I am on riskassessment '54' for company '55881'
	And I have entered a unique value into the 'Title' field
	And I press 'premisesinformation' link
	And I wait for '3000' miliseconds
	And I have entered 'Test Location' into the 'LocationAreaDepartment' field
	And I have entered 'Test Task' into the 'TaskProcessDescription' field
	And I have entered 'Dave Smith' with id '1' into the non employee search field
	When I press 'saveButton' button
	And I have waited for the page to reload
	And I wait for '5000' miliseconds
	Then the 'nonEmployeesMultiSelect' select list contains 'Dave Smith' 
	Given I select 'Dave Smith' in NonEmployees list
	And I press 'removeNonEmployeesBtn' button
	When I press 'saveButton' button
	And I have waited for the page to reload
	And I wait for '2000' miliseconds
	Then the 'nonEmployeesMultiSelect' select list should not contain 'Dave Smith' 

Scenario: Select Default Hazard
	Given I am on riskassessment '54' for company '55881'
	And I have entered a unique value into the 'Title' field
	And I press 'hazardspeople' link
	And I have waited for the page to reload
	And I have added 'Asbestos' to the hazards list
	When I press 'SaveButton' button
	And I have waited for the page to reload
	Then the selected hazards list contains 'Asbestos'

Scenario: Add Bespoke Hazard
	Given I am on riskassessment '54' for company '55881'
	And I have entered a unique value into the 'Title' field
	And I press 'hazardspeople' link
	And I have waited for the page to reload
	And I have entered 'Test Hazard' into the 'AddHazard' textfield
	When I press 'AddNewHazard' button
	And I press 'SaveButton' button
	And I have waited for the page to reload
	Then the selected hazards list contains 'Test Hazard'

Scenario: Select People at Risk
	Given I am on riskassessment '54' for company '55881'
	And I have entered a unique value into the 'Title' field
	And I press 'hazardspeople' link
	And I have added 'Employees' to the 'PeopleAtRisk' risk assessment
	When I press 'SaveButton' button
	And I have waited for the page to reload
	Then the 'PeopleAtRisk' multi-select contains 'Employees' are in the selected column

Scenario: Add Bespoke People at Risk
	Given I am on riskassessment '54' for company '55881'
	And I have entered a unique value into the 'Title' field
	And I press 'hazardspeople' link
	And I have waited for the page to reload
	And I have entered 'Test People At Risk' into the 'AddPersonAtRisk' textfield
	When I press 'AddNewPersonAtRisk' button
	And I press 'SaveButton' button
	And I have waited for the page to reload
	Then the 'PeopleAtRisk' multi-select contains 'Test People At Risk' are in the selected column

Scenario: Add Control Measure
	Given I am on riskassessment '54' for company '55881'
	And I have entered a unique value into the 'Title' field
	And I press 'hazardspeople' link
	And I have waited for the page to reload
	And I have added 'Asbestos' to the hazards list
	When I press 'SaveButton' button
	And I have waited for the page to reload
	Given I press 'controlmeasures' link
	And I have waited for the page to reload
	And I press button Add Control Measure
	And I have entered 'Test Control Measure' into the 'newControlMeasureText' field
	When I press 'saveNewControlMeasure' button
	Then the control measure table should contain 'Test Control Measure'
	
Scenario: Upload Document
	Given I am on riskassessment '54' for company '55881'
	And I have entered a unique value into the 'Title' field
	And I press 'attachdocuments' link
	And It is simulated that a document has been uploaded
	When I press 'saveButton' button
	And I have waited for the page to reload
	Then the document should be saved to the risk assessment	


Scenario: Add Review Date 
	Given I am on riskassessment '54' for company '55881'
	And I press 'review' link
	And I have waited for the page to reload
	And I press 'AddReview' link
	And I have waited for the page to reload
	And I have entered '26/09/2024' into the 'ReviewDate' field
	And I have entered 'Kim Howard' into the 'ReviewingEmployee' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'ReviewingEmployeeId' field
	When I press 'SaveButton' button
	Then the review should be saved
	
Scenario: Add Further Control Measure (single)
	Given I am on riskassessment '54' for company '55881'
	And I press 'hazardspeople' link
	And I have waited for the page to reload
	And I have added 'Asbestos' to the hazards list
	When I press 'SaveButton' button
	And I have waited for the page to reload
	Given I press 'controlmeasures' link
	And I have waited for the page to reload
	When I press 'AddFurtherActionTask1' button
	And I have entered mandatory further action task data
	When I press 'FurtherActionTaskSaveButton' button
	Then the further action task should be saved

Scenario: Add Further Control Measure (reoccurring)
	Given I am on riskassessment '54' for company '55881'
	And I press 'hazardspeople' link
	And I have waited for the page to reload
	And I have added 'Asbestos' to the hazards list
	When I press 'SaveButton' button
	And I have waited for the page to reload
	Given I press 'controlmeasures' link
	And I have waited for the page to reload
	When I press 'AddFurtherActionTask1' button
	Then I press IsReoccurring checkbox for the HSRA FCM Task
	When I have entered mandatory further control measure task data
	And I press 'FurtherActionTaskSaveButton' button
	Then the reoccurring further control measure task should be saved
	