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
VALUES (43, 14, @view, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL)

INSERT INTO [BusinessSafe].[dbo].[Permission] ([Id], [PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
VALUES (44, 14, @add, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL)

INSERT INTO [BusinessSafe].[dbo].[Permission] ([Id], [PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
VALUES (45, 14, @edit, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL)

INSERT INTO [BusinessSafe].[dbo].[Permission] ([Id], [PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
VALUES (46, 14, @delete, 0, GETDATE(), '16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL)

SET IDENTITY_INSERT [Permission] OFF
GO

GO

--//@UNDO
DELETE FROM [BusinessSafe].[dbo].[Permission]
WHERE ID IN (43, 44, 45, 46)