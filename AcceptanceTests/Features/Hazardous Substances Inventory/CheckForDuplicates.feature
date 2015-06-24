Feature: CheckForDuplicates
	In order to record the hazardous substances on site
	As a BSO user
	I want to be informed if I've already added a similar Hazardous Substance to my Inventory of Hazardous Substances
	
Background:
	Given I have logged in as company with id '55881'

@createsHazardousSubstance
Scenario: Clicking cancel closes warning and clicking save closes the warning and saves it
	Given I am on the add hazardous substance page for company '55881'
	And I have entered 'Test duplicate' into the 'Name' field
	And I have entered 'Test Supplier 1' into the 'Supplier' field
	And I have entered '1' into the 'SupplierId' field
	And I selected the Global identification standard
	And I have entered '01/01/2012' into the 'SdsDate' field
	And I have entered 'details of my usage' into the 'DetailsOfUse' field
	And I press 'AssessmentRequired' radio button with the value of 'false'
	When I press 'AddEditHazardousSubstanceSaveButton' button
	Then the element with id 'SimilarHazardousSubtanceFound' has visibility of 'true'
	When I press 'CancelDuplicatesButton' button
	Then the element with id 'SimilarHazardousSubtanceFound' has visibility of 'false'
	When I press 'AddEditHazardousSubstanceSaveButton' button
	And I press 'ConfirmDuplicatesButton' button
	Then the element with id 'SimilarHazardousSubtanceFound' has visibility of 'false'
	Then the new hazardous substance 'Test duplicate' should be visible
