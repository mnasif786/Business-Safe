Feature: AttachGeneralGRADocuments
	In order to keep track of documents associated to a risk assessment
	As a bso user
	I want to be able to upload documents to a risk assessment
	
Background:
	Given I have logged in as company with id '55881'

Scenario: In view mode can not upload documents
	Given I am on the risk assessment index page for company '55881'
	And I have clicked on the view risk assessment link for id '39'
	When I press 'attachdocuments' link
Then the element with id 'DocumentsToIncludeTable' has visibility of 'false'
