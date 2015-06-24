Feature: SearcReferenceLibraryDocuments
	In order find Reference Library Documents
	I want to be able to search the tab

Background:
	Given I have logged in as company with id '55881'

@Acceptance
Scenario: OnlyCorrectTypesForReferenceLibraryDisplayed
	Given I am on the Reference Documents Library system page for company '55881'
	Then The Document Type drop down contains the correct types:
	| DocumentType        |
	| --Select Option-- |
	| Type A |
	| Type B |
	
Scenario: OnlyCorrectTypesForBusinessSafeSystemDisplayed
	Given I am on the businesssafe system document library page for company '55881' and documentGroup 'AllDocuments'
	Then The Document Type drop down contains the correct types:
	| DocumentType        |
	| --Select Option-- |
	| Type C |
	| Type D |