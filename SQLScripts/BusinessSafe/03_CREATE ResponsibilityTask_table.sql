----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the ResponsibilityTaskCategory table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'ResponsibilityTask' AND TYPE = 'U')
BEGIN
	CREATE TABLE	[ResponsibilityTask]
	(
		Id bigint IDENTITY(1,1) NOT NULL
		,ResponsibilityTaskCategoryId bigint NOT NULL 
		,ResponsibilityTaskTypeId bigint NULL		
		,Reference varchar(50) NOT NULL
		,Title varchar(200) NOT NULL
		,AssignedToId bigint  NULL
		,[Description] varchar(500) NULL
		,CompletionDueDate datetime NOT NULL DEFAULT GetDate()
		,Urgent  bit NOT NULL DEFAULT 0
		,CompletionDate datetime NULL		
		,Deleted bit NOT NULL DEFAULT 0
		,CreatedOn datetime NOT NULL DEFAULT GetDate()	
		,CreatedBy uniqueidentifier NULL
		,LastModifiedOn datetime NULL DEFAULT GetDate()
		,LastModifiedBy uniqueidentifier NULL
	)
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [ResponsibilityTask] TO [AllowAll]

GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'ResponsibilityTask' AND TYPE = 'U')
BEGIN
	DROP TABLE [ResponsibilityTask]
END