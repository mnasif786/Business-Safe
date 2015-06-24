@DeletingAddedDocument
Feature: Deleting Added Documents
	In order to delete added documents
	As a business safe online user with the correct user access rights
	I want to be able to delete added documents and only added documents

@finetune
Scenario: Delete Added Document but not other documents
	Given I have logged in as company with id '55881'
	And I navigate to added documents
	And I press 'Search' button
	When I press 'Delete' link for Document with id of '2'
	When I select 'yes' on confirmation
	Then Document with id of '2' should be deleted
	When I press 'Search' button
	Then should see document row for a document with id of '1'
	And should not be a delete link for a document with id of '1'