Feature: Inventory Item Has Link Back To Associated RiskAssessment
	In order to monitor our Hazardous Substances and their associated Risk Assessment
	As a BSO user
	I want to be able to view related Risk Assessments for a given Hazardous Substance

Background:
	Given I have logged in as company with id '55881'


Scenario: When a Hazardous Substance has a single Risk Assessment Then the Link goes to that Risk Assessment
	Given I am on the hazardous substance inventory page for company '55881'
	Then the Hazardous Substances table should contain the following data:
	| Substance Name             | Supplier        | Date Of SDS Sheet | Hazard Classification | Risk No And Phrases | Safety No And Phrases | Usage                 |
	| Test Hazardous Substance 1 | Test Supplier 1 | 01/10/2012        | Global                | RX01, RX02          | SX01, SX02            | Test details of use 1 |
	| Test Hazardous Substance 2 | Test Supplier 1 | 01/10/2012        | Global                | RX02, RX03          | SX02, SX03            | Test details of use 2 |
	| Test Hazardous Substance 3 | Test Supplier 2 | 01/10/2012        | Global                |                     |                       | Test details of use 3 |
	| Toilet Cleaner             | Test Supplier 2 | 01/10/2012        | Global                | RX02, RX03          | SX03                  | Cleaning the toilets  |
	When I click on the view risk assessment for the hazardous substance with id '2'
	Then I am redirected to the url 'HazardousSubstanceRiskAssessments/Description/View'
	And the hazardous substance risk assessment id is '44'

@finetune
Scenario: When a Hazardous Substance has several Risk Assessment Then the Link goes to a Search result of its Risk Assessments
	Given I am on the hazardous substance inventory page for company '55881'
	Then the Hazardous Substances table should contain the following data:
	| Substance Name             | Supplier        | Date Of SDS Sheet | Hazard Classification | Risk No And Phrases | Safety No And Phrases | Usage                 |
	| Test Hazardous Substance 1 | Test Supplier 1 | 01/10/2012        | Global                | RX01, RX02          | SX01, SX02            | Test details of use 1 |
	| Test Hazardous Substance 2 | Test Supplier 1 | 01/10/2012        | Global                | RX02, RX03          | SX02, SX03            | Test details of use 2 |
	| Test Hazardous Substance 3 | Test Supplier 2 | 01/10/2012        | Global                |                     |                       | Test details of use 3 |
	| Toilet Cleaner             | Test Supplier 2 | 01/10/2012        | Global                | RX02, RX03          | SX03                  | Cleaning the toilets  |
	When I click on the view risk assessment for the hazardous substance with id '1'
	Then I am redirected to the url 'HazardousSubstanceRiskAssessments?'
	Then the Hazardous Substance Risk Assessments table should contain the following data
	| Reference										| Title                                               | Site	  | Assigned To | Status | Completion Due Date |
	| HazSub 1 RA 1									| Test Hazardous Substance 1 RA 1                     | Barnsley  | Kim Howard  | Live   | 29/10/2012          |
	| HazSub 1 RA 2									| Test Hazardous Substance 1 RA 2                     |			  |             | Live   | 29/10/2012          |
	| Edinburgh Hazardous Substance Risk Assessment | Edinburgh Hazardous Substance Risk Assessment       | Edinburgh |	            | Live   | 29/10/2012          |
