----------------------------------------------------------------------------------------------------------------------------------------------------------
/* CREATE the NServiceBus database */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [master]
GO

IF EXISTS (
		SELECT *
		FROM sys.databases
		WHERE NAME = 'NServiceBus'
		)
BEGIN
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'NServiceBus'

	ALTER DATABASE [NServiceBus]

	SET SINGLE_USER
	WITH

	ROLLBACK IMMEDIATE

	DROP DATABASE [NServiceBus]
END

CREATE DATABASE [NServiceBus]
GO

USE [NServiceBus]
GO

EXEC sp_changedbowner 'IntranetAdmin'
