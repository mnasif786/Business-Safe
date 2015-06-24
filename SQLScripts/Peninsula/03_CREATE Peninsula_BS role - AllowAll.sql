----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the Peninsula_BS role */
----------------------------------------------------------------------------------------------------------------------------------------------------------

USE [Peninsula_BS]
GO

DECLARE @RoleName sysname
set @RoleName = N'AllowAll'
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = @RoleName AND type = 'R')
Begin
	DECLARE @RoleMemberName sysname
	DECLARE Member_Cursor CURSOR FOR
	select [name]
	from sys.database_principals 
	where principal_id in ( 
		select member_principal_id 
		from sys.database_role_members 
		where role_principal_id in (
			select principal_id
			FROM sys.database_principals where [name] = @RoleName  AND type = 'R' ))

	OPEN Member_Cursor;

	FETCH NEXT FROM Member_Cursor
	into @RoleMemberName

	WHILE @@FETCH_STATUS = 0
	BEGIN

		exec sp_droprolemember @rolename=@RoleName, @membername= @RoleMemberName

		FETCH NEXT FROM Member_Cursor
		into @RoleMemberName
	END;

	CLOSE Member_Cursor;
	DEALLOCATE Member_Cursor;
End

GO

IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'AllowAll' AND type = 'R')
DROP ROLE [AllowAll]
GO

USE [Peninsula_BS]
GO

CREATE ROLE [AllowAll] AUTHORIZATION [dbo]
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the Peninsula_BS user, and add the role */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

CREATE USER [intranetadmin] FOR LOGIN [intranetadmin]
GO

USE [Peninsula_BS]
GO

EXEC sp_addrolemember N'AllowAll', N'intranetadmin'
GO
