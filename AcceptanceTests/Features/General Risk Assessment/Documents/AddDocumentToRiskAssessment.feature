Feature: Add Document To Risk Assessment
	In order to add documents to a risk assessment
	As BusinessSafe user with add General Risk Assesment right
	I want to be able to upload a document as save it against the risk assessment

Background:
	Given I have logged in as company with id '55881'

@Acceptance
Scenario: Upload and Delete Document
	Given I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	And I press 'attachdocuments' link
	When It is simulated that a document has been uploaded
	When I press 'saveButton' button
	Then the document should be saved to the risk assessment
	Given the document in the attached documents table is deleted
	When I press 'saveButton' button
	Then the document should not be on the risk assessment

