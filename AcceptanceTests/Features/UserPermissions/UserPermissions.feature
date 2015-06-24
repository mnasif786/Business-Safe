Feature: User Permissions
	In order to use  business safe online
	As a user
    I only want access to my permission based functionality

@Ignore
#user does not have permission to responsibility planner, so put back when we have home page
Scenario: General User Login
	Given I have logged in as company with id '55881' as a 'General User'
	Then the 'ViewSitesLink' menu item should not be available
	Then the 'ViewEmployeesLink' menu item should not be available
	Then the 'ViewCompanyDetailsLink' menu item should be available
	Then the 'ViewGeneralRiskAssessmentsLink' menu item should not be available
	Then the 'AddEmployeeLink' menu item should not be available
	Then the 'ViewUsersLink' menu item should not be available

Scenario: Admin User Login
	Given I have logged in as company with id '55881'
	Then the 'UserRolesLink' menu item should be available
	Then the 'ViewSitesLink' menu item should be available
	Then the 'ViewUsersLink' menu item should be available
	Given I have clicked the 'ViewSitesLink'
	And I have clicked the 'main site' in the site tree structure
	Then the 'SaveSiteDetailsButton' should be available
	Given I have clicked the 'AddSiteGroupLink'
	Then the 'SaveSiteGroupButton' should be available
	Then the 'ViewEmployeesLink' menu item should be available
	Then the 'ViewCompanyDetailsLink' menu item should be available
	Then the 'ViewGeneralRiskAssessmentsLink' menu item should be available
    Given I have navigated straight to the 'view employees' url
	Then the 'EmployeeSearchAddEmployeeLink' menu item should be available
	#Then the 'EmployeeSearchBatchUploadEmployeesLink' menu item should be available
    Then the 'editLink icon-edit' command should be available
	Then the 'icon-remove' command should be available
	Then the 'viewLink icon-search' command should be available
	Given I have clicked on the 'editLink' command for the first employee record
	Then the 'SaveEmployeeButton' on the employee edit screen should be available
	Given I have navigated straight to the 'user search' url
    Then the 'EditUserIconLink' link should be available
	Then the 'DeleteUserIconLink' link should be available
	Then the 'ViewUserIconLink' link should be available

Scenario: General User Login Bypassing Navigation
	Given I have logged in as company with id '55881' as a 'General User'
	When I have navigated straight to the 'sites' url
	Then I should be redirected to Peninsula login page
	Given I have logged in as company with id '55881' as a 'General User'
	When I have navigated straight to the 'view employees' url
	Then I should be redirected to Peninsula login page
	Given I have logged in as company with id '55881' as a 'General User'
	When I have navigated straight to the 'edit employee' url
	Then I should be redirected to Peninsula login page
	Given I have logged in as company with id '55881' as a 'General User'
	When I have navigated straight to the 'user search' url
	Then I should be redirected to Peninsula login page