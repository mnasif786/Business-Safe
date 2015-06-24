Feature: CopyForMultipleSites
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@Acceptance
Scenario: Copy to multiple sites
	Given I have logged in as company with id '55881'
	And I am on the add general risk assessment index page for company '55881'
	And I click on the copy button for risk assessment with id '1'
	And I wait for '1000' miliseconds
	And I press 'copyMultipleSitesRiskAssessmentLink' link
	And I wait for '1000' miliseconds
	And I have entered 'Multiple Copy 1' into the 'Title' field of the 'formCopyMultipleSitesRiskAssessment' form
	And I check site with id '378'
	And I check site with id '379'
	And I check site with id '382'
	When I click confirm multiple copy
	And I wait for '1000' miliseconds
	Then the risk assessment table should contain the following data:
	| Reference                       | Title                              | Site      | Assigned To      | Status | Completion Due Date |
    |                                 | Multiple Copy 1                    | Aberdeen  |                  | Draft  |                     |
    |                                 | Multiple Copy 1                    | Barnsley  |                  | Draft  |                     |
    |                                 | Multiple Copy 1                    | Edinburgh |                  | Draft  |                     |
    | CFGRA                           | Core Functionality Risk Assessment | Aberdeen  | Russell Williams | Live   | 01/06/2012          |
	| TRA01                           | Test Risk Assessment 1             | Aberdeen  | Russell Williams | Live   | 01/06/2012          |
    | TRA02                           | Test Risk Assessment 2             | Aberdeen  | Russell Williams | Live   | 01/06/2012          |
    | GRA01                           | General Risk Assessment 1          | Aberdeen  | John Conner      | Live   | 08/08/2012          |
	| GRA02                           | General Risk Assessment 2          | Aberdeen  | John Conner      | Live   | 08/08/2012          |
	| Acceptance Test Risk Assessment | Acceptance Test Risk Assessment    | Aberdeen  | Barry Scott      | Live   | 30/08/2012          |

