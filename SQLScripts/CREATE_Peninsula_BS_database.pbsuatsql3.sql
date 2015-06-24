			/* CREATE the Peninsula_test database */
----------------------------------------------------------------------------------------------------------------------------------------------------------

USE [master]
GO

IF EXISTS (SELECT * FROM sys.databases WHERE NAME = 'Peninsula_BS')
	BEGIN
		EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'Peninsula_BS'
		ALTER DATABASE [Peninsula_BS] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
		DROP DATABASE [Peninsula_BS]
	END

CREATE DATABASE Peninsula_BS

USE [Peninsula]
GO

--ON
--( NAME = 'Peninsula_test_dat',
--	FILENAME = 'D:\MSSQL\Peninsula_test_dat.mdf',
--	SIZE = 200MB,
--	MAXSIZE = UNLIMITED,
--	FILEGROWTH = 10% )
--LOG ON
--( NAME = 'Peninsula_test_log',
--	FILENAME = 'D:\MSSQL\Peninsula_test_log.mdf',
--	SIZE = 200MB,
--	MAXSIZE = UNLIMITED,
--	FILEGROWTH = 10% )
--GO