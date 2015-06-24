Feature: Searching Document Library
	In order to view and download documents
	As a BSO user
	I want to be able to search the document library

Background:
	Given I have logged in as company with id '55881'

Scenario: Search for a document by document type
	Given I am on the document library page for company '55881' and documentGroup 'ReferenceLibrary'
	And I have entered 'Type A' into the 'DocumentType' field
	And I have entered '124' into the 'DocumentTypeId' field
	When I press 'Search' button
	Then the document results table should contain the following data:
	| Document Type | Document SubType | Title        | Description        |
	| Type A        | Sub Type A       | Test Title 1 | Test Description 1 |

Scenario: Search for a document by title
	Given I am on the document library page for company '55881' and documentGroup 'ReferenceLibrary'
	And I have entered 'Test Title 1' into the 'Title' field
	When I press 'Search' button
	Then the document results table should contain the following data:
	| Document Type | Document SubType | Title        | Description        |
	| Type A        | Sub Type A       | Test Title 1 | Test Description 1 |