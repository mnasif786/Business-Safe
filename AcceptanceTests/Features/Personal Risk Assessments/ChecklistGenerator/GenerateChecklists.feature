Feature: GenerateChecklists
	In order to ensure PRAs are complete
	As a BSO user with add PRA permissions
	I want to be send checklists out to a single employee

Background:
	Given I have logged in as company with id '55881'


Scenario: Send Checklists to Employee
	Given I am on the checklist generator page for risk assessment '52' and companyid '55881'
	And I click the singleEmployee radiobutton
	And I have entered 'Gary Green (test@testing.com)' into the 'Employee' field
	And I have entered '4d91b7e6-5e25-4620-bfab-d5d4b598cbf7' into the 'EmployeeId' field
	And 'IncludeChecklist_1' check box is ticked 'true'
	And I have entered 'Dear All, Please complete the checklists' into the 'Message' field
	When I press link with ID 'sendButton'
	Then I have entered '' into the 'Employee' field
	And I have entered '' into the 'EmployeeId' field
		And 'IncludeChecklist_1' check box is ticked 'true'

	And I have entered '' into the 'Message' field
	And I have entered '' into the 'CompletionDueDateForChecklists' field
	And radio button 'SendCompletedChecklistNotificationEmailYes' has value of 'True' and is 'false'
	And radio button 'SendCompletedChecklistNotificationEmailNo' has value of 'False' and is 'false'
