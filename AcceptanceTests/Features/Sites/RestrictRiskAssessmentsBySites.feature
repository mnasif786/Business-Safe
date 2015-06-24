Feature: RestrictGeneralRiskAssessmentsBySites
	In order to increase security
	As a user
	I am prevented from adding or viewing general risk assessments for sites over which I am not authorised

@Acceptance
Scenario: Adding and viewing sites for general risk assessment
	Given I have logged in as company with id '31028'
	And I am on the add users page for company '31028'
	And I have entered 'Blake Smith' into the 'Employee' field
	And I have entered 'CE94014D-B158-4FCA-92E9-1B969E3378CE' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	And I wait for '1000' miliseconds
	And I have entered 'Northern Region' into the 'SiteGroup' field
	And I have entered '31' into the 'SiteGroupId' field
	And I have entered '-- Select Option --' into the 'Site' field
	And I have entered '' into the 'SiteId' field
	And the 'PermissionsApplyToAllSites' checkbox has value of 'false' 
	And I press 'SaveButton' button
	And I wait for '1000' miliseconds
	And I have logged in as company with id '31028'
	And I am on the add general risk assessment page for company '31028'
	And I have entered 'Test Risk Assessment 1' into the 'Title' field
	And I have entered 'TRA01' into the 'Reference' field
	When I press 'createSummary' button
	And I wait for '1000' miliseconds
	And I have entered '01/01/2020' into the 'DateOfAssessment' field
	Then autocomplete select list for 'Site' should contain '107' options
	Given I have entered 'Leicester' into the 'Site' field
	And I have entered '131' into the 'SiteId' field
	And I have triggered after select drop down event for 'Site'
	And I have entered '6' into the 'RiskAssessorId' field
	And I press 'saveButton' button
	And I press 'premisesinformation' link
	And I have entered 'Test Location Area Department' into the 'LocationAreaDepartment' field
	And I have entered 'Test Task Process Description' into the 'TaskProcessDescription' field
	And I press 'saveButton' button
	And I wait for '1000' miliseconds
	And I have logged in as company with id '31028'
	When I am on the general risk assessments page for company '31028'
	Then the general risk assessments table should contain the following data: 
	| Reference |
	| TRA01     |
	Given I am on the add users page for company '31028'
	And I have entered 'Blake Smith' into the 'Employee' field
	And I have entered 'CE94014D-B158-4FCA-92E9-1B969E3378CE' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	And I wait for '1000' miliseconds
	And I have entered 'Midlands' into the 'SiteGroup' field
	And I have entered '39' into the 'SiteGroupId' field
	And I have entered '-- Select Option --' into the 'Site' field
	And I have entered '' into the 'SiteId' field
	And the 'PermissionsApplyToAllSites' checkbox has value of 'false' 
	And I press 'SaveButton' button
	And I wait for '1000' miliseconds
	And I have logged in as company with id '31028'
	And I am on the add general risk assessment page for company '31028'
	And I have entered 'Test Risk Assessment 2' into the 'Title' field
	And I have entered 'TRA02' into the 'Reference' field
	When I press 'createSummary' button
	And I wait for '1000' miliseconds
	And I have entered '01/01/2020' into the 'DateOfAssessment' field
	And I wait for '2000' miliseconds
	Then sites select list should contain the folowing data:
	| SiteName            |
	| --Select Option--   |
	| Birmingham          |
	| Cheltenham          |
	| Cirencester         |
	| Great Malvern       |
	| Hereford            |
	| Hitchin             |
	| Leamington Spa      |
	| Leicester           |
	| Lincoln             |
	| Meadowhall          |
	| Milton Keynes       |
	| Mkt Harb'ro         |
	| Monmouth            |
	| Newark              |
	| Nottingham 2        |
	| Oakham              |
	| Sheffield ER        |
	| Sheffield OS        |
	| Shrewsbury          |
	| Solihull            |
	| St Albans 2         |
	| Stratford O A       |
	| Worcester           |
	Given I have entered 'Worcester' into the 'Site' field
	Given I have entered '228' into the 'SiteId' field
	And I have entered '6' into the 'RiskAssessorId' field
	And I press 'premisesinformation' link
	And I have entered 'Test Location Area Department' into the 'LocationAreaDepartment' field
	And I have entered 'Test Task Process Description' into the 'TaskProcessDescription' field
	And I press 'saveButton' button
	And I wait for '1000' miliseconds
	And I have logged in as company with id '31028'
	When I am on the general risk assessments page for company '31028'
	Then the general risk assessments table should contain the following data: 
	| Reference |
	| TRA01     |
	| TRA02     |
	Given I am on the add users page for company '31028'
	And I have entered 'Blake Smith' into the 'Employee' field
	And I have entered 'CE94014D-B158-4FCA-92E9-1B969E3378CE' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	And I wait for '1000' miliseconds
	And I have entered '-- Select Option --' into the 'SiteGroup' field
	And I have entered '' into the 'SiteGroupId' field
	And I have entered 'Ipswich' into the 'Site' field
	And I have entered '185' into the 'SiteId' field
	And the 'PermissionsApplyToAllSites' checkbox has value of 'false' 
	And I press 'SaveButton' button
	And I wait for '1000' miliseconds
	And I have logged in as company with id '31028'
	And I am on the add general risk assessment page for company '31028'
	And I have entered 'Test Risk Assessment 3' into the 'Title' field
	And I have entered 'TRA03' into the 'Reference' field
	When I press 'createSummary' button
	And I wait for '1000' miliseconds
	And I have entered '01/01/2020' into the 'DateOfAssessment' field
	Then sites select list should contain the folowing data:
	| SiteName            |
	| --Select Option--   |
	| Ipswich             |
	Given I have entered 'Ipswich' into the 'Site' field
	And I have entered '185' into the 'SiteId' field
	And I have entered '6' into the 'RiskAssessorId' field
	And I press 'premisesinformation' link
	And I wait for '1000' miliseconds
	And I have entered 'Test Location Area Department' into the 'LocationAreaDepartment' field
	And I have entered 'Test Task Process Description' into the 'TaskProcessDescription' field
	And I press 'saveButton' button
	And I wait for '1000' miliseconds
	And I have logged in as company with id '31028'
	When I am on the general risk assessments page for company '31028'
	Then the general risk assessments table should contain the following data: 
	| Reference |
	| TRA03     |
	Given I am on the add users page for company '31028'
	And I have entered 'Blake Smith' into the 'Employee' field
	And I have entered 'CE94014D-B158-4FCA-92E9-1B969E3378CE' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	And I wait for '1000' miliseconds
	And I have entered 'East Anglia' into the 'SiteGroup' field
	And I have entered '43' into the 'SiteGroupId' field
	And I have entered '-- Select Option --' into the 'Site' field
	And I have entered '' into the 'SiteId' field
	And the 'PermissionsApplyToAllSites' checkbox has value of 'false' 
	And I press 'SaveButton' button
	And I wait for '1000' miliseconds
	And I have logged in as company with id '31028'
	When I am on the general risk assessments page for company '31028'
	Then the general risk assessments table should contain the following data: 
	| Reference |
	| TRA03     |
	Given I am on the add users page for company '31028'
	And I have entered 'Blake Smith' into the 'Employee' field
	And I have entered 'CE94014D-B158-4FCA-92E9-1B969E3378CE' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	And I wait for '1000' miliseconds
	And I have entered 'Midlands' into the 'SiteGroup' field
	And I have entered '39' into the 'SiteGroupId' field
	And I have entered '-- Select Option --' into the 'Site' field
	And I have entered '' into the 'SiteId' field
	And the 'PermissionsApplyToAllSites' checkbox has value of 'false' 
	And I press 'SaveButton' button
	And I wait for '1000' miliseconds
	And I have logged in as company with id '31028'
	When I am on the general risk assessments page for company '31028'
	Then the general risk assessments table should contain the following data: 
	| Reference |
	| TRA01     |
	| TRA02     |
	Given I am on the add users page for company '31028'
	And I have entered 'Blake Smith' into the 'Employee' field
	And I have entered 'CE94014D-B158-4FCA-92E9-1B969E3378CE' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	And I wait for '1000' miliseconds
	And I have entered 'Northern Region' into the 'SiteGroup' field
	And I have entered '31' into the 'SiteGroupId' field
	And I have entered '-- Select Option --' into the 'Site' field
	And I have entered '' into the 'SiteId' field
	And the 'PermissionsApplyToAllSites' checkbox has value of 'false' 
	And I press 'SaveButton' button
	And I wait for '1000' miliseconds
	And I have logged in as company with id '31028'
	When I am on the general risk assessments page for company '31028'
	Then the general risk assessments table should contain the following data: 
	| Reference |
	| TRA01     |
	| TRA02     |
	| TRA03     |
	Given I am on the add users page for company '31028'
	And I have entered 'Blake Smith' into the 'Employee' field
	And I have entered 'CE94014D-B158-4FCA-92E9-1B969E3378CE' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	And I wait for '1000' miliseconds
	And I have entered 'Southern Region' into the 'SiteGroup' field
	And I have entered '32' into the 'SiteGroupId' field
	And I have entered '-- Select Option --' into the 'Site' field
	And I have entered '' into the 'SiteId' field
	And the 'PermissionsApplyToAllSites' checkbox has value of 'false' 
	And I press 'SaveButton' button
	And I wait for '1000' miliseconds
	And I have logged in as company with id '31028'
	When I am on the general risk assessments page for company '31028'
	Then the general risk assessments table should contain the following data: 
	| Reference              |
	| No records to display. |
	Given I am on the add users page for company '31028'
	And I have entered 'Blake Smith' into the 'Employee' field
	And I have entered 'CE94014D-B158-4FCA-92E9-1B969E3378CE' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	And I wait for '1000' miliseconds
	And I have entered '-- Select Option --' into the 'SiteGroup' field
	And I have entered '' into the 'SiteGroupId' field
	And I have entered '-- Select Option --' into the 'Site' field
	And I have entered '' into the 'SiteId' field
	And the 'PermissionsApplyToAllSites' checkbox has value of 'true' 
	And I press 'SaveButton' button
	And I wait for '1000' miliseconds
	And I have logged in as company with id '31028'
	And I am on the add general risk assessment page for company '31028'
	And I have entered 'Test Risk Assessment 4' into the 'Title' field
	And I have entered 'TRA04' into the 'Reference' field
	When I press 'createSummary' button
	And I wait for '1000' miliseconds
	And I have entered '01/01/2020' into the 'DateOfAssessment' field
	Then autocomplete select list for 'Site' should contain '221' options
	Given I have entered 'Putney' into the 'Site' field
	And I have entered '263' into the 'SiteId' field
	And I have entered '6' into the 'RiskAssessorId' field
	And I press 'premisesinformation' link
	And I wait for '1000' miliseconds
	And I have entered 'Test Location Area Department' into the 'LocationAreaDepartment' field
	And I have entered 'Test Task Process Description' into the 'TaskProcessDescription' field
	And I press 'saveButton' button
	And I wait for '1000' miliseconds
	And I have logged in as company with id '31028'
	When I am on the general risk assessments page for company '31028'
	Then the general risk assessments table should contain the following data: 
	| Reference |
	| TRA01     |
	| TRA02     |
	| TRA03     |
	| TRA04     |
	Given I am on the add general risk assessment page for company '31028'
	And I have entered 'Test Risk Assessment 5' into the 'Title' field
	And I have entered 'TRA05' into the 'Reference' field
	When I press 'createSummary' button
	And I am on the general risk assessments page for company '31028'
	And I have waited for the page to reload
	Then the general risk assessments table should contain the following data: 
	| Reference |
	| TRA01     |
	| TRA02     |
	| TRA03     |
	| TRA04     |
	| TRA05     |
	
