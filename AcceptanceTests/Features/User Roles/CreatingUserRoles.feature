Feature: Creating User Roles
	In order to manage users
	As a business safe online user with permission
	I want to be able to add user roles

@UserRoles
Scenario: Create User Role Setting permissions before saving
	Given I have logged in as company with id '55881'
	And I have navigated to add user roles page
	And I have entered 'Test Role' into the 'RoleName' field
	And I have checked 'ViewCompanyDetails' permission checkbox
	And I have checked 'EditCompanyDetails' permission checkbox
	When I press 'SaveUserRoleButton' button
	Then the user role 'Test Role' should be created

@UserRoles
Scenario: Create User Role save then set roles
	Given I have logged in as company with id '55881'
	And I have navigated to add user roles page
	And I have entered 'Test Role' into the 'RoleName' field
	And I press 'SaveUserRoleButton' button
	When I press 'EditUserRoleButton' button
	And I have checked 'ViewCompanyDetails' permission checkbox
	And I have checked 'EditCompanyDetails' permission checkbox
	And I press 'SaveUserRoleButton' button
	Then the user role 'Test Role' should be created
	And the 'SaveUserRoleButton' button visibility is 'false'
	And the 'EditUserRoleButton' button visibility is 'true'

@UserRoles
Scenario: Create User Role Button States
	Given I have logged in as company with id '55881'
	And I have navigated to add user roles page
	Then the 'SaveUserRoleButton' button visibility is 'true'
	Then the 'EditUserRoleButton' button visibility is 'false'
	Then the 'DeleteUserRoleButton' button visibility is 'false'

