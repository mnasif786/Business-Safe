Feature: DeleteAndUndelete
	I should be able to delete a PRA
	And then restore it again
	
@Personal_Risk_Assessments
Background:
	Given I have logged in as company with id '55881'

@finetune
Scenario: Delete and re-instate a personal risk assessment
	Given I am on the personal risk assessments page for company '55881'
	When I click to delete the personal risk assessment with reference 'PRA04'
	And I click confirm button on delete
	And I wait for '1000' miliseconds
	Then the risk assessment table should contain the following data:
	| Reference			| Title		 | Site      | Assigned To      | Status | Completion Due Date |
	| CFPRA				| Core Functionality Personal Risk Assessment | Aberdeen			 |				    | Draft  |           |
	| Ref: PRA TASKs1   | PRA TASKs1 |			 |				    | Draft  | 12/02/2012          |
	| Ref: PRA TASKs2   | PRA TASKs2 |			 |				    | Draft  | 12/02/2012          |
	| PRA03				| PRA 3		 | Barnsley  | Russell Williams | Live   | 23/06/2012          |
	| PRA02				| PRA 2		 | Aberdeen  | Russell Williams | Live   | 21/04/2013          |
	| make sensitive by other user test	| PRA created by Kim		   | Aberdeen  | Kim Howard		  | Draft   | 12/06/2013         |
	| PRA01				| PRA 1		 | Aberdeen  | Russell Williams | Live   | 23/06/2013          |
	When I press 'showDeletedLink' link
	Then the risk assessment table should contain the following data:
	| Reference			| Title		 | Site      | Assigned To      | Status | Completion Due Date |
	| PRA04				| PRA 4		 | Edinburgh | Russell Williams | Live   | 21/04/2012          |
	When I click to reinstate the personal risk assessment with reference 'PRA04'
	And I click confirm button on delete
	And I wait for '5000' miliseconds
	And I am on the personal risk assessments page for company '55881'
	Then the risk assessment table should contain the following data:
	| Reference			| Title		 | Site      | Assigned To      | Status | Completion Due Date |
	| CFPRA				| Core Functionality Personal Risk Assessment | Aberdeen			 |				    | Draft  |           |
	| Ref: PRA TASKs1   | PRA TASKs1 |			 |				    | Draft  | 12/02/2012          |
	| Ref: PRA TASKs2   | PRA TASKs2 |			 |				    | Draft  | 12/02/2012          |
	| PRA04				| PRA 4		 | Edinburgh | Russell Williams | Live   | 21/04/2012          |
	| PRA03				| PRA 3		 | Barnsley  | Russell Williams | Live   | 23/06/2012          |
	| PRA02				| PRA 2		 | Aberdeen  | Russell Williams | Live   | 21/04/2013          |
	| make sensitive by other user test	| PRA created by Kim		   | Aberdeen  | Kim Howard		  | Draft   | 12/06/2013         |
	| PRA01				| PRA 1		 | Aberdeen  | Russell Williams | Live   | 23/06/2013          |
