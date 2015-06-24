Feature: View User
	When viewing users
	As a BSO employee
	I should not be able to edit those values

Scenario: View Employee in View Mode can not edit values
	Given I have logged in as company with id '55881'
	And I am on the view user page for company '55881' and employee id 'a433e9b2-84f6-4ad7-a89c-050e914dff01'
	When I press 'close-user-record' link
	Then I am redirected to the url 'Users/ViewUsers?'
	Given I am on the view user page for company '55881' and employee id 'a433e9b2-84f6-4ad7-a89c-050e914dff01'
	When I press 'edit-user-record' link
	Then I am redirected to the url '/AddUsers?employeeId=a433e9b2-84f6-4ad7-a89c-050e914dff01&companyId=55881'

Scenario: Viewing user with correct permissions
	Given I have logged in as company with id '55881'
	And I am on the search users page for company '55881'
	Then the element with id 'ViewUserIconLink' has visibility of 'true'
	Given I have clicked the 'ViewUserIconLink'
	Then I am redirected to the url 'Users/ViewUsers/ViewUser'
	And the element with id 'PermissionsApplyToAllSites' has visibility of 'false'
	And the element with id 'SaveButton' has visibility of 'false'