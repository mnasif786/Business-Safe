
Feature: Log Out
	In order to ensure no one else can access my business safe online account
	As a business safe online user who is currently logged into the system
	I want to be able to log out

@ignore
Scenario: User Log Out Link Available
	Given I have logged in as company with id '55881'
	Then the Log Out menu item should be available

@ignore
Scenario: User Logging out of Business Safe
	Given I have logged in as company with id '55881'
	And I have clicked the 'LogOutLink'
	Then I should be redirected to Peninsula login page