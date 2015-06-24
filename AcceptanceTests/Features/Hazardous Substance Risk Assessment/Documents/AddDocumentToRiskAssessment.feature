Feature: Add Document To Hazardous Substance Risk Assessment
	In order to add documents to a hazardous substance risk assessment
	As BusinessSafe user with add Hazardous Substance Risk Assesment right
	I want to be able to upload a document as save it against the risk assessment

Background:
	Given I have logged in as company with id '55881'

@Acceptance
Scenario: Upload Document
	Given I am on description tab with company Id '55881'
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	And I have entered 'Toilet Cleaner' into the 'NewHazardousSubstance' field
	And I have entered '4' into the 'NewHazardousSubstanceId' field
	When I press 'createSummary' button
	Then a new hazardous substance risk assessment should be created with reference 'Test Reference'
	When I press 'attachdocuments' link
	When It is simulated that a document has been uploaded
	When I press 'saveButton' button
	Then the document should be saved to the risk assessment

@Acceptance
Scenario: Delete Document
	Given I am on description tab with company Id '55881'
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	And I have entered 'Toilet Cleaner' into the 'NewHazardousSubstance' field
	And I have entered '4' into the 'NewHazardousSubstanceId' field
	When I press 'createSummary' button
	Then a new hazardous substance risk assessment should be created with reference 'Test Reference'
	When I press 'attachdocuments' link
	When It is simulated that a document has been uploaded
	When I press 'saveButton' button
	Then the document should be saved to the risk assessment
	Given the document in the attached documents table is deleted
	When I press 'saveButton' button
	Then the document should not be on the risk assessment

