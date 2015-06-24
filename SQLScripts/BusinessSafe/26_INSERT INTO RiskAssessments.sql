USE [BusinessSafe]
GO

SET IDENTITY_INSERT [dbo].[RiskAssessment] ON
INSERT INTO [BusinessSafe].[dbo].[RiskAssessment] ([Id], [Title], [Reference], [AssessmentDate], [PreviousAssessment], [ClientId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [SiteId], [Location], [TaskProcessDescription], [RiskAssessorEmployeeId]) VALUES (1, 'Test Risk Assessment 1', 'TRA01', '2012-06-01 10:00:00.000', '2011-06-01 10:00:00.000', 33749, 0, '2012-06-01 10:00:00.000', '790d8cc9-04f8-4643-90ee-faed4ba711ec', NULL, NULL, NULL, NULL, NULL, 100)
INSERT INTO [BusinessSafe].[dbo].[RiskAssessment] ([Id], [Title], [Reference], [AssessmentDate], [PreviousAssessment], [ClientId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [SiteId], [Location], [TaskProcessDescription], [RiskAssessorEmployeeId]) VALUES (2, 'Test Risk Assessment 2', 'TRA02', '2012-06-01 10:00:00.000', '2011-06-01 10:00:00.000', 33749, 0, '2012-06-01 10:00:00.000', '790d8cc9-04f8-4643-90ee-faed4ba711ec', NULL, NULL, NULL, NULL, NULL, 100)
SET IDENTITY_INSERT [dbo].[RiskAssessment] OFF

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM [BusinessSafe].[dbo].[RiskAssessment]
