@Acceptance
Feature: BUG 1671 Should not be able to delete or edit system defaults
	In order to keep system integrity
	As a business safe online user
	I should not be able to delete or edit system defaults

Background:
    Given I have logged in as company with id '55881'	

Scenario: System Defaults Not Editable or Deletable
	Given I am on the Company default page
	And I am working on Hazards
	Then should not be able to edit system defaults
	Then should not be able to delete system defaults
