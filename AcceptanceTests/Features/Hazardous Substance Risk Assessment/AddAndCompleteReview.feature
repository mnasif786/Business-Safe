Feature: AddAndCompleteReview
	In order to continually check the validity of a HS risk assessment
	As a business safe online user
	I must review the HS and record that I have reviewed it

Background:
	Given I have logged in as company with id '55881'
  
@Acceptance
Scenario: Add and complete a review
	Given I am on description tab with company Id '55881'
	And I wait for '1000' miliseconds
	And I have entered 'Test Title' into the 'Title' field
	And I have entered 'Test Reference' into the 'Reference' field
	And I have entered 'Toilet Cleaner' into the 'NewHazardousSubstance' field
	And I have entered '4' into the 'NewHazardousSubstanceId' field
	And I press 'createSummary' button
	And I press 'review' link
	And I press 'AddReview' link
	And I have entered '26/09/2024' into the 'ReviewDate' field
	And I have entered 'Kim Howard' into the 'ReviewingEmployee' field
	And I have entered 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' into the 'ReviewingEmployeeId' field
	When I press 'SaveButton' button
	Then a success message should be displayed 
	Given I have clicked on the complete review link for a review 
	And I press 'IsComplete' checkbox with client side events
	And I have entered 'hello world' into the 'CompletedComments' field
	And I have entered '01/01/2050' into the 'NextReviewDate' field
	And I press 'ReviewSaveButton' button
	Then the next review is created
