----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the BusinessSafe database */
----------------------------------------------------------------------------------------------------------------------------------------------------------

USE [master]
GO

IF EXISTS (SELECT * FROM sys.databases WHERE NAME = 'BusinessSafe')
	BEGIN
		EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'BusinessSafe'
		ALTER DATABASE [BusinessSafe] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
		DROP DATABASE [BusinessSafe]
	END
	
	
CREATE DATABASE [BusinessSafe] 

ON  
( NAME = N'BusinessSafe', 
	FILENAME = N'D:\UAT\Data\BusinessSafe.mdf' , 
	SIZE = 1024000KB , 
	MAXSIZE = UNLIMITED, 
	FILEGROWTH = 10%)
 LOG ON 
( NAME = N'BusinessSafe_log', 
	FILENAME = N'D:\UAT\Data\BusinessSafe_log.ldf' , 
	SIZE = 307200KB , 
	MAXSIZE = 2048GB , 
	FILEGROWTH = 10%)
GO

USE [BusinessSafe]
GO
CREATE ROLE [AllowAll] 
GO
