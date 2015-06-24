Feature: Managing User Roles
	In order to manage users
	As a business safe online user with permission
	I want to be able to edit and delete user roles

@UserRoles
Scenario: System Role Not Editable
	Given I have logged in as company with id '55881'
	And I have navigated to user roles page
	And I have entered 'User Admin' and 'BACF7C01-D210-4DBC-942F-15D8456D3B92' into the 'UserRoles' combobox
	Then the permissions checkboxes should not be enabled

@UserRoles
Scenario: Editing Custom Role 
	Given I have logged in as company with id '55881'
	And I have created 'Test Role'
	And I have navigated to user roles page
	And I have entered 'Test Role' into the 'UserRoles' field
	And I have entered 'Test Role' Id into the 'UserRolesId' field
	And I have triggered after select drop down event for 'UserRoles'
	When I press 'EditUserRoleButton' button
	And I have checked 'AddSiteDetails' permission checkbox
	And I have checked 'ViewSiteDetails' permission checkbox
	When I press 'SaveUserRoleButton' button
	And I have waited for the page to reload
	Then the user role 'Test Role' should be edited

@UserRoles
Scenario: Deleting Custom Role 
	Given I have logged in as company with id '55881'
	And I have created 'Test Role'
	And I have navigated to user roles page
	And I have entered 'Test Role' into the 'UserRoles' field
	And I have entered 'Test Role' Id into the 'UserRolesId' field
	And I have triggered after select drop down event for 'UserRoles'
	When I press 'DeleteUserRoleButton' button
	When I select 'yes' on confirmation
	Then the user role 'TestRole' should be deleted

@UserRoles
Scenario: Editing custom role with users attached launches notification dialog
	Given I have logged in as company with id '55881'
	And I have navigated to user roles page
	And I have entered 'Test Role With Users' and 'AB4117B1-C31A-4303-9EEA-5C28C6A7B009' into the 'UserRoles' combobox
	When I press 'EditUserRoleButton' button
	And I have checked 'AddSiteDetails' permission checkbox
	And I have checked 'ViewSiteDetails' permission checkbox
	When I press 'SaveUserRoleButton' button
	Then the 'dialogUsersAffectedByRoleEdit' modal confirmation should open 'true'

@UserRoles
Scenario: Cancel button closes the dialog when editing custom role with users attached
	Given I have logged in as company with id '55881'
	And I have navigated to user roles page
	And I have entered 'Test Role With Users' and 'AB4117B1-C31A-4303-9EEA-5C28C6A7B009' into the 'UserRoles' combobox
	When I press 'EditUserRoleButton' button
	And I have checked 'AddSiteDetails' permission checkbox
	And I have checked 'ViewSiteDetails' permission checkbox
	When I press 'SaveUserRoleButton' button
	When I select 'Cancel' on confirmation
	Then the 'dialogUsersAffectedByRoleEdit' modal confirmation dialog should close

@UserRoles
Scenario: Confirm button saves the dialog when editing custom role with users attached
	Given I have logged in as company with id '55881'
	And I have navigated to user roles page
	And I have entered 'Test Role With Users' and 'AB4117B1-C31A-4303-9EEA-5C28C6A7B009' into the 'UserRoles' combobox
	When I press 'EditUserRoleButton' button
	And I have checked 'AddSiteDetails' permission checkbox
	And I have checked 'ViewSiteDetails' permission checkbox
	When I press 'SaveUserRoleButton' button
	Then the 'dialogUsersAffectedByRoleEdit' modal confirmation should open 'false'
	When I select 'Yes' on confirmation
	Then the user role 'Test Role With Users' should be edited

@UserRoles
Scenario: Manage User Role Button States
	Given I have logged in as company with id '55881'
	And I have navigated to user roles page
	And I have entered 'Test Role With Users' and 'AB4117B1-C31A-4303-9EEA-5C28C6A7B009' into the 'UserRoles' combobox
	Then the 'SaveUserRoleButton' button visibility is 'false'
	Then the 'EditUserRoleButton' button visibility is 'true'
	Then the 'DeleteUserRoleButton' button visibility is 'false'

@UserRoles
Scenario: Manage User Role Button States on Click Edit
	Given I have logged in as company with id '55881'
	And I have navigated to user roles page
	And I have entered 'Test Role With Users' and 'AB4117B1-C31A-4303-9EEA-5C28C6A7B009' into the 'UserRoles' combobox
	When I press 'EditUserRoleButton' button
	Then the 'SaveUserRoleButton' button visibility is 'true'
	Then the 'EditUserRoleButton' button visibility is 'false'
	Then the 'DeleteUserRoleButton' button visibility is 'true'