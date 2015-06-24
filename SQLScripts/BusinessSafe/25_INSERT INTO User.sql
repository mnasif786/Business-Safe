USE [BusinessSafe]
GO

-- INSERT INTO [BusinessSafe].[dbo].[User]
-- 		([UserId], [EmployeeId], [RoleId], [RegistrationError], [RegistrationErrorMessage], [Deleted], [CreatedOn], [CreatedById], 
-- 		[LastModifiedOn], [LastModifiedById], [ClientId])
-- 	VALUES
-- 		('790D8CC9-04F8-4643-90EE-FAED4BA711EC', '790D8CC9-04F8-4643-90EE-FAED4BA711EC', '1e382767-93dd-47e2-88f2-b3e7f7648642', 0, null, 0, '2012-07-09 11:00:00.000',
-- 		'790D8CC9-04F8-4643-90EE-FAED4BA711EC', null, null, 4)

-- INSERT INTO [BusinessSafe].[dbo].[User] 
-- 		([UserId], [EmployeeId], [RoleId], [RegistrationError], [RegistrationErrorMessage], [Deleted], [CreatedOn], [CreatedById], 
-- 		[LastModifiedOn], [LastModifiedById], [ClientId]) 
-- 	VALUES ('16ac58fb-4ea4-4482-ac3d-000d607af67c', 'fbee2bd9-2b53-456d-89b3-b73cdade27d8', '1e382767-93dd-47e2-88f2-b3e7f7648642', 0, NULL, 0, '2012-07-06 16:00:00.000',
-- 	       '790D8CC9-04F8-4643-90EE-FAED4BA711EC', NULL, NULL, 3)
	       
-- INSERT INTO [BusinessSafe].[dbo].[User] 
-- 		([UserId], [EmployeeId], [RoleId], [RegistrationError], [RegistrationErrorMessage], [Deleted], [CreatedOn], [CreatedById], 
-- 		[LastModifiedOn], [LastModifiedById], [ClientId])
-- 	VALUES ('817927d0-ed72-44f9-bc20-fc9e26909754', '2a50d782-577b-4f1e-8b38-a9a0cec7ab54', '1e382767-93dd-47e2-88f2-b3e7f7648642', 0, NULL, 0, '2012-07-06 16:00:00.000', 
-- 		'16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL, 2)
		
-- INSERT INTO [BusinessSafe].[dbo].[User] 
-- 		([UserId], [EmployeeId], [RoleId], [RegistrationError], [RegistrationErrorMessage], [Deleted], [CreatedOn], [CreatedById], 
-- 		[LastModifiedOn], [LastModifiedById], [ClientId]) 
-- 	VALUES ('91f0e64a-7c04-4d89-a336-56c82d810652', '90562014-eac7-4d8e-8004-9254ae67190e', 'bacf7c01-d210-4dbc-942f-15d8456d3b92', 0, NULL, 0, '2012-07-06 16:00:00.000', 
-- 		'16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL, 1)

--//@UNDO 
USE [BusinessSafe]
GO

--DELETE FROM [User] WHERE UserId = '790D8CC9-04F8-4643-90EE-FAED4BA711EC'
--DELETE FROM [User] WHERE UserId = '16ac58fb-4ea4-4482-ac3d-000d607af67c'
--DELETE FROM [User] WHERE UserId = '817927d0-ed72-44f9-bc20-fc9e26909754'
--DELETE FROM [User] WHERE UserId = '91f0e64a-7c04-4d89-a336-56c82d810652'
