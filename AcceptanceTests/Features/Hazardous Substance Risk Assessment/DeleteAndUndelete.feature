Feature: DeleteAndUndelete
	I should be able to delete a PRA
	And then restore it again
	
Background:
	Given I have logged in as company with id '55881'

@finetune
Scenario: Delete and re-instate a risk assessment
	Given I am on the Index for Hazardous Substances RiskAssessments for company '55881'
	When I click to delete the hazardous substance risk assessment with reference 'HazSub 1 RA 1'
	And I click confirm button on delete
	And I wait for '2000' miliseconds
	Then the risk assessment table should contain the following data:
	| Reference											   | Title												 | Site      | Assigned To  | Status | Completion Due Date |
	| Acceptance Test HSRA								   | Acceptance Test Hazardous Substance Risk Assessment |			 |				| Live   | 29/10/2012          |
	| HazSub 1 RA 2										   | Test Hazardous Substance 1 RA 2					 |			 |	   | Live   | 29/10/2012          |
	| HazSub 2 RA 1										   | Test Hazardous Substance 2 RA 1					 |			 |			    | Live   | 29/10/2012          |
	| Edinburgh Hazardous Substance Risk Assessment		   | Edinburgh Hazardous Substance Risk Assessment		 | Edinburgh |			    | Live   | 29/10/2012          |
	When I press 'showDeletedLink' link
	And I wait for '2000' miliseconds
	Then the risk assessment table should contain the following data:
	| Reference		| Title							  | Site      | Assigned To     | Status | Completion Due Date |
	| HazSub 1 RA 1	| Test Hazardous Substance 1 RA 1 |	Barnsley  |	Kim Howard		| Live   | 29/10/2012          |
	When I click to reinstate the hazardous substance risk assessment with reference 'HazSub 1 RA 1'
	And I click confirm button on delete
	And I wait for '2000' miliseconds
	And I am on the Index for Hazardous Substances RiskAssessments for company '55881'
	Then the risk assessment table should contain the following data:
	| Reference											   | Title												 | Site      | Assigned To  | Status | Completion Due Date |
	| Acceptance Test HSRA								   | Acceptance Test Hazardous Substance Risk Assessment |			 |				| Live   | 29/10/2012          |
	| HazSub 1 RA 1										   | Test Hazardous Substance 1 RA 1					 | Barnsley  | Kim Howard   | Live   | 29/10/2012          |
	| HazSub 1 RA 2										   | Test Hazardous Substance 1 RA 2					 |			 |			    | Live   | 29/10/2012          |
	| HazSub 2 RA 1										   | Test Hazardous Substance 2 RA 1					 |			 |			    | Live   | 29/10/2012          |
	| Edinburgh Hazardous Substance Risk Assessment		   | Edinburgh Hazardous Substance Risk Assessment		 | Edinburgh |			    | Live   | 29/10/2012          |
	
