@Acceptance
Feature: SearchPersonalRiskAssessments
	In order to find a specific PRA
	As a user with the correct permissions
	I want to search the PRAs
	
@Personal_Risk_Assessments
Background:
	Given I have logged in as company with id '55881'
	And I am on the personal risk assessments page for company '55881'

Scenario: Search by site id
	Given I have entered '378' into the 'SiteId' field	
	When I press 'Search' button
	Then the risk assessment table should contain the following data:
	| Reference                         | Title                                       | Site     | Assigned To      | Status | Completion Due Date |
	| CFPRA                             | Core Functionality Personal Risk Assessment | Aberdeen |                  | Draft  |                     |
	| PRA02                             | PRA 2                                       | Aberdeen | Russell Williams | Live   | 21/04/2013          |
	| make sensitive by other user test | PRA created by Kim                          | Aberdeen | Kim Howard       | Draft  | 12/06/2013          |
	| PRA01								| PRA 1									  	  | Aberdeen | Russell Williams | Live   | 23/06/2013          |
	Given I have entered '' into the 'SiteId' field	
	Given I have entered '381' into the 'SiteGroupId' field	
	When I press 'Search' button
	Then the risk assessment table should contain the following data:
	| Reference		 | Title	 | Site      | Assigned To      | Status | Completion Due Date |
	| PRA04			 | PRA 4	 | Edinburgh | Russell Williams | Live   | 21/04/2012         |

	
Scenario: Search by created date
	Given I have entered '01/04/2012' into the 'CreatedFrom' field
	Given I have entered '01/04/2013' into the 'CreatedTo' field	
	When I press 'Search' button
	Then the risk assessment table should contain the following data:
	| Reference		 | Title	 | Site		 | Assigned To      | Status | Completion Due Date |
	| PRA03			 | PRA 3	 | Barnsley	 | Russell Williams | Live   | 23/06/2012        |
	| PRA02			 | PRA 2	 | Aberdeen	 | Russell Williams | Live   | 21/04/2013          |

Scenario: Search by title	
	Given I have entered 'PRA TASKs' into the 'Title' field	
	When I press 'Search' button
	Then the risk assessment table should contain the following data:
	| Reference		 | Title		 | Site		 | Assigned To      | Status | Completion Due Date |
	| Ref: PRA TASKs1| PRA TASKs1	 | 			 |					| Draft   | 12/02/2012        |
	| Ref: PRA TASKs2| PRA TASKs2	 | 			 |					| Draft   | 12/02/2012          |

