USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE [name] = 'AllowSelectInsertUpdate' and [type] = 'R')
BEGIN
	CREATE ROLE [AllowSelectInsertUpdate]
	EXEC sp_MSforeachtable 'GRANT SELECT, INSERT, UPDATE ON ? TO [AllowSelectInsertUpdate]'
END

IF EXISTS (SELECT * FROM sys.database_principals WHERE [name] = 'BusinessSafeDB' and [type] = 'S')
BEGIN
	IF NOT EXISTS
	(
		SELECT *
		FROM sys.database_role_members
		INNER JOIN sys.database_principals [role]
		ON sys.database_role_members.role_principal_id = [role].principal_id
		INNER JOIN sys.database_principals [member]
		ON sys.database_role_members.member_principal_id = [member].principal_id
		WHERE [role].[name] = 'AllowSelectInsertUpdate'
		AND [member].[name] = 'BusinessSafeDB'
	)
	BEGIN
		EXEC sp_addrolemember 'AllowSelectInsertUpdate', 'BusinessSafeDB'
	END
	
	IF EXISTS
	(
		SELECT *
		FROM sys.database_role_members
		INNER JOIN sys.database_principals [role]
		ON sys.database_role_members.role_principal_id = [role].principal_id
		INNER JOIN sys.database_principals [member]
		ON sys.database_role_members.member_principal_id = [member].principal_id
		WHERE [role].[name] = 'AllowAll'
		AND [member].[name] = 'BusinessSafeDB'
	)
	BEGIN
		EXEC sp_droprolemember 'AllowAll', 'BusinessSafeDB'
	END
END
ELSE
BEGIN

	IF NOT EXISTS (SELECT * FROM sys.sql_logins WHERE [name] = 'intranetuser')
	BEGIN
		CREATE LOGIN [intranetuser] WITH PASSWORD=N'intuspas', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
	END

	IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE [name] = 'intranetuser' and [type] = 'S')
	BEGIN
		CREATE USER [intranetuser] FOR LOGIN [intranetuser] WITH DEFAULT_SCHEMA=[dbo]
	END

	IF NOT EXISTS
	(
		SELECT *
		FROM sys.database_role_members
		INNER JOIN sys.database_principals [role]
		ON sys.database_role_members.role_principal_id = [role].principal_id
		INNER JOIN sys.database_principals [member]
		ON sys.database_role_members.member_principal_id = [member].principal_id
		WHERE [role].[name] = 'AllowSelectInsertUpdate'
		AND [member].[name] = 'intranetuser'
	)
	BEGIN
		EXEC sp_addrolemember 'AllowSelectInsertUpdate', 'intranetuser'
	END
END

--//@UNDO

IF EXISTS (SELECT * FROM sys.database_principals WHERE [name] = 'BusinessSafeDB' and [type] = 'S')
BEGIN
	IF EXISTS
	(
		SELECT *
		FROM sys.database_role_members
		INNER JOIN sys.database_principals [role]
		ON sys.database_role_members.role_principal_id = [role].principal_id
		INNER JOIN sys.database_principals [member]
		ON sys.database_role_members.member_principal_id = [member].principal_id
		WHERE [role].[name] = 'AllowSelectInsertUpdate'
		AND [member].[name] = 'BusinessSafeDB'
	)
	BEGIN
		EXEC sp_droprolemember 'AllowSelectInsertUpdate', 'BusinessSafeDB'
	END
	
	IF NOT EXISTS
	(
		SELECT *
		FROM sys.database_role_members
		INNER JOIN sys.database_principals [role]
		ON sys.database_role_members.role_principal_id = [role].principal_id
		INNER JOIN sys.database_principals [member]
		ON sys.database_role_members.member_principal_id = [member].principal_id
		WHERE [role].[name] = 'AllowAll'
		AND [member].[name] = 'BusinessSafeDB'
	)
	BEGIN
		EXEC sp_addrolemember 'AllowAll', 'BusinessSafeDB'
	END
END
ELSE
BEGIN
	IF  EXISTS
	(
		SELECT *
		FROM sys.database_role_members
		INNER JOIN sys.database_principals [role]
		ON sys.database_role_members.role_principal_id = [role].principal_id
		INNER JOIN sys.database_principals [member]
		ON sys.database_role_members.member_principal_id = [member].principal_id
		WHERE [role].[name] = 'AllowSelectInsertUpdate'
		AND [member].[name] = 'intranetuser'
	)
	BEGIN
		EXEC sp_droprolemember 'AllowSelectInsertUpdate', 'intranetuser'
	END

	IF EXISTS (SELECT * FROM sys.database_principals WHERE [name] = 'intranetuser' and [type] = 'S')
	BEGIN
		DROP USER [intranetuser]
	END

	IF EXISTS (SELECT * FROM sys.sql_logins WHERE [name] = 'intranetuser')
	BEGIN
		DROP LOGIN [intranetuser] 
	END
END

IF EXISTS (SELECT * FROM sys.database_principals WHERE [name] = 'AllowSelectInsertUpdate' and [type] = 'R')
BEGIN
	DROP ROLE AllowSelectInsertUpdate
END