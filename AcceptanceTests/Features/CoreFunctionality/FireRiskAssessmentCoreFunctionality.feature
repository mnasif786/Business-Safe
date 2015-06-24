@FRA_core_functionality
Feature: Fire Risk Assessment Core Functionality
	In order to use FRA
	As a business safe user
	I want the core FRA functionality to work

Background:
	Given I have logged in as company with id '55881'

Scenario: Add Fire Risk Assessment
	Given I am on create riskassessment page in area 'FireRiskAssessments' with company Id '55881'
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	When I press 'createSummary' button
	And I have waited for the page to reload
	Then the text box with id 'Title' should contain 'Test Title'
	Then the text box with id 'Reference' should contain 'Test Reference'

Scenario: Set Risk Assessor and Person Appointed
	Given I am on fireriskassessment '55' for company '55881'
	And I have waited for the page to reload
	And I have entered 'Aberdeen' into the 'Site' field
	And I have entered '371' into the 'SiteId' field
	And I have triggered after select drop down event for 'Site'
	And I have entered 'Barry Scott' into the 'RiskAssessor' field
	And I have entered '1' into the 'RiskAssessorId' field
	And I have entered 'Health and Safety Representative' into the 'PersonAppointed' field
	When I press 'saveButton' button
	Then the text box with id 'RiskAssessor' should contain 'Barry Scott (  )'
	Then the text box with id 'PersonAppointed' should contain 'Health and Safety Representative'

Scenario: Select Site and Enter Location and Enter Task Description
	Given I am on fireriskassessment '55' for company '55881'
	And I wait for '1000' miliseconds
	And I have entered 'Barnsley' into the 'Site' field
	And I have entered '379' into the 'SiteId' field
	When I press 'saveButton' button
	And I have waited for the page to reload
	Then the text box with id 'Site' should contain 'Barnsley'
	Given I press 'premisesinformation' link
	And I have waited for the page to reload
	And I have entered 'Test Location' into the 'Location' field
	And I have entered '1' into the 'NumberOfFloors' field
	And I have entered '2' into the 'NumberOfPeople' field
	And I have entered 'Gas' into the 'GasEmergencyShutOff' field
	And I have entered 'Electricity' into the 'ElectricityEmergencyShutOff' field
	And I have entered 'Water' into the 'WaterEmergencyShutOff' field
	When I press 'saveButton' button
	Then the text box with id 'Location' should contain 'Test Location'
	And the text box with id 'NumberOfFloors' should contain '1'
	And the text box with id 'NumberOfPeople' should contain '2'
	And the text box with id 'GasEmergencyShutOff' should contain 'Gas'
	And the text box with id 'ElectricityEmergencyShutOff' should contain 'Electricity'
	And the text box with id 'WaterEmergencyShutOff' should contain 'Water'

Scenario: Select Default Hazard
	Given I am on fireriskassessment '55' for company '55881'
	And I press 'hazards' link
	And I have added 'Fire fighting equipment' to the 'ControlMeasures' risk assessment
	And I have added 'Employees' to the 'PeopleAtRisk' risk assessment
	And I have added 'Wood' to the 'SourcesOfFuel' risk assessment
	And I have added 'Welding' to the 'SourcesOfIgnition' risk assessment
	When I press 'SaveButton' button
	And I have waited for the page to reload
	Then the 'FireSafetyControlMeasure' multi-select contains 'Fire fighting equipment' are in the selected column
	Then the 'PeopleAtRisk' multi-select contains 'Employees' are in the selected column
	Then the 'SourceOfFuels' multi-select contains 'Wood' are in the selected column
	Then the 'SourceOfIgnition' multi-select contains 'Welding' are in the selected column

Scenario: Add Bespoke Hazard
	Given I am on fireriskassessment '55' for company '55881'
	And I press 'hazards' link
	And I wait for '2000' miliseconds
	And I have waited for the page to reload
	And I have entered 'Test Control Measure' into the 'AddFireSafetyControlMeasure' field
	And I press 'AddNewFireSafetyControlMeasure' button
	And I have entered 'Test People at Risk' into the 'AddPersonAtRisk' field
	And I press 'AddNewPersonAtRisk' button
	And I have entered 'Test Sources of Fuel' into the 'AddSourceOfFuel' field
	And I press 'AddNewSourceOfFuel' button
	And I have entered 'Test Sources of Ignition' into the 'AddSourceOfIgnition' field
	And I press 'AddNewSourceOfIgnition' button
	When I press 'SaveButton' button
	And I have waited for the page to reload
	Then the 'FireSafetyControlMeasure' multi-select contains 'Test Control Measure' are in the selected column
	Then the 'PeopleAtRisk' multi-select contains 'Test People at Risk' are in the selected column
	Then the 'SourceOfFuels' multi-select contains 'Test Sources of Fuel' are in the selected column
	Then the 'SourceOfIgnition' multi-select contains 'Test Sources of Ignition' are in the selected column

