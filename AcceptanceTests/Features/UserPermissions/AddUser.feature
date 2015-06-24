Feature: AddUser
	In order to ensure security consistency
	As someone altering user permissions
	I want to be told when validation errors have occured.

Scenario: Validation errors are shown
	Given I have logged in as company with id '55881'
	And I am on the add users page for company '55881'
	And I have entered 'Kim Howard' into the 'Employee' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'EmployeeId' field
	And I have entered '' into the 'SiteGroupId' field
	And I have entered '' into the 'SiteId' field
	And the 'PermissionsApplyToAllSites' checkbox has value of 'false'
	When I press 'SaveButton' button
	Then the validation field is visible with message 'Either the Site field or Site Group field must be selected, or the All Sites checkbox must be checked.'
	Given I have entered 'North West Group' into the 'SiteGroup' field
	And I have entered '380' into the 'SiteGroupId' field
	And I have entered 'Main Site' into the 'Site' field
	And I have entered '371' into the 'SiteId' field
	And the 'PermissionsApplyToAllSites' checkbox has value of 'false'
	When I press 'SaveButton' button
	Then the validation field is visible with message 'The Site and Site Group fields cannot both be selected.'
	Given I have entered '' into the 'SiteGroupId' field
	And I have entered 'Main Site' into the 'Site' field
	And I have entered '371' into the 'SiteId' field
	And the 'PermissionsApplyToAllSites' checkbox has value of 'true'
	When I press 'SaveButton' button
	Then the validation field is visible with message 'If the All Sites checkbox is checked, the Site and Site Group fields must be left unselected.'
	Given I have entered 'North West Group' into the 'SiteGroup' field
	And I have entered '380' into the 'SiteGroupId' field
	And I have entered '' into the 'SiteId' field
	And the 'PermissionsApplyToAllSites' checkbox has value of 'true'
	When I press 'SaveButton' button
	Then the validation field is visible with message 'If the All Sites checkbox is checked, the Site and Site Group fields must be left unselected.'

