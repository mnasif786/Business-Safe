Feature: UploadDocument
	In order to add a document to a Responsibility Task
	As a BSO User
	I want to be able to upload a document

@AcceptanceTest
#TODO: This needs finishing when we can view a task so we can check it has worked.
#TODO: Also edit an exisiting task rather than create one when that functionality is in.
Scenario: Upload a document to a task
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
	And on form 'CreateResponsibilityTaskForm' I have entered '28/02/2050' into the 'CompletionDueDate' field
	And on form 'CreateResponsibilityTaskForm' I have entered 'Main Site' into the 'ResponsibilityTaskSite' field
	And on form 'CreateResponsibilityTaskForm' I have entered '371' into the 'ResponsibilityTaskSiteId' field
	And on form 'CreateResponsibilityTaskForm' I have entered 'Russell Williams ( Payroll Clerk )' into the 'AssignedTo' field
	And on form 'CreateResponsibilityTaskForm' I have entered 'd2122fff-1dcd-4a3c-83ae-e3503b394eb4' into the 'AssignedToId' field
	And It is simulated that a document has been uploaded
	And I press 'saveResponsibilityTaskButton' button