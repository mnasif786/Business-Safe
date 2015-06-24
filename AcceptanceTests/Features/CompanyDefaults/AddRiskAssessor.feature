Feature: AddRiskAssessor
	In order to add risk assessors to a risk assessment
	As a BSO user
	I want to be able to elevate an employee to a risk assessor

Background:
    Given I have logged in as company with id '55881'
	And I have no 'Non Employees' for company

@AddingRiskAssessor
Scenario: Add Risk Assessor with specific site
	Given I am on the Company default page
	And I am on the risk assessors tab
	And I click on the add risk asssessor button
	And I have entered 'Kim Howard ( )' into the 'Employee' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'EmployeeId' field
	And I have entered 'Aberdeen' into the 'Site' field
	And I have entered '378' into the 'SiteId' field
	And 'DoNotSendTaskOverdueNotifications' check box is ticked 'true'
	And 'DoNotSendTaskCompletedNotifications' check box is ticked 'true'
	And 'DoNotSendReviewDueNotification' check box is ticked 'true'
	And I wait for '2000' miliseconds
	When I press 'saveBtn' button
	Then the risk assessor table should contain a risk assessor with surname 'Howard'

@AddingRiskAssessor
Scenario: Add Risk Assessor with all sites
	Given I am on the Company default page
	And I am on the risk assessors tab
	And I click on the add risk asssessor button
	And I have entered 'Kim Howard ( )' into the 'Employee' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'EmployeeId' field
	And 'HasAccessToAllSites' check box is ticked 'true'
	And I wait for '2000' miliseconds
	When I press 'saveBtn' button
	Then the risk assessor table should contain a risk assessor with surname 'Howard'

Scenario: Selecting Employee Displays Warning And Job And Site
	Given I am on the Company default page
	And I am on the risk assessors tab
	And I click on the add risk asssessor button
	And I have entered 'Howard, Kim ( )' into the 'Employee' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'EmployeeId' field
	When Javascript for selecting employee has fired
	Then the element with id 'EmployeeNotAUserWarning' has visibility of 'false'
	And the label with id 'EmployeeJobTitle' should have a value of 'Business Analyst'
	And the label with id 'EmployeeSite' should be null
	Given I have entered 'Green, Gary ( )' into the 'Employee' field
	And I have entered '4d91b7e6-5e25-4620-bfab-d5d4b598cbf7' into the 'EmployeeId' field
	When Javascript for selecting employee has fired
	Then the element with id 'EmployeeNotAUserWarning' has visibility of 'true'
	And the label with id 'EmployeeJobTitle' should have a value of 'Business Analyst'
	And the label with id 'EmployeeSite' should be null

@AddingRiskAssessor
Scenario: Clicking Show Deleted Displays Deleted Risk Assessors
	Given We have the following risk assessors:
	| EmployeeId                           | HasAccessToAllSites | SiteId | DoNotSendTaskOverdueNotifications | DoNotSendTaskCompletedNotifications | DoNotSendReviewDueNotification | Deleted |
	| 9d24ae1a-6645-45fc-9d50-8fc70babeb89 | false               | 371    | false                             | false                               | false                          | true    |
	| 4d91b7e6-5e25-4620-bfab-d5d4b598cbf7 | true                | 371    | false                             | false                               | false                          | true    |
	And I am on the Company default page
	And I press 'risk-assessors' link
	When I click on show deleted button
	And I wait for '2000' miliseconds
	Then the risk assessors table should contain the following data:
	| Forename | Surname | Site      | Overdue | Completed | Due   |
	| Gary     | Green   | All Sites | false   | false     | false |
	| Glen     | Ross    | Main Site | false   | false     | false |
	When I click on show active button
	And I wait for '2000' miliseconds
	Then the risk assessors table should contain the following data:
	| Forename | Surname | Site      | Overdue | Completed | Due  |
	| John     | Conner  | All Sites | true    | true      | true |
	| Kim      | Howard  | All Sites | true    | true      | true |
	| Barry    | Scott   | All Sites | true    | true      | true |
	| Russell  | Williams| All Sites | true    | true      | true |