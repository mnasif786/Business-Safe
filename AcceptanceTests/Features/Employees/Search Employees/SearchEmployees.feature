@SearchingEmployee
Feature: Searching Employees
	In order to maintain employees
	As a business safe user with the relevant permissions
	I want to be search and view employees

Background: Setup Employees for scenario
	Given I have the following employees for company '55881':
	| Forename | Surname | Job Title | Site     | Employee Reference |
	| Bob      | Smith   | Decorator | Aberdeen | 1w                 |
	| Tracy    | Jones   | Telephone | Barnsley | 2w                 |

#Scenario: Search by employee referemce
	#Given I have logged in as company with id '55881'
	#And I am on the search employee page for company '55881'
	#And I have entered '1w' into the 'EmployeeReference' field
	#When I press 'Search' button
	#Then the result should contain row with the following:
	#| Forename | Surname | Job Title | Site     |
	#| Bob      | Smith   | Decorator | Aberdeen |

Scenario: Search by forename
	Given I have logged in as company with id '55881'
	And I am on the search employee page for company '55881'
	And I have entered 'Tracy' into the 'Forename' field
	When I press 'Search' button
	Then the result should contain row with the following:
	| Forename | Surname | Job Title | Site     |
	| Tracy   | Jones   | Telephone | Barnsley |

Scenario: Search by surname
	Given I have logged in as company with id '55881'
	And I am on the search employee page for company '55881'
	And I have entered 'Smith' into the 'Surname' field
	When I press 'Search' button
	Then the result should contain row with the following:
	| Forename | Surname | Job Title | Site     |
	| Bob      | Smith   | Decorator | Aberdeen |

Scenario: Search by site
	Given I have logged in as company with id '55881'
	And I am on the search employee page for company '55881'
	And I have entered 'Aberdeen' into the 'Site' field
	And I have entered '378' into the 'SiteId' field
	When I press 'Search' button
	Then the result should contain row with the following:
	| Forename | Surname | Job Title | Site     |
	| Bob      | Smith   | Decorator | Aberdeen |

Scenario: Search by all criteria 
	Given I have logged in as company with id '55881'
	And I am on the search employee page for company '55881'
	#And I have entered '2w' into the 'EmployeeReference' field
	And I have entered 'Tracy' into the 'Forename' field
	And I have entered 'Jones' into the 'Surname' field
	And I have entered 'Barnsley' into the 'Site' field
	And I have entered '379' into the 'SiteId' field
	When I press 'Search' button
	Then the result should contain row with the following:
	| Forename | Surname | Job Title | Site     |
	| Tracy   | Jones   | Telephone | Barnsley |