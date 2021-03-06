DECLARE @GeneralUserRoleId uniqueidentifier
DECLARE @HSManagerRoleId uniqueidentifier

DECLARE @ActionPlanPermissionTargetId int

DECLARE @ActionPlanPermission_ADD_Id int
DECLARE @ActionPlanPermission_DELETE_Id int

-- USER ROLES
SELECT  @GeneralUserRoleId = RoleId
FROM 	BusinessSafe.dbo.Role
where	Description = 'General User' 

SELECT  @HSManagerRoleId = RoleId
FROM 	BusinessSafe.dbo.Role
where	Description = 'Health And Safety Manager' 	

-- PERMISSION IDS
SELECT  @ActionPlanPermissionTargetId = Id
FROM	BusinessSafe.dbo.PermissionTarget 
where	Name = 'Action Plan'

SELECT  @ActionPlanPermission_ADD_Id = Id
FROM	[BusinessSafe].[dbo].[Permission] 
where	PermissionTargetId = @ActionPlanPermissionTargetId and PermissionActivityId = 2 -- 2 is ADD

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

-- GENERAL USER PERMISSIONS
UPDATE [BusinessSafe].[dbo].[RolesPermissions]
SET 	
	Deleted = 1, 
	LastModifiedOn = GETDATE(), 
	LastModifiedBy = null
WHERE RoleId = @GeneralUserRoleId AND (PermissionId = @ActionPlanPermission_ADD_Id OR PermissionId = @ActionPlanPermission_DELETE_Id)
	
GO