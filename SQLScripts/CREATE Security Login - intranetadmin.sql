----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the BusienssSafe Security Login */
----------------------------------------------------------------------------------------------------------------------------------------------------------

IF NOT EXISTS(SELECT name FROM master.dbo.syslogins WHERE name = 'intranetadmin')
BEGIN
	/* For security reasons the login is created disabled and with a random password. */
	CREATE LOGIN [intranetadmin] WITH PASSWORD='intadpas', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
	exec sp_change_users_login 'AUTO_FIX', 'intranetadmin'
END
GO

IF EXISTS(SELECT name FROM master.dbo.syslogins WHERE name = 'intranetadmin')
BEGIN
	ALTER LOGIN [intranetadmin] ENABLE
	
END
GO

USE [BusinessSafe]
GO
IF NOT EXISTS (SELECT * FROM sys.sysusers WHERE name = N'intranetadmin')
BEGIN
	CREATE USER [intranetadmin] FOR LOGIN [intranetadmin] WITH DEFAULT_SCHEMA=[dbo]	
END

exec sp_change_users_login 'AUTO_FIX', 'intranetadmin'


IF DATABASE_PRINCIPAL_ID('AllowAll') IS NULL
BEGIN
	CREATE ROLE [AllowAll] 	
END
EXEC sp_addrolemember 'AllowAll', 'intranetadmin'
GO