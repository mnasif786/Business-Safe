USE BusinessSafe
GO

/* mover permissions to different area */
UPDATE PermissionTarget SET PermissionGroupId = 6 WHERE Id = 6
UPDATE PermissionTarget SET PermissionGroupId = 6 WHERE Id = 17
UPDATE PermissionTarget SET PermissionGroupId = 6 WHERE Id = 19

/* remove Add and Delete Action Plan permissions*/
DELETE FROM RolesPermissions WHERE PermissionId in (64,66)

UPDATE Permission SET Deleted = 1 WHERE Id in (64,66)

GO