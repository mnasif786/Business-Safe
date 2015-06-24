Feature: Search Accident Records
	In order to find records
	As a BSO user 
	I want to be able to search through accident records

Scenario: Search by site 
	 Given I have logged in as company with id '55881'
	And I am on the accident record index page
	And I have entered 'Aberdeen' into the 'Site' field
	And I have entered '378' into the 'SiteId' field
	And I wait for '1000' miliseconds
	When I press 'searchButton' button
	And I wait for '1000' miliseconds
	Then The following record should exist in the accident record table:
	| Ref    | Title                 | Description                       | Injured Person      | Severity     | Site                  | Reported By      | Date Of Accident | Date Created |
	| REF001 | Assassinated by Alan  | One                               | Barry Scott         | Fatal        | Aberdeen			  | Russell Williams | 01/01/2011       | 01/01/2012   |

Scenario: Search by Created From date
	Given I have logged in as company with id '55881'
	And I am on the accident record index page
	And I have entered '09/01/2012' into the 'CreatedFrom' field	
	And I wait for '1000' miliseconds
	When I press 'searchButton' button
	And I wait for '1000' miliseconds
	Then The following record should exist in the accident record table:
	| Ref    | Title                 | Description                       | Injured Person      | Severity     | Site                  | Reported By      | Date Of Accident | Date Created |
	| REF011 | Impaled by Ivan       | Three                             | Glen Ross           | Fatal        | Barnsley			  | Russell Williams | 01/09/2011       | 09/01/2012   |

#Scenario: Search by Created To date
#	Given I have logged in as company with id '55881'
#	And I am on the accident record index page
#	And I have entered 'Aberdeen' into the 'Site' field
#	And I have entered '378' into the 'SiteId' field
#	And I wait for '1000' miliseconds
#	When I press 'searchButton' button
#	And I wait for '1000' miliseconds
#	Then The following record should exist in the accident record table:
#	| Ref    | Title                 | Description                       | Injured Person      | Severity     | Site                  | Reported By      | Date Of Accident | Date Created |
#	| REF001 | Assassinated by Alan  | One                               | Main Site            | Compliance    | Russell Williams     | Outstanding      | 22/07/2013       | 10/06/2050   |

Scenario: Search by Title
	Given I have logged in as company with id '55881'
	And I am on the accident record index page
	And I have entered 'Impaled by Ivan' into the 'Title' field	
	And I wait for '1000' miliseconds
	When I press 'searchButton' button
	And I wait for '1000' miliseconds
	Then The following record should be the first one returned in the accident record table:
	| Ref    | Title           | Description | Injured Person | Severity | Site     | Reported By      | Status | Date Of Accident | Date Created |
	| REF011 | Impaled by Ivan | Three       | Glen Ross      | Fatal    | Barnsley | Russell Williams | Open   | 01/09/2011       | 09/01/2012   |

#Scenario: Search by Injured peson
#	Given I have logged in as company with id '55881'
#	And I am on the accident record index page
#	And I have entered 'Aberdeen' into the 'Site' field
#	And I have entered '378' into the 'SiteId' field
#	And I wait for '1000' miliseconds
#	When I press 'searchButton' button
#	And I wait for '1000' miliseconds
#	Then The following record should exist in the accident record table::
#	| Ref    | Title                 | Description                       | Injured Person      | Severity     | Site                  | Reported By      | Date Of Accident | Date Created |
#	| REF001 | Assassinated by Alan  | One                               | Main Site            | Compliance    | Russell Williams     | Outstanding      | 22/07/2013       | 10/06/2050   |