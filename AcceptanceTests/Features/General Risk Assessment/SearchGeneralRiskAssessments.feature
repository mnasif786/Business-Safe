@RequiresSearchGRAData
@Acceptance
Feature: SearchGeneralRiskAssessments
	In order to find a specific GRA
	As a user with the correct permissions
	I want to search the GRA's

Background:
	Given I have logged in as company with id '31028'
	And I am on the general risk assessments page for company '31028'

Scenario: Search Where Site is Hitchin
	Given I have entered '50' into the 'SiteId' field	
	When I press 'Search' button
	Then the risk assessment table should contain the following data:
	| Reference | Title               | Site    | Assigned To | Status | Completion Due Date |
	| SGRA01    | Search GRAs Test 01 | Hitchin |             | Live   | 01/06/2012          |

Scenario: Search Where Site Group is Southern Region
	Given I have entered '32' into the 'SiteGroupId' field	
	When I press 'Search' button
	Then the risk assessment table should contain the following data:
	| Reference | Title               | Site     | Assigned To | Status | Completion Due Date |
	| SGRA03    | Search GRAs Test 03 | Kingston |             | Live   | 03/06/2012          |
	| SGRA04    | Search GRAs Test 04 | Brighton |             | Live   | 04/06/2012          |
	| SGRA05    | Search GRAs Test 05 | Exeter 2 |             | Live   | 05/06/2012          |

Scenario: Search Where Site Group is Southern Region and Site is Brighton
	Given I have entered '32' into the 'SiteGroupId' field	
	And I have entered '227' into the 'SiteId' field
	When I press 'Search' button
	Then the risk assessment table should contain the following data:
	| Reference | Title               | Site     | Assigned To | Status | Completion Due Date |
	| SGRA04    | Search GRAs Test 04 | Brighton |             | Live   | 04/06/2012          |

Scenario: Search Where Site Group is Southern Region and Site is Colchester
	Given I have entered '32' into the 'SiteGroupId' field	
	And I have entered '43' into the 'SiteId' field
	When I press 'Search' button
	Then the risk assessment table should contain the following data output has only one cell:
	| Reference					| Title               | Site     | Assigned To | Status | Completion Due Date |
	| No records to display.	|					  |			 |             |		|			          |

Scenario: Search Where Site Group is South Coast
	Given I have entered '35' into the 'SiteGroupId' field	
	When I press 'Search' button
	Then the risk assessment table should contain the following data:
	| Reference | Title               | Site     | Assigned To | Status | Completion Due Date |
	| SGRA04    | Search GRAs Test 04 | Brighton |             | Live   | 04/06/2012          |

Scenario: Search Where Title contains Test 03
	Given I have entered 'Test 03' into the 'Title' field	
	When I press 'Search' button
	Then the risk assessment table should contain the following data:
	| Reference | Title               | Site     | Assigned To | Status | Completion Due Date |
	| SGRA03    | Search GRAs Test 03 | Kingston   |             | Live   | 03/06/2012        |

#This is not functioning as expected?
@ignore
Scenario: Search Where Created from is 02/06/2012 and CreatedTo is 04/06/2012
	Given I have entered '02/06/2012' into the 'CreatedFrom' field	
	And I have entered '04/06/2012' into the 'createdTo' field
	When I press 'Search' button
	Then the risk assessment table should contain the following data:
	| Reference | Title               | Site       | Assigned To | Status | Completion Due Date |
	| SGRA02    | Search GRAs Test 02 | Colchester |             | Live   | 02/06/2012          |
	| SGRA03    | Search GRAs Test 03 | Kingston   |             | Live   | 03/06/2012          |
	| SGRA04    | Search GRAs Test 04 | Brighton   |             | Live   | 04/06/2012          |