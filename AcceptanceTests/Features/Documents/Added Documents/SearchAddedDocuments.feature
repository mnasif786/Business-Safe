Feature: SearchAddedDocuments
	In order to find documents
	As a user
	I want to be adle to search documents

Background:
	Given I have logged in as company with id '55881'

@Acceptance
Scenario: SearchWithoutFilter
	Given I am on the added documents page for company '55881'
	When I press 'Search' button
	Then the added document results table should contain the following data:
	| Title        | 
	| FCM Document 03 | 
	| FCM Document 02 |
	| FCM Document 01 |
	| GRA Document 05 |
	| GRA Document 04 |
	| GRA Document 03 |
	| GRA Document 02 |
	| Added Document 01 |
	| GRA Document 01 |

@Acceptance
Scenario: SearchOnDocumentType
	Given I am on the added documents page for company '55881'
	And I have entered 'GRA Document' into the 'DocumentType' field
	And I have entered '1' into the 'DocumentTypeId' field
	When I press 'Search' button
	Then the added document results table should contain the following data:
	| Title        | 
	| FCM Document 03 | 
	| FCM Document 02 |
	| FCM Document 01 |
	| Added Document 01 |
	| GRA Document 01 |

@Acceptance
Scenario: SearchOnTitle
	Given I am on the added documents page for company '55881'
	And I have entered 'Document 01' into the 'Title' field
	And I press 'Search' button
	Then the added document results table should contain the following data:
	| Title        | 
	| FCM Document 01 |
	| Added Document 01 |
	| GRA Document 01 |

@Acceptance
Scenario: SearchOnDocumentTypeAndTitle
	Given I am on the added documents page for company '55881'
	And I have entered 'GRA Document' into the 'DocumentType' field
	And I have entered '1' into the 'DocumentTypeId' field
	And I have entered 'Document 02' into the 'Title' field
	When I press 'Search' button
	Then the added document results table should contain the following data:
	| Title        | 
	| FCM Document 02 |


@Acceptance
Scenario: SortOnDateUploadedAscending
	Given I am on the added documents page for company '55881'
	When I press link with Text 'Date Uploaded'
	Then the added document results table should contain the following data:
	| Title        | 
	| GRA Document 01 |
	| Added Document 01 |
	| GRA Document 02 |
	| GRA Document 03 |
	| GRA Document 04 |
	| GRA Document 05 |
	| FCM Document 01 |
	| FCM Document 02 |
	| FCM Document 03 | 

@Acceptance
Scenario: SortOnTitleAscending
	Given I am on the added documents page for company '55881'
	When I press link with Text 'Title'
	Then the added document results table should contain the following data:
	| Title        | 
	| Added Document 01 |
	| FCM Document 01 |
	| FCM Document 02 |
	| FCM Document 03 | 
	| GRA Document 01 |
	| GRA Document 02 |
	| GRA Document 03 |
	| GRA Document 04 |
	| GRA Document 05 |

#can't get double click to work.
@ignore 
@Acceptance
Scenario: SortOnTitleDescending
	Given I am on the added documents page for company '55881'
	When I double click link with text 'Title'
	Then the added document results table should contain the following data:
	| Title        | 
	| GRA Document 05 |
	| GRA Document 04 |
	| GRA Document 03 |
	| GRA Document 02 |
	| GRA Document 01 |
	| FCM Document 03 | 
	| FCM Document 02 |
	| FCM Document 01 |
	| Added Document 01 |