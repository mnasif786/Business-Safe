
Feature: Sensitive Flag
#A Personal Risk Assessment with Sensitive flag checked should be visible to only the Created By and Risk Assessor


Background:
	Given I have logged in as company with id '55881'

@Acceptance
@Personal_Risk_Assessments
@Personal_Risk_Assessments_Sensitivity
Scenario: A sensitive personal risk assessment only viewable by risk assessor
	Given I am on create riskassessment page in area 'PersonalRiskAssessments' with company Id '55881'
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	Given I press 'createSummary' button
	And I wait for '1000' miliseconds
	And I am redirected to the summary page for the new personal risk assessment
	And I have entered 'Barnsley' into the 'Site' field
	And I have entered '379' into the 'SiteId' field
	And I have entered 'Barry Scott' into the 'RiskAssessor' field
	And I have entered '1' into the 'RiskAssessorId' field
	And I have entered '21/04/2020' into the 'DateOfAssessment' field
	And 'Sensitive' check box is ticked 'true'
	And I press 'saveButton' button
	And I have waited for the page to reload
	And I have clicked the 'LogOutLink'
	# Barry Scott
	When I have logged in as company with id '55881' as Barry Scott
	And I am on the personal risk assessments page for company '55881'
	Then the risk assessment table should contain the following data:
	| Reference			 | Title                                       | Site      | Assigned To	  | Status | Completion Due Date |
	| CFPRA				 | Core Functionality Personal Risk Assessment | Aberdeen  |				  | Draft  |                     |
	| PRA04				 | PRA 4									   | Edinburgh | Russell Williams | Live   | 21/04/2012          |
	| PRA03				 | PRA 3									   | Barnsley  | Russell Williams | Live   | 23/06/2012          |
	| PRA02				 | PRA 2									   | Aberdeen  | Russell Williams | Live   | 21/04/2013          |
	| make sensitive by other user test | PRA created by Kim                          | Aberdeen | Kim Howard       | Draft  | 12/06/2013          |
	| PRA01				 | PRA 1									   | Aberdeen  | Russell Williams | Live   | 23/06/2013          |
	| Test Reference     | Test Title								   | Barnsley  | Barry Scott	  | Draft  | 21/04/2020          |

@Acceptance
@Personal_Risk_Assessments
@Personal_Risk_Assessments_Sensitivity
@finetune
	Scenario: A sensitive personal risk assessment not viewable by anyone else
	Given I am on create riskassessment page in area 'PersonalRiskAssessments' with company Id '55881'
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	Given I press 'createSummary' button
	And I wait for '1000' miliseconds
	And I am redirected to the summary page for the new personal risk assessment
	And I have entered 'Barnsley' into the 'Site' field
	And I have entered '379' into the 'SiteId' field
	And I have entered 'Kim Howard' into the 'RiskAssessor' field
	And I have entered '2' into the 'RiskAssessorId' field
	And I have entered '23/06/2020' into the 'DateOfAssessment' field
	And 'Sensitive' check box is ticked 'true'
	And I press 'saveButton' button
	And I have clicked the 'LogOutLink'
	When I have logged in as company with id '55881' as Barry Scott
	And I am on the personal risk assessments page for company '55881'
	Then the risk assessment table should contain the following data:
	| Reference			 | Title                                       | Site      | Assigned To	  | Status | Completion Due Date |
	| CFPRA				 | Core Functionality Personal Risk Assessment | Aberdeen  |				  | Draft  |                     |
	| PRA04				 | PRA 4									   | Edinburgh | Russell Williams | Live   | 21/04/2012          |
	| PRA03				 | PRA 3									   | Barnsley  | Russell Williams | Live   | 23/06/2012          |
	| PRA02				 | PRA 2									   | Aberdeen  | Russell Williams | Live   | 21/04/2013          |
	| make sensitive by other user test | PRA created by Kim                          | Aberdeen | Kim Howard       | Draft  | 12/06/2013          |
	| PRA01				 | PRA 1									   | Aberdeen  | Russell Williams | Live   | 23/06/2013          |
	When I try to hack the url as Barry Scott to view sensitive personal risk assessment
	Then The error page is displayed

@Personal_Risk_Assessments
@Personal_Risk_Assessments_Sensitivity
Scenario: A non-sensitive personal risk assessment changed to sensitive by its creator
	# logged in as russell, editing PRA created by russell
	Given I am on the summary page of Personal Risk Assessment with id '49' and companyId '55881'
	Then the element with id 'Sensitive' has a 'readonly' attribute of 'False'
	Then the element with id 'RiskAssessor' has a 'readonly' attribute of 'False'
	Then the element with id 'RiskAssessorId' has a 'readonly' attribute of 'False'
	Then the element with id 'Site' has a 'readonly' attribute of 'False'
	Then the element with id 'SiteId' has a 'readonly' attribute of 'False'
