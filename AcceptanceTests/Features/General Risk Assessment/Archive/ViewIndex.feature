Feature: General Risk Assessment Archive View Index
	In order to check past general risk assessments
	As a business safe online user
	I want to be able to view archived general risk assessments

Background: 
	Given I have logged in as company with id '55881'

@requiresArchivedGeneralRiskAssessments
@Acceptance
@finetune
Scenario: When Viewing the General Risk Assessment Index Page Can View Table of Archived GRAs
	Given I am on the risk assessment index page for company '55881'
	Then the element with id 'ShowArchivedLink' has visibility of 'true'
	When I press link with ID 'ShowArchivedLink'
	Then the risk assessment table should contain the following data:
	| Reference			  | Title               | Site     | Assigned To | Status   | Completion Due Date |
	| Test Archived RA 01 | Test Archived RA 01 | Aberdeen | Kim Howard  | Archived | 23/06/2016          |
	| Test Archived RA 02 | Test Archived RA 02 | Aberdeen | Kim Howard  | Archived | 23/06/2016          |
	| Test Archived RA 03 | Test Archived RA 03 | Aberdeen | Kim Howard  | Archived | 23/06/2016          |
	| Test Archived RA 04 | Test Archived RA 04 | Aberdeen | Kim Howard  | Archived | 23/06/2016          |
	| Test Archived RA 05 | Test Archived RA 05 | Aberdeen | Kim Howard  | Archived | 23/06/2016          |