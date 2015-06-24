USE [BusinessSafe]
GO


DECLARE @view as int
DECLARE @add as int
DECLARE @edit as int
DECLARE @delete as int
select @view = 1
select @add = 2
select @edit = 3
select @delete = 4

SET IDENTITY_INSERT [Permission] ON

INSERT INTO [BusinessSafe].[dbo].[Permission] ([Id], [PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
VALUES (63, 19, @view, 0, GETDATE(), '16AC58FB-4EA4-4482-AC3D-000D607AF67C', NULL, NULL)

INSERT INTO [BusinessSafe].[dbo].[Permission] ([Id], [PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
VALUES (64, 19, @add, 0, GETDATE(), '16AC58FB-4EA4-4482-AC3D-000D607AF67C', NULL, NULL)

INSERT INTO [BusinessSafe].[dbo].[Permission] ([Id], [PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
VALUES (65, 19, @edit, 0, GETDATE(), '16AC58FB-4EA4-4482-AC3D-000D607AF67C', NULL, NULL)

INSERT INTO [BusinessSafe].[dbo].[Permission] ([Id], [PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
VALUES (66, 19, @delete, 0, GETDATE(), '16AC58FB-4EA4-4482-AC3D-000D607AF67C', NULL, NULL)

SET IDENTITY_INSERT [Permission] OFF
GO

--//@UNDO
DELETE FROM [BusinessSafe].[dbo].[Permission]
WHERE ID IN (63, 64, 65, 66)