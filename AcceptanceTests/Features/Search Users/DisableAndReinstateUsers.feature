Feature: Disabling and reinstaing users
	In order to maintain users
	As a business safe user with the relevant permissions
	I want to be able to disable and reinstate users

Background: 
	Given I have logged in as company with id '55881'
	And I am on the search users page for company '55881'

@finetune
@DisableUser
Scenario: Disable user
	And I have entered 'Kim' into the 'Forename' field
	And I have entered 'Howard' into the 'Surname' field
	And I press 'Search' button
	When I press 'Delete' link for user with name 'Kim' 'Howard'
	When I select 'yes' on confirmation
	And I wait for '2000' miliseconds
	Then the user 'Kim' 'Howard' should be deleted

@ReinstateUser
@finetune
@ignore
Scenario: Reinstate user
	And I have checked show deleted users
	When I press 'Reinstate' link for user with name 'Kim' 'Howard'
	When I select 'yes' on confirmation
	And I have waited for the page to reload
	Then the user 'Kim' 'Howard' should be reinstated