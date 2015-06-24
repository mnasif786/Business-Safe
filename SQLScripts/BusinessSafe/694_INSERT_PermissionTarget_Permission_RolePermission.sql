USE [BusinessSafe]
GO

    SET IDENTITY_INSERT [PermissionTarget] ON
    IF NOT EXISTS(SELECT * FROM [dbo].[PermissionTarget] WHERE [Id] = 20)
    Begin
		INSERT INTO [dbo].[PermissionTarget] ([Id], [Name], [PermissionGroupId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [Deleted])
		VALUES (20, 'View Reports', 7, '16AC58FB-4EA4-4482-AC3D-000D607AF67C', GETDATE(), NULL, NULL, 0)
	End
	SET IDENTITY_INSERT [PermissionTarget] OFF
    
    SET IDENTITY_INSERT [Permission] ON

	IF NOT EXISTS(SELECT * FROM [dbo].[Permission] WHERE [Id] = 67)
	Begin
		INSERT INTO [BusinessSafe].[dbo].[Permission] ([Id], [PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
		VALUES (67, 20, 1, 0, GETDATE(), '16AC58FB-4EA4-4482-AC3D-000D607AF67C', NULL, NULL)
	End
	
	SET IDENTITY_INSERT [Permission] OFF
	GO 

   
   DECLARE @Table TABLE( 
	Id UniqueIdentifier NOT NULL,
	Recorded INT 	
	); 

	DECLARE @Counter INT
	SET @Counter = 0 

	INSERT INTO @Table (Id)
	SELECT  RoleId 
	FROM [BusinessSafe].[dbo].[Role] WHERE Name != 'GeneralUser'

	While (@Counter < (Select Count(*) From @Table)) 
	Begin

		DECLARE @RoleID UniqueIdentifier
		SELECT Top 1 @RoleID = Id From @Table Where Recorded IS NULL
		
		IF NOT EXISTS(SELECT * FROM [dbo].[RolesPermissions] WHERE [RoleId] = @RoleID and [PermissionId] = 67)			 
		Begin
			Insert into [dbo].[RolesPermissions] (RoleId, PermissionId, CreatedOn, CreatedBy, Deleted)
			 values ( @RoleID, 67, GETDATE(), '16AC58FB-4EA4-4482-AC3D-000D607AF67C', 0);
		End
		
		UPDATE @Table SET Recorded = 1 WHERE Id = @RoleID
		    
		SET @Counter = @Counter + 1 
	   
	End
	
	
--//@UNDO
DELETE FROM [BusinessSafe].[dbo].[RolesPermissions]
where PermissionId = 67

DELETE FROM [BusinessSafe].[dbo].[Permission]
WHERE ID = 67	

DELETE FROM [BusinessSafe].[dbo].[PermissionTarget]
WHERE ID = 20