DECLARE @HSManagerRoleId uniqueidentifier
DECLARE @GeneralUserRoleId uniqueidentifier

DECLARE @ActionPlanPermissionTargetId int

DECLARE @ActionPlanPermission_VIEW_Id int
DECLARE @ActionPlanPermission_EDIT_Id int


SELECT  @HSManagerRoleId = RoleId
FROM 	BusinessSafe.dbo.Role
where	Description = 'Health And Safety Manager' 	

SELECT  @GeneralUserRoleId = RoleId
FROM 	BusinessSafe.dbo.Role
where	Description = 'General User' 

SELECT  @ActionPlanPermissionTargetId = Id
FROM	BusinessSafe.dbo.PermissionTarget 
where	Name = 'Action Plan'

SELECT  @ActionPlanPermission_VIEW_Id = Id
FROM	[BusinessSafe].[dbo].[Permission] 
where	PermissionTargetId = @ActionPlanPermissionTargetId and PermissionActivityId = 1 -- 1 is VIEW

SELECT  @ActionPlanPermission_EDIT_Id = Id
FROM	[BusinessSafe].[dbo].[Permission] 
where	PermissionTargetId = @ActionPlanPermissionTargetId and PermissionActivityId = 3 -- 3 is EDIT


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



UPDATE [BusinessSafe].[dbo].[RolesPermissions]
SET 	
	Deleted = 1, 
	LastModifiedOn = GETDATE(), 
	LastModifiedBy = null
WHERE RoleId = @GeneralUserRoleId AND (PermissionId = @ActionPlanPermission_EDIT_Id OR PermissionId = @ActionPlanPermission_VIEW_Id)
	
GO