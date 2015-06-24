Feature: InjuredPerson
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@NeedsAccidentRecordToAddInjuredPersonDetailsTo
Scenario: Save other injured person details
	Given I have logged in as company with id '55881'
	And I am on the injured person page for accident record '-1' for companyId '55881'
	And I wait for '1000' miliseconds
	And I press 'PersonInvolvedType' radio button with the value of 'Other'
	And I wait for '1000' miliseconds
	And I have entered 'Test details' into the 'PersonInvolvedOtherDescription' field
	And I have entered 'George' into the 'Forename' field
	And I have entered 'Green' into the 'Surname' field
	And I press 'saveButton' button
	And I wait for '2000' miliseconds
	Then the element with class 'alert alert-success' has visibility of 'true'

@NeedsAccidentRecordToAddInjuredPersonDetailsTo
Scenario: Save employee injured person details
	Given I have logged in as company with id '55881'
	And I am on the injured person page for accident record '-1' for companyId '55881'
	And I wait for '1000' miliseconds
	And I press 'PersonInvolvedType' radio button with the value of 'Employee'
	And I wait for '1000' miliseconds
	And I have entered 'Kim Howard' into the 'Employee' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'EmployeeId' field
	And I press 'saveButton' button
	And I wait for '2000' miliseconds
	Then the element with class 'alert alert-success' has visibility of 'true'

@NeedsAccidentRecordToAddInjuredPersonDetailsTo
Scenario: Validate other injured person details
	Given I have logged in as company with id '55881'
	And I am on the injured person page for accident record '-1' for companyId '55881'
	And I wait for '1000' miliseconds
	And I press 'PersonInvolvedType' radio button with the value of 'Other'
	And I wait for '1000' miliseconds
	And I press 'saveButton' button
	And I wait for '2000' miliseconds
	Then the element with class 'validation-summary-errors alert alert-error' has visibility of 'true'
	And the element with class 'validation-summary-errors alert alert-error' should contain 'Forename is required'
	And the element with class 'validation-summary-errors alert alert-error' should contain 'Surname is required'

@NeedsAccidentRecordToAddInjuredPersonDetailsTo
Scenario: Validate employee injured person details
	Given I have logged in as company with id '55881'
	And I am on the injured person page for accident record '-1' for companyId '55881'
	And I wait for '1000' miliseconds
	And I press 'PersonInvolvedType' radio button with the value of 'Employee'
	And I wait for '1000' miliseconds
	And I press 'saveButton' button
	And I wait for '2000' miliseconds
	Then the element with class 'validation-summary-errors alert alert-error' has visibility of 'true'
	And the element with class 'validation-summary-errors alert alert-error' should contain 'Employee is required'