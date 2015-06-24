Feature: User Core Functionality
	In order to organise my company
	As a business safe online user
	I want to be able to organise add, edit and delete users

@core_functionality
Scenario: Adding User With Mandatory Fields
	Given I have logged in as company with id '55881'
	And I am on the add user page for company '55881'
	And I have entered 'Gary Green' and '4D91B7E6-5E25-4620-BFAB-D5D4B598CBF7' into the 'Employee' combobox
	And UserId select list change event is fired
	And I have entered 'UserAdmin' into the 'Role' field
	And I have entered 'BACF7C01-D210-4DBC-942F-15D8456D3B92' into the 'RoleId' field
	And I have entered 'Aberdeen' into the 'Site' field
	And I have entered '378' into the 'SiteId' field
	And I press 'PermissionsApplyToAllSites' checkbox
	And I press 'SaveButton' button
	Then the save success notification box should be displayed

@ResetRussellWilliamsSiteId
@core_functionality
Scenario: Editing User With Mandatory Fields
	Given I have logged in as company with id '55881'
	And I am on the search users page for company '55881'
	Given I have clicked the 'EditUserIconLink'
	When I have entered 'Aberdeen' into the 'Site' field
	And I have entered '378' into the 'SiteId' field
	And 'PermissionsApplyToAllSites' check box is ticked 'false'
	And I press 'SaveButton' button
	Then I am redirected to the url 'Users/ViewUsers'

@core_functionality
@DisableUser
Scenario: Delete User
	Given I have logged in as company with id '55881'
	And I am on the search users page for company '55881'
    When I click to delete the user with reference 'SDTO1'
	And I select 'yes' on confirmation
	And I wait for '2000' miliseconds
	When I press 'showDeletedLink' link
	And I wait for '1000' miliseconds
    Then the user with reference 'SDTO1' is visible
