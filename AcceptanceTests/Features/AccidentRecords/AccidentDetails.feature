Feature: AccidentDetails
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@NeedsAccidentRecordToAddAccidentDetailsTo
Scenario: Validate accident details
	Given I have logged in as company with id '55881'
	And I am on the accident details page for accident record '-1' for companyId '55881'
	And I wait for '1000' miliseconds
	And I press 'saveButton' button
	And I wait for '2000' miliseconds
	Then the element with class 'validation-summary-errors alert alert-error' has visibility of 'true'


@NeedsAccidentRecordToAddAccidentDetailsTo
Scenario: Save accident details
	Given I have logged in as company with id '55881'
	And I am on the accident details page for accident record '-1' for companyId '55881'
	And I have entered '26/09/2024' into the 'DateOfAccident' field
	And I have entered '12:00' into the 'TimeOfAccident' field
	And I have entered 'Aberdeen' into the 'Site' field	
	And I have entered '378' into the 'SiteId' field
	And I have entered 'Other Location' into the 'Location' field
	And I have entered 'Contact with electricity' into the 'AccidentType' field
	And I have entered '1' into the 'AccidentTypeId' field
	And I have entered 'Being caught or carried away by something (or by momentum)' into the 'AccidentCause' field
	And I have entered '1' into the 'AccidentCauseId' field
	And I wait for '1000' miliseconds
	And I press 'saveButton' button
	And I wait for '2000' miliseconds
	Then the element with class 'alert alert-success' has visibility of 'true'

@NeedsAccidentRecordToAddAccidentDetailsTo
Scenario: Validate Offsite details
	Given I have logged in as company with id '55881'
	And I am on the accident details page for accident record '-1' for companyId '55881'
	And I have entered '26/09/2024' into the 'DateOfAccident' field
	And I have entered '12:00' into the 'TimeOfAccident' field
	And I have entered 'Off-Site' into the 'Site' field	
	And I have entered '-1' into the 'SiteId' field
	And I have triggered after select drop down event for 'Site'
	And I have entered 'Other Location' into the 'Location' field
	And I have entered 'Contact with electricity' into the 'AccidentType' field
	And I have entered '1' into the 'AccidentTypeId' field
	And I have entered 'Being caught or carried away by something (or by momentum)' into the 'AccidentCause' field
	And I have entered '1' into the 'AccidentCauseId' field
	And I wait for '1000' miliseconds
	And I press 'saveButton' button
	And I wait for '2000' miliseconds
	Then the element with id 'offsite-name' has visibility of 'true'
	Then Error Message 'errorSaving' Contains:
	| Error Message				   |
	| Please specify an off-site name	   |

@NeedsAccidentRecordToAddAccidentDetailsTo
Scenario: Validate Another kind of accident
	Given I have logged in as company with id '55881'
	And I am on the accident details page for accident record '-1' for companyId '55881'
	And I have entered '26/09/2024' into the 'DateOfAccident' field
	And I have entered '12:00' into the 'TimeOfAccident' field
	And I have entered 'Aberdeen' into the 'Site' field	
	And I have entered '378' into the 'SiteId' field
	And I have entered 'Other Location' into the 'Location' field
	And I have entered 'Another kind of accident' into the 'AccidentType' field
	And I have entered '16' into the 'AccidentTypeId' field
	And I have triggered after select drop down event for 'AccidentType'
	And I have entered 'Being caught or carried away by something (or by momentum)' into the 'AccidentCause' field
	And I have entered '1' into the 'AccidentCauseId' field
	And I wait for '1000' miliseconds
	And I press 'saveButton' button
	And I wait for '2000' miliseconds
	Then the element with id 'other-accident-type' has visibility of 'true'
	Then Error Message 'errorSaving' Contains:
	| Error Message				   |
	| Please describe the kind of accident	   |

@NeedsAccidentRecordToAddAccidentDetailsTo
Scenario: Validate Another cause of accident
	Given I have logged in as company with id '55881'
	And I am on the accident details page for accident record '-1' for companyId '55881'
	And I have entered '26/09/2024' into the 'DateOfAccident' field
	And I have entered '12:00' into the 'TimeOfAccident' field
	And I have entered 'Aberdeen' into the 'Site' field	
	And I have entered '378' into the 'SiteId' field
	And I have entered 'Other Location' into the 'Location' field
	And I have entered 'Contact with electricity' into the 'AccidentType' field
	And I have entered '1' into the 'AccidentTypeId' field
	And I have entered 'Other cause not listed' into the 'AccidentCause' field
	And I have entered '14' into the 'AccidentCauseId' field
	And I have triggered after select drop down event for 'AccidentType'
	And I wait for '1000' miliseconds
	And I press 'saveButton' button
	And I wait for '2000' miliseconds
	Then the element with id 'other-accident-cause' has visibility of 'true'
	Then Error Message 'errorSaving' Contains:
	| Error Message				   |
	| Please describe the cause of the accident	   |