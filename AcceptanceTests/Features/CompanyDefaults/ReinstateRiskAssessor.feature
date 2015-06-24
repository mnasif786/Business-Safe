Feature: ReinstateRiskAssessor
	In order to correct a wrongly deleted risk assessors to a risk assessment
	As a BSO user
	I want to be able to reinstate

Background:
    Given I have logged in as company with id '55881'

@AddingRiskAssessor
Scenario: Clicking Show Deleted Displays Deleted Risk Assessors
	Given We have the following risk assessors:
	| EmployeeId                           | HasAccessToAllSites | SiteId | DoNotSendTaskOverdueNotifications | DoNotSendTaskCompletedNotifications | DoNotSendReviewDueNotification | Deleted |
	| 9d24ae1a-6645-45fc-9d50-8fc70babeb89 | false               | 371    | false                             | false                               | false                          | true    |
	And I am on the Company default page
	And I press 'risk-assessors' link
	When I click on show deleted button
	And I wait for '1000' miliseconds
	Then the risk assessors table should contain the following data:
	| Forename | Surname | Site      | Overdue | Completed | Due   |
	| Glen     | Ross    | Main Site | false   | false     | false |
	When I click on the reinstate button for the first deleted user
	And I have waited for element 'ui-dialog-title-dialogReinstateRiskAssessor' to exist
	And I click confirm button on delete
	And I wait for '1000' miliseconds
	And I click on show active button
	And I have waited for element 'add-new-riskassessor-button' to exist
	Then the risk assessors table should contain the following data:
	| Forename | Surname | Site      | Overdue | Completed | Due  |
	| John     | Conner  | All Sites | true    | true      | true |
	| Kim      | Howard  | All Sites | true    | true      | true |
	| Glen     | Ross    | Main Site | false   | false     | false |
	| Barry    | Scott   | All Sites | true    | true      | true |
	| Russell  | Williams| All Sites | true    | true      | true |