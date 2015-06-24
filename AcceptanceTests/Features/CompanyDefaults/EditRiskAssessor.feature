Feature: EditRiskAssessor
	In order to edit risk assessors to a risk assessment
	As a BSO user
	I want to be able to select risk assessor from grid

Background:
    Given I have logged in as company with id '55881'
	And I have no 'Non Employees' for company

@ChangesKnownRiskAssessors
Scenario: Set Site for Risk Assessor
	Given I am on the Company default page
	And I am on the risk assessors tab
	And I click on the edit button for risk assessor with id '4'
	And 'HasAccessToAllSites' check box is ticked 'false'
	And I have entered 'Aberdeen' into the 'Site' field
	And I have entered '378' into the 'SiteId' field
	When I press 'saveBtn' button
	And I wait for '2000' miliseconds
	Then the risk assessors table should contain the following data:
	| Forename | Surname  | Site      | Overdue | Completed | Due  |
	| John     | Conner   | Aberdeen  | true    | true      | true |
	| Kim      | Howard   | All Sites | true    | true      | true |
	| Barry    | Scott    | All Sites | true    | true      | true |
	| Russell  | Williams | All Sites | true    | true      | true |
	Given I click on the edit button for risk assessor with id '4'
	And I wait for '1000' miliseconds
	And 'HasAccessToAllSites' check box is ticked 'true'
	And I have entered '' into the 'Site' field
	And I have entered '' into the 'SiteId' field
	When I press 'saveBtn' button
	And I wait for '2000' miliseconds
	Then the risk assessors table should contain the following data:
	| Forename | Surname  | Site      | Overdue | Completed | Due  |
	| John     | Conner   | All Sites | true    | true      | true |
	| Kim      | Howard   | All Sites | true    | true      | true |
	| Barry    | Scott    | All Sites | true    | true      | true |
	| Russell  | Williams | All Sites | true    | true      | true |

@ChangesKnownRiskAssessors
Scenario: Set Risk Assessor Notifications
	Given I am on the Company default page
	And I am on the risk assessors tab
	And I click on the edit button for risk assessor with id '4'
	And 'DoNotSendTaskOverdueNotifications' check box is ticked 'false'
	And 'DoNotSendTaskCompletedNotifications' check box is ticked 'false'
	And 'DoNotSendReviewDueNotification' check box is ticked 'false'
	And I wait for '2000' miliseconds
	When I press 'saveBtn' button
	Then the risk assessors table should contain the following data:
	| Forename | Surname  | Site      | Overdue | Completed | Due   |
	| John     | Conner   | All Sites | false   | false     | false |
	| Kim      | Howard   | All Sites | true    | true      | true  |
	| Barry    | Scott    | All Sites | true    | true      | true  |
	| Russell  | Williams | All Sites | true    | true      | true  |