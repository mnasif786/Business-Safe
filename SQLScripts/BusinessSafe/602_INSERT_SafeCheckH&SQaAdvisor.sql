USE [BusinessSafe]
GO

IF NOT EXISTS( SELECT * FROM SafeCheckQaAdvisor where Id =  '3A204FB3-1956-4EFC-BE34-89F7897570DB')
BEGIN		
	INSERT INTO [dbo].[SafeCheckQaAdvisor]([Id], [Forename], [Surname], [Email], [Deleted]) VALUES ('3A204FB3-1956-4EFC-BE34-89F7897570DB', 'H&S Reports', '','H&SReports@Peninsula-uk.com', 0)
END

GO

--//@UNDO 

IF EXISTS( SELECT * FROM SafeCheckQaAdvisor where Id =  '3A204FB3-1956-4EFC-BE34-89F7897570DB')
BEGIN		
	DELETE FROM [dbo].[SafeCheckQaAdvisor] WHERE Id = '3A204FB3-1956-4EFC-BE34-89F7897570DB'
END
GO