----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the BusinessSafe database */
----------------------------------------------------------------------------------------------------------------------------------------------------------
IF (@@SERVERNAME = 'PBSPROD2SQL\PROD2')
BEGIN
	RAISERROR (N'dont drop the live database',	18 ,1)
END


USE [master]


IF EXISTS (SELECT * FROM sys.databases WHERE NAME = 'BusinessSafe')
BEGIN
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'BusinessSafe'
	ALTER DATABASE [BusinessSafe] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE [BusinessSafe]
END
	
	
CREATE DATABASE [BusinessSafe] 
Go
