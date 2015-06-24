Feature: InjuryDetails
	In order to show a country specific question
	As a BSO User
	I want to see a jurisdiction specific message 

@NeedsAccidentRecordWithJurisdictionSetToROI
Scenario: ROI Message appears when it should
	Given I have logged in as company with id '55881'	
	And I am on the injury page for accident record '-1' for companyId '55881'
	And I wait for '1000' miliseconds	
	Then element with id 'takenToHospitalMessage' has value 'Was the injured person taken to hospital or treated by a registered medical practitioner?' 


@NeedsAccidentRecordWithJurisdictionNotSetToROI
Scenario: ROI Message does not appear when it shouldn't
	Given I have logged in as company with id '55881'	
	And I am on the injury page for accident record '-1' for companyId '55881'
	And I wait for '1000' miliseconds	
	Then element with id 'takenToHospitalMessage' has value 'Was the injured person taken to hospital?' 

	
@NeedsAccidentRecordWithJurisdictionSetToGB
Scenario: No guidance notes for Fatal injuries
	Given I am on the injury page for accident record '-1' for companyId '55881'
	And I press 'SeverityOfInjury' radio button with the value of 'Fatal'
	And I wait for '1000' miliseconds	
	Then the element with id 'GuidanceNotes' has visibility of 'false'

#@NeedsAccidentRecordWithJurisdictionSetToGB
#Scenario: Show guidance notes for Major injuries
#	Given I am on the injury page for accident record '-1' for companyId '55881'
#	And I press 'SeverityOfInjury' radio button with the value of 'Major'
#	And I wait for '1000' miliseconds	
#	Then the element with id 'GuidanceNotes' does not have class of 'hide'
#	And the GuidanceNotes href is 'http://businesssafe.dev-peninsula-online.com/Documents/Document/DownloadPublicDocument?enc=QfbCd5M90Wq0rgrXIMv7oFoPN0NoADDouoaNs5MdFPO9XBj2cqVLwkqwkxnclsF6'
#	
@NeedsAccidentRecordToAddInjuryDetailsTo
Scenario: Save injury details
	Given I have logged in as company with id '55881'
	And I am on the injury page for accident record '-1' for companyId '55881'
	And I wait for '1000' miliseconds
	And I press 'saveButton' button
	And I wait for '2000' miliseconds
	Then the element with class 'validation-summary-errors alert alert-error' has visibility of 'true'

@NeedsAccidentRecordWithVisitorToAddInjuryDetailsTo
Scenario: Hide Injured Person Able To Carry Out Work Section For Visitor
	Given I have logged in as company with id '55881'
	And I am on the injury page for accident record '-1' for companyId '55881'
	Then the element with id 'InjuredPersonAbleToCarryOutWorkSection' does not exist

@NeedsAccidentRecordWithEmployeeToAddInjuryDetailsTo
Scenario: Show Injured Person Able To Carry Out Work Section For Employee
	Given I have logged in as company with id '55881'
	And I am on the injury page for accident record '-1' for companyId '55881'
	Then the element with id 'InjuredPersonAbleToCarryOutWorkSection' exist

@NeedsAccidentRecordToAddInjuryDetailsTo
Scenario: Show Custom Injury Description
	Given I have logged in as company with id '55881'
	And I am on the injury page for accident record '-1' for companyId '55881'
	Given I have selected the option label 'Other unknown injury' from multi-select control 'injury' 
	Then the element with id 'otherInjuryDescription' has visibility of 'true'
	
@NeedsAccidentRecordToAddInjuryDetailsTo
Scenario: Show Custom Body Part Description
	Given I have logged in as company with id '55881'
	And I am on the injury page for accident record '-1' for companyId '55881'
	Given I have selected the option label 'Unknown location' from multi-select control 'bodypart' 
	Then the element with id 'otherBodyPartDescription' has visibility of 'true'