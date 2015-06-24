Feature: Viewing Document Library
	In order to view and download documents
	As a BSO user
	I want to be able to view the contents of the document library

Background:
	Given I have logged in as company with id '55881'

Scenario: View all documents
	Given I am on the document library page for company '55881' and documentGroup 'AllDocuments'
	Then the document results table should contain the following data:
	| Document Type | Document SubType | Title        | Description        |
	| Type A        | Sub Type A       | Test Title 1 | Test Description 1 |
	| Type B        | Sub Type B       | Test Title 2 | Test Description 2 |

Scenario: View document link is encrypted
	Given I am on the document library page for company '55881' and documentGroup 'AllDocuments'
	Then the link to view a document's href is encrypted

@ignore 
#watin doesn't appear to support checking tabs
Scenario: Clicking on view document link opens file in new browser window
	Given I am on the document library page for company '55881' and documentGroup 'AllDocuments'
	When I clicked on a view document link
	Then that document is displayed in a new tab