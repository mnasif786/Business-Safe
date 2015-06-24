Feature: Managing Site Structures
	In order to organise my company
	As a business safe online user
	I want to be able to organise my site address into site structures for my company

Scenario: Deleting a site group
	Given I have logged in as company with id '37634'
	And I have navigated to site structure page
	And I have site group called 'site group to delete'
	And I have selected site group called 'site group to delete'
	When I press 'deleteSiteBtn' button
	And I select 'No' on confirmation
	Then I should remain on the site group details
	When I press 'deleteSiteBtn' button
	And I select 'Yes' on confirmation
	Then the site group should be deleted

Scenario: Validation For New Site
	Given I have logged in as company with id '24072'
	And I have navigated to site structure page
	And I have clicked on the first unlinked site 
	#And I have selected 'Main Site' as link to site for site
	And I have entered '375' into the 'LinkToSiteId' field
	And I have entered '' into site name text field
	When I press 'SaveSiteDetailsButton' button
	Then the should get validation message
	Given I have entered 'Now Got Site' into site name text field
	#And I have selected '--Select Option--' as link to site for site
	And I have entered '' into the 'LinkToSiteId' field
	When I press 'SaveSiteDetailsButton' button
	Then the should get validation message
	Given I have entered 'new site' into site name text field
	#And I have selected 'Main Site' as link to site for site
	And I have entered '375' into the 'LinkToSiteId' field
	When I press 'SaveSiteDetailsButton' button
	Then the site should be created and linked to correct site '375'

Scenario: Validation For New Site Group
	Given I have logged in as company with id '37634'
	#And I have no sites for company with id '37643'
	And I have navigated to site structure page
	And I press 'AddSiteGroupLink' button
	#And I have entered '' into site group name text field
	And I have selected 'Main Site' as link to site for site group
	And I have entered '377' into the 'GroupLinkToSiteId' field
	When I select save group
	Then the should get validation message
	Given I have entered 'Testing' into site group name text field
	#And I have selected '--Select Option--' as link to site for site group
	And I have entered '' into the 'GroupLinkToSiteId' field
	When I select save group
	Then the should get validation message
	Given I have entered 'new site group' into site group name text field
	#And I have selected 'Main Site' as link to site for site group
	And I have entered '377' into the 'GroupLinkToSiteId' field
	When I select save group
	Then the site group should be created and linked to correct site '377'
