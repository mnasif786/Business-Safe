Feature: Viewing Document Library for BusinessSafe System
	In order to view and download documents
	As a BSO user
	I want to be able to view the contents of the document library

Background:
	Given I have logged in as company with id '55881'

Scenario: View all documents
	Given I am on the businesssafe system document library page for company '55881' and documentGroup 'AllDocuments'
	Then the BS system document results table should contain the following data:
	| Document Type | Title        |
	| Type C        | Test Title 3 |
	| Type D        | Test Title 4 |

Scenario: View document link is encrypted
	Given I am on the businesssafe system document library page for company '55881' and documentGroup 'AllDocuments'
	Then the link to view a document's href is encrypted

@ignore 
#watin doesn't appear to support checking tabs
Scenario: Clicking on view document link opens file in new browser window
	Given I am on the businesssafe system document library page for company '55881' and documentGroup 'AllDocuments'
	When I clicked on a view document link
	Then that document is displayed in a new tab