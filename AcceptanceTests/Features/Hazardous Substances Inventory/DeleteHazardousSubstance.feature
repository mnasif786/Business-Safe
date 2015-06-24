@DeletingHazardousSubstance
Feature: Deleting Hazardous Substance
	In order to delete hazardous substances
	As a business safe online user with the correct user access rights
	I want to be able to delete hazardous substances


Scenario: Delete Hazardous Substance
	Given I have logged in as company with id '55881'
	Given I am on the search hazardous substances page for company '55881'
	When I press 'Delete' link for hazardous substance with id '3'
	And I wait for '2000' miliseconds
	And I select 'yes' on confirmation
	And I wait for '1000' miliseconds
	Then the hazardous substance with id '3' should then be deleted
	Given I press 'showDeletedLink' link
	And I press 'Reinstate' link for hazardous substance with id '3'
	And I select 'yes' on confirmation
	When I press 'showDeletedLink' link
	Then the Hazardous Substances table should contain the following data:
	| Substance Name             | Supplier        | Date Of SDS Sheet | Hazard Classification | Risk No And Phrases | Safety No And Phrases | Usage                 |
	| Test Hazardous Substance 1 | Test Supplier 1 | 01/10/2012        | Global                | RX01, RX02          | SX01, SX02            | Test details of use 1 |
	| Test Hazardous Substance 2 | Test Supplier 1 | 01/10/2012        | Global                | RX02, RX03          | SX02, SX03            | Test details of use 2 |
	| Test Hazardous Substance 3 | Test Supplier 2 | 01/10/2012        | Global                |                     |                       | Test details of use 3 |
	| Toilet Cleaner             | Test Supplier 2 | 01/10/2012        | Global                | RX02, RX03          | SX03                  | Cleaning the toilets  |