@Acceptance
Scenario: Adding and viewing sites for hazardous substances risk assessment
	Given I have logged in as company with id '31028'
	And I am on the add users page for company '31028'
	And I have entered 'Blake Smith' into the 'Employee' field
	And I have entered 'ce94014d-b158-4fca-92e9-1b969e3378ce' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	And I wait for '1000' miliseconds
	And I have entered 'Northern Region' into the 'SiteGroup' field
	And I have entered '31' into the 'SiteGroupId' field
	And I have entered '-- Select Option --' into the 'Site' field
	And I have entered '' into the 'SiteId' field
	And the 'PermissionsApplyToAllSites' checkbox has value of 'false' 
	And I press 'SaveButton' button
	And I wait for '1000' miliseconds
	And I have logged in as company with id '31028'
	Given I am on the add hazardous substance risk assessment page for company '31028'
	And I have entered 'Test HS Risk Assessment 1' into the 'Title' field
	And I have entered 'THSRA01' into the 'Reference' field
	And I have entered 'Test Hazardous Substance 5' into the 'NewHazardousSubstance' field
	And I have entered '5' into the 'NewHazardousSubstanceId' field
	When I press 'createSummary' button
	Then autocomplete select list for 'Site' should contain '101' options
	Given I have entered 'Leicester' into the 'Site' field
	And I have entered '131' into the 'SiteId' field
	And I have entered '6' into the 'RiskAssessorId' field
	When I press 'saveButton' button
	Given I am on the Index for Hazardous Substances RiskAssessments for company '31028'
	Then the hazardous substances risk assessments table should contain the following data:
	| Reference     |
	| THSRA01		|
	Given I am on the add users page for company '31028'
	And I have entered 'Blake Smith' into the 'Employee' field
	And I have entered 'ce94014d-b158-4fca-92e9-1b969e3378ce' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	And I wait for '1000' miliseconds
	And I have entered 'East Anglia' into the 'SiteGroup' field
	And I have entered '43' into the 'SiteGroupId' field
	And I have entered '-- Select Option --' into the 'Site' field
	And I have entered '' into the 'SiteId' field
	And the 'PermissionsApplyToAllSites' checkbox has value of 'false' 
	And I press 'SaveButton' button
	And I wait for '1000' miliseconds
	And I have logged in as company with id '31028'
	Given I am on the Index for Hazardous Substances RiskAssessments for company '31028'
	Then the hazardous substances risk assessments table should not contain the following data:
	| Reference |
	| THSRA01   |
	Given I am on the add hazardous substance risk assessment page for company '31028'
	And I have entered 'Test HS Risk Assessment 2' into the 'Title' field
	And I have entered 'THSRA02' into the 'Reference' field
	And I have entered 'Test Hazardous Substance 5' into the 'NewHazardousSubstance' field
	And I have entered '5' into the 'NewHazardousSubstanceId' field
	When I press 'createSummary' button
	Then autocomplete select list for 'Site' should contain '25' options
	Given I am on the add users page for company '31028'
	And I have entered 'Blake Smith' into the 'Employee' field
	And I have entered 'ce94014d-b158-4fca-92e9-1b969e3378ce' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	And I wait for '1000' miliseconds
	And I have entered '-- Select Option --' into the 'SiteGroup' field
	And I have entered '' into the 'SiteGroupId' field
	And I have entered 'Leicester' into the 'Site' field
	And I have entered '131' into the 'SiteId' field
	And the 'PermissionsApplyToAllSites' checkbox has value of 'false' 
	And I press 'SaveButton' button
	And I wait for '1000' miliseconds
	And I have logged in as company with id '31028'
	Given I am on the Index for Hazardous Substances RiskAssessments for company '31028'
	Then the hazardous substances risk assessments table should contain the following data:
	| Reference |
	| THSRA01   |
	| THSRA02   |
	Given I am on the add hazardous substance risk assessment page for company '31028'
	And I have entered 'Test HS Risk Assessment 3' into the 'Title' field
	And I have entered 'THSRA03' into the 'Reference' field
	And I have entered 'Test Hazardous Substance 5' into the 'NewHazardousSubstance' field
	And I have entered '5' into the 'NewHazardousSubstanceId' field
	When I press 'createSummary' button
	And I have waited for the page to reload
	Then autocomplete select list for 'Site' should contain '2' options