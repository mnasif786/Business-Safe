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
Go

USE [BusinessSafe]
GO
CREATE ROLE [AllowAll] 
GO
