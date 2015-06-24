Feature: SleepingAccommodation
	In order to make sure I get a warning
	As a BSO User
	I want to be alerted when clicking 'yes' to the sleeping accomodation question

Background:
	Given I have logged in as company with id '55881'

Scenario: Add two numbers
	Given I am on the Fire Risk Assessment Premises Information page with id '55' and companyId '55881'
	Then the hidden field with id 'PremisesProvidesSleepingAccommodationConfirmed' has null value
	Given I press 'PremisesProvidesSleepingAccommodation' radio button with the value of 'true'
	And The javascript for clicking sleeping accommodation provided yes is fired
	And I wait for '1000' miliseconds
	And I press button with text 'Proceed'
	Then the hidden field with id 'PremisesProvidesSleepingAccommodationConfirmed' should have value 'true'
	Given I press 'PremisesProvidesSleepingAccommodation' radio button with the value of 'false'
	And The javascript for clicking sleeping accommodation provided no is fired
	And I wait for '1000' miliseconds
	Then the hidden field with id 'PremisesProvidesSleepingAccommodationConfirmed' has null value
	Given I press 'PremisesProvidesSleepingAccommodation' radio button with the value of 'true'
	And The javascript for clicking sleeping accommodation provided yes is fired
	And I wait for '1000' miliseconds
	And I press button with text 'Cancel'
	Then I am redirected to the index page for the fire risk assessment for risk assessment id '55' and company id '55881'
