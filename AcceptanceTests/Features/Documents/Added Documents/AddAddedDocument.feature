Feature: AddDocumentToAddedDocuments
	In order to add make documents available to all users
	As BusinessSafe user with Upload Documents Permissions
	I want to be able to upload a document to added documents

@createsAddedDocument
Scenario: Can add document to Added Documents
	Given I have logged in as company with id '55881'
	And I navigate to added documents
	And I press link with ID 'AddNewDocumentsLink'
	And It is simulated that an added document has been uploaded
	And I have entered 'Checklist' into the 'DocumentGridRow_53984_DocumentType' field
	And I have entered 'Barnsley' into the 'DocumentGridRow_53984_Site' field
	And I have entered 'test title' into the 'DocumentGridRow_53984_Title' field
	And I have entered 'test description' into the 'DocumentGridRow_53984_Description' field
	When I press 'SaveAddedDocuments' button
	Then the file is saved to the database