Feature: Sites Core Functionality
	In order to organise my company
	As a business safe online user
	I want to be able to organise my site address into site structures for my company

@integration
@core_functionality
Scenario: Add site group
	Given I have logged in as company with id '55881'
	And I have navigated to site structure page
	And I press 'AddSiteGroupLink' button
	And I have entered 'new site group' into site group name text field
	#And I have entered 'Main Site' into the 'GroupLinkToSite' field
	And I have entered '371' into the 'GroupLinkToSiteId' field
	When I press 'SaveSiteGroupButton' button
	Then the site group should be created and linked to correct site '371'
	
@core_functionality
Scenario: Link site 
	Given I have logged in as company with id '24072'
	And I have navigated to site structure page
	And I have clicked on the first unlinked site 
	And I have entered 'new site' into site name text field
	#And I have entered 'Main Site' into the 'LinkToSite' field
	And I have entered '375' into the 'LinkToSiteId' field
	When I press 'SaveSiteDetailsButton' button
	Then the site should be created and linked to correct site '375'
	
@core_functionality
Scenario: Delink site
	Given I have logged in as company with id '31028'
	And I have navigated to site structure page
	And I have clicked on the 'Newark' linked site 
	#And I have entered 'York' into the 'LinkToSite' field
	And I have entered '105' into the 'LinkToSiteId' field
	#And I have entered '' into the 'LinkToGroup' field
	And I have entered '' into the 'LinkToGroupId' field
	When I press 'SaveSiteDetailsButton' button
	Then the site should be moved to new parent '105'
	Given I have clicked on the 'Newark' linked site 
	#And I have entered '' into the 'LinkToSite' field
	And I have entered '' into the 'LinkToSiteId' field
	#And I have entered 'Midlands' into the 'LinkToGroup' field
	And I have entered '39' into the 'LinkToGroupId' field
	When I press 'SaveSiteDetailsButton' button
	Then the site should be moved to new parent site group '39'
