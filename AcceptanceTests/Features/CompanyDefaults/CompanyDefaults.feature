@Acceptance 
Feature: Managing Company Defaults
	In order to have custom company defaults in drop down lists in business safe online
	As a business safe online user
	I want to be add edit and remove custom company defaults in the various sections allowed

Background:
    Given I have logged in as company with id '55881'
	And I have no 'Non Employees' for company
		

@ignore
Scenario: Add Organisational Unit Classification Company Default
	Given I am on the Company default page
	And I am working on Organisational Unit Classification
	When I enter 'Test Clarification' into the add textbox
	And I select 'add' command button
	Then should have 'created' the Organisational Unit Classification Company Default

@ignore
Scenario: Edit Organisational Unit Classification Company Default
	Given I am working on Organisational Unit Classification
	And I have added a Organisational Unit Classification Company Default
	When I edit the new Organisational Unit Classification Company Default to 'Test Clarification Modified'
	And I select 'edit' command button
	Then should have 'edited' the Organisational Unit Classification Company Default

@ignore
Scenario: Delete Organisational Unit Classification Company Default
	Given I am working on Organisational Unit Classification
	And I have added a Organisational Unit Classification Company Default	
	When I delete the new Organisational Unit Classification Company Default
	And I confirm delete
	Then should have deleted the Organisational Unit Classification Company Default

@NonEmployee
Scenario: Add Non Employee Company Default With Existing Match Name
	Given I am working on Non Employee
	And I have added a Non Employee Company Default	
	When I select 'add new non employee' command button
	Then should have the new Non Employee form display
	Given I have entered exact'New Non Employee'
	And I select 'create' command button
	Then should have Matching Names displayed
	Given I select 'create' command button
	Then should have 'created' the Non Employee Company Default

@NonEmployee
Scenario: Add Non Employee Company Default
	Given I am on the Company default page
	And I am working on Non Employee
	When I select 'add new non employee' command button
	Then should have the new Non Employee form display
	Given I have entered 'New Non Employee'
	And I select 'create' command button
	Then should have 'created' the Non Employee Company Default

@NonEmployee
Scenario: Add Non Employee Then Edit Previously Entered Non Employee Should Show Previously Entered Non Employee Details
	Given I am on the Company default page	
	And I am working on Non Employee
	And I select 'add new non employee' command button
	And I have entered 'New Employee Name A' into the 'Name' field
	And I have entered 'New Employee Company A' into the 'Company' field
	And I have entered 'New Employee Position A' into the 'Position' field
	And I select 'create' command button
	And I select 'add new non employee' command button
	And I have entered 'New Employee Name B' into the 'Name' field
	And I have entered 'New Employee Company B' into the 'Company' field
	And I have entered 'New Employee Position B' into the 'Position' field
	And I select 'create' command button
	When I click on edit for the first new non employee
	Then the input with id 'Name' has value 'New Employee Name A'
	Then the input with id 'Company' has value 'New Employee Company A'
	Then the input with id 'Position' has value 'New Employee Position A'


@NonEmployee
@ignore
Scenario: Delete Non Employee Company Default
	Given I am working on Non Employee
	And I have added a Non Employee Company Default	
	When I delete the new Non Employee Company Default
	And I select 'yes' on confirmation
	Then should have deleted the Non Employee Company Default


@NonEmployee	
Scenario: Edit Non Employee Company Default
	Given I am working on Non Employee
	And I have added a Non Employee Company Default	
	And I edit the new Non Employee Company Default
	Then the element with id 'dialogAddNonEmployee' has visibility of 'true'
	Given I have entered 'Editing Non Employee'	
	And I select 'update' command button
	Then should have 'updated' the Non Employee Company Default

