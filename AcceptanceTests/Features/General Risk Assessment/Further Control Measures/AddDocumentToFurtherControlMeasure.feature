Feature: UploadDocument
	In order to add documents to a further control measure
	As BusinessSafe user with add General Risk Assesment right
	I want to be able to upload a document as save it against the FCM

Background:
	Given I have logged in as company with id '55881'

@Acceptance
Scenario: Upload Document
	Given I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	And I press 'hazardspeople' link
	And I have added 'Asbestos' to the 'Hazards' risk assessment
	And I press 'SaveButton' button
	And I have waited for the page to reload
	And I press 'controlmeasures' link
	And I wait for '2000' miliseconds
	And I press 'AddFurtherActionTask1' button
	When It is simulated that a document has been uploaded
	Then DocumentsToIncludeTable contains one row for Test Test File 05.txt

@Acceptance
Scenario: Delete Document
	Given I am on the risk assessment page for company '55881'
	And I have created a new risk assessment
	And I press 'hazardspeople' link
	And I have added 'Asbestos' to the 'Hazards' risk assessment
	And I press 'SaveButton' button
	And I press 'controlmeasures' link
	And I press 'AddFurtherActionTask1' button
	And It is simulated that a document has been uploaded
	When The document in the table is deleted
	Then DocumentsToIncludeTable contains no rows
