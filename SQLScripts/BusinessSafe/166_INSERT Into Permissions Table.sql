USE [BusinessSafe]
GO

TRUNCATE TABLE [BusinessSafe].[dbo].[Permission]

DECLARE @view as int
DECLARE @add as int
DECLARE @edit as int
DECLARE @delete as int
select @view = 1
select @add = 2
select @edit = 3
select @delete = 4

INSERT INTO [BusinessSafe].[dbo].[Permission] ([PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])

 -- Company Details
SELECT 1, @view, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 1, @add, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 1, @edit, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION
SELECT 1, @delete, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL	UNION

-- Site Details
SELECT 2, @view, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 2, @add, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 2, @edit, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION
SELECT 2, @delete, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL	UNION

-- Users
SELECT 3, @view, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 3, @add, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 3, @edit, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION
SELECT 3, @delete, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL	UNION

-- Employee Records
SELECT 4, @view, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 4, @add, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 4, @edit, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION
SELECT 4, @delete, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL	UNION

-- Risk Assessment Records
SELECT 5, @view, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 5, @add, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 5, @edit, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION
SELECT 5, @delete, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL	UNION

-- Risk Assessment Tasks
SELECT 6, @view, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 6, @add, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 6, @edit, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION
SELECT 6, @delete, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL	UNION

-- Added Documents
SELECT 7, @view, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 7, @add, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 7, @edit, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION
SELECT 7, @delete, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL	UNION

-- BusinessSafe System
SELECT 8, @view, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
--SELECT 8, @add, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
--SELECT 8, @edit, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION
--SELECT 8, @delete, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL	UNION

-- Reference Library
SELECT 9, @view, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
--SELECT 9, @add, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
--SELECT 9, @edit, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION
--SELECT 9, @delete, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL	UNION

-- User Roles
SELECT 10, @view, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 10, @add, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 10, @edit, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION
SELECT 10, @delete, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION

-- User Permissions
SELECT 11, @view, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 11, @add, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 11, @edit, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION
SELECT 11, @delete, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION

-- Company Defaults
SELECT 12, @view, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 12, @add, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION 
SELECT 12, @edit, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL UNION
SELECT 12, @delete, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL	

GO

--//@UNDO
DELETE FROM [BusinessSafe].[dbo].[Permission]