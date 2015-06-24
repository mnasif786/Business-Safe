Feature: ViewInventory
	In order to know all hazardous substances i nuse in the company
	As a BSO user
	I want to be able to view the inventory of all substances.

Background:
	Given I have logged in as company with id '55881'


Scenario: ViewHazardousSubstancesInventory
	Given I am on the hazardous substance inventory page for company '55881'
	Then the Hazardous Substances table should contain the following data:
	| Substance Name             | Supplier        | Date Of SDS Sheet | Hazard Classification | Risk No And Phrases | Safety No And Phrases | Usage                 |
	| Test Hazardous Substance 1 | Test Supplier 1 | 01/10/2012        | Global                | RX01, RX02          | SX01, SX02            | Test details of use 1 |
	| Test Hazardous Substance 2 | Test Supplier 1 | 01/10/2012        | Global                | RX02, RX03          | SX02, SX03            | Test details of use 2 |
	| Test Hazardous Substance 3 | Test Supplier 2 | 01/10/2012        | Global                |                     |                       | Test details of use 3 |
	| Toilet Cleaner             | Test Supplier 2 | 01/10/2012        | Global                | RX02, RX03          | SX03                  | Cleaning the toilets  |


Scenario: SearchHazardousSubstancesInventoryOnSupplier
	Given I am on the hazardous substance inventory page for company '55881'
	And I have entered 'Test Supplier 1' into the 'Supplier' field
	And I have entered '1' into the 'SupplierId' field
	When I press 'Search' button
	Then the Hazardous Substances table should contain the following data:
	| Substance Name             | Supplier        | Date Of SDS Sheet | Hazard Classification | Risk No And Phrases | Safety No And Phrases | Usage                 |
	| Test Hazardous Substance 1 | Test Supplier 1 | 01/10/2012        | Global                | RX01, RX02          | SX01, SX02            | Test details of use 1 |
	| Test Hazardous Substance 2 | Test Supplier 1 | 01/10/2012        | Global                | RX02, RX03          | SX02, SX03            | Test details of use 2 |


Scenario: SearchHazardousSubstancesInventoryOnSubstanceNameLike
	Given I am on the hazardous substance inventory page for company '55881'
	And I have entered 'oilet' into the 'SubstanceNameLike' field
	When I press 'Search' button
	Then the Hazardous Substances table should contain the following data:
	| Substance Name             | Supplier        | Date Of SDS Sheet | Hazard Classification | Risk No And Phrases | Safety No And Phrases | Usage                 |
	| Toilet Cleaner             | Test Supplier 2 | 01/10/2012        | Global                | RX02, RX03          | SX03                  | Cleaning the toilets  |

Scenario: WhenHaveRelatedRiskAssessmentsInventoryHasLinksToThem
	Given I am on the hazardous substance inventory page for company '55881'

Scenario: ShowDeleted
	Given I am on the hazardous substance inventory page for company '55881'
	When I press 'showDeletedLink' link
	Then the Hazardous Substances table should contain the following data:
	| Substance Name              | Supplier        | Date Of SDS Sheet | Hazard Classification | Risk No And Phrases | Safety No And Phrases | Usage             |
	| Deleted Hazardous Substance | Test Supplier 3 | 01/10/2012        | Global                |                     |                       | Test show deleted |
	Given I press 'showDeletedLink' link
	Then the Hazardous Substances table should contain the following data:
	| Substance Name             | Supplier        | Date Of SDS Sheet | Hazard Classification | Risk No And Phrases | Safety No And Phrases | Usage                 |
	| Test Hazardous Substance 1 | Test Supplier 1 | 01/10/2012        | Global                | RX01, RX02          | SX01, SX02            | Test details of use 1 |
	| Test Hazardous Substance 2 | Test Supplier 1 | 01/10/2012        | Global                | RX02, RX03          | SX02, SX03            | Test details of use 2 |
	| Test Hazardous Substance 3 | Test Supplier 2 | 01/10/2012        | Global                |                     |                       | Test details of use 3 |
	| Toilet Cleaner             | Test Supplier 2 | 01/10/2012        | Global                | RX02, RX03          | SX03                  | Cleaning the toilets  |
