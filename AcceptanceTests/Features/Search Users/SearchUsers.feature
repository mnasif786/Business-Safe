Feature: Searching Users
	In order to maintain users
	As a business safe user with the relevant permissions
	I want to be search and view users

Scenario: Search by forename
	Given I have logged in as company with id '55881'
	And I am on the search users page for company '55881'
	And I have entered 'Glen' into the 'Forename' field
	When I press 'Search' button
	Then the user results table should contain the following data:
	| Ref      | Forename | Surname | Job Title  | Site       | Role         |
	| Tester 3 | Glen     | Ross    | HR Manager | ALL		  | General User |

Scenario: Search by surname
	Given I have logged in as company with id '55881'
	And I am on the search users page for company '55881'
	And I have entered 'Conner' into the 'Surname' field
	When I press 'Search' button
	Then the user results table should contain the following data:
	| Ref      | Forename | Surname | Job Title | Site       | Role        |
	| ICTO1    | John     | Conner  | HR Manager | Aberdeen  | General User |

Scenario: Search by site
	Given I have logged in as company with id '55881'
	And I am on the search users page for company '55881'
	And I have entered 'Barnsley' into the 'Site' field
	And I have entered '379' into the 'SiteId' field
	When I press 'Search' button
	Then the user results table should contain the following data:
	| Ref   | Forename | Surname | Job Title        | Site     | Role                      |
	| XDTO1 | Barry      | Brown  | Team leader | Barnsley | Test Role With Users |

Scenario: Search for deleted users
	Given I have logged in as company with id '55881'
	And I am on the search users page for company '55881'
	And I have checked show deleted users
	Then the user results table should contain the following data:
	| Ref      | Forename | Surname | Job Title        | Site       | Role         |
	| KLTO1    | Deleted  | user    | Business Analyst | Barnsley   | General User |

Scenario: Search by all criteria 
	Given I have logged in as company with id '55881'
	And I am on the search users page for company '55881'
	And I have entered 'John' into the 'Forename' field
	And I have entered 'Conner' into the 'Surname' field
	And I have entered 'Aberdeen' into the 'Site' field
	And I have entered '378' into the 'SiteId' field
	When I press 'Search' button
	Then the user results table should contain the following data:
	| Ref      | Forename | Surname | Job Title | Site       | Role         |
	| ICTO1    | John     | Conner  | HR Manager | Aberdeen  | General User |