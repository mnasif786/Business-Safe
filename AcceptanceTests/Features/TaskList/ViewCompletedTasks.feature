Feature: view Completed Tasks
	As A BSO User
	When I click show completed tasks in my task list
	I should be shown a list of my completed tasks

Scenario: View completed fcm tasks
	Given I have logged in as company with id '55881'
	And I have entered 'John Conner' into the 'Employee' field
	And I have entered '8929BAC1-5403-4837-A72F-A077AA0C4E81' into the 'EmployeeId' field
	And user click on GoToTasks for Assigned To Filter
	And I press link with ID 'showCompletedLink'
	Then the completed task lists results table should contain the following data:
	| Task Reference | Task Category           | Title                   | Description             | Assigned To | Completed By		| Due Date   | Completed Date | Status	  |
	| 007            | General Risk Assessment | Completed FCM Task Test | Completed FCM Task Test | John Conner | Russell Williams | 19/09/2012 | 19/09/2012	  | Completed |

Scenario: View deleted fcm tasks
	Given I have logged in as company with id '55881'
	And I have entered 'John Conner' into the 'Employee' field
	And I have entered '8929BAC1-5403-4837-A72F-A077AA0C4E81' into the 'EmployeeId' field
	And user click on GoToTasks for Assigned To Filter
	And I press link with ID 'showDeletedLink'
	Then the task lists results table should contain the following data:
	| Task Reference | Task Category           | Title                 | Description           | Assigned To | Created Date | Due Date   | Completed   |
	| 008            | General Risk Assessment | Deleted FCM Task Test | Deleted FCM Task Test | John Conner | 01/09/2012   | 19/09/2012 | Overdue |