Feature: BUG FRA 07
	In order to avoid exceptions when I enter massive names into the person appointed
	As a BSO User
	I want to see a the person appointed text cut down to allowed length

Background:
	Given I have logged in as company with id '55881'

@finetune
Scenario: Enter more than 100 characters should cut down to allowed length
	Given I am on the Fire Risk Assessment Summary page with id '55' and companyId '55881'
	And I have waited for the page to reload
	And I have entered '01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567891' into the 'PersonAppointed' field
	When I press 'saveButton' button
	And I wait for '4000' miliseconds
	Then the text box with id 'PersonAppointed' should contain '0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789'
