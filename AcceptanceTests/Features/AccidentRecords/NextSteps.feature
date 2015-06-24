Feature: NextSteps
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@NeedsAccidentRecordThatDoesShowNextSteps
Scenario: Next steps appears when it should
	Given I have logged in as company with id '55881'
	And I am on the injured person page for accident record '-3' for companyId '55881'
	And I wait for '1000' miliseconds
	Then element with id 'nextsteps' exists 
	Given I wait for '100' miliseconds

@NeedsAccidentRecordThatDoesntShowNextSteps
Scenario: Next steps does not appear when it shouldnt
	Given I have logged in as company with id '55881'
	And I am on the injured person page for accident record '-2' for companyId '55881'
	And I wait for '1000' miliseconds
	Then element with id 'nextsteps' does not exist 
	Given I wait for '100' miliseconds
