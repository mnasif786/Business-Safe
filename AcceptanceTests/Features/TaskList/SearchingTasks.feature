Feature: Searching Tasks
	As A BSO User
	When I search tasks in my task list
	I should be shown a list of tasks

@finetune
Scenario: Search by Site Id
	Given I have logged in as company with id '55881'
	#And I have entered '--Select Option--' into the 'Employee' field
	And I have entered '' into the 'EmployeeId' field
	#And I have entered 'Barnsley' into the 'Site' field
	And I have entered '379' into the 'SiteId' field
	And I have triggered after select drop down event for 'Site'
	When I press 'ResponsibilityTaskCategoryGoToResponsibilityTasks' button
	And I have waited for the page to reload
	Then the task lists results table should contain the following data:
	| Task Reference				 | Task Category						| Title								| Description						| Assigned To | Created Date | Due Date		| Completed |
	| Task To Delete From Task List  | Hazardous Substance Risk Assessment	| Task To Delete From Task List		| Task To Delete From Task List		| Barry Brown | 02/10/2012   | 31/10/2012	| Outstanding |
	| Edit Task Test				 | Hazardous Substance Risk Assessment	| Edit Task Test					| Edit Task Test					| Barry Brown | 02/10/2012   | 31/10/2012	| Outstanding |
	
	
@finetune
Scenario: Search by Site Id Aberdeen
	Given I have logged in as company with id '55881'
	#And I have entered '--Select Option--' into the 'Employee' field
	And I have entered '' into the 'EmployeeId' field
	#And I have entered 'Aberdeen' into the 'Site' field
	And I have entered '378' into the 'SiteId' field
	When I press 'ResponsibilityTaskCategoryGoToResponsibilityTasks' button
	And I have waited for the page to reload
	Then the task lists results table should contain the following data:
	| Task Reference					| Task Category				| Title								| Description						| Assigned To	| Created Date | Due Date	| Completed		|
	| Task To Delete From Task List		| General Risk Assessment	| Task To Delete From Task List		| Task To Delete From Task List		| Barry Brown	| 02/10/2012   | 31/10/2012 | Outstanding	|
	| Buy Bigger Sign					| General Risk Assessment	| Buy Bigger Sign					| Buy Bigger Sign					| Kim Howard	| 30/08/2012   | 31/08/2016 | Outstanding	|
	| Reoccuring Task Test				| General Risk Assessment	| Reoccuring Task Test				| Reoccuring Task Test				| Kim Howard	| 30/08/2012   | 31/08/2016 | Outstanding	|
	
	
@finetune
Scenario: Search by Task Category Responsibility
	Given I have logged in as company with id '55881'
	And I have entered '' into the 'EmployeeId' field
	And I have entered '7' into the 'TaskCategoryId' field
	When I press 'ResponsibilityTaskCategoryGoToResponsibilityTasks' button
	And I have waited for the page to reload
	Then the task list results table should contain '4' rows
	
@finetune
Scenario: Search by Site Group
	Given I have logged in as company with id '55881'
	#And I have entered '--Select Option--' into the 'Employee' field
	And I have entered '' into the 'EmployeeId' field
	#And I have entered 'Task Search Group' into the 'SiteGroup' field
	And I have entered '381' into the 'SiteGroupId' field
	And I have triggered after select drop down event for 'SiteGroup'
	When I press 'ResponsibilityTaskCategoryGoToResponsibilityTasks' button
	And I have waited for the page to reload
	Then the task lists results table should contain the following data:
	| Task Reference		| Task Category							| Title				| Description			| Assigned To	| Created Date | Due Date	| Completed		|
	| Edinburgh Task		| Hazardous Substance Risk Assessment	| Edinburgh Task	| Edinburgh Task		| Barry Brown	| 02/10/2012   | 31/10/2012 | Outstanding	|
	


Scenario: Task Summary Updates On Changing Task Search Form
	Given I have logged in as company with id '55255'
	And I have entered '--Select Option--' into the 'Employee' field
	And I have entered '' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	And the 'statusWrapperRed' summary should be '11'
	And the 'statusWrapperGreen' summary should be '9'
	And the 'statusWrapperGray' summary should be '20'
	And I have entered 'Manchester' into the 'Site' field
	And I have entered '393' into the 'SiteId' field
	And I have triggered after select drop down event for 'Site'
	And the 'statusWrapperRed' summary should be '4'
	And the 'statusWrapperGreen' summary should be '3'
	And the 'statusWrapperGray' summary should be '7'
	And I have entered 'Buzz Lightyear ( Space Ranger )' into the 'Employee' field
	And I have entered 'c582c3c0-11b9-4f11-be7a-4f0def837634' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	And the 'statusWrapperRed' summary should be '2'
	And the 'statusWrapperGreen' summary should be '1'
	And the 'statusWrapperGray' summary should be '3'
	And I have entered '--Select Option--' into the 'Employee' field
	And I have entered '' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	And I have entered '--Select Option--' into the 'Site' field
	And I have entered '' into the 'SiteId' field
	And I have triggered after select drop down event for 'Site'
	And I have entered 'Hazardous Substance Risk Assessment' into the 'TaskCategory' field
	And I have entered '6' into the 'TaskCategoryId' field
	And I have triggered after select drop down event for 'TaskCategory'
	And the 'statusWrapperRed' summary should be '7'
	And the 'statusWrapperGreen' summary should be '6'
	And the 'statusWrapperGray' summary should be '13'
	And I have entered 'North East' into the 'SiteGroup' field
	And I have entered '395' into the 'SiteGroupId' field
	And I have triggered after select drop down event for 'SiteGroup'
	And I have entered '--Select Option--' into the 'TaskCategory' field
	And I have entered '' into the 'TaskCategoryId' field
	And I have triggered after select drop down event for 'TaskCategory'
	And I have entered 'Stretch Armstrong ( Stretchy Guy )' into the 'Employee' field
	And I have entered 'eaf7d42b-0542-4f94-8b59-c83ec8e8c026' into the 'EmployeeId' field
	And I have triggered after select drop down event for 'Employee'
	And the 'statusWrapperRed' summary should be '3'
	And the 'statusWrapperGreen' summary should be '4'
	And the 'statusWrapperGray' summary should be '7'