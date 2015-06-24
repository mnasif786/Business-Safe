use businesssafe
go

IF NOT EXISTS (SELECT ID FROM [dbo].[Permission] WHERE PermissionTargetId = 18 and PermissionActivityId = 1)
BEGIN
	INSERT INTO [dbo].[Permission] ([PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
    VALUES (18, 1, 0, getdate(), 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', null, null)
END

IF NOT EXISTS (SELECT ID FROM [dbo].[Permission] WHERE PermissionTargetId = 18 and PermissionActivityId = 2)
BEGIN
	INSERT INTO [dbo].[Permission] ([PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
    VALUES (18, 2, 0, getdate(), 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', null, null)
END

IF NOT EXISTS (SELECT ID FROM [dbo].[Permission] WHERE PermissionTargetId = 18 and PermissionActivityId = 3)
BEGIN
	INSERT INTO [dbo].[Permission] ([PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
    VALUES (18, 3, 0, getdate(), 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', null, null)
END

IF NOT EXISTS (SELECT ID FROM [dbo].[Permission] WHERE PermissionTargetId = 18 and PermissionActivityId = 4)
BEGIN
	INSERT INTO [dbo].[Permission] ([PermissionTargetId], [PermissionActivityId], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
    VALUES (18, 4, 0, getdate(), 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', null, null)     
END

--//@UNDO 

IF  EXISTS (SELECT ID FROM [dbo].[Permission] WHERE PermissionTargetId = 18 and PermissionActivityId = 1)
BEGIN
	delete from Permission WHERE PermissionTargetId = 18 and PermissionActivityId = 1)
END

IF  EXISTS (SELECT ID FROM [dbo].[Permission] WHERE PermissionTargetId = 18 and PermissionActivityId = 2)
BEGIN
	delete from Permission WHERE PermissionTargetId = 18 and PermissionActivityId = 2)
END

IF  EXISTS (SELECT ID FROM [dbo].[Permission] WHERE PermissionTargetId = 18 and PermissionActivityId = 3)
BEGIN
	delete from Permission WHERE PermissionTargetId = 18 and PermissionActivityId = 3)
END

IF  EXISTS (SELECT ID FROM [dbo].[Permission] WHERE PermissionTargetId = 18 and PermissionActivityId = 4)
BEGIN
	delete from Permission WHERE PermissionTargetId = 18 and PermissionActivityId = 4)
END


