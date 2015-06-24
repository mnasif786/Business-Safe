Feature: DisplayLogin
	In order to know who is logged
	As a BSO User
	I want  my name to be displayed in the top right.

Scenario: Display login in top right
	Given I have logged in as company with id '55881'
	Then the element with id 'WelcomeBar' should contain 'Russell Williams'
