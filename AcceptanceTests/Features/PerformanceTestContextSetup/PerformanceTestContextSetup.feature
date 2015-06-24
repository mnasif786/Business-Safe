@PerformanceTestContextSteps
@ignore
Feature: Setup Performance Test Context
	In order to test performance of the added documents
	As a business safe developer
	I want to be create a test performance context

Scenario: Setup Performance Test Context
	Given I have entered '1000' Risk Assessments 
	And I have entered '1000' Risk Assessments each with '10' Risk Assessment Documents
	And I have entered '1000' Risk Assessments each with '1000' Further Control Measure Task Documents
	
