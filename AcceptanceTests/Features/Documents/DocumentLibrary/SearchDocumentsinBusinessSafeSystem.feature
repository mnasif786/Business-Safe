Feature: Searching Document Library for BusinessSafe System Docs
	In order to view and download documents
	As a BSO user
	I want to be able to search the document library

Background:
	Given I have logged in as company with id '55881'

Scenario: Search for a document by document type
	Given I am on the businesssafe system document library page for company '55881' and documentGroup 'AllDocuments'
	And I have entered 'Type C' into the 'DocumentType' field
	And I have entered '136' into the 'DocumentTypeId' field
	When I press 'Search' button
	Then the BS system document results table should contain the following data:
	| Document Type | Title        |
	| Type C        | Test Title 3 |

Scenario: Search for a document by title
	Given I am on the businesssafe system document library page for company '55881' and documentGroup 'AllDocuments'
	And I have entered 'Test Title 3' into the 'Title' field
	When I press 'Search' button
	Then the BS system document results table should contain the following data:
	| Document Type | Title        |
	| Type C        | Test Title 3 |