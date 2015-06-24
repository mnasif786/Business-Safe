Feature: Add Document To Further Control Measure
	In order to add documents to a further control measure
	As BusinessSafe user with add General Risk Assesment right
	I want to be able to upload a document as save it against the FCM

Background:
	Given I have logged in as company with id '55881'

@Acceptance
Scenario: Add Document To Further Control Measure
	Given I am on description tab with company Id '55881'
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	And I have entered 'Toilet Cleaner' into the 'NewHazardousSubstance' field
	And I have entered '4' into the 'NewHazardousSubstanceId' field
	And I press 'createSummary' button
	And I press 'controlmeasures' link
	When I press 'AddFurtherControlMeasureTask' button
	And It is simulated that a document has been uploaded
	And I wait for '1000' miliseconds
	Then DocumentsToIncludeTable contains one row for Test Test File 05.txt
	#When The document in the table is deleted
	#Then DocumentsToIncludeTable contains no rows
