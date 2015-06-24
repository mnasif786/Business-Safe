@user
Feature: Promote Employee To User
	In order to create a new user
	As a BSO Admin User
	I want to create a new User from an existing Employee

Scenario: Validate email is not already being used in Peninsula Online
	Given I have an employee using an email that is already registered in Peninsula Online
	When I attempt to promote them to a user
	Then the Error List Contains
         | Error Message																		   |
         | Sorry you are unable to create this User: the email address has already been registered |
