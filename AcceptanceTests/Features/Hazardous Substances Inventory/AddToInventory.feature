Feature: AddToInventory
	In order to record the hazardous substances on site
	As a BSO user
	I want to be add a Hazardous Substance to my Inventory of Hazardous Substances
	
Background:
	Given I have logged in as company with id '55881'

@createsHazardousSubstance
Scenario: When I click Add Hazardous Substance I can save the hazardous substance then edit it
	Given I am on the add hazardous substance page for company '55881'
	And I press 'AddEditHazardousSubstanceCancelButton' link
	And I have waited for the page to reload
	Then I should be redirected to the inventory page
	Given I am on the add hazardous substance page for company '55881'
	And I have entered 'My completely new one' into the 'Name' field
	And I have entered 'Test Supplier 1' into the 'Supplier' field
	And I have entered '1' into the 'SupplierId' field
	And I have entered '01/01/2012' into the 'SdsDate' field
	And I selected the Global identification standard
	And I have selected the option label 'RX01 Test Risk Phrase 1' from multi-select control 'risk-phrase' 
	And I have selected the option label 'RX03 Test Risk Phrase 3' from multi-select control 'risk-phrase' 
	And I have selected the option label 'SX01 Test Safety Phrase 1' from multi-select control 'safety-phrase' 
	And I have selected the option label 'SX03 Test Safety Phrase 3' from multi-select control 'safety-phrase'
	And I have entered 'details of my usage' into the 'DetailsOfUse' field
	And I press 'AssessmentRequired' radio button with the value of 'false'
	When I press 'AddEditHazardousSubstanceSaveButton' button
	Then I should be redirected to the inventory page
	And the Hazardous Substances table should contain the following data:
	| Substance Name             | Supplier        | Date Of SDS Sheet | Hazard Classification | Risk No And Phrases | Safety No And Phrases | Usage                 |
	| My completely new one      | Test Supplier 1 | 01/01/2012        | Global                | RX01, RX03          | SX01, SX03            | details of my usage   |
	| Test Hazardous Substance 1 | Test Supplier 1 | 01/10/2012        | Global                | RX01, RX02          | SX01, SX02            | Test details of use 1 |
	| Test Hazardous Substance 2 | Test Supplier 1 | 01/10/2012        | Global                | RX02, RX03          | SX02, SX03            | Test details of use 2 |
	| Test Hazardous Substance 3 | Test Supplier 2 | 01/10/2012        | Global                |                     |                       | Test details of use 3 |
	| Toilet Cleaner             | Test Supplier 2 | 01/10/2012        | Global                | RX02, RX03          | SX03                  | Cleaning the toilets  |
	Given I press the edit icon for hazardous substance with name 'My completely new one'
	And I have entered 'My edited title' into the 'Name' field
	And I have deselected option label 'RX01 Test Risk Phrase 1' from multi-select control 'risk-phrase'
	And I have deselected option label 'SX03 Test Safety Phrase 3' from multi-select control 'safety-phrase'
	When I press 'AddEditHazardousSubstanceSaveButton' button
	Then I should be redirected to the inventory page
	And the Hazardous Substances table should contain the following data:
	| Substance Name             | Supplier        | Date Of SDS Sheet | Hazard Classification | Risk No And Phrases | Safety No And Phrases | Usage                 |
	| My edited title	         | Test Supplier 1 | 01/01/2012        | Global                | RX03				 | SX01					 | details of my usage   |
	| Test Hazardous Substance 1 | Test Supplier 1 | 01/10/2012        | Global                | RX01, RX02          | SX01, SX02            | Test details of use 1 |
	| Test Hazardous Substance 2 | Test Supplier 1 | 01/10/2012        | Global                | RX02, RX03          | SX02, SX03            | Test details of use 2 |
	| Test Hazardous Substance 3 | Test Supplier 2 | 01/10/2012        | Global                |                     |                       | Test details of use 3 |
	| Toilet Cleaner             | Test Supplier 2 | 01/10/2012        | Global                | RX02, RX03          | SX03                  | Cleaning the toilets  |

@createsHazardousSubstance
Scenario: When I click Add Hazardous Substance with assessment required checked I am directed to risk assessment
Given I am on the add hazardous substance page for company '55881'
	And I have entered 'My completely new one' into the 'Name' field
	And I have entered '01/01/2012' into the 'SdsDate' field
	And I have entered 'Test Supplier 1' into the 'Supplier' field
	And I have entered '1' into the 'SupplierId' field
	And I selected the Global identification standard
	And I have entered 'details of my usage' into the 'DetailsOfUse' field
	And I press 'AssessmentRequired' radio button with the value of 'true'
	When I press 'AddEditHazardousSubstanceSaveButton' button
	And I wait for '1000' miliseconds
	Then I should be redirected to the create hazardous substance risk assessment page
	And the text box with id 'Title' should contain 'My completely new one'
