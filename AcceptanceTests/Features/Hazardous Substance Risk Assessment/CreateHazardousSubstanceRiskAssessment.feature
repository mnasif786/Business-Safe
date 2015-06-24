@Acceptance
@HazardousSubstanceRiskAssessments
Feature: Create Hazardous Substance Risk Assessment
	In order to assign assessors to hazardous substance risk assessment
	As a business safe online user
	I want to be able to create a new hazardous substance risk assessment

Background:
	Given I have logged in as company with id '55881'
  
Scenario: Hazardous Substance Risk Assessment is created successfully
	Given I am on the create hazardous substance risk assessment page for company '55881'
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	And I have entered 'Toilet Cleaner' into the 'NewHazardousSubstance' field
	And I have entered '4' into the 'NewHazardousSubstanceId' field
	When I press 'createSummary' button
	Then a new hazardous substance risk assessment should be created with reference 'Test Reference'
	Given I wait for '2000' miliseconds
	And I have entered 'Aberdeen' into the 'Site' field
	And I have entered '378' into the 'SiteId' field
	And I have triggered after select drop down event for 'Site'
	And I have entered 'A Risk Assessor' into the 'RiskAssessor' field
	And I have entered '1' into the 'RiskAssessorId' field
	When I press 'saveButton' button
	And I wait for '2000' miliseconds
	Given I press 'description' link
	And I wait for '4000' miliseconds
	Then the element with id 'HazardousSubstance' should contain 'Toilet Cleaner'
	And the element with class 'Global acute-toxicity' has visibility of 'true'
	And the element with class 'Global caution' has visibility of 'true'
	And the element with class 'Global corrosive' has visibility of 'true'
	And the risk phrase 'RX02 Test Risk Phrase 2' is visible
	And the risk phrase 'RX03 Test Risk Phrase 3' is visible
	When I press 'assessment' link
	And I have waited for the page to reload	
	Then Hazard group 'B' is selected
	Given value 'Medium' is selected for radio button 'Quantity'
	And value 'Liquid' is selected for radio button 'MatterState'
	And value 'High' is selected for radio button 'DustinessOrVolatility'
	And value 'true' is selected for radio button 'HealthSurveillanceRequired'
	When I press 'saveButton' button
	Then the element with class 'alert alert-success' has visibility of 'true'
	And the element with id 'WorkApproach' should contain 'Engineering Controls'
	And radio button 'Quantity' has value of 'Medium' and is 'true'
	And radio button 'MatterState' has value of 'Liquid' and is 'true'
	And radio button 'DustinessOrVolatility' has value of 'High' and is 'true'
	And radio button 'HealthSurveillanceRequired' has value of 'true' and is 'true'
	Given I press 'controlmeasures' link
	And I have waited for the page to reload	
	And I press button Add Control Measure
	And I have entered 'test control measure' into the 'newControlMeasureText' field
	When I press 'saveNewControlMeasure' button
	Then the control measure 'test control measure' should be saved to the hazardous substance risk assessment
	Given I highlight the newly created control measure
	And I press 'editControlMeasure' button
	And I have entered 'test control measure updated' into the 'updateControlMeasureText' field
	When I press 'saveNewControlMeasure' button
	Then the control measure 'test control measure updated' should be saved to the hazardous substance risk assessment
	Given I highlight the newly created control measure
	When I press 'removeControlMeasure' button
	Then the control measure 'test control measure updated' should be removed from the hazardous substance risk assessment

Scenario: Clicking Create Draft saves HSRA and redirects to index
	Given I am on description tab with company Id '55881'
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	And I have entered 'Toilet Cleaner' into the 'NewHazardousSubstance' field
	And I have entered '4' into the 'NewHazardousSubstanceId' field
	When I press 'createDraft' button
	Then a new hazardous substance risk assessment should be created with reference 'Test Reference'
	And I should be redirected to the hazardous substance risk assessment index page


