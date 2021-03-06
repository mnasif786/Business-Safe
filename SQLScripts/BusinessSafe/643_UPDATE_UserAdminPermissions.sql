DECLARE @GeneralUserRoleId uniqueidentifier
DECLARE @HSManagerRoleId uniqueidentifier

DECLARE @ActionPlanPermissionTargetId int

DECLARE @ActionPlanPermission_ADD_Id int
DECLARE @ActionPlanPermission_DELETE_Id int
DECLARE @ActionPlanPermission_VIEW_Id int
DECLARE @ActionPlanPermission_EDIT_Id int

-- USER ROLES
SELECT  @GeneralUserRoleId = RoleId
FROM 	BusinessSafe.dbo.Role
where	Description = 'General User' AND ClientId = 0

SELECT  @HSManagerRoleId = RoleId
FROM 	BusinessSafe.dbo.Role
where	Description = 'Health And Safety Manager' AND ClientId = 0	

-- PERMISSION IDS
SELECT  @ActionPlanPermissionTargetId = Id
FROM	BusinessSafe.dbo.PermissionTarget 
where	Name = 'Action Plan'

SELECT  @ActionPlanPermission_VIEW_Id = Id
FROM	[BusinessSafe].[dbo].[Permission] 
where	PermissionTargetId = @ActionPlanPermissionTargetId and PermissionActivityId = 1 -- 1 is VIEW

SELECT  @ActionPlanPermission_ADD_Id = Id
FROM	[BusinessSafe].[dbo].[Permission] 
where	PermissionTargetId = @ActionPlanPermissionTargetId and PermissionActivityId = 2 -- 2 is ADD

SELECT  @ActionPlanPermission_EDIT_Id = Id
FROM	[BusinessSafe].[dbo].[Permission] 
where	PermissionTargetId = @ActionPlanPermissionTargetId and PermissionActivityId = 3 -- 3 is EDIT

SELECT  @ActionPlanPermission_DELETE_Id = Id
FROM	[BusinessSafe].[dbo].[Permission] 
where	PermissionTargetId = @ActionPlanPermissionTargetId and PermissionActivityId = 4 -- 4 is DELETE



-- HS MANAGER PERMISSIONS
IF  NOT EXISTS( SELECT * FROM [BusinessSafe].[dbo].[RolesPermissions] WHERE RoleId = @HSManagerRoleId AND PermissionId = @ActionPlanPermission_ADD_Id )
BEGIN		
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]
           ([RoleId], [PermissionId], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [Deleted])
    VALUES
           ( @HSManagerRoleId, @ActionPlanPermission_ADD_Id, GETDATE(), '16AC58FB-4EA4-4482-AC3D-000D607AF67C', GETDATE(), '16AC58FB-4EA4-4482-AC3D-000D607AF67C', 0)
END


IF  NOT EXISTS( SELECT * FROM [BusinessSafe].[dbo].[RolesPermissions] WHERE RoleId = @HSManagerRoleId AND PermissionId = @ActionPlanPermission_DELETE_Id )
BEGIN		
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]
           ([RoleId], [PermissionId], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [Deleted])
    VALUES
           ( @HSManagerRoleId, @ActionPlanPermission_DELETE_Id, GETDATE(), '16AC58FB-4EA4-4482-AC3D-000D607AF67C', GETDATE(), '16AC58FB-4EA4-4482-AC3D-000D607AF67C', 0)
END



IF  NOT EXISTS( SELECT * FROM [BusinessSafe].[dbo].[RolesPermissions] WHERE RoleId = @HSManagerRoleId AND PermissionId = @ActionPlanPermission_VIEW_Id )
BEGIN		
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]
           ([RoleId], [PermissionId], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [Deleted])
    VALUES
           ( @HSManagerRoleId, @ActionPlanPermission_VIEW_Id, GETDATE(), '16AC58FB-4EA4-4482-AC3D-000D607AF67C', GETDATE(), '16AC58FB-4EA4-4482-AC3D-000D607AF67C', 0)
END


IF  NOT EXISTS( SELECT * FROM [BusinessSafe].[dbo].[RolesPermissions] WHERE RoleId = @HSManagerRoleId AND PermissionId = @ActionPlanPermission_EDIT_Id )
BEGIN		
	INSERT INTO [BusinessSafe].[dbo].[RolesPermissions]
           ([RoleId], [PermissionId], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [Deleted])
    VALUES
           ( @HSManagerRoleId, @ActionPlanPermission_EDIT_Id, GETDATE(), '16AC58FB-4EA4-4482-AC3D-000D607AF67C', GETDATE(), '16AC58FB-4EA4-4482-AC3D-000D607AF67C', 0)
END

-- GENERAL USER PERMISSIONS
UPDATE [BusinessSafe].[dbo].[RolesPermissions]
SET 	
	Deleted = 1, 
	LastModifiedOn = GETDATE(), 
	LastModifiedBy = null
WHERE RoleId = @GeneralUserRoleId AND 
	(PermissionId = @ActionPlanPermission_ADD_Id 
		OR PermissionId = @ActionPlanPermission_DELETE_Id 
		OR PermissionId = @ActionPlanPermission_EDIT_Id 
		OR PermissionId = @ActionPlanPermission_VIEW_Id)
	
GO