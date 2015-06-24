USE [BusinessSafe]
GO

IF NOT EXISTS( SELECT * FROM SafeCheckQaAdvisor where Id =  '3A204FB3-1956-4EFC-BE34-89F7897570DB')
BEGIN		
	INSERT INTO [dbo].[SafeCheckQaAdvisor]([Id], [Forename], [Surname], [Email], [Deleted]) VALUES ('3A204FB3-1956-4EFC-BE34-89F7897570DB', 'H&S', 'Reports','H&S.Reports@Peninsula-uk.com', 0)
END

IF EXISTS ( SELECT * FROM SafeCheckQaAdvisor where Id =  '3A204FB3-1956-4EFC-BE34-89F7897570DB')
BEGIN
	UPDATE [dbo].[SafeCheckQaAdvisor] set Forename = 'H&S' where Id = '3A204FB3-1956-4EFC-BE34-89F7897570DB'
	UPDATE [dbo].[SafeCheckQaAdvisor] set Surname = 'Reports' where Id = '3A204FB3-1956-4EFC-BE34-89F7897570DB'
	UPDATE [dbo].[SafeCheckQaAdvisor] set email = 'H&S.Reports@Peninsula-uk.com'  where Id = '3A204FB3-1956-4EFC-BE34-89F7897570DB'
END	
GO

--//@UNDO 

IF EXISTS( SELECT * FROM SafeCheckQaAdvisor where Id =  '3A204FB3-1956-4EFC-BE34-89F7897570DB')
BEGIN		
	UPDATE [dbo].[SafeCheckQaAdvisor] set Forename = 'H&SReports' where Id = '3A204FB3-1956-4EFC-BE34-89F7897570DB'
	UPDATE [dbo].[SafeCheckQaAdvisor] set Surname = '' where Id = '3A204FB3-1956-4EFC-BE34-89F7897570DB'
	UPDATE [dbo].[SafeCheckQaAdvisor] set email = 'H&SReports@Peninsula-uk.com'  where Id = '3A204FB3-1956-4EFC-BE34-89F7897570DB'
END
GO