Scenario: Filling out Fire Checklist should generate Significant Findings and then be able to add Further Control Measure Tasks to each Finding
	Given I am on create riskassessment page in area 'FireRiskAssessments' with company Id '55881'
	And I have entered 'Test Title for FRA SF Test' into the 'Title' field
	And I press 'createSummary' button
	And I have waited for the page to reload
	And I have entered '371' into the 'SiteId' field
	And I have triggered after select drop down event for 'Site'
	And I have entered 'Barry Scott' into the 'RiskAssessor' field
	And I have entered '1' into the 'RiskAssessorId' field
	And I have entered 'Chief Fire Officer' into the 'PersonAppointed' field
	And I press 'firechecklist' link
	And I have waited for the page to reload
	And I wait for '2000' miliseconds
	And I press 'YesNoNotApplicable_147' radio button with the value of 'No'
	And I press 'YesNoNotApplicable_148' radio button with the value of 'No'
	And I press 'significantfindings' link
	And I have waited for the page to reload
	And I wait for '2000' miliseconds
	When I press button with css class 'btn add-further-action-task' first
	#Then popup shows up
	Given I have waited for element 'dialogFurtherControlMeasureTask' to exist
	And I wait for '5000' miliseconds
	When I have entered mandatory further action task data
	And I press 'FurtherActionTaskSaveButton' button
	And I have waited for the page to reload
	Then the further action task should be saved
	When I press button with css class 'btn add-further-action-task' last
	#Then popup shows up
	Given I have waited for element 'dialogFurtherControlMeasureTask' to exist
	And I wait for '5000' miliseconds
	When I have entered mandatory further action task data
	And I press 'FurtherActionTaskSaveButton' button
	And I wait for '5000' miliseconds
	Then the further action task should be saved

Scenario: Complete Fire Checklist
	Given I am on create riskassessment page in area 'FireRiskAssessments' with company Id '55881'
	And I have entered 'Test Title for FRA SF Test' into the 'Title' field
	And I press 'createSummary' button
	And I have waited for the page to reload
	And I have entered '371' into the 'SiteId' field
	And I have triggered after select drop down event for 'Site'
	And I have entered 'Barry Scott' into the 'RiskAssessor' field
	And I have entered '1' into the 'RiskAssessorId' field
	And I have entered 'Chief Fire Officer' into the 'PersonAppointed' field
	And I press 'firechecklist' link
	And I have waited for the page to reload
	And I select 'Na' for questions '147' to '199'
	And I press 'checklist-review-tab' link
	And I wait for '2000' miliseconds
	When I press 'saveButton' button
	And I have waited for the page to reload
	Then the notice 'Checklist has been saved' should be displayed
	When I press 'complete-checklists' button
	And I wait for '5000' miliseconds
	Then the notice 'Checklist has been completed' should be displayed

Scenario: Upload Document
	Given I am on fireriskassessment '55' for company '55881'
	And I press 'attachdocuments' link
	And It is simulated that a document has been uploaded
	When I press 'saveButton' button
	And I have waited for the page to reload
	Then the document should be saved to the risk assessment

Scenario: Add Review Date 
	Given I am on fireriskassessment '55' for company '55881'
	And I press 'review' link
	And I have waited for the page to reload
	And I press 'AddReview' link
	And I have waited for the page to reload
	And I have entered '26/09/2024' into the 'ReviewDate' field
	And I have entered 'Kim Howard' into the 'ReviewingEmployee' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'ReviewingEmployeeId' field
	When I press 'SaveButton' button
	Then the review should be saved

Scenario: Complete a Review
	Given I am on fireriskassessment '55' for company '55881'
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
	And I have waited for the page to reload
	And I wait for '5000' miliseconds
	Then the review should be completed

Scenario: ValidateYesAndNoAnswers
	Given I am on fireriskassessment '55' for company '55881'
	And I press 'firechecklist' link
	And I have waited for the page to reload
	And I click the radiobutton with id 'Yes_147'
	And I click the radiobutton with id 'No_148'
	And I press link with ID 'checklist-review-tab'
	And I wait for '1000' miliseconds
	When I press 'complete-checklists' button
	And I have waited for the page to reload
	Then the error message 'Please enter a comment' is displayed for question '147'
	And the error message 'Please add a Further Control Measure Task' is displayed for question '148'

