--------------------------------------------------------------------------------------------------------------------------------------------------------
			/* Task Category required for user interface ddl */
----------------------------------------------------------------------------------------------------------------------------------------------------------

SET DATEFORMAT YMD

SET IDENTITY_INSERT [ResponsibilityTask] ON;
DELETE FROM ResponsibilityTask WHERE Id = 1
DELETE FROM ResponsibilityTask WHERE Id = 2
DELETE FROM ResponsibilityTask WHERE Id = 3
DELETE FROM ResponsibilityTask WHERE Id = 4
DELETE FROM ResponsibilityTask WHERE Id = 5
DELETE FROM ResponsibilityTask WHERE Id = 6
              
INSERT INTO [BusinessSafe].[dbo].[ResponsibilityTask]
           ([Id],[ResponsibilityTaskCategoryId], [ResponsibilityTaskTypeId], [Reference], [Title], [AssignedToId], [Description], 
           [CompletionDueDate], [Urgent], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
     VALUES
           (1,1,1,'Reference 1','Title 1',1,'Description 1','2012-04-05 11:00:00.000',1,0,'2012-04-06 11:00:00.000'
           ,'790D8CC9-04F8-4643-90EE-FAED4BA711EC','2012-04-06 11:00:00.000','790D8CC9-04F8-4643-90EE-FAED4BA711EC')

INSERT INTO [BusinessSafe].[dbo].[ResponsibilityTask]
           ([Id],[ResponsibilityTaskCategoryId], [ResponsibilityTaskTypeId], [Reference], [Title], [AssignedToId], [Description] 
           , [CompletionDueDate], [Urgent], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
     VALUES
           (2,1,1,'Reference 2','Title 2',1,'Description 2','2012-04-05 11:00:00.000',1,0,'2012-04-06 11:00:00.000'
           ,'790D8CC9-04F8-4643-90EE-FAED4BA711EC','2012-04-06 11:00:00.000','790D8CC9-04F8-4643-90EE-FAED4BA711EC')

INSERT INTO [BusinessSafe].[dbo].[ResponsibilityTask]
           ([Id],[ResponsibilityTaskCategoryId], [ResponsibilityTaskTypeId], [Reference], [Title], [AssignedToId], [Description], 
           [CompletionDueDate], [Urgent], [Deleted], [CreatedOn], [CreatedBy] ,[LastModifiedOn], [LastModifiedBy])
     VALUES
           (3,1,1,'Reference 3','Title 3',1,'Description 3','2012-04-05 11:00:00.000',1,0,'2012-04-06 11:00:00.000'
           ,'790D8CC9-04F8-4643-90EE-FAED4BA711EC','2012-04-06 11:00:00.000','790D8CC9-04F8-4643-90EE-FAED4BA711EC')

INSERT INTO [BusinessSafe].[dbo].[ResponsibilityTask]
           ([Id],[ResponsibilityTaskCategoryId], [ResponsibilityTaskTypeId], [Reference], [Title], [AssignedToId], [Description], 
           [CompletionDueDate], [Urgent], [Deleted], [CreatedOn], [CreatedBy] ,[LastModifiedOn], [LastModifiedBy])
     VALUES
           (4,1,1,'Reference 4','Title 4',1,'Description 4','2012-04-05 11:00:00.000',1,0,'2012-04-06 11:00:00.000'
           ,'790D8CC9-04F8-4643-90EE-FAED4BA711EC','2012-04-06 11:00:00.000','790D8CC9-04F8-4643-90EE-FAED4BA711EC')
     
INSERT INTO [BusinessSafe].[dbo].[ResponsibilityTask]
           ([Id],[ResponsibilityTaskCategoryId], [ResponsibilityTaskTypeId], [Reference], [Title], [AssignedToId], [Description], 
           [CompletionDueDate], [Urgent], [Deleted], [CreatedOn], [CreatedBy] ,[LastModifiedOn], [LastModifiedBy])
     VALUES
           (5,2,2,'Reference 5','Title 5',1,'Description 5','2012-04-05 11:00:00.000',1,0,'2012-04-06 11:00:00.000'
           ,'790D8CC9-04F8-4643-90EE-FAED4BA711EC','2012-04-06 11:00:00.000','790D8CC9-04F8-4643-90EE-FAED4BA711EC')

INSERT INTO [BusinessSafe].[dbo].[ResponsibilityTask]
           ([Id],[ResponsibilityTaskCategoryId], [ResponsibilityTaskTypeId], [Reference], [Title], [AssignedToId], [Description], 
           [CompletionDueDate], [Urgent], [Deleted], [CreatedOn], [CreatedBy] ,[LastModifiedOn], [LastModifiedBy])
     VALUES
           (6,2,2,'Reference 6','Title 6',1,'Description 6','2012-04-05 11:00:00.000',1,0,'2012-04-06 11:00:00.000'
           ,'790D8CC9-04F8-4643-90EE-FAED4BA711EC','2012-04-06 11:00:00.000','790D8CC9-04F8-4643-90EE-FAED4BA711EC')
GO
                      
SET IDENTITY_INSERT [ResponsibilityTask] OFF;

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM ResponsibilityTask WHERE Id = 1
DELETE FROM ResponsibilityTask WHERE Id = 2
DELETE FROM ResponsibilityTask WHERE Id = 3
DELETE FROM ResponsibilityTask WHERE Id = 4
DELETE FROM ResponsibilityTask WHERE Id = 5
DELETE FROM ResponsibilityTask WHERE Id = 6
