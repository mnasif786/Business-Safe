Feature: ResponsibilityCoreFunctionality
	In order to use the Responsibility functionality
	As BSO User
	I want the core BSO Functionality to work

@core_functionality
@Acceptance
Scenario: Create responsibility and related task
	Given I have logged in as company with id '55881'
	And I am on page '1' of the Responsibility Index page for companyId '55881'
	And I press 'add-responsibility-link' button
	And I press 'add-bespoke-responsibility-link' button
	And I have entered 'Fire Safety' into the 'Category' field
	And I have entered '1' into the 'CategoryId' field
	And I have entered 'Test Responsibility 1' into the 'Title' field
	And I have entered 'Test Responsibility 1 Descritpion' into the 'Description' field
	And I have entered 'Main Site' into the 'Site' field
	And I have entered '371' into the 'SiteId' field
	And I have entered 'Compliance' into the 'Reason' field
	And I have entered '1' into the 'ReasonId' field
	And I have entered 'Weekly' into the 'Frequency' field
	And I have entered '1' into the 'FrequencyId' field
	When I press 'createResponsibility' button
	And I wait for '2000' miliseconds
	Then the element with id 'dialogAddEditResponsibilityTask' has visibility of 'true'
	Given on form 'CreateResponsibilityTaskForm' I have entered '10/06/2050' into the 'CompletionDueDate' field	
	And on form 'CreateResponsibilityTaskForm' I have entered 'Main Site' into the 'ResponsibilityTaskSite' field 
	And on form 'CreateResponsibilityTaskForm' I have entered '371' into the 'ResponsibilityTaskSiteId' field 
	And on form 'CreateResponsibilityTaskForm' I have entered 'Kim Howard ( Business Analyst )' into the 'AssignedTo' field
	And on form 'CreateResponsibilityTaskForm' I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'AssignedToId' field
	When I press 'saveResponsibilityTaskButton' button
	And I wait for '5000' miliseconds
	Then the element with id 'dialogAddEditResponsibilityTask' has visibility of 'false'
	And responsibility task table should contain the following data:
	| Title                      | Description                            | Assigned To | Site      | Created      | Due Date   | Status      |
	| Test Responsibility 1 | Test Responsibility 1 Descritpion | Kim Howard  | Main Site | DateTime.Now | 10/06/2050 | Outstanding |
	Given I wait for '2000' miliseconds
	And I press 'saveResponsibility' button
	And I am on page '2' of the Responsibility Index page for companyId '55881'
	And I wait for '5000' miliseconds
	Then A Responsibility has been created with title 'Test Responsibility 1'
	#Then The following record should exist in the responsibility table:
	#| Category    | Title                 | Description                       | Site      | Reason     | Responsibility Owner | Status      | Created Date | Frequency | Completion Due Date |
	#| Fire Safety | Test Responsibility 1 | Test Responsibility 1 Descritpion | Main Site | Compliance | Russell Williams     | Outstanding | 22/07/2013   | Weekly    | 10/06/2050          |

@core_functionality
@NeedsResponsibilityToViewEditDelete
Scenario: View responsibility
	Given I have logged in as company with id '55881'
	And I am on page '1' of the Responsibility Index page for companyId '55881'
	And I click to view the first responsibility
	Then I am redirected to the url 'http://businesssafe.dev-peninsula-online.com/Responsibilities/Responsibility/View?responsibilityId=-1&companyId=55881'

@core_functionality
@NeedsResponsibilityToViewEditDelete
Scenario: Delete responsibility
	Given I have logged in as company with id '55881'
	And I am on page '1' of the Responsibility Index page for companyId '55881'
	And I click to delete the first responsibility
	And I click confirm
	And I wait for '5000' miliseconds
	Then first row does not have title 'Responsibility For Testing'

@core_functionality
@NeedsResponsibilityToViewEditDelete
Scenario: Edit responsibility
	Given I have logged in as company with id '55881'
	And I am on page '1' of the Responsibility Index page for companyId '55881'
	And I click to edit the first responsibility
	And I have entered 'Edited Responsibility' into the 'Title' field	
	And I have entered 'Edited Responsibility' into the 'Description' field	
	And I press 'saveResponsibility' button
	And I am on page '1' of the Responsibility Index page for companyId '55881'
	Then The first record in the responsibility table should be:
	| Category    | Title                 | Description           | Site      | Reason     | Responsibility Owner | Status      | Created Date | Frequency | Completion Due Date |
	| Fire Safety | Edited Responsibility | Edited Responsibility | Main Site | Compliance | Kim Howard           |             | DateTime.Now | Weekly    |                     |

@core_functionality
@NeedsResponsibilityTasksToComplete
Scenario: Complete A Responsibility Task
	Given I have logged in as company with id '55881'
	And Complete task is clicked for 'NonRecResp01'
	And 'TaskComplete' check box is ticked 'true'
	And Complete button is enabled
	And I wait for '100' miliseconds
	When Complete is clicked
	And I wait for '5000' miliseconds 
	Then the task 'NonRecResp01' for company '55881' should be completed

@core_functionality
@NeedsResponsibilityTasksToComplete
Scenario: Reassign A Responsibility Task
	Given I have logged in as company with id '55881'
	And Reassign task is clicked for 'RecResp01'
	And on form 'FurtherActionTask' I have entered 'Gary Green ( Business Analyst )' into the 'ReassignTaskTo' field
	And on form 'FurtherActionTask' I have entered '4d91b7e6-5e25-4620-bfab-d5d4b598cbf7' into the 'ReassignTaskToId' field
	When I press 'FurtherActionTaskSaveButton' button
	And I wait for '5000' miliseconds
	And I have entered 'Gary Green ( Business Analyst )' into the 'Employee' field
	And I have entered '4d91b7e6-5e25-4620-bfab-d5d4b598cbf7' into the 'EmployeeId' field
	And I press 'ResponsibilityTaskCategoryGoToResponsibilityTasks' button
	Then the task 'RecResp01' for company '55881' should be assigned to '4d91b7e6-5e25-4620-bfab-d5d4b598cbf7'