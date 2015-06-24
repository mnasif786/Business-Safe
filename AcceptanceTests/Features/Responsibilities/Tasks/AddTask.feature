Feature: Add Responsibility Task
	In order to ensure our responsibilities are being kept up
	As a business safe online user
	I want to be able to create tasks for that responsibility

Background:
	Given I have logged in as company with id '55881'

#implicitly repeated below
#@Acceptance
#Scenario: Popup opens and validation messages display
#	Given I am on the responsibility page for responsibility with id '1' and company '55881'
#	When I click the button with id 'add-responsibility-task'
#	Then the element with id 'dialogAddEditResponsibilityTask' has visibility of 'true'
#	When I click the button with id 'saveResponsibilityTaskButton'
#	Then the 'Title is required' error message is displayed
#	And the 'Task Assigned To is required' error message is displayed
#	And the 'Description is required' error message is displayed
#	And the 'Completion Due Date is required' error message is displayed
#	When I press 'IsRecurring' checkbox
#	When I click the button with id 'saveResponsibilityTaskButton'
#	Then the 'Title is required' error message is displayed
#	And the 'Task Recurrence Frequency is required' error message is displayed
#	And the 'Task Assigned To is required' error message is displayed
#	And the 'Description is required' error message is displayed
#	And the 'First Due Date requires a valid date' error message is displayed

@Acceptance
Scenario: Selecting IsReoccuring hides completed date and shows reoccuring dates and vice versa
	Given I have logged in as company with id '55881'
	And I am on the Create Responsibility Task page for companyId '55881'
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
	And I press 'createResponsibility' button
	And I wait for '2000' miliseconds
	When I click the button with id 'add-responsibility-task'
	And on form 'CreateResponsibilityTaskForm' I have entered 'Test Responsibility Task 1' into the 'Title' field
	And on form 'CreateResponsibilityTaskForm' I have entered 'Test Responsibility Task 1 Descritpion' into the 'Description' field
	When I have checked 'IsRecurring' permission checkbox
	And I wait for '2000' miliseconds
	Then the element with id 'recurringDiv' has visibility of 'true'
	Then the element with id 'nonReoccurringDiv' has visibility of 'false'
	When I have checked 'IsRecurring' permission checkbox
	And I wait for '2000' miliseconds
	Then the element with id 'recurringDiv' has visibility of 'false'
	Then the element with id 'nonReoccurringDiv' has visibility of 'true'

@Acceptance
Scenario: Selecting IsReoccuring validates frequency first date and last date and unselecting it validates completion date
	Given I have logged in as company with id '55881'
	And I am on the Create Responsibility Task page for companyId '55881'
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
	And I press 'createResponsibility' button
	And I wait for '2000' miliseconds
	When I click the button with id 'add-responsibility-task'
	And on form 'CreateResponsibilityTaskForm' I have entered 'Test Responsibility Task 1' into the 'Title' field
	And on form 'CreateResponsibilityTaskForm' I have entered 'Test Responsibility Task 1 Descritpion' into the 'Description' field
	When I press 'saveResponsibilityTaskButton' button
	Then the 'Completion Due Date is required' error message is displayed
	And the 'Recurring Task First Due Date is required' error message is not displayed
	And the 'Task Recurrence Frequency is required' error message is not displayed
	Given I have checked 'IsRecurring' permission checkbox
	When I press 'saveResponsibilityTaskButton' button
	Then the 'Completion Due Date is required' error message is not displayed
	And the 'First Due Date requires a valid date' error message is displayed
	And the 'Task Recurrence Frequency is required' error message is displayed
