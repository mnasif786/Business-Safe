@Acceptance
Feature: Notify Peninsula Client Services of Changes
	In order to be aware of all changes made to the Company Details
	As a Admin User
	I want to recieve an email informing me of all the changes made

Background:
	Given I have logged in as company with id '55881'
	And The user is on Company Details Page

@finetune
@Acceptance
Scenario: Notify Peninsula Client Services Of Changes Made to company Details
	When User click on modify button with id modifyCompanyDetails
	And  User click on save button with id notifyAdminButton
	And I wait for '4000' miliseconds
	Then Then an email is sent to admin

@Acceptance
Scenario: Modify company details button would hide labels and display textboxes to edit the information
	And textboxes on the page are invisible and labels are visible
	When User click on modify button with id modifyCompanyDetails
	Then labels are made invisible and textboxes are made visible
	When click the cancel link
	Then textboxes on the page are invisible and labels are visible

@Acceptance
Scenario: Check validation work
	When User click on modify button with id modifyCompanyDetails
	And User remove the text from text box with id AddressLine1
	And  User click on save button with id notifyAdminButton
	Then Error message is displayed

@Acceptance
Scenario: Check When User edit a value and click cancel the textboxes display original values
	When User click on modify button with id modifyCompanyDetails
	And User remove the text from text box with id AddressLine1
	And click the cancel link
	And User click on modify button with id modifyCompanyDetails
	Then textboxes display original values	