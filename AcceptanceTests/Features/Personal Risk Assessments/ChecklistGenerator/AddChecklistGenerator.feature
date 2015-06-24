Feature: AddChecklistGenerator
	In order to ensure PRAs are complete
	As a BSO user with add PRA permissions
	I want to be add checklists to a single employee

Background:
	Given I have logged in as company with id '55881'


Scenario: Save Checklist Generator
	Given I am on the checklist generator page for risk assessment '52' and companyid '55881'
	And I click the singleEmployee radiobutton
	And I have entered 'Gary Green (test@testing.com)' into the 'Employee' field
	And I have entered '4d91b7e6-5e25-4620-bfab-d5d4b598cbf7' into the 'EmployeeId' field
	And 'IncludeChecklist_1' check box is ticked 'true'
	And I have entered 'Dear All, Please complete the checklists' into the 'Message' field
	And I press 'SendCompletedChecklistNotificationEmail' radio button with the value of 'True'
	And I have entered 'russell.williams@pbstest.com' into the 'CompletionNotificationEmailAddress' field
	When I press 'saveButton' button
	Then the notice 'Checklist Generator Successfully Updated' should be displayed
	And the input with id 'CompletionNotificationEmailAddress' has value 'russell.williams@pbstest.com'
	And 'IncludeChecklist_1' check box is ticked 'true'