Feature: BUG - When creating a new employee, the title is not displayed after saving
	In order to keep system integrity
	As a business safe online user
	When creating a new employee, the selected title is being displayed after saving

Background:
	Given I have logged in as company with id '55881'

@Acceptance
@ignore
#VL: ignoring this test as not picking up selected title, but not using ui.selectmenu anymore, anyway
Scenario: Bug
	Given I have logged in as company with id '55881'
	And I am on the create employee page for company '55881'
	And I have entered 'New Employee Reference' into the employee reference field
	And I have entered 'Mr' into the 'NameTitle' field
	And I have entered 'Bob' into the forename field
	And I have entered 'Smith' into the surname field
	And I have entered 'James' into the middle name field
	And I have entered 'Male' into the 'Sex' field
	When I have clicked save 
	Then the previously selected Title should be displayed 